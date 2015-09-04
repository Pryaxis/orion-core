using System;

namespace Orion.Commands
{
    public class ArgumentObject
    {
        public object Value { get; private set; }
        public Type Type { get; private set; }

        public ArgumentObject(object value, Type type)
        {
            Value = value;
            Type = type;
        }
    }
}