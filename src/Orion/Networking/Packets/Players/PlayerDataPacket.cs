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
using Orion.Entities;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets.Players {
    /// <summary>
    /// Packet sent to set player data.
    /// </summary>
    public sealed class PlayerDataPacket : Packet {
        private byte _playerIndex;
        private byte _playerSkinType;
        private byte _playerHairType;
        private string _playerName = "";
        private byte _playerHairDye;
        private ushort _playerHiddenVisualsFlags;
        private byte _playerHiddenMiscFlags;
        private Color _playerHairColor;
        private Color _playerSkinColor;
        private Color _playerEyeColor;
        private Color _playerShirtColor;
        private Color _playerUndershirtColor;
        private Color _playerPantsColor;
        private Color _playerShoeColor;
        private PlayerDifficulty _playerDifficulty;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's skin type.
        /// </summary>
        public byte PlayerSkinType {
            get => _playerSkinType;
            set {
                _playerSkinType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair type.
        /// </summary>
        public byte PlayerHairType {
            get => _playerHairType;
            set {
                _playerHairType = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string PlayerName {
            get => _playerName;
            set {
                _playerName = value ?? throw new ArgumentNullException(nameof(value));
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        public byte PlayerHairDye {
            get => _playerHairDye;
            set {
                _playerHairDye = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hidden visuals flags.
        /// </summary>
        public ushort PlayerHiddenVisualsFlags {
            get => _playerHiddenVisualsFlags;
            set {
                _playerHiddenVisualsFlags = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hidden misc flags.
        /// </summary>
        public byte PlayerHiddenMiscFlags {
            get => _playerHiddenMiscFlags;
            set {
                _playerHiddenMiscFlags = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair color.
        /// </summary>
        public Color PlayerHairColor {
            get => _playerHairColor;
            set {
                _playerHairColor = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's skin color.
        /// </summary>
        public Color PlayerSkinColor {
            get => _playerSkinColor;
            set {
                _playerSkinColor = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's eye color.
        /// </summary>
        public Color PlayerEyeColor {
            get => _playerEyeColor;
            set {
                _playerEyeColor = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's shirt color.
        /// </summary>
        public Color PlayerShirtColor {
            get => _playerShirtColor;
            set {
                _playerShirtColor = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's undershirt color.
        /// </summary>
        public Color PlayerUndershirtColor {
            get => _playerUndershirtColor;
            set {
                _playerUndershirtColor = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's pants color.
        /// </summary>
        public Color PlayerPantsColor {
            get => _playerPantsColor;
            set {
                _playerPantsColor = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's shoe color.
        /// </summary>
        public Color PlayerShoeColor {
            get => _playerShoeColor;
            set {
                _playerShoeColor = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        public PlayerDifficulty PlayerDifficulty {
            get => _playerDifficulty;
            set {
                _playerDifficulty = value;
                IsDirty = true;
            }
        }

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerData;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} is {PlayerName}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerSkinType = reader.ReadByte();
            _playerHairType = reader.ReadByte();
            _playerName = reader.ReadString();
            _playerHairDye = reader.ReadByte();
            _playerHiddenVisualsFlags = reader.ReadUInt16();
            _playerHiddenMiscFlags = reader.ReadByte();
            _playerHairColor = reader.ReadColor();
            _playerSkinColor = reader.ReadColor();
            _playerEyeColor = reader.ReadColor();
            _playerShirtColor = reader.ReadColor();
            _playerUndershirtColor = reader.ReadColor();
            _playerPantsColor = reader.ReadColor();
            _playerShoeColor = reader.ReadColor();
            _playerDifficulty = (PlayerDifficulty)reader.ReadByte();
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
