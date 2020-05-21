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
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Packets;
using Orion.Packets.Server;
using Serilog;

namespace Orion.Players {
    [Service("orion-players")]
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private delegate OTAPI.HookResult ReceivePacketHandler(ReadOnlySpan<byte> memory);

        private static readonly MethodInfo ReceivePacketMethod =
            typeof(OrionPlayerService).GetMethod(nameof(ReceivePacket), BindingFlags.NonPublic | BindingFlags.Instance);

        private static readonly IDictionary<byte, ReceivePacketHandler> ReceivePacketHandlers =
            new Dictionary<byte, ReceivePacketHandler>();

        private static readonly IDictionary<byte, Type> PacketIdToType = new Dictionary<byte, Type> {
            [1] = typeof(ServerConnectPacket)
        };

        public OrionPlayerService(OrionKernel kernel, ILogger log) : base(kernel, log) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
        }

        public override void Dispose() {
            OTAPI.Hooks.Net.ReceiveData = null;
        }

        private OTAPI.HookResult ReceiveDataHandler(
                Terraria.MessageBuffer buffer, ref byte packetId, ref int readOffset, ref int start, ref int length) {
            Debug.Assert(buffer != null);

            if (!ReceivePacketHandlers.TryGetValue(packetId, out var receivePacketHandler)) {
                if (!PacketIdToType.TryGetValue(packetId, out var packetType)) {
                    packetType = typeof(UnknownPacket);
                }

                var method = ReceivePacketMethod.MakeGenericMethod(packetType);
                receivePacketHandler = ReceivePacketHandlers[packetId] =
                    (ReceivePacketHandler)method.CreateDelegate(typeof(ReceivePacketHandler), this);
            }

            return receivePacketHandler(buffer.readBuffer.AsSpan((start + 1)..(start + length)));
        }

        private OTAPI.HookResult ReceivePacket<TPacket>(ReadOnlySpan<byte> span) where TPacket : struct, IPacket {
            var packet = new TPacket();
            packet.Read(span, PacketContext.Server);

            var evt = new PacketReceiveEvent<TPacket>(ref packet, null!);
            Kernel.Raise(evt, Log);

            if (evt.IsCanceled()) {
                return OTAPI.HookResult.Cancel;
            }

            if (evt.IsDirty) {

            }

            return OTAPI.HookResult.Cancel;
        }
    }
}
