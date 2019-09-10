namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a Terraria target dummy tile entity.
    /// </summary>
    public interface ITargetDummy : ITileEntity {
        /// <summary>
        /// Gets the NPC index corresponding to the target dummy.
        /// </summary>
        int NpcIndex { get; }
    }
}
