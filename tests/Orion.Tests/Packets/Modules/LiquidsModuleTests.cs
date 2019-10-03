// Copyright (c) 2019 Pryaxis & Orion Contributors
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

using System.IO;
using FluentAssertions;
using Orion.Packets.World.Tiles;
using Xunit;

namespace Orion.Packets.Modules {
    public class LiquidsModuleTests {
        [Fact]
        public void Liquids_Count() {
            var module = new LiquidsModule();
            module.Liquids.Add(new NetworkLiquid());

            module.Liquids.Count.Should().Be(1);
        }

        [Fact]
        public void Liquids_IsReadOnly() {
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

        public static readonly byte[] Bytes = {13, 0, 82, 0, 0, 1, 0, 100, 0, 0, 1, 255, 0};

        [Fact]
        public void ReadFromStream() {
            using var stream = new MemoryStream(Bytes);
            var module = (ModulePacket)Packet.ReadFromStream(stream, PacketContext.Server);

            module.Module.Should().BeOfType<LiquidsModule>();
            module.Module.As<LiquidsModule>().Liquids.Should().HaveCount(1);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
