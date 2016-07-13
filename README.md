# Orion

Orion is the next generation of Terraria Server APIs.

Orion is a modern, cross-platform, service-oriented library which makes it easy to write server plugins for Terraria, and 
serves as the API replacement for TShock v5.

It sits in between Terraria server plugins and Terraria itself, and serves as an abstraction layer between the plugin and
TerrariaServer itself.  With it, plugin authors will be able to interact with Terraria components on a higher level, saving
the amount of effort required to extend Terraria Server's functionality

### Building Orion

Orion is based on DNX projects, and we recommend using [.NET Core](https://www.microsoft.com/net/core), or Visual Studio 2015
Update 3 to easily build the project.

#### Building using `dotnet` CLI

1. Clone the code from the Orion repo
2. cd into the `Orion` directory
3. Run `dotnet restore` to restore NuGet packages
4. Run `dotnet build`

### Running Orion

`dotnet run -p Orion.Console`
