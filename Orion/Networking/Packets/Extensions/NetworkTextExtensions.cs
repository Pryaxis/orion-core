using System.Text;

namespace Orion.Networking.Packets.Extensions {
    /// <summary>
    /// Provides extension methods for the Terraria.Localization.NetworkText class.
    /// </summary>
    internal static class NetworkTextExtensions {
        /// <summary>
        /// Gets the length of the text when encoded using the given encoding.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>The length.</returns>
        public static int GetBinaryLength(this Terraria.Localization.NetworkText text, Encoding encoding) {
            var length = 1 + text._text.GetBinaryLength(encoding);
            if (text._mode == Terraria.Localization.NetworkText.Mode.Literal) {
                return length;
            }

            length += 1;
            for (var i = 0; i < text._substitutions.Length && i < byte.MaxValue; ++i) {
                length += text._substitutions[i].GetBinaryLength(encoding);
            }

            return length;
        }
    }
}
