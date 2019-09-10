namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Specifies the liquid change type in a <see cref="SquareTilesPacket"/>.
    /// </summary>
    public enum LiquidChangeType : byte {
#pragma warning disable 1591
        None = 0,
        LavaToWater,
        HoneyToWater,
        HoneyToLava,
#pragma warning restore 1591
    }
}
