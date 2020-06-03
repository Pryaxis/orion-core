## Lava Sucks

Below is an example plugin which disables lava from spawning from hellstone blocks. This plugin ties together the following concepts:
* Lazy constructor injection
* Events
* Packets

```csharp
using System;
using Orion;
using Orion.World;
using Orion.World.Tiles;
using Serilog;

[Service("lava-sucks", Author = "Pryaxis")]
public class LavaSucksPlugin : OrionPlugin {
    private readonly Lazy<IItemService> _itemService;
    private readonly Lazy<IPlayerService> _playerService;
    private readonly Lazy<IWorldService> _worldService;

    // Inject `Lazy<T>` instead of `T`. This is because if another plugin decides to rebind `T`,
    // we'll be able to see the change.
    public LavaSucksPlugin(
            OrionKernel kernel, ILogger log,
            Lazy<IItemService> itemService,
            Lazy<IPlayerService> playerService,
            Lazy<IWorldService> worldService) : base(kernel, log) {
        _itemService = itemService;
        _playerService = playerService;
        _worldService = worldService;
    }
    
    public void override Initialize() {
        // Register all of the event handlers contained within this class. In this case, there is
        // only one.
        Kernel.RegisterHandlers(this, Log);
    }
    
    public void override Dispose() {
        // Deregister all of the event handlers contained within this class. In this case, there is
        // only one.
        Kernel.DeregisterHandlers(this, Log);
    }
    
    [EventHandler(EventPriority.Lowest, "lava-sucks")]
    private void OnBlockBreak(BlockBreakEvent evt) {
        var world = _worldService.Value.World;
        if (world[evt.X, evt.Y].BlockId == BlockId.Hellstone) {
            // TODO: finish filling this out once the relevant APIs are available.
            evt.Cancel("lava-sucks: handling hellstone break");
        }
    }
}
```
