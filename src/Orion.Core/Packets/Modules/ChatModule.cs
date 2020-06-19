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

namespace Orion.Core.Packets.Modules
{
    /// <summary>
    /// A module sent for chat.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct ChatModule : IModule
    {
        [FieldOffset(0)] private string? _clientCommand;
        [FieldOffset(8)] private string? _clientMessage;
        [FieldOffset(24)] private NetworkText? _serverMessage;

        /// <summary>
        /// Gets or sets the command. This is only applicable if read in <see cref="PacketContext.Server"/> or written
        /// in <see cref="PacketContext.Client"/>.
        /// </summary>
        /// <value>The command.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientCommand
        {
            get => _clientCommand ?? string.Empty;
            set => _clientCommand = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the message. This is only applicable if read in <see cref="PacketContext.Server"/> or written
        /// in <see cref="PacketContext.Client"/>.
        /// </summary>
        /// <value>The message.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientMessage
        {
            get => _clientMessage ?? string.Empty;
            set => _clientMessage = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the author player index. This is only applicable if read in <see cref="PacketContext.Client"/>
        /// or written in <see cref="PacketContext.Server"/>.
        /// </summary>
        /// <value>The author player index.</value>
        [field: FieldOffset(16)] public byte ServerAuthorIndex { get; set; }

        /// <summary>
        /// Gets or sets the message. This is only applicable if read in <see cref="PacketContext.Client"/> or written
        /// in <see cref="PacketContext.Server"/>.
        /// </summary>
        /// <value>The message.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkText ServerMessage
        {
            get => _serverMessage ?? NetworkText.Empty;
            set => _serverMessage = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the color. This is only applicable if read in <see cref="PacketContext.Client"/> or written in
        /// <see cref="PacketContext.Server"/>.
        /// </summary>
        /// <value>The color.</value>
        [field: FieldOffset(17)] public Color3 ServerColor { get; set; }

        ModuleId IModule.Id => ModuleId.Chat;

        /// <inheritdoc/>
        public int Read(Span<byte> span, PacketContext context)
        {
            if (context == PacketContext.Server)
            {
                var index = span.Read(Encoding.UTF8, out _clientCommand);
                return index + span[index..].Read(Encoding.UTF8, out _clientMessage);
            }
            else
            {
                var index = span.Read(ref this.AsRefByte(16), 1);
                index += span[index..].Read(Encoding.UTF8, out _serverMessage);
                return index + span[index..].Read(ref this.AsRefByte(17), 3);
            }
        }

        /// <inheritdoc/>
        public int Write(Span<byte> span, PacketContext context)
        {
            if (context == PacketContext.Client)
            {
                var index = span.Write(ClientCommand, Encoding.UTF8);
                return index + span[index..].Write(ClientMessage, Encoding.UTF8);
            }
            else
            {
                var index = span.Write(ref this.AsRefByte(16), 1);
                index += span[index..].Write(ServerMessage, Encoding.UTF8);
                return index + span[index..].Write(ref this.AsRefByte(17), 3);
            }
        }
    }
}
