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
using System.Runtime.InteropServices;
using Orion.Core.Packets.DataStructures;
using Orion.Core.Utils;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent to set a player's zones.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct PlayerZones : IPacket
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.
        [FieldOffset(1)] private Flags8 _flags;
        [FieldOffset(2)] private Flags8 _flags2;
        [FieldOffset(3)] private Flags8 _flags3;
        [FieldOffset(4)] private Flags8 _flags4;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        [field: FieldOffset(0)] public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the dungeon.
        /// </summary>
        /// <value><see langword="true"/> if the player is in the dungeon; otherwise, <see langword="false"/>.</value>
        public bool Dungeon
        {
            get => _flags[0];
            set => _flags[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the corruption.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in the corruption; otherwise, <see langword="false"/>.
        /// </value>
        public bool Corruption
        {
            get => _flags[1];
            set => _flags[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the hallow.
        /// </summary>
        /// <value><see langword="true"/> if the player is in the hallow; otherwise, <see langword="false"/>.</value>
        public bool Hallow
        {
            get => _flags[2];
            set => _flags[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a meteor.
        /// </summary>
        /// <value><see langword="true"/> if the player is near a meteor; otherwise, <see langword="false"/>.</value>
        public bool Meteor
        {
            get => _flags[3];
            set => _flags[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the jungle.
        /// </summary>
        /// <value><see langword="true"/> if the player is in the jungle; otherwise, <see langword="false"/>.</value>
        public bool Jungle
        {
            get => _flags[4];
            set => _flags[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the snow.
        /// </summary>
        /// <value><see langword="true"/> if the player is in the snow; otherwise, <see langword="false"/>.</value>
        public bool Snow
        {
            get => _flags[5];
            set => _flags[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the crimson.
        /// </summary>
        /// <value><see langword="true"/> if the player is in the crimson; otherwise, <see langword="false"/>.</value>
        public bool Crimson
        {
            get => _flags[6];
            set => _flags[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a water candle.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a water candle; otherwise, <see langword="false"/>.
        /// </value>
        public bool WaterCandle
        {
            get => _flags[7];
            set => _flags[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near a peace candle.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near a peace candle; otherwise, <see langword="false"/>.
        /// </value>
        public bool PeaceCandle
        {
            get => _flags2[0];
            set => _flags2[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the solar tower.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the solar tower; otherwise, <see langword="false"/>.
        /// </value>
        public bool SolarTower
        {
            get => _flags2[1];
            set => _flags2[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the vortex tower.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the vortex tower; otherwise, <see langword="false"/>.
        /// </value>
        public bool VortexTower
        {
            get => _flags2[2];
            set => _flags2[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the nebula tower.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the nebula tower; otherwise, <see langword="false"/>.
        /// </value>
        public bool NebulaTower
        {
            get => _flags2[3];
            set => _flags2[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is near the stardust tower.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is near the stardust tower; otherwise, <see langword="false"/>.
        /// </value>
        public bool StardustTower
        {
            get => _flags2[4];
            set => _flags2[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the desert.
        /// </summary>
        /// <value><see langword="true"/> if the player is in the desert; otherwise, <see langword="false"/>.</value>
        public bool Desert
        {
            get => _flags2[5];
            set => _flags2[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in a glowing mushroom biome.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in a glowing mushroom biome; otherwise, <see langword="false"/>.
        /// </value>
        public bool Mushroom
        {
            get => _flags2[6];
            set => _flags2[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the underground desert.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in the underground desert; otherwise, <see langword="false"/>.
        /// </value>
        public bool UndergroundDesert
        {
            get => _flags2[7];
            set => _flags2[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is at the sky height.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is at the sky height; otherwise, <see langword="false"/>.
        /// </value>
        public bool Sky
        {
            get => _flags3[0];
            set => _flags3[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is at the overworld height.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is at the overworld height; otherwise, <see langword="false"/>.
        /// </value>
        public bool Overworld
        {
            get => _flags3[1];
            set => _flags3[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is at the underground height.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is at the underground height; otherwise, <see langword="false"/>.
        /// </value>
        public bool Underground
        {
            get => _flags3[2];
            set => _flags3[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is at the cavern height.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is at the cavern height; otherwise, <see langword="false"/>.
        /// </value>
        public bool Cavern
        {
            get => _flags3[3];
            set => _flags3[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is at the underworld height.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is at the underworld height; otherwise, <see langword="false"/>.
        /// </value>
        public bool Underworld
        {
            get => _flags3[4];
            set => _flags3[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is at a beach.
        /// </summary>
        /// <value><see langword="true"/> if the player is at a beach; otherwise, <see langword="false"/>.</value>
        public bool Beach
        {
            get => _flags3[5];
            set => _flags3[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the rain.
        /// </summary>
        /// <value><see langword="true"/> if the player is in the rain; otherwise, <see langword="false"/>.</value>
        public bool Rain
        {
            get => _flags3[6];
            set => _flags3[6] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in a sandstorm.
        /// </summary>
        /// <value><see langword="true"/> if the player is in a sandstorm; otherwise, <see langword="false"/>.</value>
        public bool Sandstorm
        {
            get => _flags3[7];
            set => _flags3[7] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the Old One's Army event.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in the Old One's Army event; otherwise, <see langword="false"/>.
        /// </value>
        public bool OldOnesArmy
        {
            get => _flags4[0];
            set => _flags4[0] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in a granite mini-biome.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in a granite mini-biome; otherwise, <see langword="false"/>.
        /// </value>
        public bool Granite
        {
            get => _flags4[1];
            set => _flags4[1] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in a marble mini-biome.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in a marble mini-biome; otherwise, <see langword="false"/>.
        /// </value>
        public bool Marble
        {
            get => _flags4[2];
            set => _flags4[2] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in a bee hive mini-biome.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in a bee hive mini-biome; otherwise, <see langword="false"/>.
        /// </value>
        public bool BeeHive
        {
            get => _flags4[3];
            set => _flags4[3] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in a gemstone cave micro-biome.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in a gemstone cave micro-biome; otherwise,
        /// <see langword="false"/>.
        /// </value>
        public bool GemstoneCave
        {
            get => _flags4[4];
            set => _flags4[4] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in the Lihzahrd temple.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the player is in the Lihzahrd temple; otherwise, <see langword="false"/>.
        /// </value>
        public bool LihzahrdTemple
        {
            get => _flags4[5];
            set => _flags4[5] = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in a graveyard.
        /// </summary>
        /// <value><see langword="true"/> if the player is in a graveyard; otherwise, <see langword="false"/>.</value>
        public bool Graveyard
        {
            get => _flags4[6];
            set => _flags4[6] = value;
        }

        PacketId IPacket.Id => PacketId.PlayerZones;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 5);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 5);
    }
}
