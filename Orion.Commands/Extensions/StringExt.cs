namespace Orion.Commands.Extensions
{
    public static class StringExt
    {
        public static bool IsFlagOrSwitch(this string self)
        {
            //TODO: Extensibility for custom flag specifiers.
            return self.StartsWith("/") || self.StartsWith("-");
        }
    }
}