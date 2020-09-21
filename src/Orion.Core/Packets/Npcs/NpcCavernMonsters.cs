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
