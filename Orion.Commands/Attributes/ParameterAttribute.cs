using System;

namespace Orion.Commands.Attributes
{
    public class ParameterAttribute : Attribute
    {
        public string HelpText { get; set; }

        public ParameterAttribute()
        {
        }

        public ParameterAttribute(string helpText)
        {
            HelpText = helpText;
        }
    }
}