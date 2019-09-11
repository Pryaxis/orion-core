// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent from the server to the client to show a combat number.
    /// </summary>
    public sealed class CombatNumberPacket : Packet {
        /// <summary>
        /// Gets or sets the number's position.
        /// </summary>
        public Vector2 NumberPosition { get; set; }

        /// <summary>
        /// Gets or sets the number's color.
        /// </summary>
        public Color NumberColor { get; set; }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public int Number { get; set; }

        private protected override PacketType Type => PacketType.CombatNumber;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Number} ({NumberColor}) @ {NumberPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NumberPosition = reader.ReadVector2();
            NumberColor = reader.ReadColor();
            Number = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NumberPosition);
            writer.Write(NumberColor);
            writer.Write(Number);
        }
    }
}
