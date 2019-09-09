using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to cause an NPC to steal coins.
    /// </summary>
    public sealed class NpcStealCoinsPacket : Packet {
        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex { get; set; }

        /// <summary>
        /// Gets or sets the NPC's stolen value.
        /// </summary>
        public float NpcStolenValue { get; set; }

        /// <summary>
        /// Gets or sets the coin position.
        /// </summary>
        public Vector2 CoinPosition { get; set; }

        private protected override PacketType Type => PacketType.NpcStealCoins;

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            NpcIndex = reader.ReadInt16();
            NpcStolenValue = reader.ReadSingle();
            CoinPosition = reader.ReadVector2();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(NpcIndex);
            writer.Write(NpcStolenValue);
            writer.Write(CoinPosition);
        }
    }
}
