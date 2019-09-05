using System;
using System.Runtime.InteropServices;
using Orion.Framework;
using Orion.World.Events;
using Orion.World.Tiles;
using OTAPI;
using OTAPI.Tile;

namespace Orion.World {
    internal sealed unsafe class OrionWorldService : OrionService, IWorldService, ITileCollection {
        private byte* _tilesPtr = null;

        public override string Author => "Pryaxis";
        public override string Name => "Orion World Service";

        public string WorldName { get; set; }

        public int WorldWidth { get; private set; }
        public int WorldHeight { get; private set; }

        public Tile this[int x, int y] {
            get {
                /*
                 * Lazily initialize _tilesPtr. This allows us to use less memory if the world size is Small or Medium;
                 * however, this comes at the cost of a null check for every Tile access.
                 */
                if (_tilesPtr == null) {
                    WorldWidth = Terraria.Main.maxTilesX;
                    WorldHeight = Terraria.Main.maxTilesY;

                    /*
                     * Allocate with AllocHGlobal so that the memory is pre-pinned. We could also use a GCHandle here,
                     * but this is just simpler.
                     */
                    _tilesPtr = (byte*)Marshal.AllocHGlobal(OrionTile.ByteCount * (WorldWidth + 1) * (WorldHeight + 1));
                }
                return new OrionTile(_tilesPtr + OrionTile.ByteCount * ((WorldWidth + 1) * y + x));
            }
            set => ((ITileCollection)this)[x, y] = value;
        }

        int ITileCollection.Width => WorldWidth;
        int ITileCollection.Height => WorldHeight;

        ITile ITileCollection.this[int x, int y] {
            get => this[x, y];
            set {
                var location = new OrionTile(_tilesPtr + OrionTile.ByteCount * ((WorldWidth + 1) * y + x));
                ((ITile)location).CopyFrom(value);
            }
        }

        public bool IsDaytime {
            get => Terraria.Main.dayTime;
            set => Terraria.Main.dayTime = value;
        }

        public bool IsHardmode {
            get => Terraria.Main.hardMode;
            set => Terraria.Main.hardMode = value;
        }

        public bool IsExpertMode {
            get => Terraria.Main.expertMode;
            set => Terraria.Main.expertMode = value;
        }

        public event EventHandler<CheckingHalloweenEventArgs> CheckingHalloween;
        public event EventHandler<CheckingChristmasEventArgs> CheckingChristmas;
        public event EventHandler<LoadingWorldEventArgs> LoadingWorld;
        public event EventHandler<LoadedWorldEventArgs> LoadedWorld;
        public event EventHandler<SavingWorldEventArgs> SavingWorld;
        public event EventHandler<SavedWorldEventArgs> SavedWorld;
        public event EventHandler<StartingHardmodeEventArgs> StartingHardmode;
        public event EventHandler<StartedHardmodeEventArgs> StartedHardmode;
        public event EventHandler<UpdatingHardmodeTileEventArgs> UpdatingHardmodeTile;

        public OrionWorldService() {
            Hooks.Tile.CreateCollection = () => this;
            Hooks.Game.Halloween = HalloweenHandler;
            Hooks.Game.Christmas = ChristmasHandler;
            Hooks.World.IO.PreLoadWorld = PreLoadWorldHandler;
            Hooks.World.IO.PostLoadWorld = PostLoadWorldHandler;
            Hooks.World.IO.PreSaveWorld = PreSaveWorldHandler;
            Hooks.World.IO.PostSaveWorld = PostSaveWorldHandler;
            Hooks.World.PreHardmode = PreHardmodeHandler;
            Hooks.World.PostHardmode = PostHardmodeHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            Marshal.FreeHGlobal((IntPtr)_tilesPtr);
            if (!disposeManaged) {
                return;
            }

            Hooks.Tile.CreateCollection = null;
            Hooks.Game.Halloween = null;
            Hooks.Game.Christmas = null;
            Hooks.World.IO.PreLoadWorld = null;
            Hooks.World.IO.PostLoadWorld = null;
            Hooks.World.IO.PreSaveWorld = null;
            Hooks.World.IO.PostSaveWorld = null;
            Hooks.World.PreHardmode = null;
            Hooks.World.PostHardmode = null;
        }

        public void SaveWorld() => Terraria.IO.WorldFile.saveWorld();


        private HookResult HalloweenHandler() {
            var args = new CheckingHalloweenEventArgs();
            CheckingHalloween?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult ChristmasHandler() {
            var args = new CheckingChristmasEventArgs();
            CheckingChristmas?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private HookResult PreLoadWorldHandler(ref bool loadFromCloud) {
            var args = new LoadingWorldEventArgs();
            LoadingWorld?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostLoadWorldHandler(bool loadFromCloud) {
            var args = new LoadedWorldEventArgs();
            LoadedWorld?.Invoke(this, args);
        }

        private HookResult PreSaveWorldHandler(ref bool useCloudSaving, ref bool resetTime) {
            var args = new SavingWorldEventArgs {ShouldResetTime = resetTime};
            SavingWorld?.Invoke(this, args);
            
            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostSaveWorldHandler(bool useCloudSaving, bool resetTime) {
            var args = new SavedWorldEventArgs {ShouldResetTime = resetTime};
            SavedWorld?.Invoke(this, args);
        }

        private HookResult PreHardmodeHandler() {
            var args = new StartingHardmodeEventArgs();
            StartingHardmode?.Invoke(this, args);

            return args.Handled ? HookResult.Cancel : HookResult.Continue;
        }

        private void PostHardmodeHandler() {
            var args = new StartedHardmodeEventArgs();
            StartedHardmode?.Invoke(this, args);
        }
    }
}
