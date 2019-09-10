using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Projectiles;

namespace Orion.Networking.Packets.Items {
    /// <summary>
    /// Packet sent from the server to the client to alter an item.
    /// </summary>
    // TODO: write tests for this.
    public sealed class AlterItemPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the item color.
        /// </summary>
        public Color? ItemColorOverride { get; set; }

        /// <summary>
        /// Gets or sets the item damage.
        /// </summary>
        public ushort? ItemDamageOverride { get; set; }

        /// <summary>
        /// Gets or sets the item knockback.
        /// </summary>
        public float? ItemKnockbackOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's animation time.
        /// </summary>
        public ushort? ItemAnimationTimeOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's use time.
        /// </summary>
        public ushort? ItemUseTimeOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's projectile type.
        /// </summary>
        public ProjectileType? ItemProjectileTypeOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's projectile speed.
        /// </summary>
        public float? ItemProjectileSpeedOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's width.
        /// </summary>
        public short? ItemWidthOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's height.
        /// </summary>
        public short? ItemHeightOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's scale.
        /// </summary>
        public float? ItemScaleOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's ammo type.
        /// </summary>
        public AmmoType? ItemAmmoTypeOverride { get; set; }

        /// <summary>
        /// Gets or sets the item's used ammo type.
        /// </summary>
        public AmmoType? ItemUsesAmmoTypeOverride { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is not ammo.
        /// </summary>
        public bool? ItemIsNotAmmoOverride { get; set; }

        private protected override PacketType Type => PacketType.AlterItem;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={ItemIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            ItemIndex = reader.ReadInt16();

            Terraria.BitsByte flags = reader.ReadByte();
            if (flags[0]) ItemColorOverride = new Color(reader.ReadUInt32());
            if (flags[1]) ItemDamageOverride = reader.ReadUInt16();
            if (flags[2]) ItemKnockbackOverride = reader.ReadSingle();
            if (flags[3]) ItemAnimationTimeOverride = reader.ReadUInt16();
            if (flags[4]) ItemUseTimeOverride = reader.ReadUInt16();
            if (flags[5]) ItemProjectileTypeOverride = (ProjectileType)reader.ReadInt16();
            if (flags[6]) ItemProjectileSpeedOverride = reader.ReadSingle();
            if (!flags[7]) return;

            Terraria.BitsByte flags2 = reader.ReadByte();
            if (flags2[0]) ItemWidthOverride = reader.ReadInt16();
            if (flags2[1]) ItemHeightOverride = reader.ReadInt16();
            if (flags2[2]) ItemScaleOverride = reader.ReadSingle();
            if (flags2[3]) ItemAmmoTypeOverride = (AmmoType)reader.ReadInt16();
            if (flags2[4]) ItemUsesAmmoTypeOverride = (AmmoType)reader.ReadInt16();
            if (flags2[5]) ItemIsNotAmmoOverride = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(ItemIndex);

            Terraria.BitsByte flags2 = 0;
            flags2[0] = ItemWidthOverride != null;
            flags2[1] = ItemHeightOverride != null;
            flags2[2] = ItemScaleOverride != null;
            flags2[3] = ItemAmmoTypeOverride != null;
            flags2[4] = ItemUsesAmmoTypeOverride != null;
            flags2[5] = ItemIsNotAmmoOverride != null;

            Terraria.BitsByte flags = 0;
            flags[0] = ItemColorOverride != null;
            flags[1] = ItemDamageOverride != null;
            flags[2] = ItemKnockbackOverride != null;
            flags[3] = ItemAnimationTimeOverride != null;
            flags[4] = ItemUseTimeOverride != null;
            flags[5] = ItemProjectileTypeOverride != null;
            flags[6] = ItemProjectileSpeedOverride != null;
            flags[7] = flags2 != 0;

            writer.Write(flags);
            if (flags[0]) writer.Write(ItemColorOverride.Value.PackedValue);
            if (flags[1]) writer.Write(ItemDamageOverride.Value);
            if (flags[2]) writer.Write(ItemKnockbackOverride.Value);
            if (flags[3]) writer.Write(ItemAnimationTimeOverride.Value);
            if (flags[4]) writer.Write(ItemUseTimeOverride.Value);
            if (flags[5]) writer.Write((short)ItemProjectileTypeOverride.Value);
            if (flags[6]) writer.Write(ItemProjectileSpeedOverride.Value);
            if (flags[7]) writer.Write(flags2);

            if (flags2[0]) writer.Write(ItemWidthOverride.Value);
            if (flags2[1]) writer.Write(ItemHeightOverride.Value);
            if (flags2[2]) writer.Write(ItemScaleOverride.Value);
            if (flags2[3]) writer.Write((short)ItemAmmoTypeOverride.Value);
            if (flags2[4]) writer.Write((short)ItemUsesAmmoTypeOverride.Value);
            if (flags2[5]) writer.Write(ItemIsNotAmmoOverride.Value);
        }
    }
}
