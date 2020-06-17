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
using System.Diagnostics;
using System.IO;
using System.Threading;
using Destructurama.Attributed;
using Orion.Core.Buffs;
using Orion.Core.Collections;
using Orion.Core.Entities;
using Orion.Core.Events.Packets;
using Orion.Core.Packets;
using Serilog;

namespace Orion.Core.Players
{
    [LogAsScalar]
    internal sealed class OrionPlayer : OrionEntity<Terraria.Player>, IPlayer
    {
        private readonly OrionKernel _kernel;
        private readonly ILogger _log;
        private readonly ThreadLocal<byte[]> _sendBuffer = new ThreadLocal<byte[]>(() => new byte[ushort.MaxValue]);

        public OrionPlayer(int playerIndex, Terraria.Player terrariaPlayer, OrionKernel kernel, ILogger log)
                : base(playerIndex, terrariaPlayer)
        {
            Debug.Assert(kernel != null);
            Debug.Assert(log != null);

            _kernel = kernel;
            _log = log;

            Buffs = new BuffArray(terrariaPlayer);
        }

        public OrionPlayer(Terraria.Player terrariaPlayer, OrionKernel kernel, ILogger log)
            : this(-1, terrariaPlayer, kernel, log) { }

        public override string Name
        {
            get => Wrapped.name;
            set => Wrapped.name = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Health
        {
            get => Wrapped.statLife;
            set => Wrapped.statLife = value;
        }

        public int MaxHealth
        {
            get => Wrapped.statLifeMax;
            set => Wrapped.statLifeMax = value;
        }

        public int Mana
        {
            get => Wrapped.statMana;
            set => Wrapped.statMana = value;
        }

        public int MaxMana
        {
            get => Wrapped.statManaMax;
            set => Wrapped.statManaMax = value;
        }

        public IArray<Buff> Buffs { get; }

        public CharacterDifficulty Difficulty
        {
            get => (CharacterDifficulty)Wrapped.difficulty;
            set => Wrapped.difficulty = (byte)value;
        }

        public bool IsInPvp
        {
            get => Wrapped.hostile;
            set => Wrapped.hostile = value;
        }

        public PlayerTeam Team
        {
            get => (PlayerTeam)Wrapped.team;
            set => Wrapped.team = (int)value;
        }

        public void SendPacket<TPacket>(ref TPacket packet) where TPacket : struct, IPacket
        {
            var terrariaClient = Terraria.Netplay.Clients[Index];
            if (!terrariaClient.IsConnected())
            {
                return;
            }

            var evt = new PacketSendEvent<TPacket>(ref packet, this);
            _kernel.Raise(evt, _log);
            if (evt.IsCanceled)
            {
                return;
            }

            // When writing the packet, we need to use the `Server` context since this packet comes from the server. A
            // thread-local send buffer is used in case there is some concurrency.
            var sendBuffer = _sendBuffer.Value;
            var packetLength = packet.WriteWithHeader(sendBuffer, PacketContext.Server);
            try
            {
                terrariaClient.Socket.AsyncSend(sendBuffer, 0, packetLength, terrariaClient.ServerWriteCallBack);
            }
            catch (IOException) { }
        }

        private class BuffArray : IArray<Buff>
        {
            private readonly Terraria.Player _wrapped;

            public BuffArray(Terraria.Player terrariaPlayer)
            {
                Debug.Assert(terrariaPlayer != null);

                _wrapped = terrariaPlayer;
            }

            public Buff this[int index]
            {
                get
                {
                    if (index < 0 || index >= Count)
                    {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0 to {Count - 1})");
                    }

                    var ticks = _wrapped.buffTime[index];
                    if (ticks <= 0)
                    {
                        return default;
                    }

                    var id = (BuffId)_wrapped.buffType[index];
                    return new Buff(id, ticks);
                }

                set
                {
                    if (index < 0 || index >= Count)
                    {
                        // Not localized because this string is developer-facing.
                        throw new IndexOutOfRangeException($"Index out of range (expected: 0 to {Count - 1})");
                    }

                    _wrapped.buffType[index] = (int)value.Id;
                    _wrapped.buffTime[index] = value.Ticks;
                }
            }

            public int Count => Terraria.Player.maxBuffs;
        }
    }
}
