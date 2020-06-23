---
uid: index
---

## What is Orion?

Orion is a high-level API for Terraria dedicated servers that is based on [OTAPI](https://github.com/DeathCradle/Open-Terraria-API/). It serves as an abstraction on top of Terraria, and is designed to follow software best practices. Orion will serve as the base library for TShock v5.0 (codename: _alnitak_).

## Why use Orion?

There are many reasons why Orion will be an improvement over the current Terraria server API:

### .NET Core Compatibility

Orion is built using .NET Core 3.1, meaning that Orion is cross-platform compatible out of the box. No more fiddling with `xbuild` or having to install Mono -- that's all a thing of the past.

### Abstractability

Orion will provide abstractions for Terraria concepts to shield developers from Terraria's version changes. This is accomplished by providing interfaces (such as [`IPlayer`](xref:Orion.Players.IPlayer)) for developers to program against. The following table shows Terraria concepts along with their corresponding Orion abstractions:

| Terraria Concept | Orion Abstraction |
|------------------|-------------------|
| `Terraria.Chest` | [`IChest`](xref:Orion.Core.World.Chests.IChest) |
| `Terraria.Item` | [`IItem`](xref:Orion.Core.Items.IItem) |
| `Terraria.NetMessage` | [`IPacket`](xref:Orion.Core.Packets.IPacket) |
| `Terraria.NPC` | [`INpc`](xref:Orion.Core.Npcs.INpc) |
| `Terraria.Player` | [`IPlayer`](xref:Orion.Core.Players.IPlayer) |
| `Terraria.Projectile` | [`IProjectile`](xref:Orion.Core.Projectiles.IProjectile) |
| `Terraria.Sign` | [`ISign`](xref:Orion.Core.World.Signs.ISign) |
| `Terraria.Tile` | [`Tile`](xref:Orion.Core.World.Tiles.Tile) |
| `Terraria.Main.tile` | [`IWorld`](xref:Orion.Core.World.IWorld) |

### Flexibility

Orion allows different implementations for the interfaces defined above, which provides for much greater control and flexibility than was previously possible.

For example, an InfiniteChests plugin could provide its own [`IChest`](xref:Orion.Core.World.Chests.IChest) implementation which uses a database-backed storage solution. This new [`IChest`](xref:Orion.Core.World.Chests.IChest) implementation would then immediately be available to other consumers of the [`IChest`](xref:Orion.Core.World.Chests.IChest) interface, which would allow other chest-based plugins (such as chest protection plugins) to work seamlessly with InfiniteChests.

### Testability

Orion and consumers are much more testable due to the interfaces. Since consumers no longer interact directly with Terraria state (which uses a bunch of nasty `static` state!) and instead interact with interfaces, mocks can be used for testing purposes.

This will allow the codebase to be fully tested, which should improve overall code quality and developer happiness.

### Better APIs

Orion will have better APIs than the current Terraria server API. Here is a non-exhaustive list of API improvements:

* Events are easier to register and deregister now (with different priorities, too!).
* Plugins can act as both subscribers _and_ publishers of any event.
* Logging is now handled in a structured manner via [Serilog](https://serilog.net/).
* Packets are now typed and implemented as `struct`s so that they can be both safe and blazing fast.

### Other Improvements

Here is a non-exhaustive list of other planned improvements:

* Unloadable plugins. This means that hot reloading of _some_ plugins (i.e., plugins which do not provide interface overridings) is viable.
* Resource strings. This means that localization is possible.

## Developers

To get started with developing on Orion, please take a look at the [Getting Started](xref:getting_started_devs) page. If you wish to contribute to the project, please take a look at the [Contributing Guidelines](xref:contributing_guidelines) page, as well.

## API Users

To get started with developing for Orion, please take a look at the [Getting Started](xref:getting_started_api) page.
