==================
Orion
==================

Orion is a plugin that serves as the base API library for TShock v5.0 which uses [Open Terraria API](https://github.com/DeathCradle/Open-Terraria-API) as a server backend.

Orion is written in C#6.0 and thus requires Visual Studio 2015, or Xamarin Studio to compile and run.

## Features

* Modular design and plugin support
* Automatic transparent plugin configuration
* Revamped command system
* Revamped user, group and permission database

## Compiling

* Clone the Orion repository with `git clone https://github.com/NyxStudios/Orion.git`
* Update submodules with `git submodule update --init --recursive`

> **Note:**
> 
> In Visual Studio 2015, there is an issue where the submodule's dependencies don't download properly leading to compilation issues. To fix this, follow these steps:
> * In Visual Studio 2015, open the package manager console
> * Select `API` as the default project in the package manager console window
> * Run `Update-Package -Reinstall`
> The solution should now build.



