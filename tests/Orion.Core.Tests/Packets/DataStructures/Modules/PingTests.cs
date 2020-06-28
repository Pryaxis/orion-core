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

using System.Diagnostics.CodeAnalysis;
using Orion.Core.Utils;
using Xunit;

namespace Orion.Core.Packets.DataStructures.Modules
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PingTests
    {
        private readonly byte[] _bytes = { 2, 0, 128, 51, 131, 71, 0, 112, 212, 69 };

        [Fact]
        public void Position_Set_Get()
        {
            var ping = new Ping();

            ping.Position = new Vector2f(67175, 6798);

            Assert.Equal(new Vector2f(67175, 6798), ping.Position);
        }

        [Fact]
        public void Read()
        {
            var ping = TestUtils.ReadModule<Ping>(_bytes, PacketContext.Server);

            Assert.Equal(new Vector2f(67175, 6798), ping.Position);
        }
    }
}
