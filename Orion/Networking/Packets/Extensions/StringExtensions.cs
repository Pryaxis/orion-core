using System.Text;

namespace Orion.Networking.Packets.Extensions {
    /// <summary>
    /// Provides extension methods for the <see cref="string"/> class.
    /// </summary>
    internal static class StringExtensions {
        /// <summary>
        /// Gets the length of the string when encoded using the given encoding.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The length.</returns>
        public static int GetBinaryLength(this string str, Encoding encoding) {
            var byteLength = encoding.GetByteCount(str);

            // Compute length of variable-length quantity. (https://en.wikipedia.org/wiki/Variable-length_quantity)
            var counter = 1;
            var temp = byteLength;
            while (temp >= 128) {
                counter += 1;
                temp /= 256;
            }

            return counter + byteLength;
        }
    }
}
