---
uid: getting_started_api
---

## Getting Started

### Requirements

Before you begin, you should first install the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) -- Orion is built on .NET Core to provide cross-platform compatibility out of the box. You should also make sure that your IDE also supports C# 8 -- here are a few which do:
* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [Visual Studio Code](https://code.visualstudio.com/)
* [JetBrains Rider](https://www.jetbrains.com/rider/)

Once you have the SDK and an appropriate IDE, you should be good to go.

### Setting up your Project

To set up your plugin project, you should create a **.NET Standard 2.1 class library** project. You can do this via the project templates in your IDE, or with the following `dotnet` command:

```shell
    $ dotnet new classlib -f netstandard2.1 -n [<plugin name>]
```

### Adding Orion.Core

Once your project is set up, you should add the [Orion.Core](https://www.nuget.org/packages/Orion.Core) NuGet package to your project. You can do this via the IDE, or with the following `dotnet` command:

```shell
    $ dotnet add package Orion.Core
```

### [Optional] Directory Structure

Suppose your plugin is titled `Example`. If you have tests for the plugin and it is version-controlled via Git, the directory structure should look like the following:

```
    .
    +--- src/Example/
    |    +--- Example.csproj
    |    +--- ExamplePlugin.cs
    +--- tests/Example.Tests/
    |    +--- Example.Tests.csproj
    |    +--- ExamplePluginTests.cs
    +--- .editorconfig
    +--- .gitignore
    +--- Example.sln
```

Having your directory structure like this is entirely optional, but Orion and any "official" plugins will all adhere to this directory structure.
