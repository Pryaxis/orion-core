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

using System.IO;
using Microsoft.Xna.Framework;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to cause an NPC to steal coins.
    /// </summary>
    /// <remarks>
    /// There appears to be a bug where clients are sending the packet to the server instead. The server echoes the
    /// packet back, which can cause desync issues.
    /// </remarks>
    public sealed class NpcStealCoinsPacket : Packet {
        private short _npcIndex;
        private float _value;
        private Vector2 _position;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcStealCoins;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's stolen value.
        /// </summary>
        /// <value>The NPC's stolen value.</value>
        /// <remarks>
        /// This property's value corresponds to copper coins 1:1. Thus, a value of <c>100</c> corresponds to a silver
        /// coin, a value of <c>10000</c> corresponds to a gold coin, and a value of <c>1000000</c> corresponds to a
        /// platinum coin.
        /// </remarks>
        public float Value {
            get => _value;
            set {
                _value = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the coins' position. The components are pixels.
        /// </summary>
        /// <value>The coins' position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _value = reader.ReadSingle();
            _position = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_value);
            writer.Write(in _position);
        }
    }
}
