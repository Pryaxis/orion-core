using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public sealed class PlayerSpawnTests
    {
        private readonly byte[] _bytes = { 13, 0, 12, 5, 50, 0, 100, 0, 15, 0, 0, 0, 1 };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var playerSpawn = new PlayerSpawn();

            playerSpawn.PlayerIndex = 5;

            Assert.Equal(5, playerSpawn.PlayerIndex);
        }

        [Fact]
        public void Position_Set_Get()
        {
            var playerSpawn = new PlayerSpawn();

            playerSpawn.SpawnX = 555;
            playerSpawn.SpawnY = 666;

            Assert.Equal(555, playerSpawn.SpawnX);
            Assert.Equal(666, playerSpawn.SpawnY);
        }

        [Fact]
        public void TimeUntilRespawn_Set_Get()
        {
            var playerSpawn = new PlayerSpawn();

            playerSpawn.TimeUntilRespawn = 15;

            Assert.Equal(15, playerSpawn.TimeUntilRespawn);
        }

        [Fact]
        public void SpawnContext_Set_Get()
        {
            var playerSpawn = new PlayerSpawn();

            playerSpawn.SpawnContext = PlayerSpawnContext.RecallFromItem;

            Assert.Equal(PlayerSpawnContext.RecallFromItem, playerSpawn.SpawnContext);
        }

        [Fact]
        public void Read()
        {
            var packet = TestUtils.ReadPacket<PlayerSpawn>(_bytes, PacketContext.Server);

            Assert.Equal(5, packet.PlayerIndex);
            Assert.Equal(50, packet.SpawnX);
            Assert.Equal(100, packet.SpawnY);
            Assert.Equal(15, packet.TimeUntilRespawn);
            Assert.Equal(PlayerSpawnContext.SpawningIntoTheWorld, packet.SpawnContext);
        }
    }
}
