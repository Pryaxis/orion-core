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

using System.Diagnostics.CodeAnalysis;
using FluentAssertions;

using Microsoft.Xna.Framework;
using Xunit;

namespace Orion.Entities {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class OrionEntityTests {
        [Fact]
        public void GetIndex_IsCorrect() {
            var terrariaEntity = new TestTerrariaEntity {whoAmI = 100};
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.Index.Should().Be(100);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void GetIsActive_IsCorrect(bool isActive) {
            var terrariaEntity = new TestTerrariaEntity {active = isActive};
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.IsActive.Should().Be(isActive);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SetIsActive_IsCorrect(bool isActive) {
            var terrariaEntity = new TestTerrariaEntity();
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.IsActive = isActive;

            terrariaEntity.active.Should().Be(isActive);
        }

        [Fact]
        public void GetPosition_IsCorrect() {
            var terrariaEntity = new TestTerrariaEntity {position = new Vector2(100, 100)};
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.Position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetPosition_IsCorrect() {
            var terrariaEntity = new TestTerrariaEntity();
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.Position = new Vector2(100, 100);

            terrariaEntity.position.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetVelocity_IsCorrect() {
            var terrariaEntity = new TestTerrariaEntity {velocity = new Vector2(100, 100)};
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.Velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetVelocity_IsCorrect() {
            var terrariaEntity = new TestTerrariaEntity();
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.Velocity = new Vector2(100, 100);

            terrariaEntity.velocity.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void GetSize_IsCorrect() {
            var terrariaEntity = new TestTerrariaEntity {Size = new Vector2(100, 100)};
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.Size.Should().Be(new Vector2(100, 100));
        }

        [Fact]
        public void SetSize_IsCorrect() {
            var terrariaEntity = new TestTerrariaEntity();
            IEntity entity = new TestOrionEntity(terrariaEntity);

            entity.Size = new Vector2(100, 100);

            terrariaEntity.Size.Should().Be(new Vector2(100, 100));
        }

        private class TestOrionEntity : OrionEntity<TestTerrariaEntity> {
            public override string Name { get; set; } = "test";

            public TestOrionEntity( TestTerrariaEntity terrariaEntity) : base(terrariaEntity) { }
        }

        private class TestTerrariaEntity : Terraria.Entity { }
    }
}
