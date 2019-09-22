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
using System.IO;
using FluentAssertions;
using Orion.Networking.World.Tiles;
using Xunit;

namespace Orion.Networking.Packets.Modules {
    public class LiquidsModuleTests {
        [Fact]
        public void Liquids_Count_IsCorrect() {
            var module = new LiquidsModule();
            module.Liquids.Add(new NetworkLiquid());

            module.Liquids.Count.Should().Be(1);
        }

        [Fact]
        public void Liquids_IsReadOnly_IsCorrect() {
            var module = new LiquidsModule();

            module.Liquids.IsReadOnly.Should().BeFalse();
        }

        [Fact]
        public void Liquids_MarksAsDirty() {
            var module = new LiquidsModule();
            var liquid = new NetworkLiquid();

            module.Liquids.Add(liquid);
            module.ShouldBeDirty();
            module.Liquids.Remove(liquid);
            module.ShouldBeDirty();
            module.Liquids.Insert(0, liquid);
            module.ShouldBeDirty();
            module.Liquids.RemoveAt(0);
            module.ShouldBeDirty();
            module.Liquids.Add(liquid);
            module.ShouldBeDirty();
            module.Liquids[0] = liquid;
            module.ShouldBeDirty();
            module.Liquids.Clear();
            module.ShouldBeDirty();
        }

        [Fact]
        public void Liquids_SetNullItem_ThrowsArgumentNullException() {
            var module = new LiquidsModule();
            module.Liquids.Add(new NetworkLiquid());
            Action action = () => module.Liquids[0] = null;

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Liquids_AddNullItem_ThrowsArgumentNullException() {
            var module = new LiquidsModule();
            Action action = () => module.Liquids.Add(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Liquids_InsertNullItem_ThrowsArgumentNullException() {
            var module = new LiquidsModule();
            Action action = () => module.Liquids.Insert(0, null);

            action.Should().Throw<ArgumentNullException>();
        }

        public static readonly byte[] Bytes = {13, 0, 82, 0, 0, 1, 0, 100, 0, 0, 1, 255, 0};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(Bytes)) {
                var module = (ModulePacket)Packet.ReadFromStream(stream, PacketContext.Server);

                module.Module.Should().BeOfType<LiquidsModule>();
                module.Module.As<LiquidsModule>().Liquids.Should().HaveCount(1);
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
