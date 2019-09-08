using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to update a tile entity.
    /// </summary>
    public sealed class UpdateTileEntityPacket : Packet {
        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            throw new NotImplementedException();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            throw new NotImplementedException();
        }
    }
}
