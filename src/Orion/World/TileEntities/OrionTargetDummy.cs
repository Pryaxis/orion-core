using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.World.TileEntities {
    internal sealed class OrionTargetDummy : OrionTileEntity<TGCTE.TETrainingDummy>, ITargetDummy {
        public int NpcIndex => Wrapped.npc;

        public OrionTargetDummy(TGCTE.TETrainingDummy trainingDummy) : base(trainingDummy) { }
    }
}
