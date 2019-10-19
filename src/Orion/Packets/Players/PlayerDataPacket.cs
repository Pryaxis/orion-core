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
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Packets.Extensions;
using Orion.Players;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set player data. This is sent in response to a <see cref="PlayerContinueConnectingPacket"/>.
    /// </summary>
    public sealed class PlayerDataPacket : Packet {
        private byte _playerIndex;
        private byte _playerClothesStyle;
        private byte _playerHairstyle;
        private string _playerName = string.Empty;
        private byte _playerHairDye;
        private ushort _playerEquipmentHiddenFlags;
        private byte _playerMiscEquipmentHiddenFlags;
        private Color _playerHairColor;
        private Color _playerSkinColor;
        private Color _playerEyeColor;
        private Color _playerShirtColor;
        private Color _playerUndershirtColor;
        private Color _playerPantsColor;
        private Color _playerShoeColor;
        private PlayerDifficulty _playerDifficulty;
        private bool _playerHasExtraAccessorySlot;

        /// <inheritdoc/>
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
        public byte PlayerClothesStyle {
            get => _playerClothesStyle;
            set {
                _playerClothesStyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair type.
        /// </summary>
        public byte PlayerHairstyle {
            get => _playerHairstyle;
            set {
                _playerHairstyle = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
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
        public ushort PlayerEquipmentHiddenFlags {
            get => _playerEquipmentHiddenFlags;
            set {
                _playerEquipmentHiddenFlags = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hidden misc flags.
        /// </summary>
        public byte PlayerMiscEquipmentHiddenFlags {
            get => _playerMiscEquipmentHiddenFlags;
            set {
                _playerMiscEquipmentHiddenFlags = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's hair color. The alpha component is ignored.
        /// </summary>
        public Color PlayerHairColor {
            get => _playerHairColor;
            set {
                _playerHairColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's skin color. The alpha component is ignored.
        /// </summary>
        public Color PlayerSkinColor {
            get => _playerSkinColor;
            set {
                _playerSkinColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's eye color. The alpha component is ignored.
        /// </summary>
        public Color PlayerEyeColor {
            get => _playerEyeColor;
            set {
                _playerEyeColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's shirt color. The alpha component is ignored.
        /// </summary>
        public Color PlayerShirtColor {
            get => _playerShirtColor;
            set {
                _playerShirtColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's undershirt color. The alpha component is ignored.
        /// </summary>
        public Color PlayerUndershirtColor {
            get => _playerUndershirtColor;
            set {
                _playerUndershirtColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's pants color. The alpha component is ignored.
        /// </summary>
        public Color PlayerPantsColor {
            get => _playerPantsColor;
            set {
                _playerPantsColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's shoe color. The alpha component is ignored.
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

        /// <summary>
        /// Gets or sets a value indicating whether the player has an extra accessory.
        /// </summary>
        public bool PlayerHasExtraAccessorySlot {
            get => _playerHasExtraAccessorySlot;
            set {
                _playerHasExtraAccessorySlot = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        [Pure, ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={PlayerIndex} is {PlayerName}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _playerClothesStyle = reader.ReadByte();
            _playerHairstyle = reader.ReadByte();
            _playerName = reader.ReadString();
            _playerHairDye = reader.ReadByte();
            _playerEquipmentHiddenFlags = reader.ReadUInt16();
            _playerMiscEquipmentHiddenFlags = reader.ReadByte();
            _playerHairColor = reader.ReadColor();
            _playerSkinColor = reader.ReadColor();
            _playerEyeColor = reader.ReadColor();
            _playerShirtColor = reader.ReadColor();
            _playerUndershirtColor = reader.ReadColor();
            _playerPantsColor = reader.ReadColor();
            _playerShoeColor = reader.ReadColor();

            Terraria.BitsByte flags = reader.ReadByte();
            if (flags[0]) {
                _playerDifficulty = PlayerDifficulty.Mediumcore;
            }

            if (flags[1]) {
                _playerDifficulty = PlayerDifficulty.Hardcore;
            }

            if (flags[2]) {
                _playerHasExtraAccessorySlot = true;
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_playerClothesStyle);
            writer.Write(_playerHairstyle);
            writer.Write(_playerName);
            writer.Write(_playerHairDye);
            writer.Write(_playerEquipmentHiddenFlags);
            writer.Write(_playerMiscEquipmentHiddenFlags);
            writer.Write(in _playerHairColor);
            writer.Write(in _playerSkinColor);
            writer.Write(in _playerEyeColor);
            writer.Write(in _playerShirtColor);
            writer.Write(in _playerUndershirtColor);
            writer.Write(in _playerPantsColor);
            writer.Write(in _playerShoeColor);

            Terraria.BitsByte flags = 0;
            flags[0] = _playerDifficulty == PlayerDifficulty.Mediumcore;
            flags[1] = _playerDifficulty == PlayerDifficulty.Hardcore;
            flags[2] = _playerHasExtraAccessorySlot;
            writer.Write(flags);
        }
    }
}
