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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Destructurama.Attributed;
using Orion.Buffs;
using Orion.Collections;
using Orion.Entities;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Packets;

namespace Orion.Players {
    [LogAsScalar]
    internal sealed class OrionPlayer : OrionEntity<Terraria.Player>, IPlayer {
        private readonly OrionPlayerService _playerService;
        private readonly byte[] _sendBuffer = new byte[ushort.MaxValue];

        public override string Name {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public IPlayerStats Stats { get; }
        public IArray<Buff> Buffs { get; }

        public PlayerDifficulty Difficulty {
            get => (PlayerDifficulty)Wrapped.difficulty;
            set => Wrapped.difficulty = (byte)value;
        }

        public bool IsInPvp {
            get => Wrapped.hostile;
            set => Wrapped.hostile = value;
        }

        public PlayerTeam Team {
            get => (PlayerTeam)Wrapped.team;
            set => Wrapped.team = (int)value;
        }

        // We need to inject an `OrionPlayerService` so that we can raise a `PacketSendEvent` in `SendPacket`.
        public OrionPlayer(int playerIndex, Terraria.Player terrariaPlayer, OrionPlayerService playerService)
                : base(playerIndex, terrariaPlayer) {
            Debug.Assert(terrariaPlayer != null);
            Debug.Assert(playerService != null);

            _playerService = playerService;

            Stats = new PlayerStats(terrariaPlayer);
            Buffs = new BuffArray(terrariaPlayer);
        }

        public OrionPlayer(Terraria.Player terrariaPlayer, OrionPlayerService playerService)
            : this(-1, terrariaPlayer, playerService) { }

        public void SendPacket<TPacket>(ref TPacket packet) where TPacket : struct, IPacket {
            var terrariaClient = Terraria.Netplay.Clients[Index];
            if (!terrariaClient.IsConnected()) {
                return;
            }

            var evt = new PacketSendEvent<TPacket>(ref packet, this);
            _playerService.Kernel.Raise(evt, _playerService.Log);
            if (evt.IsCanceled()) {
                return;
            }

            // When writing the packet, we need to use the `Server` context since this packet comes from the server.
            var packetLength = packet.WriteWithHeader(_sendBuffer, PacketContext.Server);
            terrariaClient.Socket?.AsyncSend(_sendBuffer, 0, packetLength, terrariaClient.ServerWriteCallBack);
        }

        private class PlayerStats : IPlayerStats, IWrapping<Terraria.Player> {
            public int Health {
                get => Wrapped.statLife;
                set => Wrapped.statLife = value;
            }

            public int MaxHealth {
                get => Wrapped.statLifeMax;
                set => Wrapped.statLifeMax = value;
            }

            public int Mana {
                get => Wrapped.statMana;
                set => Wrapped.statMana = value;
            }

            public int MaxMana {
                get => Wrapped.statManaMax;
                set => Wrapped.statManaMax = value;
            }

            public int Defense => Wrapped.statDefense;

            public Terraria.Player Wrapped { get; }

            public PlayerStats(Terraria.Player terrariaPlayer) {
                Debug.Assert(terrariaPlayer != null);

                Wrapped = terrariaPlayer;
            }
        }

        private class BuffArray : IArray<Buff>, IWrapping<Terraria.Player> {
            public Buff this[int index] {
                get {
                    if (index < 0 || index >= Count) {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0-{Count})");
                    }

                    var id = (BuffId)Wrapped.buffType[index];
                    var duration = TimeSpan.FromSeconds(Wrapped.buffTime[index] / 60.0);
                    return duration > TimeSpan.Zero ? new Buff(id, duration) : default;
                }
                set {
                    if (index < 0 || index >= Count) {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0-{Count})");
                    }

                    Wrapped.buffType[index] = (int)value.Id;
                    Wrapped.buffTime[index] = (int)(value.Duration.TotalSeconds * 60.0);
                }
            }

            public int Count => Terraria.Player.maxBuffs;

            public Terraria.Player Wrapped { get; }

            public BuffArray(Terraria.Player terrariaPlayer) {
                Debug.Assert(terrariaPlayer != null);

                Wrapped = terrariaPlayer;
            }

            public IEnumerator<Buff> GetEnumerator() {
                for (var i = 0; i < Count; ++i) {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
