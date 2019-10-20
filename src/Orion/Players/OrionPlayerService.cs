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
using System.Diagnostics.CodeAnalysis;
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
        public EventHandlerCollection<PlayerSpawnEventArgs> PlayerSpawn { get; }
        public EventHandlerCollection<PlayerInfoEventArgs> PlayerInfo { get; }
        public EventHandlerCollection<PlayerHealthEventArgs> PlayerHealth { get; }
        public EventHandlerCollection<PlayerPvpEventArgs> PlayerPvp { get; }
        public EventHandlerCollection<PlayerHealEffectEventArgs> PlayerHealEffect { get; }
        public EventHandlerCollection<PlayerPasswordResponseEventArgs> PlayerPasswordResponse { get; }
        public EventHandlerCollection<PlayerManaEventArgs> PlayerMana { get; }
        public EventHandlerCollection<PlayerTeamEventArgs> PlayerTeam { get; }
        public EventHandlerCollection<PlayerUuidEventArgs> PlayerUuid { get; }
        public EventHandlerCollection<PlayerTeleportationPotionEventArgs> PlayerTeleportationPotion { get; }
        public EventHandlerCollection<PlayerAnglerQuestsEventArgs> PlayerAnglerQuests { get; }
        public EventHandlerCollection<PlayerChatEventArgs> PlayerChat { get; }
        public EventHandlerCollection<PlayerQuitEventArgs> PlayerQuit { get; }

        public OrionPlayerService(ILogger log) : base(log) {
            Debug.Assert(log != null, "log should not be null");
            Debug.Assert(Main.player != null, "Terraria players should not be null");

            _packetReceiveHandlers = new Dictionary<PacketType, Action<PacketReceiveEventArgs>> {
                [PacketType.PlayerConnect] = PlayerConnectHandler,
                [PacketType.PlayerData] = PlayerDataHandler,
                [PacketType.PlayerInventorySlot] = PlayerInventorySlotHandler,
                [PacketType.PlayerJoin] = PlayerJoinHandler,
                [PacketType.PlayerSpawn] = PlayerSpawnHandler,
                [PacketType.PlayerInfo] = PlayerInfoHandler,
                [PacketType.PlayerHealth] = PlayerHealthHandler,
                [PacketType.PlayerPvp] = PlayerPvpHandler,
                [PacketType.PlayerHealEffect] = PlayerHealEffectHandler,
                [PacketType.PlayerPasswordResponse] = PlayerPasswordResponseHandler,
                [PacketType.PlayerMana] = PlayerManaHandler,
                [PacketType.PlayerTeam] = PlayerTeamHandler,
                [PacketType.PlayerUuid] = PlayerUuidHandler,
                [PacketType.PlayerTeleportationPotion] = PlayerTeleportationPotionHandler,
                [PacketType.PlayerAnglerQuests] = PlayerAnglerQuestsHandler,
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
            PlayerSpawn = new EventHandlerCollection<PlayerSpawnEventArgs>();
            PlayerInfo = new EventHandlerCollection<PlayerInfoEventArgs>();
            PlayerHealth = new EventHandlerCollection<PlayerHealthEventArgs>();
            PlayerPvp = new EventHandlerCollection<PlayerPvpEventArgs>();
            PlayerHealEffect = new EventHandlerCollection<PlayerHealEffectEventArgs>();
            PlayerPasswordResponse = new EventHandlerCollection<PlayerPasswordResponseEventArgs>();
            PlayerMana = new EventHandlerCollection<PlayerManaEventArgs>();
            PlayerTeam = new EventHandlerCollection<PlayerTeamEventArgs>();
            PlayerUuid = new EventHandlerCollection<PlayerUuidEventArgs>();
            PlayerTeleportationPotion = new EventHandlerCollection<PlayerTeleportationPotionEventArgs>();
            PlayerAnglerQuests = new EventHandlerCollection<PlayerAnglerQuestsEventArgs>();
            PlayerChat = new EventHandlerCollection<PlayerChatEventArgs>();
            PlayerQuit = new EventHandlerCollection<PlayerQuitEventArgs>();

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

        // =============================================================================================================
        // Handling PacketReceive
        // =============================================================================================================

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

            LogPacketReceive_Before(args);
            PacketReceive.Invoke(this, args);
            LogPacketReceive_After(args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            } else if (_packetReceiveHandlers.TryGetValue(packet.Type, out var handler)) {
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
            packet.WriteToStream(newStream, PacketContext.Client);
            buffer.readBuffer = newStream.ToArray();
            buffer.ResetReader();

            // Ignore the next ReceiveDataHandler call.
            _shouldIgnoreNextReceiveData.Value = true;
            buffer.GetData(2, (int)(newStream.Length - 2), out _);
            buffer.readBuffer = oldBuffer;
            buffer.ResetReader();
            return HookResult.Cancel;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPacketReceive_Before(PacketReceiveEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Verbose(
                "Invoking {Event} with [receiving {@Packet} from {Sender}]",
                PacketReceive, args.Packet, args.Sender);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPacketReceive_After(PacketReceiveEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {Reason}", PacketReceive, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Verbose(
                    "Altered {Event} to [receiving {@Packet} from {Sender}]",
                    PacketReceive, args.Packet, args.Sender);
            }
        }

        // =============================================================================================================
        // Handling PacketSend
        // =============================================================================================================

        private HookResult SendBytesHandler(
                ref int remoteClient, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback callback, ref object state) {
            Debug.Assert(remoteClient >= 0 && remoteClient < Players.Count, "remote client should be a valid index");

            var stream = new MemoryStream(data, offset, size);
            var receiver = Players[remoteClient];
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);
            var args = new PacketSendEventArgs(receiver, packet);

            LogPacketSend_Before(args);
            PacketSend.Invoke(this, args);
            LogPacketSend_After(args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            } else if (!args.IsDirty) {
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
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPacketSend_Before(PacketSendEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Verbose(
                "Invoking {Event} with [sending {Packet} to {Receiver}]",
                PacketSend, args.Packet, args.Receiver);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPacketSend_After(PacketSendEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Verbose("Canceled {Event} for {Reason}", PacketSend, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Verbose(
                    "Alterd {Event} to [sending {Packet} to {Receiver}]",
                    PacketSend, args.Packet, args.Receiver);
            }
        }

        // =============================================================================================================
        // Handling PlayerQuit
        // =============================================================================================================

        private HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            Debug.Assert(remoteClient != null, "remote client should not be null");
            Debug.Assert(remoteClient.Id >= 0 && remoteClient.Id < Players.Count,
                "remote client should have a valid index");

            // Check if the client was active since this gets called when setting up RemoteClient as well.
            if (!remoteClient.IsActive) {
                return HookResult.Continue;
            }

            var player = Players[remoteClient.Id];
            var args = new PlayerQuitEventArgs(player);

            LogPlayerQuit(args);
            PlayerQuit.Invoke(this, args);
            return HookResult.Continue;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerQuit(PlayerQuitEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player}]", PlayerQuit, args.Player);
        }

        // =============================================================================================================
        // Handling PlayerConnect
        // =============================================================================================================

        private void PlayerConnectHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerConnectPacket)args_.Packet;
            var args = new PlayerConnectEventArgs(args_.Sender, packet);

            LogPlayerConnect_Before(args);
            PlayerConnect.Invoke(this, args);
            LogPlayerConnect_After(args);

            args_.CancellationReason = args.CancellationReason;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerConnect_Before(PlayerConnectEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [#={PlayerIndex} connecting with {VersionString}]",
                PlayerConnect, args.Player.Index, args.VersionString);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerConnect_After(PlayerConnectEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerConnect, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [#={PlayerIndex} connecting with {VersionString}]",
                    PlayerConnect, args.Player.Index, args.VersionString);
            }
        }

        // =============================================================================================================
        // Handling PlayerData
        // =============================================================================================================

        private void PlayerDataHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerDataPacket)args_.Packet;
            var args = new PlayerDataEventArgs(args_.Sender, packet);

            LogPlayerData_Before(args);
            PlayerData.Invoke(this, args);
            LogPlayerData_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerData_Before(PlayerDataEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{PlayerName}, ...]", PlayerData, args.Name);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerData_After(PlayerDataEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerData, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug("Altered {Event} to [{PlayerName}, ...]", PlayerData, args.Name);
            }
        }

        // =============================================================================================================
        // Handling PlayerInventorySlot
        // =============================================================================================================

        private void PlayerInventorySlotHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerInventorySlotPacket)args_.Packet;
            var args = new PlayerInventorySlotEventArgs(args_.Sender, packet);

            LogPlayerInventorySlot_Before(args);
            PlayerInventorySlot.Invoke(this, args);
            LogPlayerInventorySlot_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerInventorySlot_Before(PlayerInventorySlotEventArgs args) {
            if (args.ItemType == ItemType.None) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Player} has {PlayerInventorySlotIndex} = {ItemType}]",
                    PlayerInventorySlot, args.Player, args.InventorySlot, args.ItemType);
            } else if (args.ItemPrefix == ItemPrefix.None) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Player} has {PlayerInventorySlotIndex} = {ItemType} x{ItemStackSize}]",
                    PlayerInventorySlot, args.Player, args.InventorySlot, args.ItemType, args.StackSize);
            } else {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Invoking {Event} with [{Player} has {PlayerInventorySlotIndex} = {ItemPrefix} {ItemType}]",
                    PlayerInventorySlot, args.Player, args.InventorySlot, args.ItemPrefix, args.ItemType);
            }
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerInventorySlot_After(PlayerInventorySlotEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerInventorySlot, args.CancellationReason);
            } else if (args.IsDirty) {
                if (args.ItemType == ItemType.None) {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Player} has {PlayerInventorySlotIndex} = {ItemType}]",
                        PlayerInventorySlot, args.Player, args.InventorySlot, args.ItemType);
                } else if (args.ItemPrefix == ItemPrefix.None) {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Player} has {PlayerInventorySlotIndex} = {ItemType} x{ItemStackSize}]",
                        PlayerInventorySlot, args.Player, args.InventorySlot, args.ItemType,
                        args.StackSize);
                } else {
                    // Not localized because this string is developer-facing.
                    Log.Debug(
                        "Altered {Event} to [{Player} has {PlayerInventorySlotIndex} = {ItemPrefix} {ItemType}]",
                        PlayerInventorySlot, args.Player, args.InventorySlot, args.ItemPrefix,
                        args.ItemType);
                }
            }
        }

        // =============================================================================================================
        // Handling PlayerJoin
        // =============================================================================================================

        private void PlayerJoinHandler(PacketReceiveEventArgs args_) {
            var args = new PlayerJoinEventArgs(args_.Sender);

            PlayerJoin_Before(args);
            PlayerJoin.Invoke(this, args);
            PlayerJoin_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerJoin_Before(PlayerJoinEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player}]", PlayerJoin, args.Player);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerJoin_After(PlayerJoinEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerJoin, args.CancellationReason);
            }
        }

        // =============================================================================================================
        // Handling PlayerSpawn
        // =============================================================================================================

        private void PlayerSpawnHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerSpawnPacket)args_.Packet;
            var args = new PlayerSpawnEventArgs(args_.Sender, packet);

            PlayerSpawn_Before(args);
            PlayerSpawn.Invoke(this, args);
            PlayerSpawn_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerSpawn_Before(PlayerSpawnEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} at {PlayerSpawnX}, {PlayerSpawnY}]",
                PlayerSpawn, args.Player, args.X, args.Y);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerSpawn_After(PlayerSpawnEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerSpawn, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} at {PlayerSpawnX}, {PlayerSpawnY}]",
                    PlayerSpawn, args.Player, args.X, args.Y);
            }
        }

        // =============================================================================================================
        // Handling PlayerInfo
        // =============================================================================================================

        private void PlayerInfoHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerInfoPacket)args_.Packet;
            var args = new PlayerInfoEventArgs(args_.Sender, packet);

            LogPlayerInfo_Before(args);
            PlayerInfo.Invoke(this, args);
            LogPlayerInfo_After(args);

            args_.CancellationReason = args.CancellationReason;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerInfo_Before(PlayerInfoEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} at {PlayerPosition}, ...]",
                PlayerInfo, args.Player, args.Position);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerInfo_After(PlayerInfoEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerInfo, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} at {PlayerPosition}, ...]",
                    PlayerInfo, args.Player, args.Position);
            }
        }

        // =============================================================================================================
        // Handling PlayerHealth
        // =============================================================================================================

        private void PlayerHealthHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerHealthPacket)args_.Packet;
            var args = new PlayerHealthEventArgs(args_.Sender, packet);

            LogPlayerHealth_Before(args);
            PlayerHealth.Invoke(this, args);
            LogPlayerHealth_After(args);

            args_.CancellationReason = args.CancellationReason;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerHealth_Before(PlayerHealthEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} has {PlayerHealth}/{PlayerMaxHealth} hp]",
                PlayerHealth, args.Player, args.Health, args.MaxHealth);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerHealth_After(PlayerHealthEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerHealth, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} has {PlayerHealth}/{PlayerMaxHealth} hp]",
                    PlayerHealth, args.Player, args.Health, args.MaxHealth);
            }
        }

        // =============================================================================================================
        // Handling PlayerPvp
        // =============================================================================================================

        private void PlayerPvpHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerPvpPacket)args_.Packet;
            var args = new PlayerPvpEventArgs(args_.Sender, packet);

            LogPlayerPvp_Before(args);
            PlayerPvp.Invoke(this, args);
            LogPlayerPvp_After(args);

            args_.CancellationReason = args.CancellationReason;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerPvp_Before(PlayerPvpEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} is in pvp: {IsPlayerInPvp}]",
                PlayerPvp, args.Player, args.IsInPvp);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerPvp_After(PlayerPvpEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerPvp, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} is in pvp: {IsPlayerInPvp}]",
                    PlayerPvp, args.Player, args.IsInPvp);
            }
        }

        // =============================================================================================================
        // Handling PlayerHealEffect
        // =============================================================================================================

        private void PlayerHealEffectHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerHealEffectPacket)args_.Packet;
            var args = new PlayerHealEffectEventArgs(args_.Sender, packet);

            PlayerHealEffect_Before(args);
            PlayerHealEffect.Invoke(this, args);
            PlayerHealEffect_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerHealEffect_Before(PlayerHealEffectEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} for {HealAmount}]",
                PlayerHealEffect, args.Player, args.HealAmount);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerHealEffect_After(PlayerHealEffectEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerHealEffect, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} for {HealAmount}]",
                    PlayerHealEffect, args.Player, args.HealAmount);
            }
        }

        // =============================================================================================================
        // Handling PlayerPasswordResponse
        // =============================================================================================================

        private void PlayerPasswordResponseHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerPasswordResponsePacket)args_.Packet;
            var args = new PlayerPasswordResponseEventArgs(args_.Sender, packet);

            LogPlayerPasswordResponse_Before(args);
            PlayerPasswordResponse.Invoke(this, args);
            LogPlayerPasswordResponse_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerPasswordResponse_Before(PlayerPasswordResponseEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} trying {PasswordAttempt}]",
                PlayerPasswordResponse, args.Player, args.PasswordAttempt);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerPasswordResponse_After(PlayerPasswordResponseEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerPasswordResponse, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} trying {PasswordAttempt}]",
                    PlayerPasswordResponse, args.Player, args.PasswordAttempt);
            }
        }

        // =============================================================================================================
        // Handling PlayerMana
        // =============================================================================================================

        private void PlayerManaHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerManaPacket)args_.Packet;
            var args = new PlayerManaEventArgs(args_.Sender, packet);

            LogPlayerMana_Before(args);
            PlayerMana.Invoke(this, args);
            LogPlayerMana_After(args);

            args_.CancellationReason = args.CancellationReason;
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerMana_Before(PlayerManaEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} has {PlayerMana}/{PlayerMaxMana} mp]",
                PlayerMana, args.Player, args.Mana, args.MaxMana);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerMana_After(PlayerManaEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerMana, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} has {PlayerMana}/{PlayerMaxMana} mp]",
                    PlayerMana, args.Player, args.Mana, args.MaxMana);
            }
        }

        // =============================================================================================================
        // Handling PlayerTeam
        // =============================================================================================================

        private void PlayerTeamHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerTeamPacket)args_.Packet;
            var args = new PlayerTeamEventArgs(args_.Sender, packet);

            LogPlayerTeam_Before(args);
            PlayerTeam.Invoke(this, args);
            LogPlayerTeam_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerTeam_Before(PlayerTeamEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player} joining {PlayerTeam}]", PlayerTeam, args.Player, args.Team);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerTeam_After(PlayerTeamEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerTeam, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} joining {PlayerTeam}]",
                    PlayerTeam, args.Player, args.Team);
            }
        }

        // =============================================================================================================
        // Handling PlayerUuid
        // =============================================================================================================

        private void PlayerUuidHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerUuidPacket)args_.Packet;
            var args = new PlayerUuidEventArgs(args_.Sender, packet);

            LogPlayerUuid_Before(args);
            PlayerUuid.Invoke(this, args);
            LogPlayerUuid_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerUuid_Before(PlayerUuidEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player} is {PlayerUuid}]", PlayerUuid, args.Player, args.Uuid);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerUuid_After(PlayerUuidEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerUuid, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug("Altered {Event} to [{Player} is {PlayerUuid}]", PlayerUuid, args.Player, args.Uuid);
            }
        }

        // =============================================================================================================
        // Handling PlayerTeleportationPotion
        // =============================================================================================================

        private void PlayerTeleportationPotionHandler(PacketReceiveEventArgs args_) {
            var args = new PlayerTeleportationPotionEventArgs(args_.Sender);

            PlayerTeleportationPotion_Before(args);
            PlayerTeleportationPotion.Invoke(this, args);
            PlayerTeleportationPotion_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerTeleportationPotion_Before(PlayerTeleportationPotionEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug("Invoking {Event} with [{Player}]", PlayerTeleportationPotion, args.Player);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void PlayerTeleportationPotion_After(PlayerTeleportationPotionEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerTeleportationPotion, args.CancellationReason);
            }
        }

        // =============================================================================================================
        // Handling PlayerAnglerQuests
        // =============================================================================================================

        private void PlayerAnglerQuestsHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerAnglerQuestsPacket)args_.Packet;
            var args = new PlayerAnglerQuestsEventArgs(args_.Sender, packet);

            LogPlayerAnglerQuests_Before(args);
            PlayerAnglerQuests.Invoke(this, args);
            LogPlayerAnglerQuests_After(args);

            args_.CancellationReason = args.CancellationReason;
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerAnglerQuests_Before(PlayerAnglerQuestsEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} completing {PlayerNumberOfAnglerQuestsCompleted} quests]",
                PlayerAnglerQuests, args.Player, args.NumberOfAnglerQuestsCompleted);
        }

        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerAnglerQuests_After(PlayerAnglerQuestsEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerAnglerQuests, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} completing {PlayerNumberOfAnglerQuestsCompleted} quests]",
                    PlayerAnglerQuests, args.Player, args.NumberOfAnglerQuestsCompleted);
            }
        }

        // =============================================================================================================
        // Handling PlayerChat
        // =============================================================================================================

        private void ModuleHandler(PacketReceiveEventArgs args_) {
            var module = ((ModulePacket)args_.Packet).Module;
            if (module is ChatModule chatModule) {
                var args = new PlayerChatEventArgs(args_.Sender, chatModule);

                LogPlayerChat_Before(args);
                PlayerChat.Invoke(this, args);
                LogPlayerChat_After(args);

                args_.CancellationReason = args.CancellationReason;
            }
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerChat_Before(PlayerChatEventArgs args) {
            // Not localized because this string is developer-facing.
            Log.Debug(
                "Invoking {Event} with [{Player} chatting {ChatCommand} {ChatText}]",
                PlayerChat, args.Player, args.Command, args.Text);
        }
        
        [Conditional("DEBUG"), ExcludeFromCodeCoverage]
        private void LogPlayerChat_After(PlayerChatEventArgs args) {
            if (args.IsCanceled()) {
                // Not localized because this string is developer-facing.
                Log.Debug("Canceled {Event} for {Reason}", PlayerChat, args.CancellationReason);
            } else if (args.IsDirty) {
                // Not localized because this string is developer-facing.
                Log.Debug(
                    "Altered {Event} to [{Player} chatting {ChatCommand} {ChatText}]",
                    PlayerChat, args.Player, args.Command, args.Text);
            }
        }
    }
}
