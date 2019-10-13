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
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent from the server to the client to show a combat number.
    /// 
    /// <para/>
    /// 
    /// Combat numbers are the numbers that show up when, e.g., a player is hurt or an NPC is damaged.
    /// </summary>
    public sealed class CombatNumberPacket : Packet {
        private Vector2 _numberPosition;
        private Color _numberColor;
        private int _number;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.CombatNumber;

        /// <summary>
        /// Gets or sets the number's position. The components are pixel-based.
        /// </summary>
        public Vector2 NumberPosition {
            get => _numberPosition;
            set {
                _numberPosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number's color. The alpha component is ignored.
        /// </summary>
        public Color NumberColor {
            get => _numberColor;
            set {
                _numberColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number.
        /// </summary>
        public int Number {
            get => _number;
            set {
                _number = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Number} ({NumberColor}) @ {NumberPosition}]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _numberPosition = reader.ReadVector2();
            _numberColor = reader.ReadColor();
            _number = reader.ReadInt32();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_numberPosition);
            writer.Write(_numberColor);
            writer.Write(_number);
        }
    }
}
