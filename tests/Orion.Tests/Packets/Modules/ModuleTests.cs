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

namespace Orion.Packets.Modules {
    public class ModuleTests {
        [Fact]
        public void WriteWithHeader() {
            var bytes = new byte[3];
            var module = new TestModule();

            Assert.Equal(3, module.WriteWithHeader(bytes, PacketContext.Server));

            Assert.Equal(new byte[] { 255, 255, 42 }, bytes);
        }

        private struct TestModule : IModule {
            public ModuleId Id => (ModuleId)65535;

            public int Read(Span<byte> span, PacketContext context) => throw new NotImplementedException();

            public int Write(Span<byte> span, PacketContext context) {
                span[0] = 42;
                return 1;
            }
        }
    }
}
