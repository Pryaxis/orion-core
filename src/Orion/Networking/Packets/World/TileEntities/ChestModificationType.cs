using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Networking.Packets.World.TileEntities {
    /// <summary>
    /// Specifies the type of modification in a <see cref="ModifyChestPacket"/>.
    /// </summary>
    public enum ChestModificationType {
#pragma warning disable 1591
        PlaceContainers,
        BreakContainers,
        PlaceDressers,
        BreakDressers,
        PlaceContainers2,
        BreakContainers2,
#pragma warning restore 1591
    }
}
