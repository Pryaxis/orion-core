using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Orion.Commands
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

        public void AddConverter<T>(Func<string, object> converter)
        {
            Converters.Add(typeof (T), converter);
        }

        public static string GetCommandNameFromCommandString(string commandString)
        {
            return commandString.Split(' ').First().Replace("/", "");
        }

        public List<object> ParseCommandStringIntoArguments(string command, List<Type> expectedTypes)
        {
            var args = SplitCommandStringIntoArguments(command);

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
                        throw new ArgumentParsingException($"String \"{item.Value}\" could not be parsed as type \"{item.Type}\".");
                    }
                }
                else
                {
                    throw new ArgumentParsingException($"No convertor exists for type {item.Type}. String value: {item.Value}");
                }
            }

            return returnList;
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