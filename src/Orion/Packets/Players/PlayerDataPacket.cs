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
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Packets.Extensions;
using Orion.Players;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set player data. This is sent in response to a <see cref="PlayerContinueConnectingPacket"/>.
    /// </summary>
    public sealed class PlayerDataPacket : Packet {
        private byte _playerIndex;
        private byte _clothesStyle;
        private byte _hairstyle;
        private string _name = string.Empty;
        private byte _hairDye;
        private ushort _equipmentHiddenFlags;
        private byte _miscEquipmentHiddenFlags;
        private Color _hairColor;
        private Color _skinColor;
        private Color _eyeColor;
        private Color _shirtColor;
        private Color _undershirtColor;
        private Color _pantsColor;
        private Color _shoeColor;
        private PlayerDifficulty _difficulty;
        private bool _hasExtraAccessorySlot;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.PlayerData;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        public byte PlayerIndex {
            get => _playerIndex;
            set {
                _playerIndex = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's clothes style.
        /// </summary>
        /// <value>The player's clothes style.</value>
        public byte ClothesStyle {
            get => _clothesStyle;
            set {
                _clothesStyle = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's hairstyle.
        /// </summary>
        /// <value>The player's hairstyle.</value>
        public byte Hairstyle {
            get => _hairstyle;
            set {
                _hairstyle = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <value>The player's name.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Name {
            get => _name;
            set {
                _name = value ?? throw new ArgumentNullException(nameof(value));
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        /// <value>The player's hair dye.</value>
        public byte HairDye {
            get => _hairDye;
            set {
                _hairDye = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's equipment hiding flags.
        /// </summary>
        /// <value>The player's equipment hiding flags.</value>
        /// <remarks>This property specifies which of the player's equipment is visible.</remarks>
        public ushort EquipmentHiddenFlags {
            get => _equipmentHiddenFlags;
            set {
                _equipmentHiddenFlags = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's miscellaneous equipment hiding flags.
        /// </summary>
        /// <value>The player's miscellaneous equipment hiding flags.</value>
        /// <remarks>This property specifies which of the player's miscellaneous equipment is visible.</remarks>
        public byte MiscEquipmentHiddenFlags {
            get => _miscEquipmentHiddenFlags;
            set {
                _miscEquipmentHiddenFlags = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's hair color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's hair color.</value>
        public Color HairColor {
            get => _hairColor;
            set {
                _hairColor = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's skin color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's skin color.</value>
        public Color SkinColor {
            get => _skinColor;
            set {
                _skinColor = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's eye color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's eye color.</value>
        public Color EyeColor {
            get => _eyeColor;
            set {
                _eyeColor = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's shirt color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's shirt color.</value>
        public Color ShirtColor {
            get => _shirtColor;
            set {
                _shirtColor = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's undershirt color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's undershirt color.</value>
        public Color UndershirtColor {
            get => _undershirtColor;
            set {
                _undershirtColor = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets the player's pants color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's pants color.</value>
        public Color PantsColor {
            get => _pantsColor;
            set {
                _pantsColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's shoe color. The alpha component is ignored.
        /// </summary>
        /// <value>The player's shoe color.</value>
        public Color ShoeColor {
            get => _shoeColor;
            set {
                _shoeColor = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        /// <value>The player's difficulty.</value>
        public PlayerDifficulty Difficulty {
            get => _difficulty;
            set {
                _difficulty = value;
                _isDirty = true;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether the player has an extra accessory slot.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player has an extra accessory slot; otherwise, <see langword="false"/>.
        /// </value>
        /// <remarks>
        /// The extra accessory slot can be obtained with the <see cref="ItemType.DemonHeart"/> item. There is also
        /// support for a seventh accessory slot but it is not legitimately obtainable.
        /// </remarks>
        public bool HasExtraAccessorySlot {
            get => _hasExtraAccessorySlot;
            set {
                _hasExtraAccessorySlot = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _playerIndex = reader.ReadByte();
            _clothesStyle = reader.ReadByte();
            _hairstyle = reader.ReadByte();
            _name = reader.ReadString();
            _hairDye = reader.ReadByte();
            _equipmentHiddenFlags = reader.ReadUInt16();
            _miscEquipmentHiddenFlags = reader.ReadByte();
            _hairColor = reader.ReadColor();
            _skinColor = reader.ReadColor();
            _eyeColor = reader.ReadColor();
            _shirtColor = reader.ReadColor();
            _undershirtColor = reader.ReadColor();
            _pantsColor = reader.ReadColor();
            _shoeColor = reader.ReadColor();

            Terraria.BitsByte flags = reader.ReadByte();
            if (flags[0]) _difficulty = PlayerDifficulty.Mediumcore;
            if (flags[1]) _difficulty = PlayerDifficulty.Hardcore;
            _hasExtraAccessorySlot = flags[2];
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_playerIndex);
            writer.Write(_clothesStyle);
            writer.Write(_hairstyle);
            writer.Write(_name);
            writer.Write(_hairDye);
            writer.Write(_equipmentHiddenFlags);
            writer.Write(_miscEquipmentHiddenFlags);
            writer.Write(in _hairColor);
            writer.Write(in _skinColor);
            writer.Write(in _eyeColor);
            writer.Write(in _shirtColor);
            writer.Write(in _undershirtColor);
            writer.Write(in _pantsColor);
            writer.Write(in _shoeColor);

            Terraria.BitsByte flags = 0;
            flags[0] = _difficulty == PlayerDifficulty.Mediumcore;
            flags[1] = _difficulty == PlayerDifficulty.Hardcore;
            flags[2] = _hasExtraAccessorySlot;
            writer.Write(flags);
        }
    }
}
