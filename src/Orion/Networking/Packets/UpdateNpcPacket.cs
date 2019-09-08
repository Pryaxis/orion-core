using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;
using Orion.Npcs;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update an NPC.
    /// </summary>
    public sealed class UpdateNpcPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's position.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the NPC's velocity.
        /// </summary>
        public Vector2 Velocity { get; set; }

        /// <summary>
        /// Gets or sets the NPC's target index.
        /// </summary>
        public ushort TargetIndex { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the horizontal direction of the NPC.
        /// </summary>
        public bool HorizontalDirection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the vertical direction of the NPC.
        /// </summary>
        public bool VerticalDirection { get; set; }

        /// <summary>
        /// Gets the NPC's AI values.
        /// </summary>
        public float[] AiValues { get; } = new float[4];

        /// <summary>
        /// Gets or sets a value indicating the direction of the NPC sprite.
        /// </summary>
        public bool SpriteDirection { get; set; }

        /// <summary>
        /// Gets or set a value indicating whether the NPC is at maximum HP.
        /// </summary>
        public bool IsAtMaxHp { get; set; }

        /// <summary>
        /// Gets or sets the NPC type.
        /// </summary>
        public NpcType NpcType { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes that represent the NPC's HP.
        /// </summary>
        public byte NumberOfHpBytes { get; set; }

        /// <summary>
        /// Gets or sets the NPC's HP.
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        /// Gets or sets the NPC's release owner index.
        /// </summary>
        public byte ReleaseOwnerIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            Position = reader.ReadVector2();
            Velocity = reader.ReadVector2();

            var targetIndex = reader.ReadUInt16();
            TargetIndex = targetIndex != ushort.MaxValue ? targetIndex : (ushort)0;

            Terraria.BitsByte header = reader.ReadByte();
            HorizontalDirection = header[0];
            VerticalDirection = header[1];
            if (header[2]) AiValues[0] = reader.ReadSingle();
            if (header[3]) AiValues[1] = reader.ReadSingle();
            if (header[4]) AiValues[2] = reader.ReadSingle();
            if (header[5]) AiValues[3] = reader.ReadSingle();
            SpriteDirection = header[6];
            IsAtMaxHp = header[7];

            NpcType = (NpcType)reader.ReadInt16();

            if (!IsAtMaxHp) {
                NumberOfHpBytes = reader.ReadByte();
                switch (NumberOfHpBytes) {
                case 2:
                    Hp = reader.ReadInt16();
                    break;
                case 4:
                    Hp = reader.ReadInt32();
                    break;
                default:
                    Hp = reader.ReadSByte();
                    break;
                }
            }

            if (Terraria.Main.npcCatchable[(int)NpcType]) {
                ReleaseOwnerIndex = reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(Position);
            writer.Write(Velocity);
            writer.Write(TargetIndex);

            Terraria.BitsByte header = 0;
            header[0] = HorizontalDirection;
            header[1] = VerticalDirection;
            header[2] = AiValues[0] != 0;
            header[3] = AiValues[1] != 0;
            header[4] = AiValues[2] != 0;
            header[5] = AiValues[3] != 0;
            header[6] = SpriteDirection;
            header[7] = IsAtMaxHp;

            writer.Write(header);
            if (header[2]) writer.Write(AiValues[0]);
            if (header[3]) writer.Write(AiValues[1]);
            if (header[4]) writer.Write(AiValues[2]);
            if (header[5]) writer.Write(AiValues[3]);

            writer.Write((short)NpcType);

            if (!IsAtMaxHp) {
                writer.Write(NumberOfHpBytes);
                switch (NumberOfHpBytes) {
                case 4:
                    writer.Write(Hp);
                    break;
                case 2:
                    writer.Write((short)Hp);
                    break;
                default:
                    writer.Write((byte)Hp);
                    break;
                }
            }

            if (Terraria.Main.npcCatchable[(int)NpcType]) {
                writer.Write(ReleaseOwnerIndex);
            }
        }
    }
}
