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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent to cause an NPC to steal a coin. This is sent from clients and the logic occurs clientside, but the
    /// server echoes the packet back to clients, which may cause desync issues.
    /// </summary>
    public sealed class NpcStealCoinsPacket : Packet {
        private short _npcIndex;
        private float _npcStolenValue;
        private Vector2 _coinsPosition;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcStealCoins;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
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
        public float NpcStolenValue {
            get => _npcStolenValue;
            set {
                _npcStolenValue = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the coins' position. The components are pixel-based.
        /// </summary>
        public Vector2 CoinsPosition {
            get => _coinsPosition;
            set {
                _coinsPosition = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} stole {NpcStolenValue} @ {CoinsPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _npcStolenValue = reader.ReadSingle();
            _coinsPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_npcStolenValue);
            writer.Write(_coinsPosition);
        }
    }
}
