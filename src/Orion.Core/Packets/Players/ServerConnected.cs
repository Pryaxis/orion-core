using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent from the server to the client upon initial spawn. Instructs the client to set the UI scale and
    /// process world information.
    /// </summary>
    public struct ServerConnected : IPacket
    {
        PacketId IPacket.Id => PacketId.ServerConnected;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => 0;

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => 0;
    }
}
