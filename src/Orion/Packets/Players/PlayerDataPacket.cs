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
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;
using Orion.Players;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set player data. This is sent in response to a <see cref="PlayerContinueConnectingPacket"/>.
    /// </summary>
    [PublicAPI]
    public sealed class PlayerDataPacket : Packet {
        private byte _playerIndex;
        private byte _playerSkinType;
        private byte _playerHairType;
        [NotNull] private string _playerName = "";
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

        /// <inheritdoc />
        public override PacketType Type => PacketType.PlayerData;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's skin type.
        /// </summary>
        public byte PlayerSkinType {
            get => _playerSkinType;
            set {
                _playerSkinType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair type.
        /// </summary>
        public byte PlayerHairType {
            get => _playerHairType;
            set {
                _playerHairType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        [NotNull]
        public string PlayerName {
            get => _playerName;
            set {
                _playerName = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        public byte PlayerHairDye {
            get => _playerHairDye;
            set {
                _playerHairDye = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hidden visuals flags.
        /// </summary>
        public ushort PlayerHiddenVisualsFlags {
            get => _playerHiddenVisualsFlags;
            set {
                _playerHiddenVisualsFlags = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hidden misc flags.
        /// </summary>
        public byte PlayerHiddenMiscFlags {
            get => _playerHiddenMiscFlags;
            set {
                _playerHiddenMiscFlags = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair color.
        /// </summary>
        public Color PlayerHairColor {
            get => _playerHairColor;
            set {
                _playerHairColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's skin color.
        /// </summary>
        public Color PlayerSkinColor {
            get => _playerSkinColor;
            set {
                _playerSkinColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's eye color.
        /// </summary>
        public Color PlayerEyeColor {
            get => _playerEyeColor;
            set {
                _playerEyeColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's shirt color.
        /// </summary>
        public Color PlayerShirtColor {
            get => _playerShirtColor;
            set {
                _playerShirtColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's undershirt color.
        /// </summary>
        public Color PlayerUndershirtColor {
            get => _playerUndershirtColor;
            set {
                _playerUndershirtColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's pants color.
        /// </summary>
        public Color PlayerPantsColor {
            get => _playerPantsColor;
            set {
                _playerPantsColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's shoe color.
        /// </summary>
        public Color PlayerShoeColor {
            get => _playerShoeColor;
            set {
                _playerShoeColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        public PlayerDifficulty PlayerDifficulty {
            get => _playerDifficulty;
            set {
                _playerDifficulty = value;
                _isDirty = true;
            }
        }

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
            writer.Write(_playerIndex);
            writer.Write(_playerSkinType);
            writer.Write(_playerHairType);
            writer.Write(_playerName);
            writer.Write(_playerHairDye);
            writer.Write(_playerHiddenVisualsFlags);
            writer.Write(_playerHiddenMiscFlags);
            writer.Write(_playerHairColor);
            writer.Write(_playerSkinColor);
            writer.Write(_playerEyeColor);
            writer.Write(_playerShirtColor);
            writer.Write(_playerUndershirtColor);
            writer.Write(_playerPantsColor);
            writer.Write(_playerShoeColor);
            writer.Write((byte)_playerDifficulty);
        }
    }
}
