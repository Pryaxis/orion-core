using System;
using System.Linq;
using FluentAssertions;
using Moq;
using Orion.Items;
using Orion.Npcs;
using Orion.Players;
using Xunit;

namespace Orion.Tests.Npcs {
    public class OrionNpcServiceTests : IDisposable {
        private readonly INpcService _npcService;

        public OrionNpcServiceTests() {
            for (var i = 0; i < Terraria.Main.maxNPCs; ++i) {
                Terraria.Main.npc[i] = new Terraria.NPC {whoAmI = i};
            }

            var itemService = new Mock<IItemService>();
            var playerService = new Mock<IPlayerService>();
            _npcService = new OrionNpcService(itemService.Object, playerService.Object);
        }

        public void Dispose() {
            _npcService.Dispose();
        }

        [Fact]
        public void GetNpc_IsCorrect() {
            var npc = _npcService[0];

            npc.WrappedNpc.Should().BeSameAs(Terraria.Main.npc[0]);
        }

        [Fact]
        public void GetNpc_MultipleTimes_ReturnsSameInstance() {
            var npc = _npcService[0];
            var npc2 = _npcService[0];

            npc.Should().BeSameAs(npc2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void GetNpc_InvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            Func<INpc> func = () => _npcService[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void GetEnumerator_IsCorrect() {
            var npcs = _npcService.ToList();

            for (var i = 0; i < npcs.Count; ++i) {
                npcs[i].WrappedNpc.Should().BeSameAs(Terraria.Main.npc[i]);
            }
        }
    }
}
