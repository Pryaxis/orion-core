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
using System.Linq;
using System.Threading;
using JetBrains.Annotations;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.Players;
using Orion.Packets;
using Orion.Packets.Players;
using OTAPI;

namespace Orion.Players {
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        [NotNull, ItemNotNull] private readonly IList<Terraria.Player> _terrariaPlayers;
        [NotNull, ItemCanBeNull] private readonly IList<OrionPlayer> _players;
        [NotNull] private readonly ThreadLocal<bool> _shouldIgnoreNextReceiveData = new ThreadLocal<bool>();

        [NotNull] private readonly IDictionary<PacketType, Action<PacketReceiveEventArgs>> _packetReceiveHandlers =
            new Dictionary<PacketType, Action<PacketReceiveEventArgs>>();

        // Subtract 1 from the count. This is because Terraria has an extra slot.
        public int Count => _players.Count - 1;

        [NotNull]
        public IPlayer this[int index] {
            get {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                if (_players[index]?.Wrapped != _terrariaPlayers[index]) {
                    _players[index] = new OrionPlayer(_terrariaPlayers[index]);
                }

                Debug.Assert(_players[index] != null, "_players[index] != null");
                return _players[index];
            }
        }

        public EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; set; }
        public EventHandlerCollection<PacketSendEventArgs> PacketSend { get; set; }
        public EventHandlerCollection<PlayerConnectEventArgs> PlayerConnect { get; set; }
        public EventHandlerCollection<PlayerDataEventArgs> PlayerData { get; set; }
        public EventHandlerCollection<PlayerInventorySlotEventArgs> PlayerInventorySlot { get; set; }
        public EventHandlerCollection<PlayerJoinEventArgs> PlayerJoin { get; set; }
        public EventHandlerCollection<PlayerDisconnectedEventArgs> PlayerDisconnected { get; set; }

        public OrionPlayerService() {
            Debug.Assert(Terraria.Main.player != null, "Terraria.Main.player != null");
            Debug.Assert(Terraria.Main.player.All(p => p != null), "Terraria.Main.player.All(p => p != null)");

            _terrariaPlayers = Terraria.Main.player;
            _players = new OrionPlayer[_terrariaPlayers.Count];

            _packetReceiveHandlers[PacketType.PlayerConnect] = PlayerConnectHandler;
            _packetReceiveHandlers[PacketType.PlayerData] = PlayerDataHandler;
            _packetReceiveHandlers[PacketType.PlayerInventorySlot] = PlayerInventorySlotHandler;
            _packetReceiveHandlers[PacketType.PlayerJoin] = PlayerJoinHandler;

            Hooks.Net.ReceiveData = ReceiveDataHandler;
            Hooks.Net.SendBytes = SendBytesHandler;
            Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        public IEnumerator<IPlayer> GetEnumerator() {
            for (var i = 0; i < Count; ++i) {
                yield return this[i];
            }
        }

        [ExcludeFromCodeCoverage]
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void BroadcastPacket(Packet packet) {
            throw new NotImplementedException();
        }

        private HookResult ReceiveDataHandler([NotNull] Terraria.MessageBuffer buffer, ref byte packetId,
                                              ref int readOffset, ref int start, ref int length) {
            if (_shouldIgnoreNextReceiveData.Value) return HookResult.Continue;

            // Offset start and length by two since the packet length field is not included.
            var stream = new MemoryStream(buffer.readBuffer, start - 2, length + 2);
            var sender = this[buffer.whoAmI];
            var packet = Packet.ReadFromStream(stream, PacketContext.Server);
            var args = new PacketReceiveEventArgs(sender, packet);
            PacketReceive?.Invoke(this, args);
            if (args.IsCanceled) return HookResult.Cancel;
            packet = args.Packet;

            if (_packetReceiveHandlers.TryGetValue(packet.Type, out var handler)) {
                handler(args);
                if (args.IsCanceled) return HookResult.Cancel;
                packet = args.Packet;
            }

            if (!args.IsDirty) return HookResult.Continue;

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
            return HookResult.Cancel;
        }

        private HookResult SendBytesHandler(ref int remoteClient, ref byte[] data, ref int offset, ref int size,
                                            [CanBeNull] ref Terraria.Net.Sockets.SocketSendCallback callback,
                                            [CanBeNull] ref object state) {
            var stream = new MemoryStream(data, offset, size);
            var receiver = this[remoteClient];
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);
            var args = new PacketSendEventArgs(receiver, packet);
            PacketSend?.Invoke(this, args);
            if (args.IsCanceled) return HookResult.Cancel;
            if (!args.IsDirty) return HookResult.Continue;

            var newStream = new MemoryStream();
            args.Packet.WriteToStream(newStream, PacketContext.Server);
            data = newStream.ToArray();
            offset = 0;
            size = data.Length;
            return HookResult.Continue;
        }

        private HookResult PreResetHandler([NotNull] Terraria.RemoteClient remoteClient) {
            var player = this[remoteClient.Id];
            var args = new PlayerDisconnectedEventArgs(player);
            PlayerDisconnected?.Invoke(this, args);
            return HookResult.Continue;
        }

        private void PlayerConnectHandler([NotNull] PacketReceiveEventArgs args_) {
            var packet = (PlayerConnectPacket)args_.Packet;
            var args = new PlayerConnectEventArgs(args_.Sender, packet);
            PlayerConnect?.Invoke(this, args);
            args_.IsCanceled = args.IsCanceled;
        }

        private void PlayerDataHandler([NotNull] PacketReceiveEventArgs args_) {
            var packet = (PlayerDataPacket)args_.Packet;
            var args = new PlayerDataEventArgs(args_.Sender, packet);
            PlayerData?.Invoke(this, args);
            args_.IsCanceled = args.IsCanceled;
        }

        private void PlayerInventorySlotHandler([NotNull] PacketReceiveEventArgs args_) {
            var packet = (PlayerInventorySlotPacket)args_.Packet;
            var args = new PlayerInventorySlotEventArgs(args_.Sender, packet);
            PlayerInventorySlot?.Invoke(this, args);
            args_.IsCanceled = args.IsCanceled;
        }

        private void PlayerJoinHandler([NotNull] PacketReceiveEventArgs args_) {
            var args = new PlayerJoinEventArgs(args_.Sender);
            PlayerJoin?.Invoke(this, args);
            args_.IsCanceled = args.IsCanceled;
        }
    }
}
