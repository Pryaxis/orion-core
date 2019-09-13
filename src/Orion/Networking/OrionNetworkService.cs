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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Orion.Events.Players;
using Orion.Hooks;
using Orion.Networking.Events;
using Orion.Networking.Packets;
using Orion.Players;
using OTAPI;
using Serilog;
using Terraria;
using Terraria.Localization;
using Terraria.Net.Sockets;

namespace Orion.Networking {
    internal sealed class OrionNetworkService : OrionService, INetworkService {
        private readonly Lazy<IPlayerService> _playerService;

        private readonly IList<RemoteClient> _terrariaClients;
        private readonly IList<OrionClient> _clients;

        [ExcludeFromCodeCoverage] public override string Author => "Pryaxis";

        public int Count => _clients.Count;

        public IClient this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                if (_clients[index]?.Wrapped != _terrariaClients[index]) {
                    _clients[index] = new OrionClient(this, _terrariaClients[index]);
                }

                var client = _clients[index];
                Debug.Assert(client != null, $"{nameof(client)} should not be null.");
                Debug.Assert(client.Wrapped != null, $"{nameof(client.Wrapped)} should not be null.");
                return client;
            }
        }

        public HookHandlerCollection<ReceivedPacketEventArgs> ReceivedPacket { get; set; }
        public HookHandlerCollection<ReceivingPacketEventArgs> ReceivingPacket { get; set; }
        public HookHandlerCollection<SentPacketEventArgs> SentPacket { get; set; }
        public HookHandlerCollection<SendingPacketEventArgs> SendingPacket { get; set; }

        public OrionNetworkService(Lazy<IPlayerService> playerService) {
            _playerService = playerService ?? throw new ArgumentNullException(nameof(playerService));

            _terrariaClients = Netplay.Clients;
            _clients = new OrionClient[_terrariaClients.Count];

            OTAPI.Hooks.Net.ReceiveData = ReceiveDataHandler;
            OTAPI.Hooks.Net.SendBytes = SendBytesHandler;
            OTAPI.Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) return;

            OTAPI.Hooks.Net.ReceiveData = null;
            OTAPI.Hooks.Net.SendBytes = null;
            OTAPI.Hooks.Net.RemoteClient.PreReset = null;
        }

        public IEnumerator<IClient> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void BroadcastPacket(Packet packet, int excludeIndex = -1) {
            if (packet == null) throw new ArgumentNullException(nameof(packet));

            Log.Debug("[Net] Broadcasting {Packet}", packet);

            for (var i = 0; i < Netplay.MaxConnections; ++i) {
                if (i != excludeIndex) {
                    this[i].SendPacket(packet);
                }
            }
        }

        public void BroadcastPacket(PacketType packetType, int excludeIndex = -1, string text = "", int number = 0,
                                    float number2 = 0, float number3 = 0, float number4 = 0, int number5 = 0,
                                    int number6 = 0, int number7 = 0) {
            Log.Debug("[Net] Broadcasting {Packet} (\"{Text}\", {Number1}, {Number2}, {Number3}, {Number4}, " +
                      "{Number5}, {Number6}, {Number7} to {Receiver}", packetType, Name, text, number, number2, number3,
                      number4, number5, number6, number7);

            NetMessage.SendData((int)packetType, -1, excludeIndex,
                                NetworkText.FromLiteral(text), number, number2, number3,
                                number4, number5, number6, number7);
        }


        private HookResult ReceiveDataHandler(MessageBuffer buffer, ref byte packetId,
                                              ref int readOffset, ref int start, ref int length) {
            var data = buffer.readBuffer;
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Netplay.MaxConnections,
                         $"{nameof(buffer.whoAmI)} should be a valid index.");
            Debug.Assert(start >= 2 && start + length <= data.Length,
                         $"{nameof(start)} and {nameof(length)} should be valid indices into {nameof(data)}.");

            // start and length need to be adjusted by two to account for the packet header's length field.
            var stream = new MemoryStream(data, start - 2, length + 2, false);
            var sender = this[buffer.whoAmI];
            var packet = Packet.ReadFromStream(stream, PacketContext.Server);
            var args = new ReceivingPacketEventArgs(sender, packet);
            ReceivingPacket?.Invoke(this, args);
            if (args.Handled) return HookResult.Cancel;

            var args2 = new ReceivedPacketEventArgs(sender, packet);
            ReceivedPacket?.Invoke(this, args2);

            Log.Verbose("[Net] Received {Packet} from {Sender}", packet, sender);

            return HookResult.Continue;
        }

        private HookResult SendBytesHandler(ref int remoteId, ref byte[] data, ref int start, ref int length,
                                            ref SocketSendCallback callback,
                                            ref object state) {
            Debug.Assert(remoteId >= 0 && remoteId < Netplay.MaxConnections,
                         $"{nameof(remoteId)} should be a valid index.");
            Debug.Assert(start >= 0 && start + length <= data.Length,
                         $"{nameof(start)} and {nameof(length)} should be valid indices into {nameof(data)}.");

            var stream = new MemoryStream(data, start, length, false);
            var receiver = this[remoteId];
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);
            var args = new SendingPacketEventArgs(receiver, packet);
            SendingPacket?.Invoke(this, args);
            if (args.Handled) return HookResult.Cancel;

            if (args.IsPacketDirty) {
                packet = args.Packet;

                var stream2 = new MemoryStream();
                packet.WriteToStream(stream2, PacketContext.Server);
                data = stream2.ToArray();
                start = 0;
                length = (int)stream2.Position;
            }

            var args2 = new SentPacketEventArgs(receiver, packet);
            SentPacket?.Invoke(this, args2);

            Log.Verbose("[Net] Sent {Packet} to {Receiver}", packet, receiver);

            return HookResult.Continue;
        }

        private HookResult PreResetHandler(RemoteClient remoteClient) {
            // Ensure that the client was previously active so that we don't get any false positives.
            if (!remoteClient.IsActive) return HookResult.Continue;

            var playerIndex = remoteClient.Id;
            Debug.Assert(playerIndex >= 0 && playerIndex < _playerService.Value.Count,
                         $"{nameof(playerIndex)} should be a valid player index.");
            var player = _playerService.Value[playerIndex];
            var args = new PlayerDisconnectEventArgs(player);
            _playerService.Value.PlayerDisconnect?.Invoke(this, args);

            Log.Information("{Player} disconnected", player);

            return HookResult.Continue;
        }
    }
}
