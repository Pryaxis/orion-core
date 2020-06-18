---
uid: tutorial_events
---

## Tutorial: Events

*Events* are the main form of communication between Orion and its plugins. They signal plugins that a certain action is occurring or has occurred. 

## Event Handlers

*Event handlers* are used to run code on a given event. They are the main way for plugins to provide custom behavior. An example event handler is below:

# [C#](#tab/c-sharp)

```csharp
    [EventHandler("example", Priority = EventPriority.Lowest)]
    private void OnBlockBreak(BlockBreakEvent evt)
    {
        var world = evt.World;
        var player = evt.Player;
        var x = evt.X;
        var y = evt.Y;
        
        if (world[x, y].BlockId == BlockId.Hellstone)
        {
            world[x, y].IsBlockActive = false;
            _itemService.SpawnItem(
                new ItemStack(ItemId.HellStone), new Vector2f(16 * x, 16 * y));
            _playerService.BroadcastTiles(x, y, world.Slice(x, y, 1, 1));
            evt.Cancel("example: handled hellstone break");
        }
    }
```

***

&nbsp;

This event handler "breaks" hellstone without triggering any lava from the block. The [`EventHandler`](xref:Orion.Core.Framework.Events.EventHandlerAttribute) attribute specifies the event handler name (which should be unique among all of the event handlers for that type of event, and is used for logging/debugging purposes) and optionally the event handler priority, which specifies the relative ordering of event handlers for that type of event.

### Event Handler Priorities

These are the possible event handler priorities:

| Order | Event Handler Priority | Example Use |
|-------|------------------------|-------------|
| 1. | `EventPriority.Highest` | A plugin wants to filter malformed events. |
| 2. | `EventPriority.High` | |
| 3. | `EventPriority.Normal` | A plugin wants to perform less-critical event filtering. |
| 4. | `EventPriority.Low` | |
| 5. | `EventPriority.Lowest` | A plugin wants to be the "sink" for an event. |

## Asynchronous Event Handlers

Event handlers may also be *asynchronous* and either *blocking* (the default) or *non-blocking*:

| Type | Effect | Restrictions |
|------|--------|--------------|
| Blocking | If the event is finished, the event handler *must* be completed.| Only thread-safe Orion APIs may be used during asynchronous execution. |
| Non-blocking | If the event is finished, the event handler *may not* be completed. | Only thread-safe Orion APIs __which do not interface with Terraria state__ may be used during asynchronous execution. |

An example asynchronous event handler is below:

# [C#](#tab/c-sharp)

```csharp
    [EventHandler("example-async", IsBlocking = false)]
    private async Task OnPlayerQuit(PlayerQuitEvent evt)
    {
        var fileName = Path.Join("players", $"{evt.Player.Name}.data");
        using (var stream = File.Create(fileName))
        using (var writer = new StreamWriter(stream))
        {
            await writer.WriteAsync("test").ConfigureAwait(false);
        }
    }
```

***

&nbsp;

This event handler asynchronously writes a player's data file when they quit. Note that even though the `IPlayer.Name` property is accessed (which interfaces with Terraria state), this example is thread-safe because the asynchronous execution only begins with `WriteAsync`. 

### Registering and Deregistering Event Handlers

Event handlers are registered and deregistered using the [`OrionKernel`](xref:Orion.Core.OrionKernel) class. The following methods register and deregister a single event handler:

* `RegisterHandler<TEvent>(Action<TEvent> handler, ILogger log)`
* `RegisterAsyncHandler<TEvent>(Func<TEvent, Task> handler, ILogger log)`
* `DeregisterHandler<TEvent>(Action<TEvent> handler, ILogger log)`
* `DeregisterAsyncHandler<TEvent>(Func<TEvent, Task> handler, ILogger log)`

The following methods register and deregister event handlers en masse, using reflection to gather any methods marked with the [`EventHandler`](xref:Orion.Core.Framework.Events.EventHandlerAttribute) attribute:

* `RegisterHandlers(object handlerObject, ILogger log)`
* `DeregisterHandlers(object handlerObject, ILogger log)`

The `log` parameter is used to log the registrations and deregistrations.

