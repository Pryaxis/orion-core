using System.Collections.Generic;

namespace Orion.Commands.Extensions
{
    public static class ListExt
    {
        public static int IndexOfFlag(this List<string> self, string flagName)
        {
            //TODO: Make extensible for custom flag specifiers.
            var index = self.IndexOf($"/{flagName}");
            if (index == -1)
                return self.IndexOf($"-{flagName}");
            return index;
        } 
    }
}