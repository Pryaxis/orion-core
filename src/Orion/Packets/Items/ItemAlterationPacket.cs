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

using System.IO;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Projectiles;

namespace Orion.Packets.Items {
    /// <summary>
    /// Packet sent from the server to the client to perform an item alteration. This can be used to provide "custom"
    /// items to a vanilla client.
    /// </summary>
    // TODO: write tests for this class.
    public sealed class ItemAlterationPacket : Packet {
        private short _itemIndex;
        private Color? _colorOverride;
        private ushort? _damageOverride;
        private float? _knockbackOverride;
        private ushort? _animationTimeOverride;
        private ushort? _useTimeOverride;
        private ProjectileType? _projectileTypeOverride;
        private float? _projectileSpeedOverride;
        private short? _widthOverride;
        private short? _heightOverride;
        private float? _scaleOverride;
        private ItemType? _ammoTypeOverride;
        private ItemType? _usesAmmoTypeOverride;
        private bool? _isNotAmmoOverride;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.ItemAlteration;

        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        /// <value>The item index.</value>
        public short ItemIndex {
            get => _itemIndex;
            set {
                _itemIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's color. The alpha component is ignored. If <see langword="null"/>,
        /// then there is no override.
        /// </summary>
        /// <value>The override for the item's color.</value>
        public Color? ColorOverride {
            get => _colorOverride;
            set {
                _colorOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's damage. If <see langword="null"/>, then there is no override.
        /// </summary>
        /// <value>The override for the item's damage.</value>
        public ushort? DamageOverride {
            get => _damageOverride;
            set {
                _damageOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's knockback. If <see langword="null"/>, then there is no override.
        /// </summary>
        /// <value>The override for the item's knockback.</value>
        public float? KnockbackOverride {
            get => _knockbackOverride;
            set {
                _knockbackOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's animation time. If <see langword="null"/>, then there is no
        /// override.
        /// </summary>
        /// <value>The override for the item's animation time.</value>
        public ushort? AnimationTimeOverride {
            get => _animationTimeOverride;
            set {
                _animationTimeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's use time. If <see langword="null"/>, then there is no override.
        /// </summary>
        /// <value>The override for the item's use time.</value>
        public ushort? UseTimeOverride {
            get => _useTimeOverride;
            set {
                _useTimeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's projectile type. If <see langword="null"/>, then there is no
        /// override.
        /// </summary>
        /// <value>The override for the item's projectile type.</value>
        public ProjectileType? ProjectileTypeOverride {
            get => _projectileTypeOverride;
            set {
                _projectileTypeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's projectile speed. If <see langword="null"/>, then there is no
        /// override.
        /// </summary>
        /// <value>The override for the item's projectile speed.</value>
        public float? ProjectileSpeedOverride {
            get => _projectileSpeedOverride;
            set {
                _projectileSpeedOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's width. If <see langword="null"/>, then there is no override.
        /// </summary>
        /// <value>The override for the item's width.</value>
        public short? WidthOverride {
            get => _widthOverride;
            set {
                _widthOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's height. If <see langword="null"/>, then there is no override.
        /// </summary>
        /// <value>The override for the item's height.</value>
        public short? HeightOverride {
            get => _heightOverride;
            set {
                _heightOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's scale. If <see langword="null"/>, then there is no override.
        /// </summary>
        /// <value>The override for the item's scale.</value>
        public float? ScaleOverride {
            get => _scaleOverride;
            set {
                _scaleOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's ammo type. If <see langword="null"/>, then there is no override.
        /// </summary>
        /// <value>The override for the item's ammo type.</value>
        public ItemType? AmmoTypeOverride {
            get => _ammoTypeOverride;
            set {
                _ammoTypeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the ammo type that the item uses. If <see langword="null"/>, then there is no
        /// override.
        /// </summary>
        /// <value>The override for the ammo type that the item uses.</value>
        public ItemType? UsesAmmoTypeOverride {
            get => _usesAmmoTypeOverride;
            set {
                _usesAmmoTypeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the value indicating whether the item is not ammo. If <see langword="null"/>,
        /// then there is no override.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the item is overriden to not be ammo, <see langword="false"/> if it is overriden
        /// to not be ammo; otherwise, <see langword="null"/>.</value>
        public bool? IsNotAmmoOverride {
            get => _isNotAmmoOverride;
            set {
                _isNotAmmoOverride = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _itemIndex = reader.ReadInt16();

            Terraria.BitsByte flags = reader.ReadByte();
            if (flags[0]) _colorOverride = new Color(reader.ReadUInt32());
            if (flags[1]) _damageOverride = reader.ReadUInt16();
            if (flags[2]) _knockbackOverride = reader.ReadSingle();
            if (flags[3]) _animationTimeOverride = reader.ReadUInt16();
            if (flags[4]) _useTimeOverride = reader.ReadUInt16();
            if (flags[5]) _projectileTypeOverride = (ProjectileType)reader.ReadInt16();
            if (flags[6]) _projectileSpeedOverride = reader.ReadSingle();
            if (!flags[7]) return;

            Terraria.BitsByte flags2 = reader.ReadByte();
            if (flags2[0]) _widthOverride = reader.ReadInt16();
            if (flags2[1]) _heightOverride = reader.ReadInt16();
            if (flags2[2]) _scaleOverride = reader.ReadSingle();
            if (flags2[3]) _ammoTypeOverride = (ItemType)reader.ReadInt16();
            if (flags2[4]) _usesAmmoTypeOverride = (ItemType)reader.ReadInt16();
            if (flags2[5]) _isNotAmmoOverride = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_itemIndex);

            Terraria.BitsByte flags2 = 0;
            flags2[0] = _widthOverride != null;
            flags2[1] = _heightOverride != null;
            flags2[2] = _scaleOverride != null;
            flags2[3] = _ammoTypeOverride != null;
            flags2[4] = _usesAmmoTypeOverride != null;
            flags2[5] = _isNotAmmoOverride != null;

            Terraria.BitsByte flags = 0;
            flags[0] = _colorOverride != null;
            flags[1] = _damageOverride != null;
            flags[2] = _knockbackOverride != null;
            flags[3] = _animationTimeOverride != null;
            flags[4] = _useTimeOverride != null;
            flags[5] = _projectileTypeOverride != null;
            flags[6] = _projectileSpeedOverride != null;
            flags[7] = flags2 != 0;

            writer.Write(flags);

            if (flags[0]) writer.Write(_colorOverride!.Value.PackedValue);
            if (flags[1]) writer.Write(_damageOverride!.Value);
            if (flags[2]) writer.Write(_knockbackOverride!.Value);
            if (flags[3]) writer.Write(_animationTimeOverride!.Value);
            if (flags[4]) writer.Write(_useTimeOverride!.Value);
            if (flags[5]) writer.Write((short)_projectileTypeOverride!.Value);
            if (flags[6]) writer.Write(_projectileSpeedOverride!.Value);
            if (flags[7]) writer.Write(flags2);

            if (flags2[0]) writer.Write(_widthOverride!.Value);
            if (flags2[1]) writer.Write(_heightOverride!.Value);
            if (flags2[2]) writer.Write(_scaleOverride!.Value);
            if (flags2[3]) writer.Write((short)_ammoTypeOverride!.Value);
            if (flags2[4]) writer.Write((short)_usesAmmoTypeOverride!.Value);
            if (flags2[5]) writer.Write(_isNotAmmoOverride!.Value);
        }
    }
}
