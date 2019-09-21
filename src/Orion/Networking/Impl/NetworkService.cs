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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Entities;
using Orion.Events.Networking;
using Orion.Networking.Packets;
using Orion.Networking.Packets.Entities;

namespace Orion.Networking.Impl {
    internal sealed class NetworkService : OrionService, INetworkService {
        private readonly Lazy<IPlayerService> _playerService;
        private readonly ThreadLocal<bool> _shouldIgnoreNextReceiveData = new ThreadLocal<bool>();

        private readonly IDictionary<PacketType, Action<PacketReceiveEventArgs>> _receiveHandlers =
            new Dictionary<PacketType, Action<PacketReceiveEventArgs>>();

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";
        public EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; set; }
        public EventHandlerCollection<PacketSendEventArgs> PacketSend { get; set; }

        public NetworkService(Lazy<IPlayerService> playerService) {
            _playerService = playerService;

            _receiveHandlers[PacketType.PlayerConnect] = PlayerConnectHandler;

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
            OTAPI.Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            OTAPI.Hooks.Net.ReceiveData = null;
            OTAPI.Hooks.Net.SendBytes = null;
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
            packet = args.Packet;

            if (_receiveHandlers.TryGetValue(packet.Type, out var handler)) {
                handler(args);
                if (args.IsCanceled) return OTAPI.HookResult.Cancel;
                packet = args.Packet;
            }

            if (!args.IsDirty) return OTAPI.HookResult.Continue;

            var oldBuffer = buffer.readBuffer;
            var newStream = new MemoryStream();
            packet.WriteToStream(newStream, PacketContext.Client);

            // Use _shouldIgnoreNextReceiveData so that we don't trigger this handler again, potentially causing a stack
            // overflow error.
            buffer.readBuffer = newStream.ToArray();
            buffer.ResetReader();
            _shouldIgnoreNextReceiveData.Value = true;
            buffer.GetData(2, (int)(newStream.Length - 2), out _);
            _shouldIgnoreNextReceiveData.Value = false;
            buffer.readBuffer = oldBuffer;
            buffer.ResetReader();
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
            if (!args.IsDirty) return OTAPI.HookResult.Continue;

            var newStream = new MemoryStream();
            args.Packet.WriteToStream(newStream, PacketContext.Server);
            data = newStream.ToArray();
            offset = 0;
            size = data.Length;
            return OTAPI.HookResult.Continue;
        }

        private OTAPI.HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            var player = _playerService.Value[remoteClient.Id];
            var args = new PlayerDisconnectEventArgs(player);
            _playerService.Value.PlayerDisconnect?.Invoke(this, args);
            return OTAPI.HookResult.Continue;
        }

        private void PlayerConnectHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerConnectPacket)args_.Packet;
            var args = new PlayerConnectEventArgs(args_.Sender, packet);
            _playerService.Value.PlayerConnect?.Invoke(this, args);
            args_.IsCanceled = args.IsCanceled;
        }
    }
}
