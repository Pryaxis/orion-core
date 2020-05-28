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

using System;
using System.Linq;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.Players;
using Orion.Events.World.Tiles;
using Orion.Items;
using Orion.Packets;
using Orion.Packets.Client;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Orion.Packets.World.Tiles;
using Orion.World.Tiles;
using Serilog.Core;
using Xunit;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    public class OrionPlayerServiceTests {
        private static readonly byte[] ServerConnectBytes;

        static OrionPlayerServiceTests() {
            var bytes = new byte[100];
            var packet = new ClientConnectPacket { Version = "Terraria" + Terraria.Main.curRelease };
            var packetLength = packet.WriteWithHeader(bytes, PacketContext.Client);

            ServerConnectBytes = bytes[..packetLength];
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Players_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            Assert.Throws<IndexOutOfRangeException>(() => playerService.Players[index]);
        }

        [Fact]
        public void Players_Item_Get() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            var player = playerService.Players[1];

            Assert.Equal(1, player.Index);
            Assert.Equal(Terraria.Main.player[1], ((OrionPlayer)player).Wrapped);
        }

        [Fact]
        public void Players_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            var player = playerService.Players[0];
            var player2 = playerService.Players[0];

            Assert.Same(player2, player);
        }

        [Fact]
        public void Players_GetEnumerator() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);

            var players = playerService.Players.ToList();

            for (var i = 0; i < players.Count; ++i) {
                Assert.Equal(Terraria.Main.player[i], ((OrionPlayer)players[i]).Wrapped);
            }
        }

        [Fact]
        public void PacketReceive_EventTriggered() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PacketReceiveEvent<ClientConnectPacket>>(evt => {
                Assert.Same(playerService.Players[5], evt.Sender);
                Assert.Equal("Terraria" + Terraria.Main.curRelease, evt.Packet.Version);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, ServerConnectBytes);

            Assert.True(isRun);
            Assert.Equal(1, Terraria.Netplay.Clients[5].State);
        }

        [Fact]
        public void PacketReceive_EventModified() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PacketReceiveEvent<ClientConnectPacket>>(
                evt => evt.Packet.Version = "Terraria1", Logger.None);

            TestUtils.FakeReceiveBytes(5, ServerConnectBytes);

            Assert.Equal(0, Terraria.Netplay.Clients[5].State);
        }

        [Fact]
        public void PacketReceive_EventCanceled() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PacketReceiveEvent<ClientConnectPacket>>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, ServerConnectBytes);

            Assert.Equal(0, Terraria.Netplay.Clients[5].State);
        }

        [Fact]
        public void PacketReceive_PlayerJoinEventTriggered() {
            // Set `State` to 1 so that the join packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 1 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerJoinEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerJoinPacketTests.Bytes);

            Assert.True(isRun);
            Assert.Equal(2, Terraria.Netplay.Clients[5].State);
        }

        [Fact]
        public void PacketReceive_PlayerJoinEventCanceled() {
            // Set `State` to 1 so that the join packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 1 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerJoinEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerJoinPacketTests.Bytes);

            Assert.Equal(1, Terraria.Netplay.Clients[5].State);
        }

        [Fact]
        public void PacketReceive_PlayerHealthEventTriggered() {
            // Set `State` to 10 so that the health packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerHealthEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal(100, evt.Health);
                Assert.Equal(500, evt.MaxHealth);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerHealthPacketTests.Bytes);

            Assert.True(isRun);
            Assert.Equal(100, Terraria.Main.player[5].statLife);
            Assert.Equal(500, Terraria.Main.player[5].statLifeMax);
        }

        [Fact]
        public void PacketReceive_PlayerHealthEventCanceled() {
            // Set `State` to 10 so that the health packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerHealthEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerHealthPacketTests.Bytes);

            Assert.Equal(100, Terraria.Main.player[5].statLife);
            Assert.Equal(100, Terraria.Main.player[5].statLifeMax);
        }

        [Fact]
        public void PacketReceive_BlockBreakEventTriggered() {
            // Set `State` to 10 so that the tile modify packet is not ignored by the server, and mark the relevant
            // `TileSections` entry so that the tile modify packet is not treated with failure.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Netplay.Clients[5].TileSections[0, 1] = true;
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.tile[100, 256] = new Terraria.Tile { type = (ushort)BlockId.Stone };
            Terraria.Main.tile[100, 256].active(true);
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<BlockBreakEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal(100, evt.X);
                Assert.Equal(256, evt.Y);
                Assert.False(evt.IsFailure);
                Assert.False(evt.ShouldSuppressItems);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, TileModifyPacketTests.BreakBlockBytes);

            Assert.True(isRun);
            Assert.False(Terraria.Main.tile[100, 256].active());
            Assert.Equal(ItemId.StoneBlock, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void PacketReceive_BlockBreakEventCanceled() {
            // Set `State` to 10 so that the tile modify packet is not ignored by the server, and mark the relevant
            // `TileSections` entry so that the tile modify packet is not treated with failure.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Netplay.Clients[5].TileSections[0, 1] = true;
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.tile[100, 256] = new Terraria.Tile { type = (ushort)BlockId.Stone };
            Terraria.Main.tile[100, 256].active(true);

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<BlockBreakEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, TileModifyPacketTests.BreakBlockBytes);

            Assert.True(Terraria.Main.tile[100, 256].active());
        }

        [Fact]
        public void PacketReceive_BlockPlaceEventTriggered() {
            // Set `State` to 10 so that the tile modify packet is not ignored by the server, and mark the relevant
            // `TileSections` entry so that the tile modify packet is not treated with failure.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Netplay.Clients[5].TileSections[0, 1] = true;
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.tile[100, 256] = new Terraria.Tile();
            Terraria.Main.tile[100, 257] = new Terraria.Tile();
            Terraria.Main.tile[100, 257].active(true);
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<BlockPlaceEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal(100, evt.X);
                Assert.Equal(256, evt.Y);
                Assert.Equal(BlockId.Torches, evt.Id);
                Assert.Equal(1, evt.Style);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, TileModifyPacketTests.PlaceBlockBytes);

            Assert.True(isRun);
            Assert.True(Terraria.Main.tile[100, 256].active());
            Assert.Equal(BlockId.Torches, (BlockId)Terraria.Main.tile[100, 256].type);
        }

        [Fact]
        public void PacketReceive_BlockPlaceEventCanceled() {
            // Set `State` to 10 so that the tile modify packet is not ignored by the server, and mark the relevant
            // `TileSections` entry so that the tile modify packet is not treated with failure.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Netplay.Clients[5].TileSections[0, 1] = true;
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.tile[100, 256] = new Terraria.Tile();

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<BlockPlaceEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, TileModifyPacketTests.PlaceBlockBytes);

            Assert.False(Terraria.Main.tile[100, 256].active());
        }

        [Fact]
        public void PacketReceive_BlockBreakNoItemsEventTriggered() {
            // Set `State` to 10 so that the tile modify packet is not ignored by the server, and mark the relevant
            // `TileSections` entry so that the tile modify packet is not treated with failure.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Netplay.Clients[5].TileSections[0, 1] = true;
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.tile[100, 256] = new Terraria.Tile { type = (ushort)BlockId.Stone };
            Terraria.Main.tile[100, 256].active(true);
            Terraria.Main.item[0] = new Terraria.Item { whoAmI = 0 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<BlockBreakEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal(100, evt.X);
                Assert.Equal(256, evt.Y);
                Assert.False(evt.IsFailure);
                Assert.True(evt.ShouldSuppressItems);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, TileModifyPacketTests.BreakBlockNoItemsBytes);

            Assert.True(isRun);
            Assert.False(Terraria.Main.tile[100, 256].active());
            Assert.Equal(ItemId.None, (ItemId)Terraria.Main.item[0].type);
        }

        [Fact]
        public void PacketReceive_BlockBreakNoItemsEventCanceled() {
            // Set `State` to 10 so that the tile modify packet is not ignored by the server, and mark the relevant
            // `TileSections` entry so that the tile modify packet is not treated with failure.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Netplay.Clients[5].TileSections[0, 1] = true;
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };
            Terraria.Main.tile[100, 256] = new Terraria.Tile { type = (ushort)BlockId.Stone };
            Terraria.Main.tile[100, 256].active(true);

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<BlockBreakEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, TileModifyPacketTests.BreakBlockNoItemsBytes);

            Assert.True(Terraria.Main.tile[100, 256].active());
        }

        [Fact]
        public void PacketReceive_PlayerPvpEventTriggered() {
            // Set `State` to 10 so that the PvP packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerPvpEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.True(evt.IsInPvp);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerPvpPacketTests.Bytes);

            Assert.True(isRun);
            Assert.True(Terraria.Main.player[5].hostile);
        }

        [Fact]
        public void PacketReceive_PlayerPvpEventCanceled() {
            // Set `State` to 10 so that the PvP packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerPvpEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerPvpPacketTests.Bytes);

            Assert.False(Terraria.Main.player[5].hostile);
        }

        [Fact]
        public void PacketReceive_PlayerManaEventTriggered() {
            // Set `State` to 10 so that the mana packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerManaEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal(100, evt.Mana);
                Assert.Equal(200, evt.MaxMana);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerManaPacketTests.Bytes);

            Assert.True(isRun);
            Assert.Equal(100, Terraria.Main.player[5].statMana);
            Assert.Equal(200, Terraria.Main.player[5].statManaMax);
        }

        [Fact]
        public void PacketReceive_PlayerManaEventModified() {
            // Set `State` to 10 so that the mana packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerManaEvent>(evt => evt.Mana = 200, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerManaPacketTests.Bytes);

            Assert.Equal(200, Terraria.Main.player[5].statMana);
            Assert.Equal(200, Terraria.Main.player[5].statManaMax);
        }

        [Fact]
        public void PacketReceive_PlayerManaEventCanceled() {
            // Set `State` to 10 so that the mana packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerManaEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerManaPacketTests.Bytes);

            Assert.Equal(0, Terraria.Main.player[5].statMana);
            Assert.Equal(20, Terraria.Main.player[5].statManaMax);
        }

        [Fact]
        public void PacketReceive_PlayerTeamEventTriggered() {
            // Set `State` to 10 so that the team packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerTeamEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal(PlayerTeam.Red, evt.Team);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerTeamPacketTests.Bytes);

            Assert.True(isRun);
            Assert.Equal(PlayerTeam.Red, (PlayerTeam)Terraria.Main.player[5].team);
        }

        [Fact]
        public void PacketReceive_PlayerTeamEventModified() {
            // Set `State` to 10 so that the team packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerTeamEvent>(evt => evt.Team = PlayerTeam.Blue, Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerTeamPacketTests.Bytes);

            Assert.Equal(PlayerTeam.Blue, (PlayerTeam)Terraria.Main.player[5].team);
        }

        [Fact]
        public void PacketReceive_PlayerTeamEventCanceled() {
            // Set `State` to 10 so that the team packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerTeamEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, PlayerTeamPacketTests.Bytes);

            Assert.Equal(0, Terraria.Main.player[5].team);
        }

        [Fact]
        public void PacketReceive_PlayerUuidEventTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerUuidEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal("Terraria", evt.Uuid);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, ClientUuidPacketTests.Bytes);

            Assert.True(isRun);
        }

        [Fact]
        public void PacketReceive_PlayerChatEventTriggered() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5 };

            // Set up another player for the chat to be broadcast to.
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[6] = new Terraria.RemoteClient { Id = 6, State = 10, Socket = socket };
            Terraria.Main.player[6] = new Terraria.Player { whoAmI = 6 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerChatEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                Assert.Equal("Say", evt.Command);
                Assert.Equal("/command test", evt.Text);
                isRun = true;
            }, Logger.None);

            TestUtils.FakeReceiveBytes(5, ChatModuleTests.ServerBytes);

            Assert.True(isRun);
            Assert.NotEmpty(socket.SendData);
        }

        [Fact]
        public void PacketReceive_PlayerChatEventCanceled() {
            // Set `State` to 10 so that the chat packet is not ignored by the server.
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, State = 10 };
            Terraria.Main.player[5] = new Terraria.Player { whoAmI = 5 };

            // Set up another player for the chat to be broadcast to.
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[6] = new Terraria.RemoteClient { Id = 6, State = 10, Socket = socket };
            Terraria.Main.player[6] = new Terraria.Player { whoAmI = 6 };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PlayerChatEvent>(evt => evt.Cancel(), Logger.None);

            TestUtils.FakeReceiveBytes(5, ChatModuleTests.ServerBytes);

            Assert.Empty(socket.SendData);
        }

        [Fact]
        public void PacketSend_EventTriggered() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, Socket = socket };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PacketSendEvent<ClientConnectPacket>>(evt => {
                Assert.Same(playerService.Players[5], evt.Receiver);
                Assert.Equal("Terraria" + Terraria.Main.curRelease, evt.Packet.Version);
                isRun = true;
            }, Logger.None);

            Terraria.NetMessage.SendData((byte)PacketId.ClientConnect, 5);

            Assert.True(isRun);
            Assert.Equal(ServerConnectBytes, socket.SendData);
        }

        [Fact]
        public void PacketSend_EventCanceled() {
            var socket = new TestSocket { Connected = true };
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, Socket = socket };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            kernel.RegisterHandler<PacketSendEvent<ClientConnectPacket>>(evt => evt.Cancel(), Logger.None);

            Terraria.NetMessage.SendData((byte)PacketId.ClientConnect, 5);

            Assert.Empty(socket.SendData);
        }

        [Fact]
        public void ResetClient_PlayerQuitEventTriggered() {
            Terraria.Netplay.Clients[5] = new Terraria.RemoteClient { Id = 5, IsActive = true };

            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerQuitEvent>(evt => {
                Assert.Same(playerService.Players[5], evt.Player);
                isRun = true;
            }, Logger.None);

            Terraria.Netplay.Clients[5].Reset();

            Assert.True(isRun);
        }

        private class TestSocket : Terraria.Net.Sockets.ISocket {
            public bool Connected { get; set; }
            public byte[] SendData { get; private set; } = Array.Empty<byte>();

            public void AsyncReceive(
                byte[] data, int offset, int size, Terraria.Net.Sockets.SocketReceiveCallback callback,
                object? state = null) =>
                    throw new NotImplementedException();
            public void AsyncSend(
                byte[] data, int offset, int size, Terraria.Net.Sockets.SocketSendCallback callback,
                object? state = null) =>
                    SendData = data[offset..(offset + size)];
            public void Close() => throw new NotImplementedException();
            public void Connect(Terraria.Net.RemoteAddress address) => throw new NotImplementedException();
            public Terraria.Net.RemoteAddress GetRemoteAddress() => throw new NotImplementedException();
            public bool IsConnected() => Connected;
            public bool IsDataAvailable() => throw new NotImplementedException();
            public void SendQueuedPackets() => throw new NotImplementedException();
            public bool StartListening(Terraria.Net.Sockets.SocketConnectionAccepted callback) =>
                throw new NotImplementedException();
            public void StopListening() => throw new NotImplementedException();
        }
    }
}
