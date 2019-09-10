using FluentAssertions;
using Orion.World.TileEntities;
using Xunit;
using TDS = Terraria.DataStructures;
using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.Tests.World.TileEntities {
    public class OrionTargetDummyTests {
        [Theory]
        [InlineData(100)]
        public void GetIndex_IsCorrect(int index) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy {ID = index};
            var targetDummy = new OrionTargetDummy(terrariaTrainingDummy);

            targetDummy.Index.Should().Be(index);
        }

        [Theory]
        [InlineData(100)]
        public void GetX_IsCorrect(int x) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy {Position = new TDS.Point16(x, 0)};
            var targetDummy = new OrionTargetDummy(terrariaTrainingDummy);

            targetDummy.X.Should().Be(x);
        }

        [Theory]
        [InlineData(100)]
        public void SetX_IsCorrect(int x) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy();
            var targetDummy = new OrionTargetDummy(terrariaTrainingDummy);

            targetDummy.X = x;

            terrariaTrainingDummy.Position.X.Should().Be((short)x);
        }

        [Theory]
        [InlineData(100)]
        public void GetY_IsCorrect(int y) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy {Position = new TDS.Point16(0, y)};
            var targetDummy = new OrionTargetDummy(terrariaTrainingDummy);

            targetDummy.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(100)]
        public void SetY_IsCorrect(int y) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy();
            var targetDummy = new OrionTargetDummy(terrariaTrainingDummy);

            targetDummy.Y = y;

            terrariaTrainingDummy.Position.Y.Should().Be((short)y);
        }

        [Theory]
        [InlineData(100)]
        public void GetNpcIndex_IsCorrect(int npcIndex) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy {npc = npcIndex};
            var targetDummy = new OrionTargetDummy(terrariaTrainingDummy);

            targetDummy.NpcIndex.Should().Be(npcIndex);
        }
    }
}
