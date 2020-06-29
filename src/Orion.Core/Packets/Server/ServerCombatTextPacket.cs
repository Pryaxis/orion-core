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
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Server
{
    /// <summary>
    /// A packet sent from the server to the client to show combat text.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 24)]
    public sealed class ServerCombatTextPacket : IPacket
    {
        [FieldOffset(0)] private byte _bytes;
        [field: FieldOffset(16)] private NetworkText _text = NetworkText.Empty;

        /// <summary>
        /// Gets or sets the combat text's position.
        /// </summary>
        /// <value>The combat text's position.</value>
        [field: FieldOffset(0)] public Vector2f Position { get; set; }

        /// <summary>
        /// Gets or sets the combat text's color.
        /// </summary>
        /// <value>The combat text's color.</value>
        [field: FieldOffset(8)] public Color3 Color { get; set; }

        /// <summary>
        /// Gets or sets the combat text.
        /// </summary>
        /// <value>The combat text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public NetworkText Text
        {
            get => _text ?? NetworkText.Empty;
            set => _text = value ?? throw new ArgumentNullException(nameof(value));
        }

        PacketId IPacket.Id => PacketId.ServerCombatText;

        int IPacket.ReadBody(Span<byte> span, PacketContext context)
        {
            var length = span.Read(ref _bytes, 11);
            return length + NetworkText.Read(span[length..], out _text);
        }

        int IPacket.WriteBody(Span<byte> span, PacketContext context)
        {
            var length = span.Write(ref _bytes, 11);
            return length + Text.Write(span[length..]);
        }
    }
}
