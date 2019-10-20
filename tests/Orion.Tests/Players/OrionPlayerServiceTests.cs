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
using System.Linq;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Events;
using Orion.Items;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Serilog.Core;
using Xunit;
using Main = Terraria.Main;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    [Collection("TerrariaTestsCollection")]
    public class OrionPlayerServiceTests {
        public OrionPlayerServiceTests() {
            for (var i = 0; i < Main.player.Length; ++i) {
                Main.player[i] = new TerrariaPlayer { whoAmI = i };
            }
        }

        [Fact]
        public void Players_Item_Get() {
            using var playerService = new OrionPlayerService(Logger.None);
            var player = playerService.Players[1];

            player.Index.Should().Be(1);
            ((OrionPlayer)player).Wrapped.Should().BeSameAs(Main.player[1]);
        }

        [Fact]
        public void Players_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var playerService = new OrionPlayerService(Logger.None);
            var player = playerService.Players[0];
            var player2 = playerService.Players[0];

            player.Should().BeSameAs(player2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Players_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var playerService = new OrionPlayerService(Logger.None);
            Func<IPlayer> func = () => playerService.Players[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Players_GetEnumerator() {
            using var playerService = new OrionPlayerService(Logger.None);
            var players = playerService.Players.ToList();

            for (var i = 0; i < players.Count; ++i) {
                ((OrionPlayer)players[i]).Wrapped.Should().BeSameAs(Main.player[i]);
            }
        }

        [Fact]
        public void PacketReceive_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PacketReceive.RegisterHandler((sender, args) => {
                isRun = true;
                args.Sender.Should().BeSameAs(playerService.Players[1]);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerConnect_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerConnect.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.VersionString.Should().Be("Terraria194");
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerData_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerData.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.ClothesStyle.Should().Be(2);
                args.Hairstyle.Should().Be(50);
                args.Name.Should().Be("f");
                args.HairDye.Should().Be(0);
                args.EquipmentHiddenFlags.Should().Be(0);
                args.MiscEquipmentHiddenFlags.Should().Be(0);
                args.HairColor.Should().Be(new Color(26, 131, 54));
                args.SkinColor.Should().Be(new Color(158, 74, 51));
                args.EyeColor.Should().Be(new Color(47, 39, 88));
                args.ShirtColor.Should().Be(new Color(184, 58, 43));
                args.UndershirtColor.Should().Be(new Color(69, 8, 97));
                args.PantsColor.Should().Be(new Color(162, 167, 255));
                args.ShoeColor.Should().Be(new Color(212, 159, 76));
                args.Difficulty.Should().Be(PlayerDifficulty.Softcore);
                args.HasExtraAccessorySlot.Should().BeFalse();
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerDataPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerInventorySlot_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerInventorySlot.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.InventorySlot.Should().Be(0);
                args.StackSize.Should().Be(1);
                args.ItemPrefix.Should().Be(ItemPrefix.Godly);
                args.ItemType.Should().Be(ItemType.CopperShortsword);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerInventorySlotPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerJoin_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerJoin.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerJoinPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerSpawn_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerSpawn.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.X.Should().Be(-1);
                args.Y.Should().Be(-1);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerSpawnPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerInfo_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerInfo.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.IsHoldingUp.Should().BeFalse();
                args.IsHoldingDown.Should().BeFalse();
                args.IsHoldingLeft.Should().BeFalse();
                args.IsHoldingRight.Should().BeTrue();
                args.IsHoldingJump.Should().BeFalse();
                args.IsHoldingUseItem.Should().BeFalse();
                args.Direction.Should().BeTrue();
                args.IsClimbingRope.Should().BeFalse();
                args.ClimbingRopeDirection.Should().BeFalse();
                args.IsVortexStealthed.Should().BeFalse();
                args.IsRightSideUp.Should().BeTrue();
                args.IsRaisingShield.Should().BeFalse();
                args.HeldItemSlot.Should().Be(0);
                args.Position.Should().Be(new Vector2(67134, 6790));
                args.Velocity.Should().Be(Vector2.Zero);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerInfoPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerHealth_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerHealth.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.Health.Should().Be(100);
                args.MaxHealth.Should().Be(100);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerHealthPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerPvp_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerPvp.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.IsInPvp.Should().Be(true);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerPvpPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerHealEffect_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerHealEffect.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.HealAmount.Should().Be(100);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerHealEffectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerPasswordResponse_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerPasswordResponse.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.PasswordAttempt.Should().Be("Terraria");
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerPasswordResponsePacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerMana_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerMana.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.Mana.Should().Be(100);
                args.MaxMana.Should().Be(100);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerManaPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerManaEffect_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerManaEffect.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.ManaAmount.Should().Be(100);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerManaEffectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerTeam_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerTeam.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.Team.Should().Be(PlayerTeam.Red);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerTeamPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerUuid_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerUuid.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.Uuid.Should().Be("Terraria");
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerUuidPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerTeleportationPotion_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerTeleportationPotion.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerTeleportationPotionPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerAnglerQuests_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerAnglerQuests.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
                args.NumberOfAnglerQuestsCompleted.Should().Be(257);
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerAnglerQuestsPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerChat_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerChat.RegisterHandler((sender, args) => {
                isRun = true;
                args.Command.Should().Be("Say");
                args.Text.Should().Be("/command test");
                args.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, ChatModuleTests.ClientBytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void ResetClient_PlayerQuit_IsTriggered() {
            using var playerService = new OrionPlayerService(Logger.None);
            var isRun = false;
            playerService.PlayerQuit.RegisterHandler((sender, args) => {
                isRun = true;
                args.Player.Should().BeSameAs(playerService.Players[1]);
            });
            Terraria.Netplay.Clients[1].IsActive = true;
            Terraria.Netplay.Clients[1].Id = 1;

            Terraria.Netplay.Clients[1].Reset();

            isRun.Should().BeTrue();
        }
    }
}
