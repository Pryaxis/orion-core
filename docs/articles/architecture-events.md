---
uid: architecture_events
---

## Architecture: Events

### Overview

_Events_ in Orion act as the form of communication between Orion and its plugins.

Each event has some number of _publishers_ and _subscribers_. The publishers generate the events, and the subscribers can handle events as needed. Events can be raised and event handlers can be registered and deregistered with an [`OrionKernel`](xref:Orion.OrionKernel) instance.

Some events implement the [`ICancelable`](xref:Orion.Events.ICancelable) interface, and these events can be canceled using the [`Cancel`](xref:Orion.Events.CancelableExtensions.Cancel(Orion.Events.ICancelable,System.String)) extension method.

### Priorities

Each event handler has the notion of _priority_ attached to them. When an event is raised, the event handlers are run in the following order:

| Order | Priority | Example |
|-------|----------|---------|
| 1. | `EventPriority.Highest` | A plugin wants to perform cheat protections. They get to perform sanity checks first and cancel the event so that the cheating is ignored by later event handlers. |
| 2. | `EventPriority.High` | |
| 3. | `EventPriority.Normal` | |
| 4. | `EventPriority.Low` | |
| 5. | `EventPriority.Lowest` | |
| 6. | `EventPriority.Monitor` | A plugin wants to monitor players joining the server. They are guaranteed to know the final state of whether the player is _actually_ joining the server (as other earlier event handlers may cancel the event). |

Note that there are multiple interpretations of "high priority". A higher priority event handler gets to act first on an event, but the lower priority event handler gets to modify the event last.

### Defining an Event

An event can be defined as follows:

```csharp
    [Event("my-event", LoggingLevel = LogEventLevel.Debug)]
    public class MyEvent : Event {
        public int Value { get; set; }
    }
```

This event can then be raised via an [`OrionKernel`](xref:Orion.OrionKernel) instance:

```csharp
    Kernel.Raise(new MyEvent { Value = 100 }, Log);
```

### Defining an Event Handler

An event handler can be defined as follows:

```csharp
    [EventHandler("my-event-handler", Priority = EventPriority.Monitor)]
    private void MyEventHandler(MyEvent evt) {
        Console.WriteLine(evt.Value);
    }
```

The handler must then be registered via an [`OrionKernel`](xref:Orion.OrionKernel) instance:

```csharp
    Kernel.RegisterHandler(MyEventHandler, Log);
```

It must then be deregistered when the handler owner is being disposed:

```csharp
    Kernel.DeregisterHandler(MyEventHandler, Log);
```

Event handlers can be registered and deregistered en masse using the [`RegisterHandlers`](xref:Orion.OrionKernel.RegisterHandlers(System.Object,ILogger)) and [`DeregisterHandlers`](xref:Orion.OrionKernel.DeregisterHandlers(System.Object,ILogger)) methods, which register and deregister all methods marked with [`EventHandlerAttribute`](xref:Orion.Events.EventHandlerAttribute), respectively.
