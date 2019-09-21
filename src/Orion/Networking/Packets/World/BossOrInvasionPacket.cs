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
using Orion.Entities;
using Orion.Networking.World;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Packet sent to summon a boss or an invasion.
    /// </summary>

    // TODO: maybe rethink how this is stored. Either? Or just make a Invasion/Boss enum.
    public sealed class BossOrInvasionPacket : Packet {
        private byte _summmonOnPlayerIndex;
        private short _type;

        /// <inheritdoc />
        public override PacketType Type => PacketType.BossOrInvasion;

        /// <summary>
        /// Gets or sets the player index to summon the boss on.
        /// </summary>
        public byte SummmonOnPlayerIndex {
            get => _summmonOnPlayerIndex;
            set {
                _summmonOnPlayerIndex = value;
                _isDirty = true;
            }
        }

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
        public NpcType Boss {
            get {
                if (!IsBoss) throw new InvalidOperationException();

                return NpcType.FromId(_type);
            }
            set {
                if (value == null) throw new ArgumentNullException(nameof(value));

                _type = value.Id;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the invasion type.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The value is being retrieved and <see cref="IsBoss"/> is <c>true</c>.
        /// </exception>
        public NetworkInvasion Invasion {
            get {
                if (IsBoss) throw new InvalidOperationException();

                return NetworkInvasion.FromId(_type);
            }
            set {
                if (value == null) throw new ArgumentNullException(nameof(value));

                _type = value.Id;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => IsBoss ? $"{Type}[{Boss}]" : $"{Type}[{Invasion}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SummmonOnPlayerIndex = (byte)reader.ReadInt16();

            var type = reader.ReadInt16();
            if (type < 0) {
                Invasion = NetworkInvasion.FromId(type) ?? throw new PacketException("Invasion is invalid.");
            } else {
                Boss = NpcType.FromId(type) ?? throw new PacketException("NPC type is invalid.");
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write((short)SummmonOnPlayerIndex);
            writer.Write(_type);
        }
    }
}
