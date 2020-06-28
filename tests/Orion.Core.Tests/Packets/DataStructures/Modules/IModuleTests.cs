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
    public class IModuleTests
    {
        [Fact]
        public void Write_NullModule_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IModuleExtensions.Write(null!, default, PacketContext.Server));
        }

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
        public void Write_Struct_AsServer()
        {
            var module = new TestStructModule { Value = 42 };
            var bytes = new byte[1000];

            var length = module.Write(bytes, PacketContext.Server);

            Assert.Equal(new byte[] { 255, 255, 42 }, bytes[..length]);
        }

        [Fact]
        public void Write_Struct_AsClient()
        {
            var module = new TestStructModule { Value = 42 };
            var bytes = new byte[1000];

            var length = module.Write(bytes, PacketContext.Client);

            Assert.Equal(new byte[] { 255, 255, 0 }, bytes[..length]);
        }

        private sealed class TestModule : IModule
        {
            public byte Value { get; set; }

            public ModuleId Id => (ModuleId)65535;

            public int ReadBody(Span<byte> span, PacketContext context)
            {
                Value = (byte)(span[0] + (context == PacketContext.Server ? 0 : 42));
                return 1;
            }

            public int WriteBody(Span<byte> span, PacketContext context)
            {
                span[0] = (byte)(Value - (context == PacketContext.Server ? 0 : 42));
                return 1;
            }
        }

        private struct TestStructModule : IModule
        {
            public byte Value { get; set; }

            public ModuleId Id => (ModuleId)65535;

            public int ReadBody(Span<byte> span, PacketContext context)
            {
                Value = (byte)(span[0] + (context == PacketContext.Server ? 0 : 42));
                return 1;
            }

            public int WriteBody(Span<byte> span, PacketContext context)
            {
                span[0] = (byte)(Value - (context == PacketContext.Server ? 0 : 42));
                return 1;
            }
        }
    }
}
