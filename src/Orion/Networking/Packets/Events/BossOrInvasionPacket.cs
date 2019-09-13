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

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Npcs;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent to summon a boss or an invasion.
    /// </summary>
    public sealed class BossOrInvasionPacket : Packet {
        private short _type;

        /// <summary>
        /// Gets or sets the player index to summon the boss on.
        /// </summary>
        public byte SummmonOnPlayerIndex { get; set; }

        /// <summary>
        /// Gets a value indicating whether the packet is for a boss.
        /// </summary>
        public bool IsBoss => _type > 0;

        /// <summary>
        /// Gets or sets the boss type.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The value is being retrieved and <see cref="IsBoss"/> is <c>false</c>.
        /// </exception>
        public NpcType BossType {
            get {
                if (!IsBoss) throw new InvalidOperationException();

                return (NpcType)_type;
            }
            set => _type = (short)value;
        }

        /// <summary>
        /// Gets or sets the invasion type.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The value is being retrieved and <see cref="IsBoss"/> is <c>true</c>.
        /// </exception>
        public NetworkInvasionType InvasionType {
            get {
                if (IsBoss) throw new InvalidOperationException();

                return (NetworkInvasionType)_type;
            }
            set => _type = (short)value;
        }

        internal override PacketType Type => PacketType.BossOrInvasion;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => IsBoss ? $"{Type}[{BossType}]" : $"{Type}[{InvasionType}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SummmonOnPlayerIndex = (byte)reader.ReadInt16();
            _type = reader.ReadInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)SummmonOnPlayerIndex);
            writer.Write(_type);
        }
    }
}
