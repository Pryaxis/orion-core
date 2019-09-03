using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Represents a Terraria packet.
    /// </summary>
    /// <remarks>
    /// Terraria packets are limited to a maximum of 65536 bytes, but this restriction is not enforced immediately on
    /// each of the packet types. Instead, this restriction is enforced when the packet is sent.
    /// </remarks>
    public abstract class TerrariaPacket {
        private static readonly Dictionary<TerrariaPacketType, Func<TerrariaPacket>> PacketConstructors =
            new Dictionary<TerrariaPacketType, Func<TerrariaPacket>> {
                [TerrariaPacketType.RequestConnection] = () => new RequestConnectionPacket(),
                [TerrariaPacketType.DisconnectPlayer] = () => new DisconnectPlayerPacket(),
                [TerrariaPacketType.ContinueConnection] = () => new ContinueConnectionPacket(),
                [TerrariaPacketType.UpdatePlayerInfo] = () => new UpdatePlayerInfoPacket(),
                [TerrariaPacketType.UpdatePlayerInventorySlot] = () => new UpdatePlayerInventorySlotPacket(),
                [TerrariaPacketType.FinishConnection] = () => new FinishConnectionPacket(),
                [TerrariaPacketType.UpdateWorldInfo] = () => new UpdateWorldInfoPacket(),
                [TerrariaPacketType.RequestWorldSection] = () => new RequestWorldSectionPacket(),
                [TerrariaPacketType.UpdateClientStatus] = () => new UpdateClientStatusPacket(),
            };

        /// <summary>
        /// Gets the length of a packet header.
        /// </summary>
        internal static int HeaderLength => sizeof(TerrariaPacketType) + sizeof(short);

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
        public static TerrariaPacket ReadFromStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
#if DEBUG
                var position = stream.Position;
#endif

                var packetLength = reader.ReadUInt16();
                var packetType = (TerrariaPacketType)reader.ReadByte();
                var packetCtor =
                    PacketConstructors.TryGetValue(packetType, out var f) ? f : () => new UnknownPacket(packetType);
                var packet = packetCtor();
                packet.ReadFromReader(reader, packetLength);

                Debug.Assert(stream.Position - position == packetLength, "Packet should be fully consumed.");

                return packet;
            }
        }

        /// <summary>
        /// Writes the packet to the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">The packet cannot be written due to its length.</exception>
        public void WriteToStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true)) {
                var startPosition = stream.Position;

                writer.Write((ushort)0);
                writer.Write((byte)Type);
                WriteToWriter(writer);

                var finalPosition = stream.Position;
                var packetLength = finalPosition - startPosition;
                if (packetLength > ushort.MaxValue) {
                    throw new InvalidOperationException("Packet is too long.");
                }

                stream.Position = startPosition;
                writer.Write((ushort)packetLength);
                stream.Position = finalPosition;
            }
        }

        private protected abstract void ReadFromReader(BinaryReader reader, ushort packetLength);
        private protected abstract void WriteToWriter(BinaryWriter writer);
    }
}
