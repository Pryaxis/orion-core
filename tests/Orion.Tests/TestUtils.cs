// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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

namespace Orion {
    public class TestUtils {
        public static void FakeReceiveBytes(int index, byte[] bytes) {
            var buffer = Terraria.NetMessage.buffer[index];
            var oldBytes = buffer.readBuffer;
            buffer.readBuffer = bytes;
            buffer.ResetReader();
            buffer.GetData(2, bytes.Length - 2, out _);
            buffer.readBuffer = oldBytes;
        }
    }

    public class TerrariaTestsFixture : IDisposable {
        private readonly Terraria.Main _main;

        public TerrariaTestsFixture() {
            _main = new Terraria.Main();

            _main.Initialize();
        }

        public void Dispose() {
            _main.Dispose();
        }
    }

    // These tests cannot run in parallel since they interact heavily with Terraria's static state. So we have to run
    // these in series.
    [CollectionDefinition("TerrariaTestsCollection", DisableParallelization = true)]
    public class TerrariaTestsCollection : ICollectionFixture<TerrariaTestsFixture> { }
}
