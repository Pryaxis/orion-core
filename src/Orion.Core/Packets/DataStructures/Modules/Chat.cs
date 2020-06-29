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
using System.Runtime.InteropServices;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures.Modules
{
    /// <summary>
    /// A module sent for chat.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public sealed class Chat : IModule
    {
        [FieldOffset(0)] private string _clientCommand = string.Empty;
        [FieldOffset(8)] private string _clientMessage = string.Empty;
        [FieldOffset(16)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(17)] private byte _bytes2;  // Used to obtain an interior reference.
        [FieldOffset(24)] private NetworkText _serverMessage = NetworkText.Empty;

        /// <summary>
        /// Gets or sets the command. <i>This is only applicable if read in <see cref="PacketContext.Server"/> or
        /// written in <see cref="PacketContext.Client"/>!</i>
        /// </summary>
        /// <value>The command.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientCommand
        {
            get => _clientCommand;
            set => _clientCommand = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the message. <i>This is only applicable if read in <see cref="PacketContext.Server"/> or
        /// written in <see cref="PacketContext.Client"/>!</i>
        /// </summary>
        /// <value>The message.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string ClientMessage
        {
            get => _clientMessage;
            set => _clientMessage = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the author player index. <i>This is only applicable if read in
        /// <see cref="PacketContext.Client"/> or written in <see cref="PacketContext.Server"/>!</i>
        /// </summary>
        /// <value>The author player index.</value>
        [field: FieldOffset(16)] public byte ServerAuthorIndex { get; set; }

        /// <summary>
        /// Gets or sets the message. <i>This is only applicable if read in <see cref="PacketContext.Client"/> or
        /// written in <see cref="PacketContext.Server"/>!</i>
        /// </summary>
        /// <value>The message.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkText ServerMessage
        {
            get => _serverMessage;
            set => _serverMessage = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the color. <i>This is only applicable if read in <see cref="PacketContext.Client"/> or written
        /// in <see cref="PacketContext.Server"/>!</i>
        /// </summary>
        /// <value>The color.</value>
        [field: FieldOffset(17)] public Color3 ServerColor { get; set; }

        ModuleId IModule.Id => ModuleId.Chat;

        int IModule.ReadBody(Span<byte> span, PacketContext context)
        {
            if (context == PacketContext.Server)
            {
                var length = span.Read(out _clientCommand);
                return length + span[length..].Read(out _clientMessage);
            }
            else
            {
                var length = span.Read(ref _bytes, 1);
                length += NetworkText.Read(span[length..], out _serverMessage);
                return length + span[length..].Read(ref _bytes2, 3);
            }
        }

        int IModule.WriteBody(Span<byte> span, PacketContext context)
        {
            if (context == PacketContext.Client)
            {
                var length = span.Write(_clientCommand);
                return length + span[length..].Write(_clientMessage);
            }
            else
            {
                var length = span.Write(ref _bytes, 1);
                length += _serverMessage.Write(span[length..]);
                return length + span[length..].Write(ref _bytes2, 3);
            }
        }
    }
}
