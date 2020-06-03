---
uid: architecture_services
---

## Architecture: Services

### Overview

_Services_ in Orion are self-contained modules which provide a specific type of functionality to consumers.

Each service is split into an interface and an implementation. The interface defines the service's contract, and the implementation must adhere to that contract. This pattern allows consumers to program against an interface and not worry about any implementation details.

Service interfaces are bound to default implementations inside of the `OrionKernel` constructor, but consumers may re-bind the interfaces to their own implementations with the following:
```csharp
    Kernel.Container.Bind<INpcService>().To<MyNpcService>().InSingletonScope();
```

To request services, consumers should use the [dependency injection pattern](https://en.wikipedia.org/wiki/Dependency_injection). The usage of `Lazy<T>` allows other consumers to get the chance to re-bind `T` if necessary. If just `T` were used, then the default implementation would always be requested.
```csharp
    public MyPlugin(
        OrionKernel kernel, ILogger log,
        Lazy<INpcService> npcService) : base(kernel, log) { }
```

### Defining a Service

A service can be defined as follows:
```csharp
    public interface IMyService {
        void DoSomething();
    }
```

That service must then be implemented:
```csharp
    [Service("my-service", Author = "???")]
    internal sealed class MyServiceImpl : OrionService, IMyService {
        public MyServiceImpl(
            OrionKernel kernel, ILogger log) : base(kernel, log) { }

        public void DoSomething() {
            Console.WriteLine("Test");
        }
    }
```

The service interface must then be bound to the implementation somehow. This can technically be done at any time, but is usually done in the constructor of a plugin:
```csharp
    Kernel.Container.Bind<IMyService>().To<MyServiceImpl>().In???Scope();
```

Here are a few of the possible scopes:

| Scope | Result | Rationale | Example |
|-------|--------|-----------|---------|
| `Singleton` | Only a single instance of the service is ever created. | Service is functionally "`static`". | `INpcService` should be singleton, since it deals with the static `Terraria.Main.npc` array. |
| `Transient` | A new instance of the service is created each time. | Service has instance state. | Configuration services should be transient, since there may be multiple configurations. |
