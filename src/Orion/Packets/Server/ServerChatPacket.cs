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
using Orion.Packets.DataStructures;

namespace Orion.Packets.Server {
    /// <summary>
    /// A packet sent from the server to the client to show chat.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ServerChatPacket : IPacket {
        [FieldOffset(8)]
        private NetworkText _text;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        [field: FieldOffset(0)]
        public Color3 Color { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkText Text {
            get => _text ?? NetworkText.Empty;
            set => _text = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the line width. A value of <c>-1</c> indicates that the screen width should be used.
        /// </summary>
        /// <value>The line width.</value>
        [field: FieldOffset(3)]
        public short LineWidth { get; set; }

        PacketId IPacket.Id => PacketId.ServerChat;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(0), ref span[0], 3);
            var numTextBytes = NetworkText.Read(span[3..], Encoding.UTF8, out _text);
            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(3), ref span[3 + numTextBytes], 2);
            return 3 + numTextBytes + 2;
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref span[0], ref this.AsRefByte(0), 3);
            var numTextBytes = Text.Write(span[3..], Encoding.UTF8);
            Unsafe.CopyBlockUnaligned(ref span[3 + numTextBytes], ref this.AsRefByte(3), 2);
            return 3 + numTextBytes + 2;
        }
    }
}
