---
uid: tutorial_injecting_services
---

## Tutorial: Injecting Services

Some plugins might need to access more than just the information retrieved from events. For example, a plugin might want to loop through all players or NPCs to perform some sort of logic. Before Orion, this was done by reading and modifying Terraria's `static` state, like the `Terraria.Main.npc` or `Terraria.Main.player` arrays.

With Orion, however, this functionality is provided using *services*. Services essentially provide a certain type of functionality to plugins. Here is a list of the services currently defined in Orion:

| Service Name | Functionality |
|--------------|---------------|
| [`IChestService`](xref:Orion.Core.World.Chests.IChestService) | Terraria chests |
| [`IItemService`](xref:Orion.Core.Items.IItemService) | Terraria items |
| [`INpcService`](xref:Orion.Core.Npcs.INpcService) | Terraria NPCs |
| [`IPlayerService`](xref:Orion.Core.Players.IPlayerService) | Terraria players |
| [`IProjectileService`](xref:Orion.Core.Projectiles.IProjectileService) | Terraria projectiles |
| [`ISignService`](xref:Orion.Core.World.Signs.ISignService) | Terraria signs |
| [`IWorldService`](xref:Orion.Core.World.IWorldService) | Terraria world |

### Constructor Injection

To access instances of these services, plugins need to request them with [*constructor injection*](https://en.wikipedia.org/wiki/Dependency_injection). This involves adding the service interface as a parameter in the constructor. At runtime, the Orion framework will provide an implementation of the service. An example of constructor injection is below:

# [C#](#tab/c-sharp)

```csharp
    public ExamplePlugin(OrionKernel kernel, ILogger log,
        IItemService itemService) : base(kernel, log)
    {
        _itemService = itemService;
    }
```

***
