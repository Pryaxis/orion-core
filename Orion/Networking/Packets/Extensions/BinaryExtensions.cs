using System.IO;
using Microsoft.Xna.Framework;

namespace Orion.Networking.Packets.Extensions {
    /// <summary>
    /// Provides extension methods for the <see cref="BinaryReader"/> and <see cref="BinaryWriter"/> classes.
    /// </summary>
    internal static class BinaryExtensions {
        /// <summary>
        /// Reads a color from the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The color.</returns>
        public static Color ReadColor(this BinaryReader reader) {
            byte red = reader.ReadByte();
            byte green = reader.ReadByte();
            byte blue = reader.ReadByte();
            return new Color(red, green, blue);
        }

        /// <summary>
        /// Reads a Terraria.Localization.NetworkText from the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The text.</returns>
        public static Terraria.Localization.NetworkText ReadNetworkText(this BinaryReader reader) {
            return Terraria.Localization.NetworkText.Deserialize(reader);
        }

        /// <summary>
        /// Writes a color to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="color">The color.</param>
        public static void Write(this BinaryWriter writer, Color color) {
            writer.Write(color.R);
            writer.Write(color.G);
            writer.Write(color.B);
        }

        /// <summary>
        /// Writes a Terraria.Localization.NetworkText to the specified writer.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <param name="text">The text.</param>
        public static void Write(this BinaryWriter writer, Terraria.Localization.NetworkText text) {
            text.Serialize(writer);
        }
    }
}
