using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.World.TileEntities {
    internal sealed class OrionTrainingDummy : OrionTileEntity<TGCTE.TETrainingDummy>, ITrainingDummy {
        public int NpcIndex => Wrapped.npc;

        public OrionTrainingDummy(TGCTE.TETrainingDummy trainingDummy) : base(trainingDummy) { }
    }
}
