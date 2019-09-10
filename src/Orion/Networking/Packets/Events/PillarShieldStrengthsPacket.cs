using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Orion.Networking.Packets.Events {
    /// <summary>
    /// Packet sent from the server to the client to set the pillars' shield strengths.
    /// </summary>
    public sealed class PillarShieldStrengthsPacket : Packet {
        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort SolarPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort VortexPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort NebulaPillarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the solar pillar's shield strength.
        /// </summary>
        public ushort StardustPillarShieldStrength { get; set; }

        private protected override PacketType Type => PacketType.PillarShieldStrengths;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() {
            return $"{nameof(PacketType.PillarShieldStrengths)}[S={SolarPillarShieldStrength}, " +
                   $"V={VortexPillarShieldStrength}, N={NebulaPillarShieldStrength}, T={StardustPillarShieldStrength}]";
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            SolarPillarShieldStrength = reader.ReadUInt16();
            VortexPillarShieldStrength = reader.ReadUInt16();
            NebulaPillarShieldStrength = reader.ReadUInt16();
            StardustPillarShieldStrength = reader.ReadUInt16();
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(SolarPillarShieldStrength);
            writer.Write(VortexPillarShieldStrength);
            writer.Write(NebulaPillarShieldStrength);
            writer.Write(StardustPillarShieldStrength);
        }
    }
}
