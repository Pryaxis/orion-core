using System.IO;
using Orion.World.TileEntities;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Represents a logic sensor that is transmitted over the network.
    /// </summary>
    public sealed class NetworkLogicSensor : NetworkTileEntity, ILogicSensor {
        /// <inheritdoc />
        public LogicSensorType SensorType { get; set; }

        /// <inheritdoc />
        public bool IsActivated { get; set; }

        /// <inheritdoc />
        public int Data { get; set; }

        private protected override TileEntityType Type => TileEntityType.LogicSensor;

        private protected override void ReadFromReaderImpl(BinaryReader reader) {
            SensorType = (LogicSensorType)reader.ReadByte();
            IsActivated = reader.ReadBoolean();
        }

        private protected override void WriteToWriterImpl(BinaryWriter writer) {
            writer.Write((byte)SensorType);
            writer.Write(IsActivated);
        }
    }
}
