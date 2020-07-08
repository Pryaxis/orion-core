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
using Moq;
using Orion.Core.Items;
using Orion.Core.Packets.Players;
using Orion.Core.Packets.Server;
using Orion.Core.Packets.World.Tiles;
using Orion.Core.Utils;
using Orion.Core.World;
using Xunit;

namespace Orion.Core.Players
{
    public class IPlayerTests
    {
        [Fact]
        public void Disconnect_NullPlayer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IPlayerExtensions.Disconnect(null!, "test"));
        }

        [Fact]
        public void Disconnect_NullReason_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(() => player.Disconnect(null!));
        }

        [Fact]
        public void Disconnect()
        {
            var player = Mock.Of<IPlayer>();
            Mock.Get(player)
                .Setup(p => p.SendPacket(It.Is<ClientDisconnect>(p => p.Reason == "test")));

            player.Disconnect("test");

            Mock.Get(player).VerifyAll();
        }

        [Fact]
        public void SendMessage_NullPlayer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IPlayerExtensions.SendMessage(null!, "test", Color3.White));
        }

        [Fact]
        public void SendMessage_NullReason_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(() => player.SendMessage(null!, Color3.White));
        }

        [Fact]
        public void SendMessage()
        {
            var player = Mock.Of<IPlayer>();
            Mock.Get(player)
                .Setup(p => p.SendPacket(
                    It.Is<ServerMessage>(p => p.Message == "test" && p.Color == Color3.White && p.LineWidth == -1)));

            player.SendMessage("test", Color3.White);

            Mock.Get(player).VerifyAll();
        }

        [Fact]
        public void SendInventory_NullPlayer_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => IPlayerExtensions.SendInventory(null!, 33));
        }

        [Fact]
        public void SendInventory()
        {
            var player = Mock.Of<IPlayer>(p => p.Index == 5 && p.Inventory == Mock.Of<IArray<ItemStack>>());
            Mock.Get(player)
                .Setup(p => p.SendPacket(
                    It.Is<PlayerInventory>(
                        p => p.PlayerIndex == 5 && p.Slot == 33 && p.StackSize == 1 && p.Prefix == ItemPrefix.Unreal &&
                            p.Id == ItemId.Sdmg)));
            Mock.Get(player.Inventory)
                .Setup(i => i[33])
                .Returns(new ItemStack(ItemId.Sdmg, ItemPrefix.Unreal, 1));

            player.SendInventory(33);

            Mock.Get(player).VerifyAll();
            Mock.Get(player.Inventory).VerifyAll();
        }

        [Fact]
        public void SendTiles_NullPlayer_ThrowsArgumentNullException()
        {
            var tiles = Mock.Of<ITileSlice>();

            Assert.Throws<ArgumentNullException>(() => IPlayerExtensions.SendTiles(null!, 123, 456, tiles));
        }

        [Fact]
        public void SendTiles_NullTiles_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(() => player.SendTiles(123, 456, null!));
        }

        [Fact]
        public void SendTiles_NonSquareTiles_ThrowsNotSupportedException()
        {
            var player = Mock.Of<IPlayer>();
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 2);

            Assert.Throws<NotSupportedException>(() => player.SendTiles(123, 456, tiles));
        }

        [Fact]
        public void SendTiles()
        {
            var tiles = Mock.Of<ITileSlice>(t => t.Width == 1 && t.Height == 1);
            var player = Mock.Of<IPlayer>();
            Mock.Get(player)
                .Setup(p => p.SendPacket(It.Is<TileSquare>(p => p.X == 123 && p.Y == 456 && p.Tiles == tiles)));

            player.SendTiles(123, 456, tiles);

            Mock.Get(player).VerifyAll();
        }
    }
}
