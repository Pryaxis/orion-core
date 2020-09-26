using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Schema;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Items
{
    /// <summary>
    /// A packet sent to tweak an item.
    /// </summary>
    public struct ItemTweak : IPacket
    {
        private Flags8 _flags;
        private Flags8 _flags2;
        private uint _packedColor;

        /// <summary>
        /// Gets or sets the item index.
        /// </summary>
        public int ItemIndex { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color3? Color
        {
            get
            {
                var r = (byte)(_packedColor >> 16);
                var g = (byte)(_packedColor >> 8);
                var b = (byte)_packedColor;
                return new Color3(r, g, b);
            }
            set
            {
                if (value is null)
                {
                    return;
                }

                _packedColor = 0;
                _packedColor = (_packedColor & 0xff00ffff) | ((uint)(value.Value.R << 16));
                _packedColor = (_packedColor & 0xffff00ff) | ((uint)(value.Value.G << 8));
                _packedColor = (_packedColor & 0xffffff00) | value.Value.B;
            }
        }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public ushort? Damage { get; set; }

        /// <summary>
        /// Gets or sets the knockback.
        /// </summary>
        public float? Knockback { get; set; }

        /// <summary>
        /// Gets or sets the animation time.
        /// </summary>
        public ushort? AnimationTime { get; set; }

        /// <summary>
        /// Gets or sets the use time.
        /// </summary>
        public ushort? UseTime { get; set; }

        /// <summary>
        /// Gets or sets the type of projectile spawned by the item.
        /// </summary>
        public short? ShootProjectileType { get; set; }

        /// <summary>
        /// Gets or sets the shoot speed.
        /// </summary>
        public float? ShootSpeed { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public short? Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public short? Height { get; set; }

        /// <summary>
        /// Gets or sets the item's texture scale.
        /// </summary>
        public float? Scale { get; set; }

        /// <summary>
        /// Gets or sets the type of ammo this item represents.
        /// </summary>
        public short? AmmoType { get; set; }

        /// <summary>
        /// Gets or sets the type of ammo required to use this item.
        /// </summary>
        public short? AmmoRequiredToUseItem { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item requires "real" ammo (i.e, anything but coins).
        /// </summary>
        public bool? NotRealAmmo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item's dimensions and ammo data are sent.
        /// </summary>
        public bool? IncludeDimensionsAndAmmo { get; set; }

        PacketId IPacket.Id => PacketId.ItemTweak;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            ItemIndex = MemoryMarshal.Read<int>(span);

            var length = 4;
            _flags = MemoryMarshal.Read<Flags8>(span[(length++)..]);
            if (_flags[0])
            {
                _packedColor = MemoryMarshal.Read<uint>(span[length..]);
                length += 4;
            }
            if (_flags[1])
            {
                Damage = MemoryMarshal.Read<ushort>(span[length..]);
                length += 2;
            }
            if (_flags[2])
            {
                Knockback = MemoryMarshal.Read<float>(span[length..]);
                length += 4;
            }
            if (_flags[3])
            {
                AnimationTime = MemoryMarshal.Read<ushort>(span[length..]);
                length += 2;
            }
            if (_flags[4])
            {
                UseTime = MemoryMarshal.Read<ushort>(span[length..]);
                length += 2;
            }
            if (_flags[5])
            {
                ShootProjectileType = MemoryMarshal.Read<short>(span[length..]);
                length += 2;
            }
            if (_flags[6])
            {
                ShootSpeed = MemoryMarshal.Read<float>(span[length..]);
                length += 4;
            }
            if (_flags[7])
            {
                IncludeDimensionsAndAmmo = true;
                var flags2 = MemoryMarshal.Read<Flags8>(span[(length++)..]);
                if (flags2[0])
                {
                    Width = MemoryMarshal.Read<short>(span[length..]);
                    length += 2;
                }
                if (flags2[1])
                {
                    Height = MemoryMarshal.Read<short>(span[length..]);
                    length += 2;
                }
                if (flags2[2])
                {
                    Scale = MemoryMarshal.Read<float>(span[length..]);
                    length += 4;
                }
                if (flags2[3])
                {
                    AmmoType = MemoryMarshal.Read<short>(span[length..]);
                    length += 2;
                }
                if (flags2[4])
                {
                    AmmoRequiredToUseItem = MemoryMarshal.Read<short>(span[length..]);
                    length += 2;
                }
                if (flags2[5])
                {
                    NotRealAmmo = MemoryMarshal.Read<bool>(span[length..]);
                    ++length;
                }
            }

            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = Write(span, ItemIndex);
            var flagsOffset = length++;
            if (Color.HasValue)
            {
                length += Write(span[length..], _packedColor);
                _flags[0] = true;
            }
            if (Damage.HasValue)
            {
                length += Write(span[length..], Damage.Value);
                _flags[1] = true;
            }
            if (Knockback.HasValue)
            {
                length += Write(span[length..], Knockback.Value);
                _flags[2] = true;
            }
            if (AnimationTime.HasValue)
            {
                length += Write(span[length..], AnimationTime.Value);
                _flags[3] = true;
            }
            if (UseTime.HasValue)
            {
                length += Write(span[length..], UseTime.Value);
                _flags[4] = true;
            }
            if (ShootProjectileType.HasValue)
            {
                length += Write(span[length..], ShootProjectileType.Value);
                _flags[5] = true;
            }
            if (ShootSpeed.HasValue)
            {
                length += Write(span[length..], ShootSpeed.Value);
                _flags[6] = true;
            }
            if (IncludeDimensionsAndAmmo == true)
            {
                _flags[7] = true;
                var flags2Offset = length++;
                span[flagsOffset] = Unsafe.As<Flags8, byte>(ref _flags);
                if (Width.HasValue)
                {
                    length += Write(span[length..], Width.Value);
                    _flags2[0] = true;
                }
                if (Height.HasValue)
                {
                    length += Write(span[length..], Height.Value);
                    _flags2[1] = true;
                }
                if (Scale.HasValue)
                {
                    length += Write(span[length..], Scale.Value);
                    _flags2[2] = true;
                }
                if (AmmoType.HasValue)
                {
                    length += Write(span[length..], AmmoType.Value);
                    _flags2[3] = true;
                }
                if (AmmoRequiredToUseItem.HasValue)
                {
                    length += Write(span[length..], AmmoRequiredToUseItem.Value);
                    _flags2[4] = true;
                }
                if (NotRealAmmo.HasValue)
                {
                    length += Write(span[length..], NotRealAmmo.Value);
                    _flags2[5] = true;
                }

                span[flags2Offset] = Unsafe.As<Flags8, byte>(ref _flags2);
            }

            return length;
        }

        private static unsafe int Write<T>(Span<byte> span, T value) where T : unmanaged
        {
            MemoryMarshal.Write(span, ref value);
            return sizeof(T);
        }
    }
}
