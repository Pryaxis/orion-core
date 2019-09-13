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

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to damage an NPC.
    /// </summary>
    public sealed class DamageNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public short Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float Knockback { get; set; }

        /// <summary>
        /// Gets or sets the hit direction.
        /// </summary>
        public int HitDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the hit is critical.
        /// </summary>
        public bool IsCriticalHit { get; set; }

        internal override PacketType Type => PacketType.DamageNpc;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} by {Damage}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            Damage = reader.ReadInt16();
            Knockback = reader.ReadSingle();
            HitDirection = reader.ReadByte() - 1;
            IsCriticalHit = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(Damage);
            writer.Write(Knockback);
            writer.Write((byte)(HitDirection + 1));
            writer.Write(IsCriticalHit);
        }
    }
}
