using System.IO;
using Microsoft.Xna.Framework;
using Orion.Items;
using Orion.Projectiles;

namespace Orion.Networking.Packets.Items {
    /// <summary>
    /// Packet sent from the server to the client to alter an item.
    /// </summary>
    public sealed class AlterItemPacket : Packet {
        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public short ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the item color.
        /// </summary>
        public Color? ItemColor { get; set; }

        /// <summary>
        /// Gets or sets the item damage.
        /// </summary>
        public ushort? ItemDamage { get; set; }

        /// <summary>
        /// Gets or sets the item knockback.
        /// </summary>
        public float? ItemKnockback { get; set; }

        /// <summary>
        /// Gets or sets the item's animation time.
        /// </summary>
        public ushort? ItemAnimationTime { get; set; }

        /// <summary>
        /// Gets or sets the item's use time.
        /// </summary>
        public ushort? ItemUseTime { get; set; }

        /// <summary>
        /// Gets or sets the item's projectile type.
        /// </summary>
        public ProjectileType? ItemProjectileType { get; set; }

        /// <summary>
        /// Gets or sets the item's projectile speed.
        /// </summary>
        public float? ItemProjectileSpeed { get; set; }

        /// <summary>
        /// Gets or sets the item's width.
        /// </summary>
        public short? ItemWidth { get; set; }

        /// <summary>
        /// Gets or sets the item's height.
        /// </summary>
        public short? ItemHeight { get; set; }

        /// <summary>
        /// Gets or sets the item's scale.
        /// </summary>
        public float? ItemScale { get; set; }

        /// <summary>
        /// Gets or sets the item's ammo type.
        /// </summary>
        public AmmoType? ItemAmmoType { get; set; }

        /// <summary>
        /// Gets or sets the item's used ammo type.
        /// </summary>
        public AmmoType? ItemUsesAmmoType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is not ammo.
        /// </summary>
        public bool? ItemIsNotAmmo { get; set; }

        private protected override PacketType Type => PacketType.AlterItem;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            ItemIndex = reader.ReadInt16();

            Terraria.BitsByte flags = reader.ReadByte();
            if (flags[0]) ItemColor = new Color(reader.ReadUInt32());
            if (flags[1]) ItemDamage = reader.ReadUInt16();
            if (flags[2]) ItemKnockback = reader.ReadSingle();
            if (flags[3]) ItemAnimationTime = reader.ReadUInt16();
            if (flags[4]) ItemUseTime = reader.ReadUInt16();
            if (flags[5]) ItemProjectileType = (ProjectileType)reader.ReadInt16();
            if (flags[6]) ItemProjectileSpeed = reader.ReadSingle();
            if (!flags[7]) return;

            Terraria.BitsByte flags2 = reader.ReadByte();
            if (flags2[0]) ItemWidth = reader.ReadInt16();
            if (flags2[1]) ItemHeight = reader.ReadInt16();
            if (flags2[2]) ItemScale = reader.ReadSingle();
            if (flags2[3]) ItemAmmoType = (AmmoType)reader.ReadInt16();
            if (flags2[4]) ItemUsesAmmoType = (AmmoType)reader.ReadInt16();
            if (flags2[5]) ItemIsNotAmmo = reader.ReadBoolean();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(ItemIndex);

            Terraria.BitsByte flags2 = 0;
            flags2[0] = ItemWidth != null;
            flags2[1] = ItemHeight != null;
            flags2[2] = ItemScale != null;
            flags2[3] = ItemAmmoType != null;
            flags2[4] = ItemUsesAmmoType != null;
            flags2[5] = ItemIsNotAmmo != null;

            Terraria.BitsByte flags = 0;
            flags[0] = ItemColor != null;
            flags[1] = ItemDamage != null;
            flags[2] = ItemKnockback != null;
            flags[3] = ItemAnimationTime != null;
            flags[4] = ItemUseTime != null;
            flags[5] = ItemProjectileType != null;
            flags[6] = ItemProjectileSpeed != null;
            flags[7] = flags2 != 0;

            writer.Write(flags);
            if (flags[0]) writer.Write(ItemColor.Value.PackedValue);
            if (flags[1]) writer.Write(ItemDamage.Value);
            if (flags[2]) writer.Write(ItemKnockback.Value);
            if (flags[3]) writer.Write(ItemAnimationTime.Value);
            if (flags[4]) writer.Write(ItemUseTime.Value);
            if (flags[5]) writer.Write((short)ItemProjectileType.Value);
            if (flags[6]) writer.Write(ItemProjectileSpeed.Value);
            if (flags[7]) writer.Write(flags2);

            if (flags2[0]) writer.Write(ItemWidth.Value);
            if (flags2[1]) writer.Write(ItemHeight.Value);
            if (flags2[2]) writer.Write(ItemScale.Value);
            if (flags2[3]) writer.Write((short)ItemAmmoType.Value);
            if (flags2[4]) writer.Write((short)ItemUsesAmmoType.Value);
            if (flags2[5]) writer.Write(ItemIsNotAmmo.Value);
        }
    }
}
