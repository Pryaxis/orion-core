using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Server
{
    /// <summary>
    /// A packet sent to play a sound.
    /// </summary>
    public struct ServerSound : IPacket
    {
        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the sound index.
        /// </summary>
        public ushort SoundIndex { get; set; }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public int? Style { get; set; }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        public float? Volume { get; set; }

        /// <summary>
        /// Gets or sets the pitch offset.
        /// </summary>
        public float? PitchOffset { get; set; }

        PacketId IPacket.Id => PacketId.ServerSound;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = 11;
            Position = span.Read<Vector2f>();
            SoundIndex = span.Read<ushort>();
            var flags = span.Read<Flags8>();
            if (flags[0])
            {
                Style = span.Read<int>();
                length += 4;
            }
            if (flags[1])
            {
                Volume = span.Read<float>();
                length += 4;
            }
            if (flags[2])
            {
                PitchOffset = span.Read<float>();
                length += 4;
            }
                
            return length;
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(Position);
            var flags = default(Flags8);
            length += span[length..].Write(SoundIndex);

            var flagsOffset = length++;
            if (Style.HasValue)
            {
                flags[0] = true;
                length += span[length..].Write(Style.Value);
            }
            if (Volume.HasValue)
            {
                flags[1] = true;
                length += span[length..].Write(Volume.Value);
            }
            if (PitchOffset.HasValue)
            {
                flags[2] = true;
                length += span[length..].Write(PitchOffset.Value);
            }

            span[flagsOffset] = Unsafe.As<Flags8, byte>(ref flags);
            return length;
        }
    }
}
