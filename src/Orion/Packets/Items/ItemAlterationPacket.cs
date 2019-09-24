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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Projectiles;

namespace Orion.Packets.Items {
    /// <summary>
    /// Packet sent from the server to the client to perform an item alteration. This can be used to provide "custom"
    /// items to a vanilla client.
    /// </summary>

    // TODO: write tests for this class.
    [PublicAPI]
    public sealed class ItemAlterationPacket : Packet {
        private short _itemIndex;
        [CanBeNull] private Color? _itemColorOverride;
        [CanBeNull] private ushort? _itemDamageOverride;
        [CanBeNull] private float? _itemKnockbackOverride;
        [CanBeNull] private ushort? _itemAnimationTimeOverride;
        [CanBeNull] private ushort? _itemUseTimeOverride;
        [CanBeNull] private ProjectileType? _itemProjectileTypeOverride;
        [CanBeNull] private float? _itemProjectileSpeedOverride;
        [CanBeNull] private short? _itemWidthOverride;
        [CanBeNull] private short? _itemHeightOverride;
        [CanBeNull] private float? _itemScaleOverride;
        [CanBeNull] private ItemType? _itemAmmoTypeOverride;
        [CanBeNull] private ItemType? _itemUsesAmmoTypeOverride;
        [CanBeNull] private bool? _itemIsNotAmmoOverride;

        /// <inheritdoc />
        public override PacketType Type => PacketType.ItemAlteration;

        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex {
            get => _itemIndex;
            set {
                _itemIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's color. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public Color? ItemColorOverride {
            get => _itemColorOverride;
            set {
                _itemColorOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's damage. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public ushort? ItemDamageOverride {
            get => _itemDamageOverride;
            set {
                _itemDamageOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's knockback. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public float? ItemKnockbackOverride {
            get => _itemKnockbackOverride;
            set {
                _itemKnockbackOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's animation time. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public ushort? ItemAnimationTimeOverride {
            get => _itemAnimationTimeOverride;
            set {
                _itemAnimationTimeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's use time. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public ushort? ItemUseTimeOverride {
            get => _itemUseTimeOverride;
            set {
                _itemUseTimeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's projectile type. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public ProjectileType? ItemProjectileTypeOverride {
            get => _itemProjectileTypeOverride;
            set {
                _itemProjectileTypeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's projectile speed. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public float? ItemProjectileSpeedOverride {
            get => _itemProjectileSpeedOverride;
            set {
                _itemProjectileSpeedOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's width. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public short? ItemWidthOverride {
            get => _itemWidthOverride;
            set {
                _itemWidthOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's height. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public short? ItemHeightOverride {
            get => _itemHeightOverride;
            set {
                _itemHeightOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's scale. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public float? ItemScaleOverride {
            get => _itemScaleOverride;
            set {
                _itemScaleOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the item's ammo type. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public ItemType? ItemAmmoTypeOverride {
            get => _itemAmmoTypeOverride;
            set {
                _itemAmmoTypeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the ammo type that the item uses. A value of <c>null</c> indicates no override.
        /// </summary>
        [CanBeNull]
        public ItemType? ItemUsesAmmoTypeOverride {
            get => _itemUsesAmmoTypeOverride;
            set {
                _itemUsesAmmoTypeOverride = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the override for the value indicating whether the item is not ammo. A value of <c>null</c>
        /// indicates no override.
        /// </summary>
        [CanBeNull]
        public bool? ItemIsNotAmmoOverride {
            get => _itemIsNotAmmoOverride;
            set {
                _itemIsNotAmmoOverride = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ItemIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemIndex = reader.ReadInt16();

            Terraria.BitsByte flags = reader.ReadByte();
            if (flags[0]) _itemColorOverride = new Color(reader.ReadUInt32());
            if (flags[1]) _itemDamageOverride = reader.ReadUInt16();
            if (flags[2]) _itemKnockbackOverride = reader.ReadSingle();
            if (flags[3]) _itemAnimationTimeOverride = reader.ReadUInt16();
            if (flags[4]) _itemUseTimeOverride = reader.ReadUInt16();
            if (flags[5]) _itemProjectileTypeOverride = (ProjectileType)reader.ReadInt16();
            if (flags[6]) _itemProjectileSpeedOverride = reader.ReadSingle();
            if (!flags[7]) return;

            Terraria.BitsByte flags2 = reader.ReadByte();
            if (flags2[0]) _itemWidthOverride = reader.ReadInt16();
            if (flags2[1]) _itemHeightOverride = reader.ReadInt16();
            if (flags2[2]) _itemScaleOverride = reader.ReadSingle();
            if (flags2[3]) _itemAmmoTypeOverride = (ItemType)reader.ReadInt16();
            if (flags2[4]) _itemUsesAmmoTypeOverride = (ItemType)reader.ReadInt16();
            if (flags2[5]) _itemIsNotAmmoOverride = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemIndex);

            Terraria.BitsByte flags2 = 0;
            flags2[0] = _itemWidthOverride != null;
            flags2[1] = _itemHeightOverride != null;
            flags2[2] = _itemScaleOverride != null;
            flags2[3] = _itemAmmoTypeOverride != null;
            flags2[4] = _itemUsesAmmoTypeOverride != null;
            flags2[5] = _itemIsNotAmmoOverride != null;

            Terraria.BitsByte flags = 0;
            flags[0] = _itemColorOverride != null;
            flags[1] = _itemDamageOverride != null;
            flags[2] = _itemKnockbackOverride != null;
            flags[3] = _itemAnimationTimeOverride != null;
            flags[4] = _itemUseTimeOverride != null;
            flags[5] = _itemProjectileTypeOverride != null;
            flags[6] = _itemProjectileSpeedOverride != null;
            flags[7] = flags2 != 0;

            writer.Write(flags);
            if (flags[0]) writer.Write(_itemColorOverride.Value.PackedValue);
            if (flags[1]) writer.Write(_itemDamageOverride.Value);
            if (flags[2]) writer.Write(_itemKnockbackOverride.Value);
            if (flags[3]) writer.Write(_itemAnimationTimeOverride.Value);
            if (flags[4]) writer.Write(_itemUseTimeOverride.Value);
            if (flags[5]) writer.Write((short)_itemProjectileTypeOverride);
            if (flags[6]) writer.Write(_itemProjectileSpeedOverride.Value);
            if (flags[7]) writer.Write(flags2);

            if (flags2[0]) writer.Write(_itemWidthOverride.Value);
            if (flags2[1]) writer.Write(_itemHeightOverride.Value);
            if (flags2[2]) writer.Write(_itemScaleOverride.Value);
            if (flags2[3]) writer.Write((short)_itemAmmoTypeOverride);
            if (flags2[4]) writer.Write((short)_itemUsesAmmoTypeOverride);
            if (flags2[5]) writer.Write(_itemIsNotAmmoOverride.Value);
        }
    }
}
