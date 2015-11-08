using System;

namespace Orion.Commands.Attributes
{
    public class PositionalParameterAttribute : Attribute
    {
        public int Position { get; set; }
        public bool Required { get; set; } = false;

        public PositionalParameterAttribute(int position)
        {
            Position = position;
        }

        public PositionalParameterAttribute(int position, bool required)
        {
            Position = position;
            Required = required;
        }
    }
}