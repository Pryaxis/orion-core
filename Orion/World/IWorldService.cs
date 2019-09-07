using System;
using Orion.Hooks;
using Orion.World.Events;
using Orion.World.TileEntities;
using Orion.World.Tiles;

namespace Orion.World {
    /// <summary>
    /// Provides access to Terraria's world.
    /// </summary>
    public interface IWorldService : IService {
        /// <summary>
        /// Gets or sets the world name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        string WorldName { get; set; }

        /// <summary>
        /// Gets the width of the world.
        /// </summary>
        /// <remarks>This corresponds to Terraria.Main.maxTilesX.</remarks>
        int WorldWidth { get; }

        /// <summary>
        /// Gets the height of the world.
        /// </summary>
        /// <remarks>This corresponds to Terraria.Main.maxTilesY.</remarks>
        int WorldHeight { get; }

        /// <summary>
        /// Gets or sets the <see cref="Tile"/> at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile.</returns>
        /// <remarks>For performance reasons, we don't bother bounds checking the coordinates.</remarks>
        Tile this[int x, int y] { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        double Time { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in daytime.
        /// </summary>
        bool IsDaytime { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in hardmode.
        /// </summary>
        bool IsHardmode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the world is in expert mode.
        /// </summary>
        bool IsExpertMode { get; set; }

        /// <summary>
        /// Gets the current <see cref="InvasionType"/>.
        /// </summary>
        InvasionType CurrentInvasion { get; }

        /// <summary>
        /// Occurs when the world is checking for whether it's halloween.
        /// </summary>
        HookHandlerCollection<CheckingHalloweenEventArgs> CheckingHalloween { get; set; }

        /// <summary>
        /// Occurs when the world is checking for whether it's Christmas.
        /// </summary>
        HookHandlerCollection<CheckingChristmasEventArgs> CheckingChristmas { get; set; }

        /// <summary>
        /// Occurs when the world is loading.
        /// </summary>
        HookHandlerCollection<LoadingWorldEventArgs> LoadingWorld { get; set; }

        /// <summary>
        /// Occurs when the world was loaded.
        /// </summary>
        HookHandlerCollection<LoadedWorldEventArgs> LoadedWorld { get; set; }

        /// <summary>
        /// Occurs when the world is saving.
        /// </summary>
        HookHandlerCollection<SavingWorldEventArgs> SavingWorld { get; set; }

        /// <summary>
        /// Occurs when the world was saved.
        /// </summary>
        HookHandlerCollection<SavedWorldEventArgs> SavedWorld { get; set; }

        /// <summary>
        /// Occurs when the world is starting hardmode.
        /// </summary>
        HookHandlerCollection<StartingHardmodeEventArgs> StartingHardmode { get; set; }

        /// <summary>
        /// Occurs when the world has started hardmode.
        /// </summary>
        HookHandlerCollection<StartedHardmodeEventArgs> StartedHardmode { get; set; }

        /// <summary>
        /// Occurs when the world is updating a tile in hardmode.
        /// </summary>
        HookHandlerCollection<UpdatingHardmodeTileEventArgs> UpdatingHardmodeTile { get; set; }

        /// <summary>
        /// Starts an invasion with the given <see cref="InvasionType"/>.
        /// </summary>
        /// <param name="invasionType">The invasion type.</param>
        /// <returns><c>true</c> if the invasion was successfully started; <c>false</c> otherwise.</returns>
        bool StartInvasion(InvasionType invasionType);

        /// <summary>
        /// Adds a target dummy at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting training dummy, or <c>null</c> if none was placed.</returns>
        ITargetDummy AddTargetDummy(int x, int y);

        /// <summary>
        /// Adds an item frame at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting item frame, or <c>null</c> if none was placed.</returns>
        IItemFrame AddItemFrame(int x, int y);

        /// <summary>
        /// Adds a logic sensor at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The resulting logic sensor, or <c>null</c> if none was placed.</returns>
        ILogicSensor AddLogicSensor(int x, int y);

        /// <summary>
        /// Gets the tile entity at the given coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile entity, or <c>null</c> if there is none.</returns>
        ITileEntity GetTileEntity(int x, int y);

        /// <summary>
        /// Removes the given <see cref="ITileEntity"/> from the world.
        /// </summary>
        /// <param name="tileEntity">The tile entity.</param>
        /// <returns>A value indicating whether the tile entity was successfully removed.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="tileEntity"/> is <c>null</c>.</exception>
        bool RemoveTileEntity(ITileEntity tileEntity);

        /// <summary>
        /// Saves the world.
        /// </summary>
        void SaveWorld();
    }
}
