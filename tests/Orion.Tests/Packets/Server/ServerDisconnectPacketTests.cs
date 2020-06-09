﻿// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using System.Diagnostics.CodeAnalysis;
using Orion.DataStructures;
using Orion.Packets.Server;
using Xunit;

namespace Orion.Packets.Client {
    [SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "Testing")]
    public class ServerDisconnectPacketTests {
        public static readonly byte[] Bytes = {
            21, 0, 2, 2, 15, 67, 76, 73, 46, 75, 105, 99, 107, 77, 101, 115, 115, 97, 103, 101, 0
        };

        [Fact]
        public void Reason_SetNullValue_ThrowsArgumentNullException() {
            var packet = new ServerDisconnectPacket();

            Assert.Throws<ArgumentNullException>(() => packet.Reason = null!);
        }

        [Fact]
        public void Reason_Set_Get() {
            var packet = new ServerDisconnectPacket();

            packet.Reason = NetworkText.Localized("CLI.KickMessage");

            Assert.Equal(NetworkText.Localized("CLI.KickMessage"), packet.Reason);
        }

        [Fact]
        public void Read() {
            var packet = new ServerDisconnectPacket();
            var span = Bytes.AsSpan(IPacket.HeaderSize..);
            Assert.Equal(span.Length, packet.Read(span, PacketContext.Server));

            Assert.Equal(NetworkText.Localized("CLI.KickMessage"), packet.Reason);
        }

        [Fact]
        public void RoundTrip() {
            TestUtils.RoundTripPacket<ServerDisconnectPacket>(Bytes.AsSpan(IPacket.HeaderSize..), PacketContext.Server);
        }
    }
}
