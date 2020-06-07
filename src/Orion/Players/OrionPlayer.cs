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
using Serilog;

namespace Orion.Players {
    [LogAsScalar]
    internal sealed class OrionPlayer : OrionEntity<Terraria.Player>, IPlayer {
        private readonly OrionKernel _kernel;
        private readonly ILogger _log;
        private readonly byte[] _sendBuffer = new byte[ushort.MaxValue];

        public OrionPlayer(int playerIndex, Terraria.Player terrariaPlayer, OrionKernel kernel, ILogger log)
                : base(playerIndex, terrariaPlayer) {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            _kernel = kernel;
            _log = log;

            Stats = new PlayerStats(terrariaPlayer);
            Buffs = new BuffArray(terrariaPlayer);
        }

        public OrionPlayer(Terraria.Player terrariaPlayer, OrionKernel kernel, ILogger log)
            : this(-1, terrariaPlayer, kernel, log) { }

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

        public void SendPacket<TPacket>(ref TPacket packet) where TPacket : struct, IPacket {
            var terrariaClient = Terraria.Netplay.Clients[Index];
            if (!terrariaClient.IsConnected()) {
                return;
            }

            var evt = new PacketSendEvent<TPacket>(ref packet, this);
            _kernel.Raise(evt, _log);
            if (evt.IsCanceled()) {
                return;
            }

            // When writing the packet, we need to use the `Server` context since this packet comes from the server.
            var packetLength = packet.WriteWithHeader(_sendBuffer, PacketContext.Server);
            terrariaClient.Socket?.AsyncSend(_sendBuffer, 0, packetLength, terrariaClient.ServerWriteCallBack);
        }

        private class PlayerStats : IPlayerStats {
            private readonly Terraria.Player _wrapped;

            public PlayerStats(Terraria.Player terrariaPlayer) {
                Debug.Assert(terrariaPlayer != null);

                _wrapped = terrariaPlayer;
            }

            public int Health {
                get => _wrapped.statLife;
                set => _wrapped.statLife = value;
            }

            public int MaxHealth {
                get => _wrapped.statLifeMax;
                set => _wrapped.statLifeMax = value;
            }

            public int Mana {
                get => _wrapped.statMana;
                set => _wrapped.statMana = value;
            }

            public int MaxMana {
                get => _wrapped.statManaMax;
                set => _wrapped.statManaMax = value;
            }

            public int Defense => _wrapped.statDefense;
        }

        private class BuffArray : IArray<Buff> {
            private readonly Terraria.Player _wrapped;

            public BuffArray(Terraria.Player terrariaPlayer) {
                Debug.Assert(terrariaPlayer != null);

                _wrapped = terrariaPlayer;
            }

            public Buff this[int index] {
                get {
                    if (index < 0 || index >= Count) {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0-{Count})");
                    }

                    var id = (BuffId)_wrapped.buffType[index];
                    var duration = TimeSpan.FromSeconds(_wrapped.buffTime[index] / 60.0);
                    return duration > TimeSpan.Zero ? new Buff(id, duration) : default;
                }
                set {
                    if (index < 0 || index >= Count) {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0-{Count})");
                    }

                    _wrapped.buffType[index] = (int)value.Id;
                    _wrapped.buffTime[index] = (int)(value.Duration.TotalSeconds * 60.0);
                }
            }

            public int Count => Terraria.Player.maxBuffs;

            public IEnumerator<Buff> GetEnumerator() {
                for (var i = 0; i < Count; ++i) {
                    yield return this[i];
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
