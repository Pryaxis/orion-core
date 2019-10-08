// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using TerrariaNetworkText = Terraria.Localization.NetworkText;

namespace Orion.Packets.Extensions {
    internal static class BinaryExtensions {
        public static Color ReadColor(this BinaryReader reader) {
            Debug.Assert(reader != null, "reader should not be null");

            return new Color(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        }

        public static TerrariaNetworkText ReadNetworkText(this BinaryReader reader) {
            Debug.Assert(reader != null, "reader should not be null");

            return TerrariaNetworkText.Deserialize(reader);
        }

        public static Terraria.DataStructures.PlayerDeathReason ReadPlayerDeathReason(this BinaryReader reader) {
            Debug.Assert(reader != null, "reader should not be null");

            return Terraria.DataStructures.PlayerDeathReason.FromReader(reader);
        }

        public static Vector2 ReadVector2(this BinaryReader reader) {
            Debug.Assert(reader != null, "reader should not be null");

            return new Vector2(reader.ReadSingle(), reader.ReadSingle());
        }

        public static void Write(this BinaryWriter writer, Color color) {
            Debug.Assert(writer != null, "writer should not be null");

            writer.Write(color.R);
            writer.Write(color.G);
            writer.Write(color.B);
        }

        public static void Write(this BinaryWriter writer,
                Terraria.DataStructures.PlayerDeathReason playerDeathReason) {
            Debug.Assert(writer != null, "writer should not be null");

            playerDeathReason.WriteSelfTo(writer);
        }

        public static void Write(this BinaryWriter writer, TerrariaNetworkText text) {
            Debug.Assert(writer != null, "writer should not be null");

            text.Serialize(writer);
        }

        public static void Write(this BinaryWriter writer, Vector2 vector) {
            Debug.Assert(writer != null, "writer should not be null");

            writer.Write(vector.X);
            writer.Write(vector.Y);
        }
    }
}
