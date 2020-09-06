using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Core.Packets.Players
{
    /// <summary>
    /// A packet sent from the server to client in order to spawn said client after the handshake.
    /// </summary>
    public struct PlayerEnter : IPacket
    {
        PacketId IPacket.Id => PacketId.PlayerEnter;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => 0;

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => 0;
    }
}
