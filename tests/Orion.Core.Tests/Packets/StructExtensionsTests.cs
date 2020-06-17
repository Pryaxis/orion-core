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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

namespace Orion.Core.Packets
{
    public class StructExtensionsTests
    {
        [Fact]
        public void AsRefByte()
        {
            var testStruct = new TestStruct();

            Unsafe.WriteUnaligned(ref testStruct.AsRefByte(4), 1234);

            Assert.Equal(1234, testStruct.Value2);
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct TestStruct
        {
            [FieldOffset(0)] public int Value;
            [FieldOffset(4)] public int Value2;
        }
    }
}
