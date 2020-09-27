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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Npcs
{
    /// <summary>
    /// A packet sent to set cave monsters.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct NpcCavernMonsters : IPacket
    {
        [FieldOffset(0)] private ushort[,]? _monsterTypes;

        /// <summary>
        /// Gets a two-dimensional array of monster types.
        /// </summary>
        public ushort[,] CavernMonsterTypes => _monsterTypes ??= new ushort[2, 3];

        PacketId IPacket.Id => PacketId.NpcCavernMonsters;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) =>
            span.Read(ref Unsafe.As<ushort, byte>(ref CavernMonsterTypes[0, 0]), 12);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) =>
            span.Write(ref Unsafe.As<ushort, byte>(ref CavernMonsterTypes[0, 0]), 12);
    }
}
