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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Npcs;
using Terraria;

namespace Orion.Networking.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set NPC information.
    /// </summary>
    public sealed class NpcInfoPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's position.
        /// </summary>
        public Vector2 NpcPosition { get; set; }

        /// <summary>
        /// Gets or sets the NPC's velocity.
        /// </summary>
        public Vector2 NpcVelocity { get; set; }

        /// <summary>
        /// Gets or sets the NPC's target index.
        /// </summary>
        public ushort NpcTargetIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the horizontal direction of the NPC.
        /// </summary>
        public bool NpcHorizontalDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the vertical direction of the NPC.
        /// </summary>
        public bool NpcVerticalDirection { get; set; }

        /// <summary>
        /// Gets the NPC's AI values.
        /// </summary>
        public float[] NpcAiValues { get; } = new float[NPC.maxAI];

        /// <summary>
        /// Gets or sets a value indicating the direction of the NPC sprite.
        /// </summary>
        public bool NpcSpriteDirection { get; set; }

        /// <summary>
        /// Gets or set a value indicating whether the NPC is at maximum health.
        /// </summary>
        public bool IsNpcAtMaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the NPC's <see cref="Orion.Npcs.NpcType"/>.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes that represent the NPC's health.
        /// </summary>
        public byte NpcNumberOfHealthBytes { get; set; }

        /// <summary>
        /// Gets or sets the NPC's health.
        /// </summary>
        public int NpcHealth { get; set; }

        /// <summary>
        /// Gets or sets the NPC's releaser player index.
        /// </summary>
        public byte NpcReleaserPlayerIndex { get; set; }

        public override PacketType Type => PacketType.NpcInfo;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex}, {NpcType} @ {NpcPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            NpcIndex = reader.ReadInt16();
            NpcPosition = BinaryExtensions.ReadVector2(reader);
            NpcVelocity = BinaryExtensions.ReadVector2(reader);

            var targetIndex = reader.ReadUInt16();
            NpcTargetIndex = targetIndex != ushort.MaxValue ? targetIndex : (ushort)0;

            BitsByte header = reader.ReadByte();
            NpcHorizontalDirection = header[0];
            NpcVerticalDirection = header[1];
            if (header[2]) NpcAiValues[0] = reader.ReadSingle();
            if (header[3]) NpcAiValues[1] = reader.ReadSingle();
            if (header[4]) NpcAiValues[2] = reader.ReadSingle();
            if (header[5]) NpcAiValues[3] = reader.ReadSingle();
            NpcSpriteDirection = header[6];
            IsNpcAtMaxHealth = header[7];

            NpcType = (NpcType)reader.ReadInt16();

            if (!IsNpcAtMaxHealth) {
                NpcNumberOfHealthBytes = reader.ReadByte();
                switch (NpcNumberOfHealthBytes) {
                case 2:
                    NpcHealth = reader.ReadInt16();
                    break;
                case 4:
                    NpcHealth = reader.ReadInt32();
                    break;
                default:
                    NpcHealth = reader.ReadSByte();
                    break;
                }
            }

            if (Main.npcCatchable[(int)NpcType]) {
                NpcReleaserPlayerIndex = reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(NpcIndex);
            writer.Write(NpcPosition);
            writer.Write(NpcVelocity);
            writer.Write(NpcTargetIndex);

            BitsByte header = 0;
            header[0] = NpcHorizontalDirection;
            header[1] = NpcVerticalDirection;
            header[2] = NpcAiValues[0] != 0;
            header[3] = NpcAiValues[1] != 0;
            header[4] = NpcAiValues[2] != 0;
            header[5] = NpcAiValues[3] != 0;
            header[6] = NpcSpriteDirection;
            header[7] = IsNpcAtMaxHealth;

            writer.Write(header);
            if (header[2]) writer.Write(NpcAiValues[0]);
            if (header[3]) writer.Write(NpcAiValues[1]);
            if (header[4]) writer.Write(NpcAiValues[2]);
            if (header[5]) writer.Write(NpcAiValues[3]);

            writer.Write((short)NpcType);

            if (!IsNpcAtMaxHealth) {
                writer.Write(NpcNumberOfHealthBytes);
                switch (NpcNumberOfHealthBytes) {
                case 4:
                    writer.Write(NpcHealth);
                    break;
                case 2:
                    writer.Write((short)NpcHealth);
                    break;
                default:
                    writer.Write((byte)NpcHealth);
                    break;
                }
            }

            if (Main.npcCatchable[(int)NpcType]) {
                writer.Write(NpcReleaserPlayerIndex);
            }
        }
    }
}
