using FluentAssertions;
using Orion.World.TileEntities;
using Xunit;
using TDS = Terraria.DataStructures;
using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.Tests.World.TileEntities {
    public class OrionTrainingDummyTests {
        [Theory]
        [InlineData(100)]
        public void GetNpcIndex_IsCorrect(int npcIndex) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy {npc = npcIndex};
            var trainingDummy = new OrionTrainingDummy(terrariaTrainingDummy);

            trainingDummy.NpcIndex.Should().Be(npcIndex);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetX_IsCorrect(int x) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy {Position = new TDS.Point16(x, 0)};
            var trainingDummy = new OrionTrainingDummy(terrariaTrainingDummy);

            trainingDummy.X.Should().Be(x);
        }
        
        [Theory]
        [InlineData(100)]
        public void SetX_IsCorrect(int x) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy();
            var trainingDummy = new OrionTrainingDummy(terrariaTrainingDummy);

            trainingDummy.X = x;

            terrariaTrainingDummy.Position.X.Should().Be((short)x);
        }
        
        [Theory]
        [InlineData(100)]
        public void GetY_IsCorrect(int y) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy {Position = new TDS.Point16(0, y)};
            var trainingDummy = new OrionTrainingDummy(terrariaTrainingDummy);

            trainingDummy.Y.Should().Be(y);
        }
        
        [Theory]
        [InlineData(100)]
        public void SetY_IsCorrect(int y) {
            var terrariaTrainingDummy = new TGCTE.TETrainingDummy();
            var trainingDummy = new OrionTrainingDummy(terrariaTrainingDummy);

            trainingDummy.Y = y;

            terrariaTrainingDummy.Position.Y.Should().Be((short)y);
        }
    }
}
