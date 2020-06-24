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
using System.Diagnostics.CodeAnalysis;
using Orion.Core.DataStructures;
using Orion.Core.Players;
using Xunit;

namespace Orion.Core.Packets.Players
{
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class PlayerCharacterPacketTests
    {
        private readonly byte[] _bytes = {
            35, 0, 4, 5, 2, 50, 1, 102, 1, 0, 0, 0, 26, 131, 54, 158, 74, 51, 47, 39, 88, 184, 58, 43, 69, 8, 97, 162,
            167, 255, 212, 159, 76, 0, 0
        };

        [Fact]
        public void PlayerIndex_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.PlayerIndex = 5;

            Assert.Equal(5, packet.PlayerIndex);
        }

        [Fact]
        public void SkinVariant_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.SkinVariant = 2;

            Assert.Equal(2, packet.SkinVariant);
        }

        [Fact]
        public void Hair_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.Hair = 50;

            Assert.Equal(50, packet.Hair);
        }

        [Fact]
        public void Name_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.Name = "f";

            Assert.Equal("f", packet.Name);
        }

        [Fact]
        public void HairDye_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.HairDye = 1;

            Assert.Equal(1, packet.HairDye);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideAccessorySlot1_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideAccessorySlot1 = value;

            Assert.Equal(value, packet.HideAccessorySlot1);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideAccessorySlot2_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideAccessorySlot2 = value;

            Assert.Equal(value, packet.HideAccessorySlot2);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideAccessorySlot3_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideAccessorySlot3 = value;

            Assert.Equal(value, packet.HideAccessorySlot3);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideAccessorySlot4_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideAccessorySlot4 = value;

            Assert.Equal(value, packet.HideAccessorySlot4);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideAccessorySlot5_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideAccessorySlot5 = value;

            Assert.Equal(value, packet.HideAccessorySlot5);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideAccessorySlot6_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideAccessorySlot6 = value;

            Assert.Equal(value, packet.HideAccessorySlot6);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideAccessorySlot7_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideAccessorySlot7 = value;

            Assert.Equal(value, packet.HideAccessorySlot7);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HidePetSlot_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HidePetSlot = value;

            Assert.Equal(value, packet.HidePetSlot);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HideLightPetSlot_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HideLightPetSlot = value;

            Assert.Equal(value, packet.HideLightPetSlot);
        }

        [Fact]
        public void HairColor_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.HairColor = new Color3(26, 131, 54);

            Assert.Equal(new Color3(26, 131, 54), packet.HairColor);
        }

        [Fact]
        public void SkinColor_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.SkinColor = new Color3(158, 74, 51);

            Assert.Equal(new Color3(158, 74, 51), packet.SkinColor);
        }

        [Fact]
        public void EyeColor_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.EyeColor = new Color3(47, 39, 88);

            Assert.Equal(new Color3(47, 39, 88), packet.EyeColor);
        }

        [Fact]
        public void ShirtColor_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.ShirtColor = new Color3(184, 58, 43);

            Assert.Equal(new Color3(184, 58, 43), packet.ShirtColor);
        }

        [Fact]
        public void UndershirtColor_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.UndershirtColor = new Color3(69, 8, 97);

            Assert.Equal(new Color3(69, 8, 97), packet.UndershirtColor);
        }

        [Fact]
        public void PantsColor_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.PantsColor = new Color3(162, 167, 255);

            Assert.Equal(new Color3(162, 167, 255), packet.PantsColor);
        }

        [Fact]
        public void ShoeColor_Set_Get()
        {
            var packet = new PlayerCharacterPacket();

            packet.ShoeColor = new Color3(212, 159, 76);

            Assert.Equal(new Color3(212, 159, 76), packet.ShoeColor);
        }

        [Theory]
        [InlineData(CharacterDifficulty.Classic)]
        [InlineData(CharacterDifficulty.Mediumcore)]
        [InlineData(CharacterDifficulty.Hardcore)]
        [InlineData(CharacterDifficulty.Journey)]
        public void Difficulty_Set_Get(CharacterDifficulty value)
        {
            var packet = new PlayerCharacterPacket();

            packet.Difficulty = value;

            Assert.Equal(value, packet.Difficulty);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void HasExtraAccessorySlot_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.HasExtraAccessorySlot = value;

            Assert.Equal(value, packet.HasExtraAccessorySlot);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsUsingBiomeTorches_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.IsUsingBiomeTorches = value;

            Assert.Equal(value, packet.IsUsingBiomeTorches);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void IsFightingTheTorchGod_Set_Get(bool value)
        {
            var packet = new PlayerCharacterPacket();

            packet.IsFightingTheTorchGod = value;

            Assert.Equal(value, packet.IsFightingTheTorchGod);
        }

        [Fact]
        public void Read()
        {
            var packet = new PlayerCharacterPacket();
            var span = _bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(5, packet.PlayerIndex);
            Assert.Equal(2, packet.SkinVariant);
            Assert.Equal(50, packet.Hair);
            Assert.Equal("f", packet.Name);
            Assert.False(packet.HideAccessorySlot1);
            Assert.False(packet.HideAccessorySlot2);
            Assert.False(packet.HideAccessorySlot3);
            Assert.False(packet.HideAccessorySlot4);
            Assert.False(packet.HideAccessorySlot5);
            Assert.False(packet.HideAccessorySlot6);
            Assert.False(packet.HideAccessorySlot7);
            Assert.False(packet.HidePetSlot);
            Assert.False(packet.HideLightPetSlot);
            Assert.Equal(new Color3(26, 131, 54), packet.HairColor);
            Assert.Equal(new Color3(158, 74, 51), packet.SkinColor);
            Assert.Equal(new Color3(47, 39, 88), packet.EyeColor);
            Assert.Equal(new Color3(184, 58, 43), packet.ShirtColor);
            Assert.Equal(new Color3(69, 8, 97), packet.UndershirtColor);
            Assert.Equal(new Color3(162, 167, 255), packet.PantsColor);
            Assert.Equal(new Color3(212, 159, 76), packet.ShoeColor);
            Assert.Equal(CharacterDifficulty.Classic, packet.Difficulty);
            Assert.False(packet.HasExtraAccessorySlot);
            Assert.False(packet.IsUsingBiomeTorches);
            Assert.False(packet.IsFightingTheTorchGod);
        }

        [Fact]
        public void RoundTrip()
        {
            TestUtils.RoundTripPacket<PlayerCharacterPacket>(_bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
