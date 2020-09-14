using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.World
{
    /// <summary>
    /// A packet sent to update pillar shield strengths.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 8)]
    public struct PillarShieldStrengths : IPacket
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the Solar pillar's shield strength.
        /// </summary>
        [field: FieldOffset(0)] public ushort SolarShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the Vortex pillar's shield strength.
        /// </summary>
        [field: FieldOffset(2)] public ushort VortexShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the Nebula pillar's shield strength.
        /// </summary>
        [field: FieldOffset(4)] public ushort NebulaShieldStrength { get; set; }

        /// <summary>
        /// Gets or sets the Stardust pillar's shield strength.
        /// </summary>
        [field: FieldOffset(6)] public ushort StardustShieldStrength { get; set; }

        PacketId IPacket.Id => PacketId.PillarShieldStrengths;

        int IPacket.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 8);

        int IPacket.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 8);
    }
}
