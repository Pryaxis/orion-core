namespace Orion.Networking.Packets.World {
    /// <summary>
    /// Specifies the type of modification in a <see cref="TileModificationPacket"/>.
    /// </summary>
    public enum TileModificationType : byte {
#pragma warning disable 1591
        DestroyBlock = 0,
        PlaceBlock,
        DestroyWall,
        PlaceWall,
        DestroyBlockNoItems,
        PlaceRedWire,
        RemoveRedWire,
        HalveBlock,
        PlaceActuator,
        RemoveActuator,
        PlaceBlueWire,
        RemoveBlueWire,
        PlaceGreenWire,
        RemoveGreenWire,
        SlopeBlock,
        FrameTrack,
        PlaceYellowWire,
        RemoveYellowWire,
        PokeLogicGate,
        ActuateBlock,
#pragma warning restore 1591
    }
}
