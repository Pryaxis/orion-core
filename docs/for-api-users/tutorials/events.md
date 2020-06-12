---
uid: tutorial_events
---

## Tutorial: Events

*Events* are the main form of communication between Orion and its plugins. They signal plugins that a certain action is occurring or has occurred. For example, the following are events:

| Description | Event Name |
|-------------|------------|
| A player is joining the server. | [`PlayerJoinEvent`](xref:Orion.Core.Events.Players.PlayerJoinEvent) |
| A player has quit the server. | [`PlayerQuitEvent`](xref:Orion.Core.Events.Players.PlayerQuitEvent) |
| A player is opening a chest. | [`ChestOpenEvent`](xref:Orion.Core.Events.World.Chests.ChestOpenEvent) |

## Event Handlers

*Event handlers* are used to run code on a given event. They are the main way for plugins to provide custom behavior. An example event handler is below:

# [C#](#tab/c-sharp)

```csharp
    [EventHandler("example", Priority = EventPriority.Lowest)]
    private void OnBlockBreak(BlockBreakEvent evt) {
        var world = evt.World;
        var player = evt.Player;
        var x = evt.X;
        var y = evt.Y;
        
        if (world[x, y].BlockId == BlockId.Hellstone) {
            player.SendTiles(x, y, world[x, y].Slice(x, y, 1, 1));
            evt.Cancel("example: canceled hellstone break");
        }
    }

```

***

This event handler cancels any player's attempt to break hellstone and sends the tiles back to them to keep the world consistent. The [`EventHandler`](xref:Orion.Core.Framework.EventHandlerAttribute) attribute specifies the event handler name (which should be unique among all of the event handlers for that type of event, and is used for logging/debugging purposes) and optionally the event handler priority, which specifies the relative ordering of event handlers for that type of event.

### Registering and Deregistering Event Handlers

Event handlers are registered and deregistered using the [`OrionKernel`](xref:Orion.Core.OrionKernel) class. The following methods register and deregister a single event handler:

* `RegisterHandler<TEvent>(Action<TEvent> handler, ILogger log)`
* `DeregisterHandler<TEvent>(Action<TEvent> handler, ILogger log)`

The following methods register and deregister event handlers en masse, using reflection to gather any methods marked with the [`EventHandler`](xref:Orion.Core.Framework.EventHandlerAttribute) attribute:

* `RegisterHandlers(object handlerObject, ILogger log)`
* `DeregisterHandlers(object handlerObject, ILogger log)`

The `log` parameter is used to log the registrations and deregistrations.



