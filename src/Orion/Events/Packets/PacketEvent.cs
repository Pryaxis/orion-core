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
using Orion.Packets;

namespace Orion.Events.Packets {
    /// <summary>
    /// Provides the base class for packet-related events.
    /// </summary>
    public abstract unsafe class PacketEvent<TPacket> : Event, IDirtiable where TPacket : struct, IPacket {
        // Store a pointer to the original packet. This is quite unsafe and requires callers to ensure that `TPacket` is
        // stored on the stack. However, this lets us save on a (potentially expensive) struct copy.
        private readonly void* _originalPacketPtr;

        private TPacket _currentPacket;

        /// <summary>
        /// Gets a reference to the packet.
        /// </summary>
        /// <value>A reference to the packet.</value>
        public ref TPacket Packet => ref _currentPacket;

        /// <inheritdoc/>
        public override bool IsDirty {
            get {
                // First, check if the packet is dirty. This is required in case the packet structure itself isn't
                // modified.
                if (_currentPacket.IsDirty) {
                    return true;
                }

                // It's safe to interpret the current packet as a pointer, as `ref` will essentially "pin"
                // `_currentPacket`, in case the GC decides to move around this `PacketEvent<TPacket>` instance.
                var original = new ReadOnlySpan<byte>(_originalPacketPtr, Unsafe.SizeOf<TPacket>());
                var current = new ReadOnlySpan<byte>(Unsafe.AsPointer(ref _currentPacket), Unsafe.SizeOf<TPacket>());
                return !current.SequenceEqual(original);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PacketEvent{TPacket}"/> class with the specified
        /// <paramref name="packet"/> reference.
        /// </summary>
        /// <param name="packet">The packet reference. <b>This must be on the stack!</b></param>
        public PacketEvent(ref TPacket packet) {
            _originalPacketPtr = Unsafe.AsPointer(ref packet);
            _currentPacket = packet;
        }

        /// <inheritdoc/>
        public override void Clean() {
            _currentPacket.Clean();

            var original = new Span<byte>(_originalPacketPtr, Unsafe.SizeOf<TPacket>());
            var current = new Span<byte>(Unsafe.AsPointer(ref _currentPacket), Unsafe.SizeOf<TPacket>());
            current.CopyTo(original);
        }
    }
}
