namespace Orion.World.TileEntities {
    /// <summary>
    /// Provides a wrapper around a Terraria training dummy tile entity.
    /// </summary>
    public interface ITrainingDummy : ITileEntity {
        /// <summary>
        /// Gets the NPC index corresponding to the training dummy.
        /// </summary>
        int NpcIndex { get; }
    }
}
