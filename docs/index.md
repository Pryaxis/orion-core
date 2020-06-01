## What is Orion?

Orion is a high-level API for Terraria dedicated servers that is based on [OTAPI](https://github.com/DeathCradle/Open-Terraria-API/). It serves as an abstraction on top of Terraria, and is designed to follow software best practices. Orion will serve as the base library for TShock v5.0.

### Why the change?

By serving as an abstraction on top of Terraria, Orion aims to shield developers from Terraria's version changes. This is accomplished by providing interfaces, such as `IPlayer`, for developers to write against. That way, if any changes are made to the `Terraria.Player` class, only Orion has to update for all of the changes to be reflected downstream.

Additionally, this allows plugins to provide different implementations for the interfaces, which allows much greater control and flexibility than was previously offered. For example, an InfiniteChests-esque plugin could provide its own `IChest` implementations which use a database-backed storage solution.

Orion is also a lot more testable than any previous API before due to following correct software practices. This greatly cuts down on the number of bugs we expect Orion to have, and should be a boon for any developer of Orion or any of its plugins. These testability improvements are also conferred onto any custom services and plugins, which should immensely help developers.

## Project Status

Project Orion is close to a state where concurrent development can be done alongside it.

### Developers

To get started with Orion, please read and be familiar with the [[Orion Architecture]]. Please take a look at the [[Examples]], as well. If you wish to contribute to the project, please read the [Contributing Guide](https://github.com/NyxStudios/Orion/wiki/Contributing).
