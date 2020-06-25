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
using Xunit;

namespace Orion.Core.Packets.DataStructures.Modules
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class UnknownModuleTests
    {
        private readonly byte[] _bytes = { 255, 255, 0, 1, 2, 3, 4, 5, 6, 7 };
        private readonly byte[] _emptyBytes = { 255, 255 };

        [Fact]
        public void Data_Get()
        {
            var module = new UnknownModule(10, (ModuleId)65535);

            Assert.Equal(8, module.Data.Length);
        }

        [Fact]
        public void Id_Get()
        {
            var module = new UnknownModule(10, (ModuleId)65535);

            Assert.Equal((ModuleId)65535, module.Id);
        }

        [Fact]
        public void Read()
        {
            var module = TestUtils.ReadModule<UnknownModule>(_bytes, PacketContext.Server);

            Assert.Equal(8, module.Data.Length);
            for (var i = 0; i < 8; ++i)
            {
                Assert.Equal(i, module.Data[i]);
            }
        }

        [Fact]
        public void Read_Empty()
        {
            var module = TestUtils.ReadModule<UnknownModule>(_emptyBytes, PacketContext.Server);

            Assert.Equal(0, module.Data.Length);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripModule(_bytes, PacketContext.Server);
        }

        [Fact]
        public void RoundTrip_Empty()
        {
            TestUtils.RoundTripModule(_emptyBytes, PacketContext.Server);
        }
    }
}
