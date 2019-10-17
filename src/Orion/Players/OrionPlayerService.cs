// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using System.IO;
using System.Threading;
using Orion.Events;
using Orion.Events.Players;
using Orion.Items;
using Orion.Packets;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Orion.Utils;
using OTAPI;
using Serilog;
using Main = Terraria.Main;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    [Service("orion-players")]
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private readonly ThreadLocal<bool> _shouldIgnoreNextReceiveData = new ThreadLocal<bool>();
        private readonly IDictionary<PacketType, Action<PacketReceiveEventArgs>> _packetReceiveHandlers;

        public IReadOnlyArray<IPlayer> Players { get; }
        public EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; }
        public EventHandlerCollection<PacketSendEventArgs> PacketSend { get; }
        public EventHandlerCollection<PlayerConnectEventArgs> PlayerConnect { get; }
        public EventHandlerCollection<PlayerDataEventArgs> PlayerData { get; }
        public EventHandlerCollection<PlayerInventorySlotEventArgs> PlayerInventorySlot { get; }
        public EventHandlerCollection<PlayerJoinEventArgs> PlayerJoin { get; }
        public EventHandlerCollection<PlayerPvpEventArgs> PlayerPvp { get; }
        public EventHandlerCollection<PlayerTeamEventArgs> PlayerTeam { get; }
        public EventHandlerCollection<PlayerChatEventArgs> PlayerChat { get; }
        public EventHandlerCollection<PlayerDisconnectedEventArgs> PlayerDisconnected { get; }

        public OrionPlayerService(ILogger log) : base(log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.player != null, "Terraria players should not be null");

            _packetReceiveHandlers = new Dictionary<PacketType, Action<PacketReceiveEventArgs>> {
                [PacketType.PlayerConnect] = PlayerConnectHandler,
                [PacketType.PlayerData] = PlayerDataHandler,
                [PacketType.PlayerInventorySlot] = PlayerInventorySlotHandler,
                [PacketType.PlayerJoin] = PlayerJoinHandler,
                [PacketType.PlayerPvp] = PlayerPvpHandler,
                [PacketType.PlayerTeam] = PlayerTeamHandler,
                [PacketType.Module] = ModuleHandler
            };

            // Ignore the last player since it is used as a failure slot.
            Players = new WrappedReadOnlyArray<OrionPlayer, TerrariaPlayer>(
                Main.player.AsMemory(..^1),
                (playerIndex, terrariaPlayer) => new OrionPlayer(this, playerIndex, terrariaPlayer));

            PacketReceive = new EventHandlerCollection<PacketReceiveEventArgs>();
            PacketSend = new EventHandlerCollection<PacketSendEventArgs>();
            PlayerConnect = new EventHandlerCollection<PlayerConnectEventArgs>();
            PlayerData = new EventHandlerCollection<PlayerDataEventArgs>();
            PlayerInventorySlot = new EventHandlerCollection<PlayerInventorySlotEventArgs>();
            PlayerJoin = new EventHandlerCollection<PlayerJoinEventArgs>();
            PlayerPvp = new EventHandlerCollection<PlayerPvpEventArgs>();
            PlayerTeam = new EventHandlerCollection<PlayerTeamEventArgs>();
            PlayerChat = new EventHandlerCollection<PlayerChatEventArgs>();
            PlayerDisconnected = new EventHandlerCollection<PlayerDisconnectedEventArgs>();

            PlayerTeam.RegisterHandler((sender, args) => {
                args.PlayerTeam = Orion.Players.PlayerTeam.Pink;
            });

            Hooks.Net.ReceiveData = ReceiveDataHandler;
            Hooks.Net.SendBytes = SendBytesHandler;
            Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        public override void Dispose() {
            _shouldIgnoreNextReceiveData.Dispose();

            Hooks.Net.ReceiveData = null;
            Hooks.Net.SendBytes = null;
            Hooks.Net.RemoteClient.PreReset = null;
        }

        private HookResult ReceiveDataHandler(
                Terraria.MessageBuffer buffer, ref byte packetId, ref int readOffset, ref int start, ref int length) {
            Debug.Assert(buffer != null, "buffer should not be null");
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Players.Count, "buffer should have a valid index");

            if (_shouldIgnoreNextReceiveData.Value) {
                _shouldIgnoreNextReceiveData.Value = false;
                return HookResult.Continue;
            }

            // Offset start and length by two since the packet length field is not included.
            var stream = new MemoryStream(buffer.readBuffer, start - 2, length + 2);
            var sender = Players[buffer.whoAmI];
            var packet = Packet.ReadFromStream(stream, PacketContext.Server);
            var args = new PacketReceiveEventArgs(sender, packet);

            // Not localized because this string is developer-facing.
            Log.Verbose("Invoking {Event} with [{Sender}, {Packet}]", PacketReceive, sender, packet);
            PacketReceive.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {Reason}", PacketReceive, args.CancellationReason);
                return HookResult.Cancel;
            }

            if (_packetReceiveHandlers.TryGetValue(args.Packet.Type, out var handler)) {
                handler(args);
                if (args.IsCanceled()) {
                    return HookResult.Cancel;
                }
            }

            if (!args.IsDirty) {
                return HookResult.Continue;
            }

            var oldBuffer = buffer.readBuffer;
            // TODO: consider reusing buffers here
            var newStream = new MemoryStream();
            args.Packet.WriteToStream(newStream, PacketContext.Client);
            buffer.readBuffer = newStream.ToArray();
            buffer.ResetReader();

            // Ignore the next ReceiveDataHandler call.
            _shouldIgnoreNextReceiveData.Value = true;
            buffer.GetData(2, (int)(newStream.Length - 2), out _);
            buffer.readBuffer = oldBuffer;
            buffer.ResetReader();
            return HookResult.Cancel;
        }

        private HookResult SendBytesHandler(
                ref int remoteClient, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback callback, ref object state) {
            Debug.Assert(remoteClient >= 0 && remoteClient < Players.Count, "remote client should be a valid index");

            var stream = new MemoryStream(data, offset, size);
            var receiver = Players[remoteClient];
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);
            var args = new PacketSendEventArgs(receiver, packet);

            // Not localized because this string is developer-facing.
            Log.Verbose("Invoking {Event} with [{Receiver}, {Packet}]", PacketSend, receiver, packet);
            PacketSend.Invoke(this, args);
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {Reason}", PacketSend, args.CancellationReason);
                return HookResult.Cancel;
            }

            if (!args.IsDirty) {
                return HookResult.Continue;
            }

            // TODO: consider reusing buffers here
            var newStream = new MemoryStream();
            args.Packet.WriteToStream(newStream, PacketContext.Server);
            data = newStream.ToArray();
            offset = 0;
            size = data.Length;
            return HookResult.Continue;
        }

        private HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            Debug.Assert(remoteClient != null, "remote client should not be null");
            Debug.Assert(remoteClient.Id >= 0 && remoteClient.Id < Players.Count,
                "remote client should have a valid index");

            // Check if the client was active, since this gets called when setting up RemoteClient as well.
            if (!remoteClient.IsActive) {
                return HookResult.Continue;
            }

            var player = Players[remoteClient.Id];
            var args = new PlayerDisconnectedEventArgs(player);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player}]", PlayerDisconnected, player);
            PlayerDisconnected.Invoke(this, args);
            return HookResult.Continue;
        }

        private void PlayerConnectHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerConnectPacket)args_.Packet;
            var args = new PlayerConnectEventArgs(args_.Sender, packet);

            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [#={PlayerIndex}, {PlayerVersionString}]",
                PlayerConnect, args.Player.Index, args.PlayerVersionString);
            PlayerConnect.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;

            if (args_.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerConnect, args.CancellationReason);
            } else if (args_.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [#={PlayerIndex}, {PlayerVersionString}]",
                    PlayerConnect, args.Player.Index, args.PlayerVersionString);
            }
        }

        private void PlayerDataHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerDataPacket)args_.Packet;
            var args = new PlayerDataEventArgs(args_.Sender, packet);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{PlayerName}, ...]", PlayerData, args.PlayerName);
            PlayerData.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;

            if (args_.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerData, args.CancellationReason);
            } else if (args_.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug("Altered {Event} to [{PlayerName}, ...]", PlayerData, args.PlayerName);
            }
        }

        private void PlayerInventorySlotHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerInventorySlotPacket)args_.Packet;
            var args = new PlayerInventorySlotEventArgs(args_.Sender, packet);

            if (args.ItemType == ItemType.None) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Player}, {PlayerInventorySlotIndex} = {ItemType}]",
                    PlayerInventorySlot, args.Player, args.PlayerInventorySlotIndex, args.ItemType);
            } else if (args.ItemPrefix == ItemPrefix.None) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Player}, {PlayerInventorySlotIndex} = {ItemType} x{ItemStackSize}]",
                    PlayerInventorySlot, args.Player, args.PlayerInventorySlotIndex, args.ItemType, args.ItemStackSize);
            } else {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Player}, {PlayerInventorySlotIndex} = {ItemPrefix} {ItemType}]",
                    PlayerInventorySlot, args.Player, args.PlayerInventorySlotIndex, args.ItemPrefix, args.ItemType);
            }

            PlayerInventorySlot.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;

            if (args_.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerInventorySlot, args.CancellationReason);
            } else if (args_.IsDirty) {
                if (args.ItemType == ItemType.None) {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Player}, {PlayerInventorySlotIndex} = {ItemType}]",
                        PlayerInventorySlot, args.Player, args.PlayerInventorySlotIndex, args.ItemType);
                } else if (args.ItemPrefix == ItemPrefix.None) {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Player}, {PlayerInventorySlotIndex} = {ItemType} x{ItemStackSize}]",
                        PlayerInventorySlot, args.Player, args.PlayerInventorySlotIndex, args.ItemType,
                        args.ItemStackSize);
                } else {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Player}, {PlayerInventorySlotIndex} = {ItemPrefix} {ItemType}]",
                        PlayerInventorySlot, args.Player, args.PlayerInventorySlotIndex, args.ItemPrefix,
                        args.ItemType);
                }
            }
        }

        private void PlayerJoinHandler(PacketReceiveEventArgs args_) {
            var args = new PlayerJoinEventArgs(args_.Sender);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player}]", PlayerJoin, args.Player);
            PlayerJoin.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;

            if (args_.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerJoin, args.CancellationReason);
            }
        }

        private void PlayerPvpHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerPvpPacket)args_.Packet;
            var args = new PlayerPvpEventArgs(args_.Sender, packet);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player}, {IsPlayerInPvp}]", PlayerPvp, args.Player, args.IsPlayerInPvp);
            PlayerPvp.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;

            if (args_.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerPvp, args.CancellationReason);
            } else if (args_.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug("Altered {Event} to [{Player}, {PlayerTeam}]", PlayerPvp, args.Player, args.IsPlayerInPvp);
            }
        }

        private void PlayerTeamHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerTeamPacket)args_.Packet;
            var args = new PlayerTeamEventArgs(args_.Sender, packet);

            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player}, {PlayerTeam}]", PlayerTeam, args.Player, args.PlayerTeam);
            PlayerTeam.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;

            if (args_.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerTeam, args.CancellationReason);
            } else if (args_.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug("Altered {Event} to [{Player}, {PlayerTeam}]", PlayerTeam, args.Player, args.PlayerTeam);
            }
        }

        private void ModuleHandler(PacketReceiveEventArgs args_) {
            var module = ((ModulePacket)args_.Packet).Module;
            if (module is ChatModule chatModule) {
                var args = new PlayerChatEventArgs(args_.Sender, chatModule);

                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Player}, {PlayerChatCommand}, {PlayerChatText}]",
                    PlayerChat, args.Player, args.PlayerChatCommand, args.PlayerChatText);
                PlayerChat.Invoke(this, args);
                args_.CancellationReason = args.CancellationReason;

                if (args_.IsCanceled()) {
                    // Not localized because this string is developer-facing.
                    Log.Debug("Canceled {Event} for {Reason}", PlayerChat, args.CancellationReason);
                } else if (args_.IsDirty) {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Player}, {PlayerChatCommand}, {PlayerChatText}]",
                        PlayerChat, args.Player, args.PlayerChatCommand, args.PlayerChatText);
                }
            }
        }
    }
}
