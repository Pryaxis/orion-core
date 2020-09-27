using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.DataStructures
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    public sealed class SerializableNpcBuffTests
    {
        [Fact]
        public void Type_Set_Get()
        {
            var buff = new SerializableNpcBuff();

            buff.Type = 128;

            Assert.Equal(128, buff.Type);
        }

        [Fact]
        public void Time_Set_Get()
        {
            var buff = new SerializableNpcBuff();

            buff.Time = 600;

            Assert.Equal(600, buff.Time);
        }

        [Fact]
        public void Read()
        {
            var bytes = new byte[] { 0, 1, 120, 0 };

            SerializableNpcBuff.Read(bytes, out var buff);

            Assert.Equal(256, buff.Type);
            Assert.Equal(120, buff.Time);

            var buffer = new byte[4];

            var writtenLength = buff.Write(buffer);

            Assert.Equal(bytes.Length, writtenLength);
            Assert.Equal(bytes, buffer);
        }
    }
}
