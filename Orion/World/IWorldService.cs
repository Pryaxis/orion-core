using System;
using Orion.Framework;
using Orion.World.Events;
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
        /// Gets or sets the tile at the specified coordinates.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <returns>The tile.</returns>
        /// <remarks>For performance reasons, we don't bother bounds checking the coordinates.</remarks>
        Tile this[int x, int y] { get; set; }

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
        /// Occurs when the world is checking for whether it's halloween.
        /// </summary>
        event EventHandler<CheckingHalloweenEventArgs> CheckingHalloween;

        /// <summary>
        /// Occurs when the world is checking for whether it's Christmas.
        /// </summary>
        event EventHandler<CheckingChristmasEventArgs> CheckingChristmas;

        /// <summary>
        /// Occurs when the world is loading.
        /// </summary>
        event EventHandler<LoadingWorldEventArgs> LoadingWorld;

        /// <summary>
        /// Occurs when the world was loaded.
        /// </summary>
        event EventHandler<LoadedWorldEventArgs> LoadedWorld;

        /// <summary>
        /// Occurs when the world is saving.
        /// </summary>
        event EventHandler<SavingWorldEventArgs> SavingWorld;

        /// <summary>
        /// Occurs when the world was saved.
        /// </summary>
        event EventHandler<SavedWorldEventArgs> SavedWorld;

        /// <summary>
        /// Occurs when the world is starting hardmode.
        /// </summary>
        event EventHandler<StartingHardmodeEventArgs> StartingHardmode;

        /// <summary>
        /// Occurs when the world has started hardmode.
        /// </summary>
        event EventHandler<StartedHardmodeEventArgs> StartedHardmode;

        /// <summary>
        /// Occurs when the world is updating a tile in hardmode.
        /// </summary>
        event EventHandler<UpdatingHardmodeTileEventArgs> UpdatingHardmodeTile;

        /// <summary>
        /// Saves the world.
        /// </summary>
        void SaveWorld();
    }
}
