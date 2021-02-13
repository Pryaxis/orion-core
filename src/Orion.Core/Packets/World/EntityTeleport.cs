// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Events.World.Tiles;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to teleport an entity.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct EntityTeleport : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference
        [FieldOffset(12)] private byte _bytes2; // Used to obtain an interior reference
        [FieldOffset(0)] private Flags8 _flags;

        /// <summary>
        /// Gets or sets the entity's index.
        /// </summary>
        [field: FieldOffset(1)] public short EntityIndex { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [field: FieldOffset(3)] public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        [field: FieldOffset(11)] public byte Style { get; set; }

        /// <summary>
        /// Gets or sets extra info.
        /// </summary>
        [field: FieldOffset(12)] public int ExtraInfo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the target's position is used with <see cref="TeleportationType.PlayerToPlayer"/>.
        /// </summary>
        public bool UseTargetsPosition
        {
            get => _flags[2];
            set => _flags[2] = value;
        }

        /// <summary>
        /// Gets or sets the teleportation type.
        /// </summary>
        public TeleportationType TeleportationType
        {
            get => true switch
            {
                _ when _flags[0] => TeleportationType.Npc,
                _ when _flags[1] => TeleportationType.PlayerToPlayer,
                _ => TeleportationType.Player
            };
            set
            {
                switch (value)
                {
                case TeleportationType.Npc:
                    _flags[0] = true;
                    break;
                case TeleportationType.PlayerToPlayer:
                    _flags[1] = true;
                    break;
                default:
                    _flags[0] = false;
                    _flags[1] = false;
                    break;
                }
            }
        }

        PacketId IPacket.Id => PacketId.EntityTeleport;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 12);
            if (_flags[3])
            {
                length += span[length..].Read(ref _bytes2, 4);
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 12);
            if (ExtraInfo > 0)
            {
                length += span[length..].Write(ref _bytes2, 4);
            }

            return length;
        }
    }

    /// <summary>
    /// Specifies a teleportation type used with the <see cref="EntityTeleport"/> packet.
    /// </summary>
    public enum TeleportationType : byte
    {
        /// <summary>
        /// Indicates player teleportation (player -> arbitrary location).
        /// </summary>
        Player,

        /// <summary>
        /// Indicates NPC teleportation (NPC -> arbitrary location).
        /// </summary>
        Npc,

        /// <summary>
        /// Indicates player to player teleportation.
        /// </summary>
        PlayerToPlayer
    }
}
