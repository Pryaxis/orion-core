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

namespace Orion.Packets.Modules {
    /// <summary>
    /// Module sent for chat.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ChatModule : IModule {
        [FieldOffset(0)] private string? _clientCommand;
        [FieldOffset(8)] private string? _clientText;
        [FieldOffset(24)] private NetworkText? _serverText;

        /// <summary>
        /// Gets or sets the command. This is only applicable if read in <see cref="PacketContext.Server"/> or written
        /// in <see cref="PacketContext.Client"/>.
        /// </summary>
        /// <value>The command.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientCommand {
            get => _clientCommand ?? string.Empty;
            set => _clientCommand = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the text. This is only applicable if read in <see cref="PacketContext.Server"/> or written in
        /// <see cref="PacketContext.Client"/>.
        /// </summary>
        /// <value>The text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientText {
            get => _clientText ?? string.Empty;
            set => _clientText = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the chatter player index. This is only applicable if read in <see cref="PacketContext.Client"/>
        /// or written in <see cref="PacketContext.Server"/>.
        /// </summary>
        /// <value>The chatter player index.</value>
        [field: FieldOffset(16)] public byte ServerChatterIndex { get; set; }

        /// <summary>
        /// Gets or sets the text. This is only applicable if read in <see cref="PacketContext.Client"/> or written in
        /// <see cref="PacketContext.Server"/>.
        /// </summary>
        /// <value>The text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkText ServerText {
            get => _serverText ?? NetworkText.Empty;
            set => _serverText = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the color. This is only applicable if read in <see cref="PacketContext.Client"/> or written in
        /// <see cref="PacketContext.Server"/>.
        /// </summary>
        /// <value>The color.</value>
        [field: FieldOffset(17)] public Color3 ServerColor { get; set; }

        ModuleId IModule.Id => ModuleId.Chat;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context) {
            if (context == PacketContext.Server) {
                var numCommandBytes = span.Read(Encoding.UTF8, out _clientCommand);
                var numTextBytes = span[numCommandBytes..].Read(Encoding.UTF8, out _clientText);
                return numCommandBytes + numTextBytes;
            } else {
                ServerChatterIndex = span[0];
                var numTextBytes = span[1..].Read(Encoding.UTF8, out _serverText);
                Unsafe.CopyBlockUnaligned(ref this.AsRefByte(17), ref span[1 + numTextBytes], 3);
                return 1 + numTextBytes + 3;
            }
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context) {
            if (context == PacketContext.Client) {
                var numCommandBytes = span.Write(ClientCommand, Encoding.UTF8);
                var numTextBytes = span[numCommandBytes..].Write(ClientText, Encoding.UTF8);
                return numCommandBytes + numTextBytes;
            } else {
                span[0] = ServerChatterIndex;
                var numTextBytes = span[1..].Write(ServerText, Encoding.UTF8);
                Unsafe.CopyBlockUnaligned(ref span[1 + numTextBytes], ref this.AsRefByte(17), 3);
                return 1 + numTextBytes + 3;
            }
        }
    }
}
