namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Specifies the type of a tile entity.
    /// </summary>
    public enum TileEntityType : byte {
        /// <summary>
        /// A target dummy.
        /// </summary>
        TargetDummy = 0,

        /// <summary>
        /// An item frame.
        /// </summary>
        ItemFrame = 1,

        /// <summary>
        /// A logic sensor.
        /// </summary>
        LogicSensor = 2,
    }
}
