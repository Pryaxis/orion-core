namespace Orion.World.TileEntities {
    /// <summary>
    /// Specifies the type of a <see cref="ILogicSensor"/>.
    /// </summary>
    public enum LogicSensorType {
#pragma warning disable 1591
        None,
        Day,
        Night,
        PlayerAbove,
        Water,
        Lava,
        Honey,
        Liquid,
#pragma warning restore 1591
    }
}
