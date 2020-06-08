---
uid: architecture_services
---

## Architecture: Services

### Overview

_Services_ in Orion are self-contained modules which provide a specific type of functionality to consumers.

Each service is split into an interface and an implementation. The interface defines the service's contract, and the implementation must adhere to that contract. This pattern allows consumers to program against an interface and not worry about any implementation details.

Service interfaces are bound to implementations using [`BindingAttribute`](xref:Orion.Framework.BindingAttribute), but consumers may define higher priority bindings to replace the default implementations.

To request services, consumers should use [constructor injection](https://en.wikipedia.org/wiki/Dependency_injection).

### Defining a Service

A service can be defined as follows:
```csharp
    [Service(ServiceScope.Singleton)]
    public interface IMyService {
        void DoSomething();
    }
```

That service must then be implemented and bound:
```csharp
    [Binding("my-service", Author = "Pryaxis", Priority = BindingPriority.Lowest)]
    internal sealed class MyServiceImpl : OrionService, IMyService {
        public MyServiceImpl(
            OrionKernel kernel, ILogger log) : base(kernel, log) { }

        public void DoSomething() {
            Console.WriteLine("Test");
        }
    }
```

Orion will then automatically bind `IMyService` to `MyServiceImpl`, and any plugins that request `IMyService` will receive a singleton `MyServiceImpl` instance.

Here are the possible service scopes:

| Scope | Result | Rationale | Example |
|-------|--------|-----------|---------|
| `ServiceScope.Singleton` | Only a single instance of the service is ever created. | Service is functionally "`static`". | `INpcService` should be singleton, since it deals with the static `Terraria.Main.npc` array. |
| `ServiceScope.Transient` | A new instance of the service is created each time. | Service is functionally instanced. | Configuration services should be transient, since there may be multiple configurations. |
