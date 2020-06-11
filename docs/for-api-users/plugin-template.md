---
uid: plugin_template
---

## Plugin Template

If you followed the [Getting Started](xref:getting_started_api) page, then you should have a project set up for your plugin. Replace the auto-generated `Class1.cs` file with the following file (titled `ExamplePlugin.cs`):

# [C#](#tab/c-sharp)

```csharp
using System;
using Orion.Core;
using Orion.Core.Framework;
using Serilog;

namespace Example {
    [Plugin("example", Author = "Pryaxis")]
    public sealed class ExamplePlugin : OrionPlugin {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExamplePlugin"/> class.
        /// This constructor gets called using 
        /// </summary>
        public ExamplePlugin(OrionKernel kernel, ILogger log)
            : base(kernel, log) { }

        /// <summary>
        /// Initializes the plugin. This is where you perform initialization
        /// logic, such as registering event handlers, reading configurations,
        /// etc.
        /// </summary>
        public override void Initialize() {
            Console.WriteLine("Hello, world!");
        }

        /// <summary>
        /// Disposes the plugin. This is where you perform "reverse"
        /// initialization logic, such as deregistering event handlers, writing
        /// configurations, etc.
        /// </summary>
        public override void Dispose() {
            Console.WriteLine("Goodbye, world!");
        }
    }
}
```

***

## Using your Plugin

Once you've replaced the file, build your project in debug mode. In the `bin/Debug/netstandard2.1/` directory, copy the generated `Example.dll` file to the `plugins/` directory where the Orion launcher is located. The plugin will now take effect upon server startup.
