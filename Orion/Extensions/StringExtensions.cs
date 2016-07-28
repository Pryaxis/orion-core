using System.Text;
using System.Text.RegularExpressions;

namespace Orion.Extensions
{
    /// <summary>
    /// A collection of extensions for the <see cref="System.String"/> type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts this string instance to its URL slug equivalent
        /// </summary>
        /// <param name="phrase">
        /// The string phrase to return the slug for
        /// </param>
        /// <returns>
        /// The slugified string equivalent of the <paramref name="phrase"/>.
        /// </returns>
        public static string Slugify(this string phrase)
        {
            if (phrase == null) return "";

            const int maxlen = 80;
            int len = phrase.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;

            for (int i = 0; i < len; i++)
            {
                c = phrase[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                    c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(c);
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (i == maxlen) break;
            }

            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }
    }
}
