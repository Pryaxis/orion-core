## What is Orion?

Orion is a high-level API for Terraria dedicated servers that is based on [OTAPI](https://github.com/DeathCradle/Open-Terraria-API/). It serves as an abstraction on top of Terraria, and is designed to follow software best practices. Orion will serve as the base library for TShock v5.0.

## Why use Orion?

There are many reasons why Orion will be an improvement over the current Terraria server API.

### Abstractions
By serving as an abstraction on top of Terraria, Orion aims to shield developers from Terraria's version changes. This is accomplished by, e.g., providing interfaces ( such as [`IPlayer`](http://pryaxis.github.io/orion/api/Orion.Players.IPlayer.html)) for developers to program against. That way, changes made to Terraria only need to be handled within Orion. The following table shows Terraria types along with their corresponding Orion abstractions:

| Terraria Type | Orion Abstraction |
|---------------|-------------------|
| `Terraria.Chest` | &lt;TBD&gt; |
| `Terraria.Item` | [`IItem`](https://pryaxis.github.io/orion/api/Orion.Items.IItem.html) |
| `Terraria.NetMessage` | [`IPacket`](https://pryaxis.github.io/orion/api/Orion.Packets.IPacket.html) |
| `Terraria.NPC` | [`INpc`](https://pryaxis.github.io/orion/api/Orion.Npcs.INpc.html) |
| `Terraria.Player` | [`IPlayer`](https://pryaxis.github.io/orion/api/Orion.Players.IPlayer.html) |
| `Terraria.Projectile` | &lt;TBD&gt; |
| `Terraria.Sign` | &lt;TBD&gt; |
| `Terraria.Tile` | [`Tile`](https://pryaxis.github.io/orion/api/Orion.World.Tiles.Tile.html) |


### Flexibility
One benefit of the abstraction mechanism is that plugins can provide different implementations for the interfaces, which allows much greater control and flexibility than was previously possible.

For example, an InfiniteChests plugin could provide its own `IChest` implementation which uses a database-backed storage solution. This new `IChest` implementation would then immediately be available to other consumers of the `IChest` interface, which would allow other chest-based plugins (such as chest protection plugins) to work seamlessly with InfiniteChests.

### Testability
Another benefit of the abstraction mechanism is that the Orion API and consumers are completely testable. Since consumers no longer interact directly with Terraria state (which uses a bunch of nasty `static` state!) and instead interact with interfaces, more code can now be tested with the help of mocks.

This will allow the codebase to be fully tested, which should improve overall code quality and developer happiness.

### Better APIs
Orion will have better APIs than the current Terraria server API. Here is a non-exhaustive list of API improvements:
* Events are easier to register and deregister now (with different priorities, too!).

  Before:
  ```csharp
  ServerApi.Hooks.ServerConnect.Register(this, OnConnect);
  ServerApi.Hooks.ServerJoin.Register(this, OnJoin);
  ServerApi.Hooks.ServerLeave.Register(this, OnLeave);
  
  private void OnConnect(ConnectEventArgs args) { }
  private void OnJoin(JoinEventArgs args) { }
  private void OnLeave(LeaveEventArgs args) { }
  ```
  
  After:
  ```csharp
  Kernel.Register(this, Log);
  
  [EventHandler(EventPriority.Lowest)]
  private void OnPlayerConnect(PlayerConnectEvent evt) { }
  [EventHandler(EventPriority.Lowest)]
  private void OnPlayerJoin(PlayerJoinEvent evt) { }
  [EventHandler(EventPriority.Lowest)]
  private void OnPlayerQuit(PlayerQuitEvent evt) { }
  ```
* Plugins can act as both subscribers _and_ publishers of any event.
* Logging is now handled in a structured manner via [Serilog](https://serilog.net/).
* Packets are now typed and implemented as `struct`s so that they can be both safe and blazing fast.

### Other Improvements
Here is a non-exhaustive list of other improvements:
* Plugins will now be unloadable (since they should not declare `static` state). This means that hot reloading is now viable.
* Orion will use resource strings. This means that localization will now be possible.

## Project Status

Orion is close to a state where concurrent development can be done alongside it. See [here](https://github.com/Pryaxis/orion/issues/55) for more information.

### Developers

To get started with Orion, please read and be familiar with the [[Orion Architecture]]. Please take a look at the [[Examples]], as well. If you wish to contribute to the project, please read the [Contributing Guide](https://github.com/NyxStudios/Orion/wiki/Contributing).
