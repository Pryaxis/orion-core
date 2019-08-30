namespace Orion.Networking.Packets {
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Represents a Terraria packet.
    /// </summary>
    public abstract class TerrariaPacket {
        private static readonly Dictionary<TerrariaPacketType, Func<BinaryReader, TerrariaPacket>> Deserializers =
            new Dictionary<TerrariaPacketType, Func<BinaryReader, TerrariaPacket>> {
                [TerrariaPacketType.ConnectionRequest] = br => new ConnectionRequestPacket(br),
                [TerrariaPacketType.Disconnect] = br => new DisconnectPacket(br),
            };

        /// <summary>
        /// Gets the length of a packet header.
        /// </summary>
        internal static int HeaderLength => sizeof(TerrariaPacketType) + sizeof(short);

        private protected abstract int HeaderlessLength { get; }

        /// <summary>
        /// Gets a value indicating whether the packet is sent to the client.
        /// </summary>
        public abstract bool IsSentToClient { get; }

        /// <summary>
        /// Gets a value indicating whether the packet is sent to the server.
        /// </summary>
        public abstract bool IsSentToServer { get; }

        /// <summary>
        /// Gets the type of the packet.
        /// </summary>
        public abstract TerrariaPacketType Type { get; }

        /// <summary>
        /// Reads a packet from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <returns>The packet.</returns>
        public static TerrariaPacket FromStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                var packetLength = reader.ReadInt16();
                var packetType = (TerrariaPacketType)reader.ReadByte();

                if (Deserializers.TryGetValue(packetType, out var deserializer)) {
                    return deserializer(reader);
                } else {
                    return new UnknownPacket(reader, packetType, packetLength);
                }
            }
        }

        /// <summary>
        /// Writes the packet to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        public void WriteToStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true)) {
                writer.Write((short)(HeaderLength + HeaderlessLength));
                writer.Write((byte)Type);
                WriteToWriter(writer);
            }
        }

        private protected abstract void WriteToWriter(BinaryWriter writer);
    }
}
