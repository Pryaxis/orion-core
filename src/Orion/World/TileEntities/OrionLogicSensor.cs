using TGCTE = Terraria.GameContent.Tile_Entities;

namespace Orion.World.TileEntities {
    internal sealed class OrionLogicSensor : OrionTileEntity<TGCTE.TELogicSensor>, ILogicSensor {
        public LogicSensorType SensorType {
            get => (LogicSensorType)Wrapped.logicCheck;
            set => Wrapped.logicCheck = (TGCTE.TELogicSensor.LogicCheckType)value;
        }

        public bool IsActivated {
            get => Wrapped.On;
            set => Wrapped.On = value;
        }

        public int Data {
            get => Wrapped.CountedData;
            set => Wrapped.CountedData = value;
        }

        public OrionLogicSensor(TGCTE.TELogicSensor logicSensor) : base(logicSensor) { }
    }
}
