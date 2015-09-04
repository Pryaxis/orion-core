using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orion.Commands
{
    public class CommandParser
    {
        private Dictionary<Type, Func<string, object>> Converters = new Dictionary<Type, Func<string, object>>()
    {
        {typeof(string), (string x) => {return x;}},
        {typeof(int), (string x) => {return int.Parse(x);}},
        {typeof(float), (string x) => {return float.Parse(x);}},
        {typeof(decimal), (string x) => {return decimal.Parse(x);}},
        {typeof(long), (string x) => {return long.Parse(x);}},
        {typeof(short), (string x) => {return short.Parse(x);}},
        {typeof(DateTime), (string x) => {return DateTime.Parse(x);}},
        {typeof(TimeSpan), (string x) => {return TimeSpan.Parse(x);}}
    };

        public string GetCommandName(string commandString)
        {
            return commandString.Split(' ').First().Replace("/", "");
        }

        public List<ArgumentObject> ParseCommand(string command, List<Type> expectedTypes)
        {
            var args = SplitCommandStringIntoArguments(command);

            if (args.Count != expectedTypes.Count)
            {
                throw new ArgumentException();
            }

            var returnList = new List<ArgumentObject>();
            var zipped = args.Zip(expectedTypes, (x, y) => { return new { Value = x, Type = y }; });
            foreach (var item in zipped)
            {
                Func<string, object> converter = null;
                if (Converters.TryGetValue(item.Type, out converter))
                {
                    try
                    {
                        var instance = converter(item.Value);
                        returnList.Add(new ArgumentObject(instance, item.Type));
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                else
                {
                    try
                    {
                        var instance = CreateInstanceOfUsingImport(item.Value, item.Type);
                        returnList.Add(new ArgumentObject(instance, item.Type));
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }

            return returnList;
        }

        private object CreateInstanceOfUsingImport(string value, Type type)
        {
            var instance = (type.GetConstructor(Type.EmptyTypes).Invoke(null)) as IImportable;
            instance.Import(value);
            return instance;
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