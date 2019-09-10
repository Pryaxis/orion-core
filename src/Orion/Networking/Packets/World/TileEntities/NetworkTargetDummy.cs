using System.IO;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents a target dummy that is transmitted over the network.
    /// </summary>
    public sealed class NetworkTargetDummy : NetworkTileEntity, ITargetDummy {
        /// <inheritdoc />
        public int NpcIndex { get; set; }

        private protected override TileEntityType Type => TileEntityType.TargetDummy;

        private protected override void ReadFromReaderImpl(BinaryReader reader) {
            NpcIndex = reader.ReadInt16();
        }

        private protected override void WriteToWriterImpl(BinaryWriter writer) {
            writer.Write((short)NpcIndex);
        }
    }
}
