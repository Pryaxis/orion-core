using System.IO;
using Microsoft.Xna.Framework;

namespace Orion.Networking.Packets.Extensions {
    internal static class BinaryExtensions {
        public static Color ReadColor(this BinaryReader reader) {
            byte red = reader.ReadByte();
            byte green = reader.ReadByte();
            byte blue = reader.ReadByte();
            return new Color(red, green, blue);
        }

        public static Terraria.Localization.NetworkText ReadNetworkText(this BinaryReader reader) {
            return Terraria.Localization.NetworkText.Deserialize(reader);
        }

        public static Vector2 ReadVector2(this BinaryReader reader) =>
            new Vector2(reader.ReadSingle(), reader.ReadSingle());

        public static void Write(this BinaryWriter writer, Color color) {
            writer.Write(color.R);
            writer.Write(color.G);
            writer.Write(color.B);
        }

        public static void Write(this BinaryWriter writer, Terraria.Localization.NetworkText text) {
            text.Serialize(writer);
        }

        public static void Write(this BinaryWriter writer, Vector2 vector) {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }
    }
}
