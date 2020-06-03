---
uid: getting_started
---

## Getting Started

### Requirements
Before you begin, first install the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) -- Orion is built on .NET Core to provide cross-platform compatibility out of the box. You should also make sure that your IDE also supports C# 8 -- here are a few which do:
* [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
* [Visual Studio Code](https://code.visualstudio.com/)
* [JetBrains Rider](https://www.jetbrains.com/rider/)

Once you have the SDK and an appropriate IDE, you should be good to go.

### Checking out the Code
Using Git, clone the [Orion repository](https://github.com/Pryaxis/orion). See [here](https://help.github.com/en/github/creating-cloning-and-archiving-repositories/cloning-a-repository) for instructions on how to do so.

You must now restore the projects' dependencies. On Windows (with Visual Studio, at least), this will be done automatically for you. To do this manually, you can run the following command in the path of the cloned repository:
```shell
    $ dotnet restore 
```

You should now be able to build Orion and run the launcher!
