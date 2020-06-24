// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.DataStructures;

namespace Orion.Core.Packets.Client
{
    /// <summary>
    /// A packet sent from the server to the client to set the client's status.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ClientStatusPacket : IPacket
    {
        private const byte HidePercentageMask /* */ = 0b_00000001;
        private const byte HasShadowsMask     /* */ = 0b_00000010;

        [field: FieldOffset(8)] private NetworkText? _statusText;
        [field: FieldOffset(4)] private byte _flags;

        /// <summary>
        /// Gets or sets the client's maximum status.
        /// </summary>
        /// <value>The client's maximum status.</value>
        [field: FieldOffset(0)] public int MaxStatus { get; set; }

        /// <summary>
        /// Gets or sets the client's status text.
        /// </summary>
        /// <value>The client's status text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkText StatusText
        {
            get => _statusText ?? NetworkText.Empty;
            set => _statusText = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether to hide the percentage in the status text.
        /// </summary>
        /// <value>
        /// <see langword="true"/> to hide the percentage in the status text; otherwise, <see langword="false"/>.
        /// </value>
        public bool HidePercentage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_flags & HidePercentageMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _flags |= HidePercentageMask;
                }
                else
                {
                    _flags &= unchecked((byte)~HidePercentageMask);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the status text has shadows.
        /// </summary>
        /// <value><see langword="true"/> if the status text has shadows; otherwise, <see langword="false"/>.</value>
        public bool HasShadows
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (_flags & HasShadowsMask) != 0;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value)
                {
                    _flags |= HasShadowsMask;
                }
                else
                {
                    _flags &= unchecked((byte)~HasShadowsMask);
                }
            }
        }

        PacketId IPacket.Id => PacketId.ClientStatus;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            var index = span.Read(ref this.AsRefByte(0), 4);
            index += span[index..].Read(Encoding.UTF8, out _statusText);
            return index + span[index..].Read(ref this.AsRefByte(4), 1);
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context)
        {
            var index = span.Write(ref this.AsRefByte(0), 4);
            index += span[index..].Write(StatusText, Encoding.UTF8);
            return index + span[index..].Write(ref this.AsRefByte(4), 1);
        }
    }
}
