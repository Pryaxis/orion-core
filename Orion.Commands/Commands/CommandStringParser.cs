using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orion.Commands.Commands.Exceptions;
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

        /// <summary>
        /// Attempts to parse the parameters provided in the command string into the types expected.
        /// Will parse the parameters into the types in order in which they are provided in the expected types list.
        /// </summary>
        /// <param name="command">The command string.</param>
        /// <param name="expectedTypes">The expected types for the command callback in the order in which they are expected.</param>
        /// <exception cref="ArgumentParsingException">Thrown if not all arguments are provided or if there is an expected type which cannot be parsed properly.</exception>
        /// <returns>A list of objects with the proper arguments in the proper order for the command callback.</returns>
        public List<object> ParseCommandStringIntoArguments(string command, List<Type> expectedTypes)
        {
            var args = SplitCommandStringIntoArguments(command);

            if (expectedTypes.First() == typeof (BasePlayer))
            {
                expectedTypes = expectedTypes.Skip(1).ToList();
            }

            if (args.Count != expectedTypes.Count)
            {
                throw new ArgumentParsingException("Not all required arguments have been provided.");
            }

            var returnList = new List<object>();
            var zipped = args.Zip(expectedTypes, (x, y) => new { Value = x, Type = y });
            foreach (var item in zipped)
            {
                Func<string, object> converter;
                if (Converters.TryGetValue(item.Type, out converter))
                {
                    try
                    {
                        var instance = converter(item.Value);
                        returnList.Add(instance);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentParsingException($"String \"{item.Value}\" could not be parsed as type \"{item.Type.Name}\".");
                    }
                }
                else
                {
                    throw new ArgumentParsingException($"No convertor exists for type {item.Type.Name}. String value: {item.Value}");
                }
            }

            return returnList;
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