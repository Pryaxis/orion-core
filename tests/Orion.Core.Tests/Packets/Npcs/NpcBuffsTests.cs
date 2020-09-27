using System;
using System.Collections.Generic;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Xunit;

namespace Orion.Core.Packets.Npcs
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class NpcBuffsTests
    {
        private readonly byte[] _bytes = { 7, 0, 54, 5, 0, 1, 0, 60, 0, 2, 0, 70, 0, 3, 0, 80, 0, 4, 0, 90, 0, 5, 0, 120, 0 };

        [Fact]
        public void NpcIndex_Set_Get()
        {
            var packet = new NpcBuffs();

            packet.NpcIndex = 1;

            Assert.Equal(1, packet.NpcIndex);
        }

        [Fact]
        public void Buffs_Set_Get()
        {
            var packet = new NpcBuffs();

            for (ushort i = 0; i < packet.Buffs.Length; ++i)
            {
                packet.Buffs[i] = new SerializableNpcBuff() { Type = i, Time = (short) ((i + 1) * 60) };
            }

            for (ushort i = 0; i < packet.Buffs.Length; ++i)
            {
                Assert.Equal(new SerializableNpcBuff() { Type = i, Time = (short)((i + 1) * 60) }, packet.Buffs[i]);
            }
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<NpcBuffs>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.NpcIndex);
            Assert.Equal(new SerializableNpcBuff() { Type = 1, Time = 60 }, packet.Buffs[0]);
            Assert.Equal(new SerializableNpcBuff() { Type = 2, Time = 70 }, packet.Buffs[1]);
            Assert.Equal(new SerializableNpcBuff() { Type = 3, Time = 80 }, packet.Buffs[2]);
            Assert.Equal(new SerializableNpcBuff() { Type = 4, Time = 90 }, packet.Buffs[3]);
            Assert.Equal(new SerializableNpcBuff() { Type = 5, Time = 120 }, packet.Buffs[4]);
        }
    }
}
