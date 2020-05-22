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
using System.Linq;
using Orion.Packets.Server;
using Xunit;

namespace Orion.Packets {
    public class PacketIdTests {
        [Fact]
        public void Type() {
            for (var i = 0; i < 256; ++i) {
                var id = (PacketId)i;
                var expectedType = typeof(PacketId).Assembly.ExportedTypes
                    .Where(t => t != typeof(UnknownPacket) && t != typeof(IPacket))
                    .Where(t => typeof(IPacket).IsAssignableFrom(t))
                    .Where(t => ((IPacket)Activator.CreateInstance(t)!).Id == id).FirstOrDefault()
                        ?? typeof(UnknownPacket);

                Assert.Equal(expectedType, id.Type());
            }
        }
    }
}
