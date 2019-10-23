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
using Orion.Events.Players;
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
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var player = playerService.Players[1];

            player.Index.Should().Be(1);
            ((OrionPlayer)player).Wrapped.Should().BeSameAs(Main.player[1]);
        }

        [Fact]
        public void Players_Item_GetMultipleTimes_ReturnsSameInstance() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var player = playerService.Players[0];
            var player2 = playerService.Players[0];

            player.Should().BeSameAs(player2);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(10000)]
        public void Players_Item_GetInvalidIndex_ThrowsIndexOutOfRangeException(int index) {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            Func<IPlayer> func = () => playerService.Players[index];

            func.Should().Throw<IndexOutOfRangeException>();
        }

        [Fact]
        public void Players_GetEnumerator() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var players = playerService.Players.ToList();

            for (var i = 0; i < players.Count; ++i) {
                ((OrionPlayer)players[i]).Wrapped.Should().BeSameAs(Main.player[i]);
            }
        }

        [Fact]
        public void PacketReceive_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PacketReceiveEvent>(e => {
                isRun = true;
                e.Sender.Should().BeSameAs(playerService.Players[1]);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerConnect_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerConnectEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.VersionString.Should().Be("Terraria194");
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerConnectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerData_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerDataEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.ClothesStyle.Should().Be(2);
                e.Hairstyle.Should().Be(50);
                e.Name.Should().Be("f");
                e.HairDye.Should().Be(0);
                e.EquipmentHiddenFlags.Should().Be(0);
                e.MiscEquipmentHiddenFlags.Should().Be(0);
                e.HairColor.Should().Be(new Color(26, 131, 54));
                e.SkinColor.Should().Be(new Color(158, 74, 51));
                e.EyeColor.Should().Be(new Color(47, 39, 88));
                e.ShirtColor.Should().Be(new Color(184, 58, 43));
                e.UndershirtColor.Should().Be(new Color(69, 8, 97));
                e.PantsColor.Should().Be(new Color(162, 167, 255));
                e.ShoeColor.Should().Be(new Color(212, 159, 76));
                e.Difficulty.Should().Be(PlayerDifficulty.Softcore);
                e.HasExtraAccessorySlot.Should().BeFalse();
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerDataPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerInventorySlot_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerInventoryEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.InventorySlot.Should().Be(0);
                e.StackSize.Should().Be(1);
                e.ItemPrefix.Should().Be(ItemPrefix.Godly);
                e.ItemType.Should().Be(ItemType.CopperShortsword);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerInventorySlotPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerJoin_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerJoinEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerJoinPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerSpawn_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerSpawnEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.X.Should().Be(-1);
                e.Y.Should().Be(-1);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerSpawnPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerInfo_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerInfoEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.IsHoldingUp.Should().BeFalse();
                e.IsHoldingDown.Should().BeFalse();
                e.IsHoldingLeft.Should().BeFalse();
                e.IsHoldingRight.Should().BeTrue();
                e.IsHoldingJump.Should().BeFalse();
                e.IsHoldingUseItem.Should().BeFalse();
                e.Direction.Should().BeTrue();
                e.IsClimbingRope.Should().BeFalse();
                e.ClimbingRopeDirection.Should().BeFalse();
                e.IsVortexStealthed.Should().BeFalse();
                e.IsRightSideUp.Should().BeTrue();
                e.IsRaisingShield.Should().BeFalse();
                e.HeldItemSlot.Should().Be(0);
                e.Position.Should().Be(new Vector2(67134, 6790));
                e.Velocity.Should().Be(Vector2.Zero);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerInfoPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerHealth_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerHealthEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.Health.Should().Be(100);
                e.MaxHealth.Should().Be(100);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerHealthPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerPvp_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerPvpEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.IsInPvp.Should().Be(true);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerPvpPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerHealEffect_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerHealEffectEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.HealAmount.Should().Be(100);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerHealEffectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerPasswordResponse_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerPasswordEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.PasswordAttempt.Should().Be("Terraria");
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerPasswordResponsePacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerMana_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerManaEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.Mana.Should().Be(100);
                e.MaxMana.Should().Be(100);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerManaPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerManaEffect_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerManaEffectEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.ManaAmount.Should().Be(100);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerManaEffectPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerTeam_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerTeamEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.Team.Should().Be(PlayerTeam.Red);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerTeamPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerUuid_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerUuidEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.Uuid.Should().Be("Terraria");
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerUuidPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerTeleportationPotion_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerTeleportEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, PlayerTeleportationPotionPacketTests.Bytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void PacketReceive_PlayerChat_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerChatEvent>(e => {
                isRun = true;
                e.Command.Should().Be("Say");
                e.Text.Should().Be("/command test");
                e.Cancel();
            });

            TestUtils.FakeReceiveBytes(1, ChatModuleTests.ClientBytes);

            isRun.Should().BeTrue();
        }

        [Fact]
        public void ResetClient_PlayerQuit_IsTriggered() {
            using var kernel = new OrionKernel(Logger.None);
            using var playerService = new OrionPlayerService(kernel, Logger.None);
            var isRun = false;
            kernel.RegisterHandler<PlayerQuitEvent>(e => {
                isRun = true;
                e.Player.Should().BeSameAs(playerService.Players[1]);
            });
            Terraria.Netplay.Clients[1].IsActive = true;
            Terraria.Netplay.Clients[1].Id = 1;

            Terraria.Netplay.Clients[1].Reset();

            isRun.Should().BeTrue();
        }
    }
}
