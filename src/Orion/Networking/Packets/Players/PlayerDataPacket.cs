// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Players;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set player data.
    /// </summary>
    public sealed class PlayerDataPacket : Packet {
        private string _playerName = "";

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's skin type.
        /// </summary>
        public byte PlayerSkinType { get; set; }

        /// <summary>
        /// Gets or sets the player's hair type.
        /// </summary>
        public byte PlayerHairType { get; set; }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string PlayerName {
            get => _playerName;
            set => _playerName = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        public byte PlayerHairDye { get; set; }

        /// <summary>
        /// Gets or sets the player's hidden visuals flags.
        /// </summary>
        public ushort PlayerHiddenVisualsFlags { get; set; }

        /// <summary>
        /// Gets or sets the player's hidden misc flags.
        /// </summary>
        public byte PlayerHiddenMiscFlags { get; set; }

        /// <summary>
        /// Gets or sets the player's hair color.
        /// </summary>
        public Color PlayerHairColor { get; set; }

        /// <summary>
        /// Gets or sets the player's skin color.
        /// </summary>
        public Color PlayerSkinColor { get; set; }

        /// <summary>
        /// Gets or sets the player's eye color.
        /// </summary>
        public Color PlayerEyeColor { get; set; }

        /// <summary>
        /// Gets or sets the player's shirt color.
        /// </summary>
        public Color PlayerShirtColor { get; set; }

        /// <summary>
        /// Gets or sets the player's undershirt color.
        /// </summary>
        public Color PlayerUndershirtColor { get; set; }

        /// <summary>
        /// Gets or sets the player's pants color.
        /// </summary>
        public Color PlayerPantsColor { get; set; }

        /// <summary>
        /// Gets or sets the player's shoe color.
        /// </summary>
        public Color PlayerShoeColor { get; set; }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        public PlayerDifficulty PlayerDifficulty { get; set; }

        private protected override PacketType Type => PacketType.PlayerData;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} is {PlayerName}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            PlayerIndex = reader.ReadByte();
            PlayerSkinType = reader.ReadByte();
            PlayerHairType = reader.ReadByte();
            _playerName = reader.ReadString();
            PlayerHairDye = reader.ReadByte();
            PlayerHiddenVisualsFlags = reader.ReadUInt16();
            PlayerHiddenMiscFlags = reader.ReadByte();
            PlayerHairColor = reader.ReadColor();
            PlayerSkinColor = reader.ReadColor();
            PlayerEyeColor = reader.ReadColor();
            PlayerShirtColor = reader.ReadColor();
            PlayerUndershirtColor = reader.ReadColor();
            PlayerPantsColor = reader.ReadColor();
            PlayerShoeColor = reader.ReadColor();
            PlayerDifficulty = (PlayerDifficulty)reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(PlayerIndex);
            writer.Write(PlayerSkinType);
            writer.Write(PlayerHairType);
            writer.Write(PlayerName);
            writer.Write(PlayerHairDye);
            writer.Write(PlayerHiddenVisualsFlags);
            writer.Write(PlayerHiddenMiscFlags);
            writer.Write(PlayerHairColor);
            writer.Write(PlayerSkinColor);
            writer.Write(PlayerEyeColor);
            writer.Write(PlayerShirtColor);
            writer.Write(PlayerUndershirtColor);
            writer.Write(PlayerPantsColor);
            writer.Write(PlayerShoeColor);
            writer.Write((byte)PlayerDifficulty);
        }
    }
}
