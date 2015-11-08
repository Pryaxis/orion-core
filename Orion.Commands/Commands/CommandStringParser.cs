using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Commands.Attributes;
using Orion.Commands.Commands.Exceptions;
using Orion.Commands.Extensions;
using OTA;

namespace Orion.Commands.Commands
{
    public class CommandStringParser
    {
        private Dictionary<Type, Func<string, object>> Converters = new Dictionary<Type, Func<string, object>>()
    {
        {typeof(string), x => x},
        {typeof(int), x => int.Parse(x)},
        {typeof(float), x => float.Parse(x)},
        {typeof(decimal), x => decimal.Parse(x)},
        {typeof(long), x => long.Parse(x)},
        {typeof(short), x => short.Parse(x)},
        {typeof(DateTime), x => DateTime.Parse(x)},
        {typeof(TimeSpan), x => TimeSpan.Parse(x)}
    };

        /// <summary>
        /// Add a converter for type `T` to converter's dictionary. 
        /// The parser will use this converter if type `T` arises as an expected type in a future command.
        /// </summary>
        /// <typeparam name="T">Type to be converted.</typeparam>
        /// <param name="converter">Function which takes a string and returns `T`</param>
        public void AddConverter<T>(Func<string, object> converter)
        {
            Converters.Add(typeof (T), converter);
        }

        /// <summary>
        /// Returns the command name of the command called within the command string.
        /// Example "/testcommand" returns "testcommand".
        /// </summary>
        /// <param name="commandString">The command string.</param>
        /// <returns>The name of the command called by the command string.</returns>
        public static string GetCommandNameFromCommandString(string commandString)
        {
            return commandString.Split(' ').First().Replace("/", "");
        }

        public IOrionCommand ParseArgumentsIntoCommandClass(Type commandType, string commandString)
        {
            //Init instance of command.
            var commandConstructor = commandType.GetConstructor(Type.EmptyTypes);

            if (commandConstructor == null)
            {
                //TODO: Proper exception.
                throw new Exception("Command type has no paramterless constructors.");
            }

            var commandInstance = commandConstructor.Invoke(null) as IOrionCommand;

            if (commandInstance == null)
            {
                throw new Exception("Command type was not of IOrionCommand.");
            }

            //Get info about the class properties.
            var properties = commandType.GetProperties();
            var flags = properties.Where(PropertyHasAttribute<NamedParameterAttribute>);
            var switches = properties.Where(PropertyHasAttribute<SwitchParameterAttribute>);
            var positional = properties.Where(PropertyHasAttribute<PositionalParameterAttribute>);

            var commandArgs = SplitCommandStringIntoArguments(commandString);
            
            //Process switches first.
            foreach (var prop in switches)
            {
                var name = prop.Name;
                //Only really care if it's declared at least once, so using Any.
                var matches = commandArgs.Any(x => String.Equals(name, x.TrimStart('/', '-'), StringComparison.CurrentCultureIgnoreCase));

                //Set value in to-be-returned instance of the command.
                prop.SetValue(commandInstance, matches);
            }
            
            //Process flags now.
            foreach (var prop in flags)
            {
                var attribute = prop.GetCustomAttribute<NamedParameterAttribute>();
                var name = attribute.Flag ?? prop.Name;
                var required = attribute.Required;

                bool match = false;

                //Look for any args which match this flag, but there can only be one.
                try
                {
                    match = commandArgs.Single(x => String.Equals(name, x.TrimStart('/', '-'), StringComparison.CurrentCultureIgnoreCase)).Any();
                }
                catch (InvalidOperationException)
                {
                    //TODO: Command parsing error exception throwing.
                    if (required)
                    {
                        throw;
                        //Throw exception for "Required flag could not be found."
                    }
                }

                //If we've found a match for this flag.
                if (match)
                {
                    //Get the argument from the command string for this flag.
                    //The argument is always the parameter in the next position in the command string.
                    var matchIndex = commandArgs.IndexOfFlag(name);
                    var flagArg = commandArgs[matchIndex + 1];

                    //Get the parsed argument.
                    object flagArgAsTargetType = ParseArgumentIntoObject(prop.PropertyType, flagArg);

                    //Set the property value in the instance to the parsed value.
                    prop.SetValue(commandInstance, flagArgAsTargetType);
                }
            }

            //Process positional arguments now.
            var positionalArgsFromString = commandArgs.Where(x => !x.IsFlagOrSwitch()).ToList();
            var ordered = positional.OrderBy(x => x.GetCustomAttribute<PositionalParameterAttribute>().Position).ToList();

            //We're dependent on the positional args in `positionalArgsFromString` being ordered in accordance with the props from `ordered`.
            //Using a for-loop in order to be able to easily access objects in both lists.
            for (int i = 0; i < ordered.Count; i++)
            {
                //Get the property representing our positional arg.
                var prop = ordered[i];
                //Is the arg required?
                var required = prop.GetCustomAttribute<PositionalParameterAttribute>().Required;

                //Check if we're out of bounds of the provided arguments.
                if (i < positionalArgsFromString.Count)
                {
                    //We're still in bounds, so get the argument from the args list.
                    var arg = positionalArgsFromString[i];

                    //Parse the argument into the expected type.
                    var parsedArg = ParseArgumentIntoObject(prop.PropertyType, arg);


                    //Set the value of the property in the command instance.
                    prop.SetValue(commandInstance, parsedArg);
                }
                else
                {
                    //We're out of bounds, so check if the arg was required.
                    if (required)
                    {
                        //The arg is required, so throw an exception since we do not have a value for it.
                        //TODO: Handle required positional arg not getting an arg.
                        //Throw proper exception.
                        throw new Exception();
                    }
                }
            }

            return commandInstance;
        }

        private bool PropertyHasAttribute<T>(PropertyInfo prop) where T : Attribute
        {
            var attribute = prop.GetCustomAttribute(typeof (T));
            return attribute is T;
        }

        /// <summary>
        /// Will attempt to parse the passed in <paramref name="arg"/> into the expected type <typeparamref name="T"/>
        /// </summary>
        /// <param name="arg">The argument to be parsed.</param>
        /// <typeparam name="T">The expected output type.</typeparam>
        /// <exception cref="ArgumentParsingException">Thrown if the arugment did not parse properly or if there is an expected type which has no registered parser.</exception>
        /// <returns>An object of type <typeparamref name="T"/> converted from <paramref name="arg"/>.</returns>
        public T ParseArgumentIntoObject<T>(string arg)
        {
            Func<string, object> converter;
            if (Converters.TryGetValue(typeof(T), out converter))
            {
                try
                {
                    return (T) converter(arg);
                }
                catch (Exception ex)
                {
                    throw new ArgumentParsingException($"String \"{arg}\" could not be parsed as type \"{typeof(T).Name}\".");
                }
            }
            else
            {
                throw new ArgumentParsingException($"No convertor exists for type {typeof(T).Name}. String value: {arg}");
            }
        }

        /// <summary>
        /// Will attempt to parse the passed in <paramref name="arg"/> into the passed in <paramref name="argType"/>
        /// </summary>
        /// <param name="argType">The expected output type of the conversion result.</param>
        /// <param name="arg">The argument to be parsed.</param>
        /// <exception cref="ArgumentParsingException">Thrown if the arugment did not parse properly or if there is an expected type which has no registered parser.</exception>
        /// <returns>An object of type <paramref name="argType"/> as an <see cref="Object"/></returns>
        public object ParseArgumentIntoObject(Type argType, string arg)
        {
            Func<string, object> converter;
            if (Converters.TryGetValue(argType, out converter))
            {
                try
                {
                    return converter(arg);
                }
                catch (Exception ex)
                {
                    throw new ArgumentParsingException($"String \"{arg}\" could not be parsed as type \"{argType.Name}\".");
                }
            }
            else
            {
                throw new ArgumentParsingException($"No convertor exists for type {argType.Name}. String value: {arg}");
            }
        }

        /// <summary>
        /// Returns a list containing all of the command parameters as defined by the provided command string.
        /// Example: "/testcommand 1 2 3" returns a list containing {"1", "2", "3"}
        /// </summary>
        /// <param name="command">The command string.</param>
        /// <returns>List of parameters provided to the command call within the command string.</returns>
        public static List<string> SplitCommandStringIntoArguments(string command)
        {
            var ret = new List<string>();
            StringBuilder current = new StringBuilder();
            bool quoted = false;

            foreach (char c in command)
            {
                switch (c)
                {
                    case '"':
                        quoted = !quoted;
                        break;

                    case ' ':
                        if (quoted)
                        {
                            current.Append(c);
                        }
                        else
                        {
                            ret.Add(current.ToString());
                            current.Clear();
                        }
                        break;

                    default:
                        current.Append(c);
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(current.ToString()))
            {
                ret.Add(current.ToString());
            }

            ret = ret.Skip(1).ToList();

            return ret;
        }
    }
}