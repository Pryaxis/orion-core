using System;

namespace Orion.Commands.Attributes
{
    public class PositionalParameterAttribute : Attribute
    {
        public int Position { get; set; }

        public PositionalParameterAttribute(int position)
        {
            Position = position;
        }
    }
}