using System.IO;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to the client to spawn the player for the first time.
    /// </summary>
    public sealed class FirstSpawnPlayerPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) { }
        private protected override void WriteToWriter(BinaryWriter writer) { }
    }
}
