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

using System;
using System.IO;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Orion.Entities {
    public class BuffTests {
        [Fact]
        public void Ctor_NegativeDuration_ThrowsArgumentOutOfRangeException() {
            Func<Buff> func = () => new Buff(BuffType.ObsidianSkin, TimeSpan.MinValue);

            func.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void BuffType_Get() {
            var buff = new Buff(BuffType.ObsidianSkin, TimeSpan.MaxValue);

            buff.BuffType.Should().Be(BuffType.ObsidianSkin);
        }

        [Fact]
        public void Duration_Get() {
            var duration = TimeSpan.FromHours(1);
            var buff = new Buff(BuffType.ObsidianSkin, duration);

            buff.Duration.Should().Be(duration);
        }

        [Fact]
        public void ReadFromReader_NullReader_ThrowsArgumentNullException() {
            Func<Buff> func = () => Buff.ReadFromReader(null, 2);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ReadFromReader_NumOfDurationBytesBad_ThrowsArgumentOutOfRangeException() {
            using var stream = new MemoryStream();
            using var reader = new BinaryReader(stream, Encoding.UTF8);
            Func<Buff> func = () => Buff.ReadFromReader(reader, 10);

            func.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void WriteToWriter_NullReader_ThrowsArgumentNullException() {
            var buff = new Buff();
            Action action = () => buff.WriteToWriter(null, 2);

            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void WriteToWriter_NumOfDurationBytesBad_ThrowsArgumentOutOfRangeException() {
            var buff = new Buff();
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            Action action = () => buff.WriteToWriter(writer, 10);

            action.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void WriteToWriter_ReadFromReader() {
            var duration = TimeSpan.FromHours(1);
            var buff = new Buff(BuffType.ObsidianSkin, duration);
            using var stream = new MemoryStream();
            using var writer = new BinaryWriter(stream, Encoding.UTF8);
            using var reader = new BinaryReader(stream, Encoding.UTF8);

            buff.WriteToWriter(writer, 4);
            stream.Position = 0;

            Buff.ReadFromReader(reader, 4).Should().Be(buff);
        }
    }
}
