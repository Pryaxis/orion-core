using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to update the pillar's shield strengths.
    /// </summary>
    public sealed class UpdatePillarShieldsPacket : Packet {
        /// <summary>
        /// Gets or sets the solar pillar's strength.
        /// </summary>
        public ushort SolarPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's strength.
        /// </summary>
        public ushort VortexPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's strength.
        /// </summary>
        public ushort NebulaPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's strength.
        /// </summary>
        public ushort StardustPillarShieldStrength { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            SolarPillarShieldStrength = reader.ReadUInt16();
            VortexPillarShieldStrength = reader.ReadUInt16();
            NebulaPillarShieldStrength = reader.ReadUInt16();
            StardustPillarShieldStrength = reader.ReadUInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(SolarPillarShieldStrength);
            writer.Write(VortexPillarShieldStrength);
            writer.Write(NebulaPillarShieldStrength);
            writer.Write(StardustPillarShieldStrength);
        }
    }
}
