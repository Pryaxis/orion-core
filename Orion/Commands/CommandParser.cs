using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Orion.Commands
{
    public class CommandParser
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

        public static string GetCommandNameFromCommandString(string commandString)
        {
            return commandString.Split(' ').First().Replace("/", "");
        }

        public List<object> ParseCommandStringIntoArguments(string command, List<Type> expectedTypes)
        {
            var args = SplitCommandStringIntoArguments(command);

            if (args.Count != expectedTypes.Count)
            {
                throw new ArgumentException();
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
                        throw new ArgumentParsingException($"String \"{item.Value}\" could not be parsed as type \"{item.Type}\".");
                    }
                }
                else
                {
                    try
                    {
                        var instance = CreateInstanceOfUsingImport(item.Value, item.Type);
                        returnList.Add(instance);
                    }
                    catch (Exception ex)
                    {
                        throw new ArgumentParsingException($"String \"{item.Value}\" could not be imported as type \"{item.Type}\".");
                    }
                }
            }

            return returnList;
        }

        private object CreateInstanceOfUsingImport(string value, Type type)
        {
            var constructorInfo = type.GetConstructor(Type.EmptyTypes);
            if (constructorInfo != null)
            {
                var instance = (constructorInfo.Invoke(null)) as IImportable;
                if (instance == null)
                    return null;
                instance.Import(value);
                return instance;
            }
            return null;
        }

        private List<string> SplitCommandStringIntoArguments(string command)
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