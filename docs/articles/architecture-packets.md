---
uid: architecture_packets
---

## Architecture: Packets

### Overview

_Packets_ in Orion act as the main form of communication between the server and its clients.

Each packet is documented in the [multiplayer packet structure](https://tshock.readme.io/docs/multiplayer-packet-structure) document, and is responsible for synchronizing server state with client state.

These packets are represented as `structs`s in Orion which can be read from or written to a span of bytes. By having the `struct`s represent the actual packet payloads as closely as possible, the packets can be efficiently manipulated.

### Defining a Packet

A packet can be defined and implemented as follows:

```csharp
    public struct ExamplePacket : IPacket {
        PacketId IPacket.Id => ...;

        public int Read(Span<byte> span, PacketContext context) {
            // ...
        }

        public int Write(Span<byte> span, PacketContext context) {
            // ...
        }
    }
```

The `Id` property is usually implemented explicitly, as the type of the packet already gives us the ID information.

Once a packet structure has been defined (along with a corresponding `PacketId`), the mappings in `PacketIdExtensions` must be modified in a suitable manner. The `OrionPlayerService` implementation should also be modified to recognize the new packet and generate corresponding events, if applicable.
