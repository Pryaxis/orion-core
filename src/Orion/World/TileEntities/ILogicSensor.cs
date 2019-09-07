namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a Terraria logic sensor tile entity.
    /// </summary>
    public interface ILogicSensor : ITileEntity {
        /// <summary>
        /// Gets or sets the sensor type.
        /// </summary>
        LogicSensorType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sensor is activated.
        /// </summary>
        bool IsActivated { get; set; }

        /// <summary>
        /// Gets or sets the sensor data.
        /// </summary>
        int Data { get; set; }
    }
}
