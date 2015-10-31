using System;

namespace Orion.Commands.Attributes
{
    public class NamedParameterAttribute : Attribute
    {
        public string Flag { get; set; }

        public NamedParameterAttribute()
        {
            
        }

        public NamedParameterAttribute(string flag)
        {
            Flag = flag;
        }
    }
}