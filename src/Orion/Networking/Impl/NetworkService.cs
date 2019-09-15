// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.IO;
using System.Threading;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Networking;

namespace Orion.Networking.Impl {
    internal sealed class NetworkService : OrionService, INetworkService {
        private readonly Lazy<IPlayerService> _playerService;
        private readonly ThreadLocal<bool> _shouldIgnoreNextReceiveData = new ThreadLocal<bool>();

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";
        public EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; set; }
        public EventHandlerCollection<PacketSendEventArgs> PacketSend { get; set; }

        public NetworkService(Lazy<IPlayerService> playerService) {
            _playerService = playerService;

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
        }

        private OTAPI.HookResult ReceiveDataHandler(Terraria.MessageBuffer buffer, ref byte packetId,
                                                    ref int readOffset, ref int start, ref int length) {
            if (_shouldIgnoreNextReceiveData.Value) return OTAPI.HookResult.Continue;

            // Offset start and length by two since the packet length field is not included.
            var stream = new MemoryStream(buffer.readBuffer, start - 2, length + 2);
            var sender = _playerService.Value[buffer.whoAmI];
            var packet = Packet.ReadFromStream(stream, PacketContext.Server);
            var args = new PacketReceiveEventArgs(sender, packet);
            PacketReceive?.Invoke(this, args);
            if (args.IsCanceled) return OTAPI.HookResult.Cancel;
            if (!args.IsPacketDirty) return OTAPI.HookResult.Continue;

            // Packets that didn't change in length can be modified very easily.
            if (!args.Packet.DidLengthChange) {
                stream.Position = 0;
                args.Packet.WriteToStream(stream, PacketContext.Client);
                return OTAPI.HookResult.Continue;
            }

            var oldBuffer = buffer.readBuffer;
            var newStream = new MemoryStream();
            args.Packet.WriteToStream(newStream, PacketContext.Client);

            // Use _shouldIgnoreNextReceiveData so that we don't trigger this handler again, potentially causing a stack
            // overflow error.
            _shouldIgnoreNextReceiveData.Value = true;
            buffer.readBuffer = newStream.ToArray();
            buffer.ResetReader();
            buffer.GetData(2, (int)(newStream.Length - 2), out _);
            buffer.readBuffer = oldBuffer;
            buffer.ResetReader();
            _shouldIgnoreNextReceiveData.Value = false;
            return OTAPI.HookResult.Cancel;
        }

        private OTAPI.HookResult SendBytesHandler(ref int remoteClient, ref byte[] data, ref int offset, ref int size,
                                                  ref Terraria.Net.Sockets.SocketSendCallback callback,
                                                  ref object state) {
            var stream = new MemoryStream(data, offset, size);
            var receiver = _playerService.Value[remoteClient];
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);
            var args = new PacketSendEventArgs(receiver, packet);
            PacketSend?.Invoke(this, args);
            if (args.IsCanceled) return OTAPI.HookResult.Cancel;
            if (!args.IsPacketDirty) return OTAPI.HookResult.Continue;

            var newStream = new MemoryStream();
            args.Packet.WriteToStream(newStream, PacketContext.Server);
            data = newStream.ToArray();
            offset = 0;
            size = data.Length;
            return OTAPI.HookResult.Continue;
        }
    }
}
