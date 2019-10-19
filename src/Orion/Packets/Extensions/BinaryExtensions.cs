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

using System;
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

        public static TimeSpan ReadTimeSpan(this BinaryReader reader, int numOfBytes) {
            Debug.Assert(reader != null, "reader should not be null");

            var ticks = numOfBytes switch
            {
                2 => reader.ReadInt16(),
                4 => reader.ReadInt32(),
                _ => throw new ArgumentException("Number of bytes is not 2 or 4", nameof(numOfBytes))
            };
            return TimeSpan.FromSeconds(ticks / 60.0);
        }

        public static void Write(this BinaryWriter writer, in Color color) {
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

        public static void Write(this BinaryWriter writer, in Vector2 vector) {
            Debug.Assert(writer != null, "writer should not be null");

            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

        public static void Write(this BinaryWriter writer, in TimeSpan timeSpan, int numOfBytes) {
            Debug.Assert(writer != null, "writer should not be null");

            var ticks = timeSpan.TotalSeconds * 60.0;
            switch (numOfBytes) {
            case 2:
                var shortTicks = ticks >= short.MaxValue ? short.MaxValue : (short)ticks;
                writer.Write(shortTicks);
                return;
            case 4:
                var intTicks = ticks >= int.MaxValue ? int.MaxValue : (int)ticks;
                writer.Write(intTicks);
                return;
            default:
                throw new ArgumentException("Number of bytes is not 2 or 4", nameof(numOfBytes));
            }
        }
    }
}
