using System;

namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Specifies the mass wire operations in a <see cref="RequestMassWireOperationPacket"/>.
    /// </summary>
    [Flags]
    public enum MassWireOperations : byte {
        /// <summary>
        /// Nothing.
        /// </summary>
        None = 0,

        /// <summary>
        /// Modify red wires.
        /// </summary>
        RedWire = 1,

        /// <summary>
        /// Modify green wires.
        /// </summary>
        GreenWire = 2,

        /// <summary>
        /// Modify blue wires.
        /// </summary>
        BlueWire = 4,

        /// <summary>
        /// Modify yellow wires.
        /// </summary>
        YellowWire = 8,

        /// <summary>
        /// Modify actuators.
        /// </summary>
        Actuator = 16,

        /// <summary>
        /// Remove wire components.
        /// </summary>
        Cut = 32,
    }
}
