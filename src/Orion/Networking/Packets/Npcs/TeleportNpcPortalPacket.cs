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
    /// Packet sent to teleport an NPC through a portal.
    /// </summary>
    public sealed class TeleportNpcPortalPacket : Packet {
        private short _npcIndex;
        private short _portalIndex;
        private Vector2 _newNpcPosition;
        private Vector2 _newNpcVelocity;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the portal index.
        /// </summary>
        public short PortalIndex {
            get => _portalIndex;
            set {
                _portalIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's new position.
        /// </summary>
        public Vector2 NewNpcPosition {
            get => _newNpcPosition;
            set {
                _newNpcPosition = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's new velocity.
        /// </summary>
        public Vector2 NewNpcVelocity {
            get => _newNpcVelocity;
            set {
                _newNpcVelocity = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override PacketType Type => PacketType.TeleportNpcPortal;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex} @ {NewNpcPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _portalIndex = reader.ReadInt16();
            _newNpcPosition = reader.ReadVector2();
            _newNpcVelocity = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(PortalIndex);
            writer.Write(NewNpcPosition);
            writer.Write(NewNpcVelocity);
        }
    }
}
