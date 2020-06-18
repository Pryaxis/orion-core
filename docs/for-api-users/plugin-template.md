---
uid: plugin_template
---

## Plugin Template

If you followed the [Getting Started](xref:getting_started_api) page, then you should have a project set up for your plugin. Replace the auto-generated `Class1.cs` file with the following file (titled `ExamplePlugin.cs`):

# [C#](#tab/c-sharp)

```csharp
using System;
using Orion.Core;
using Orion.Core.Framework.Extensions;
using Serilog;

namespace Example
{
    [Plugin("example", Author = "Pryaxis")]
    public sealed class ExamplePlugin : OrionExtension
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExamplePlugin"/> class.
        /// This constructor gets called using a dependency injection container.
        ///
        /// This is where you perform initialization logic, such as registering
        /// event handlers, reading configurations, etc.
        /// </summary>
        public ExamplePlugin(OrionKernel kernel, ILogger log)
            : base(kernel, log)
        {
            Console.WriteLine("Hello, world!");
        }

        /// <summary>
        /// Disposes the plugin. This is where you perform "reverse"
        /// initialization logic, such as deregistering event handlers, writing
        /// configurations, etc.
        /// </summary>
        public override void Dispose()
        {
            Console.WriteLine("Goodbye, world!");
        }
    }
}
```

***

&nbsp;

This plugin simply prints `Hello, world!` and `Goodbye, world!` upon initialization and disposal, respectively. The [`Plugin`](xref:Orion.Core.Framework.Extensions.PluginAttribute) attribute specifies the plugin name (which should be unique among all plugins, and is used for logging/debugging purposes) and optionally the plugin author.

## Using your Plugin

Once you've replaced the file, build your project in debug mode. In the `bin/Debug/netstandard2.1/` directory, copy the generated `Example.dll` file to the `plugins/` directory where the Orion launcher is located. The plugin will now take effect upon server startup.

## Behind the Scenes

Orion instantiates all plugins using [Ninject](https://www.nuget.org/packages/Ninject) to gather any of the plugin's dependencies. Here is a rough sketch of how this is done (with the Orion launcher):

1. The `plugins/` directory is scanned for .NET assemblies. For each .NET assembly:
    1. All service interfaces are loaded.
    2. All service bindings are loaded, and for each interface, the binding with the highest [`BindingPriority`](xref:Orion.Core.Framework.BindingPriority) is used.
    3. All plugin types are loaded.
2. Each service interface is bound to a single service binding via Ninject, with the scope specified by the [`Service`](xref:Orion.Core.Framework.ServiceAttribute) attribute placed on the service interface.
3. Each plugin is constructed via Ninject.

This allows plugins to define overriding service bindings, and to have dependencies on other plugins.
