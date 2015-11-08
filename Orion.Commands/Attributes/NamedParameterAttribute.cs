using System;

namespace Orion.Commands.Attributes
{
    public class NamedParameterAttribute : Attribute
    {
        public string Flag { get; set; }
        public bool Required { get; set; } = true;

        public NamedParameterAttribute()
        {
            
        }

        public NamedParameterAttribute(string flag)
        {
            Flag = flag;
        }

        public NamedParameterAttribute(string flag, bool required)
        {
            Flag = flag;
            Required = required;
        }

        public NamedParameterAttribute(bool required)
        {
            Required = required;
        }
    }
}