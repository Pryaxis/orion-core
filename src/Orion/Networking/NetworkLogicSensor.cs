using System;
using System.Collections.Generic;
using System.Text;
using Orion.Networking.Packets;
using Orion.World.TileEntities;

namespace Orion.Networking {
    /// <summary>
    /// Represents a logic sensor that is transmitted over the network.
    /// </summary>
    public sealed class NetworkLogicSensor : NetworkTileEntity, ILogicSensor {
        /// <inheritdoc />
        public LogicSensorType Type { get; set; }

        /// <inheritdoc />
        public bool IsActivated { get; set; }

        /// <inheritdoc />
        public int Data { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkTargetDummy"/> class with the specified index and
        /// coordinates.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public NetworkLogicSensor(int index, int x, int y) : base(index, x, y) { }
    }
}
