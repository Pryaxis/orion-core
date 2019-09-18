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

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent to cause an NPC to steal a coin.
    /// </summary>
    public sealed class NpcStealCoinPacket : Packet {
        private short _npcIndex;
        private float _npcStolenValue;
        private Vector2 _coinPosition;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcStealCoin;

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
        /// Gets or sets the coin's position.
        /// </summary>
        public Vector2 CoinPosition {
            get => _coinPosition;
            set {
                _coinPosition = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex}, V={NpcStolenValue} @ {CoinPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            NpcStolenValue = reader.ReadSingle();
            CoinPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(NpcStolenValue);
            writer.Write(CoinPosition);
        }
    }
}
