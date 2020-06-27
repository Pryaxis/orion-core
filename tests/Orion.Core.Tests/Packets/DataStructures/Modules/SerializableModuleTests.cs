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
using Xunit;

namespace Orion.Core.Packets.DataStructures.Modules
{
    public class SerializableModuleTests
    {
        [Fact]
        public void Write_AsServer()
        {
            var module = new TestModule { Value = 42 };
            var bytes = new byte[1000];

            var length = module.Write(bytes, PacketContext.Server);

            Assert.Equal(new byte[] { 255, 255, 42 }, bytes[..length]);
        }

        [Fact]
        public void Write_AsClient()
        {
            var module = new TestModule { Value = 42 };
            var bytes = new byte[1000];

            var length = module.Write(bytes, PacketContext.Client);

            Assert.Equal(new byte[] { 255, 255, 0 }, bytes[..length]);
        }

        [Fact]
        public void Read()
        {
            var bytes = new byte[] { 255, 255, 42 };

            var length = SerializableModule.Read(bytes, PacketContext.Server, out var module);
            Assert.Equal(bytes.Length, length);
            Assert.IsType<UnknownModule>(module);

            Assert.Equal((ModuleId)65535, module.Id);
            Assert.Equal(1, ((UnknownModule)module).Data.Length);
            Assert.Equal(42, ((UnknownModule)module).Data[0]);
        }

        private sealed class TestModule : SerializableModule
        {
            public override ModuleId Id => (ModuleId)65535;

            public byte Value { get; set; }

            protected override int ReadBody(Span<byte> span, PacketContext context)
            {
                Value = (byte)(span[0] + (context == PacketContext.Server ? 0 : 42));
                return 1;
            }

            protected override int WriteBody(Span<byte> span, PacketContext context)
            {
                span[0] = (byte)(Value - (context == PacketContext.Server ? 0 : 42));
                return 1;
            }
        }
    }
}
