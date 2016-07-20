using Orion.Framework;
using OTAPI.Tile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Services
{
    /// <summary>
    /// Service definition: ITileService
    /// 
    /// Provides access to Terraria's Main.tile mechanism, in which implementations may
    /// override the data source to and from the Terraria process.
    /// </summary>
    public interface ITileService : IService, ITileCollection 
    {
    }
}
