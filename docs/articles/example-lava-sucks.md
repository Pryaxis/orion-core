## Example: Lava Sucks

Below is an example plugin which disables lava from spawning from hellstone blocks. This plugin ties together the following concepts:
* Service injection
* Event handling
* Packets

```csharp
using System;
using Orion;
using Orion.World;
using Orion.World.Tiles;
using Serilog;

[Plugin("lava-sucks", Author = "Pryaxis")]
public class LavaSucksPlugin : OrionPlugin {
    private readonly IItemService _itemService;
    private readonly IPlayerService _playerService;
    private readonly IWorldService _worldService;

    // Inject `Lazy<T>` instead of `T`. This is because if another plugin decides to
    // rebind `T`, we'll be able to see the change.
    public LavaSucksPlugin(
            OrionKernel kernel, ILogger log,
            IItemService itemService,
            IPlayerService playerService,
            IWorldService worldService) : base(kernel, log) {
        _itemService = itemService;
        _playerService = playerService;
        _worldService = worldService;
    }
    
    public override void Initialize() {
        // Register all of the event handlers contained within this class. In this
        // case, there is only one.
        Kernel.RegisterHandlers(this, Log);
    }
    
    public override void Dispose() {
        // Deregister all of the event handlers contained within this class. In this
        // case, there is only one.
        Kernel.DeregisterHandlers(this, Log);
    }
    
    [EventHandler("lava-sucks", Priority = EventPriority.Lowest)]
    private void OnBlockBreak(BlockBreakEvent evt) {
        var world = _worldService.World;
        if (world[evt.X, evt.Y].BlockId == BlockId.Hellstone) {
            // TODO: finish filling this out once the relevant APIs are available.
            evt.Cancel("lava-sucks: handling hellstone break");
        }
    }
}
```
