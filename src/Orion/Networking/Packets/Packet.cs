using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
    public abstract class Packet {
        private static readonly IDictionary<PacketType, Func<Packet>> PacketConstructors =
            new Dictionary<PacketType, Func<Packet>> {
                [PacketType.RequestConnection] = () => new RequestConnectionPacket(),
                [PacketType.DisconnectPlayer] = () => new DisconnectPlayerPacket(),
                [PacketType.ContinueConnection] = () => new ContinueConnectionPacket(),
                [PacketType.UpdatePlayerInfo] = () => new UpdatePlayerInfoPacket(),
                [PacketType.UpdatePlayerInventorySlot] = () => new UpdatePlayerInventorySlotPacket(),
                [PacketType.FinishConnection] = () => new FinishConnectionPacket(),
                [PacketType.UpdateWorldInfo] = () => new UpdateWorldInfoPacket(),
                [PacketType.RequestWorldSection] = () => new RequestWorldSectionPacket(),
                [PacketType.UpdateClientStatus] = () => new UpdateClientStatusPacket(),
                [PacketType.UpdateWorldSection] = () => new UpdateWorldSectionPacket(),
                [PacketType.SyncTileFrames] = () => new SyncTileFramesPacket(),
                [PacketType.SpawnPlayer] = () => new SpawnPlayerPacket(),
                [PacketType.UpdatePlayer] = () => new UpdatePlayerPacket(),
                [PacketType.UpdatePlayerStatus] = () => new UpdatePlayerStatusPacket(),
                [PacketType.UpdatePlayerHp] = () => new UpdatePlayerHpPacket(),
                [PacketType.ModifyTile] = () => new ModifyTilePacket(),
                [PacketType.UpdateTime] = () => new UpdateTimePacket(),
                [PacketType.ToggleDoor] = () => new ToggleDoorPacket(),
                [PacketType.UpdateSquareOfTiles] = () => new UpdateSquareOfTilesPacket(),
                [PacketType.UpdateItem] = () => new UpdateItemPacket(),
                [PacketType.UpdateItemOwner] = () => new UpdateItemOwnerPacket(),
                [PacketType.UpdateNpc] = () => new UpdateNpcPacket(),
            };

        private static readonly IDictionary<Type, PacketType> PacketTypes = new Dictionary<Type, PacketType> {
            [typeof(RequestConnectionPacket)] = PacketType.RequestConnection,
            [typeof(DisconnectPlayerPacket)] = PacketType.DisconnectPlayer,
            [typeof(ContinueConnectionPacket)] = PacketType.ContinueConnection,
            [typeof(UpdatePlayerInfoPacket)] = PacketType.UpdatePlayerInfo,
            [typeof(UpdatePlayerInventorySlotPacket)] = PacketType.UpdatePlayerInventorySlot,
            [typeof(FinishConnectionPacket)] = PacketType.FinishConnection,
            [typeof(UpdateWorldInfoPacket)] = PacketType.UpdateWorldInfo,
            [typeof(RequestWorldSectionPacket)] = PacketType.RequestWorldSection,
            [typeof(UpdateClientStatusPacket)] = PacketType.UpdateClientStatus,
            [typeof(UpdateWorldSectionPacket)] = PacketType.UpdateWorldSection,
            [typeof(SyncTileFramesPacket)] = PacketType.SyncTileFrames,
            [typeof(SpawnPlayerPacket)] = PacketType.SpawnPlayer,
            [typeof(UpdatePlayerPacket)] = PacketType.UpdatePlayer,
            [typeof(UpdatePlayerStatusPacket)] = PacketType.UpdatePlayerStatus,
            [typeof(UpdatePlayerHpPacket)] = PacketType.UpdatePlayerHp,
            [typeof(ModifyTilePacket)] = PacketType.ModifyTile,
            [typeof(UpdateTimePacket)] = PacketType.UpdateTime,
            [typeof(ToggleDoorPacket)] = PacketType.ToggleDoor,
            [typeof(UpdateSquareOfTilesPacket)] = PacketType.UpdateSquareOfTiles,
            [typeof(UpdateItemPacket)] = PacketType.UpdateItem,
            [typeof(UpdateItemOwnerPacket)] = PacketType.UpdateItemOwner,
            [typeof(UpdateNpcPacket)] = PacketType.UpdateNpc,
        };

        [ExcludeFromCodeCoverage]
        internal static int HeaderLength => sizeof(PacketType) + sizeof(short);

        /// <summary>
        /// Reads a packet from the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <returns>The packet.</returns>
        public static Packet ReadFromStream(Stream stream) {
            if (stream == null) {
                throw new ArgumentNullException(nameof(stream));
            }

            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                var position = stream.Position;
                var packetLength = reader.ReadUInt16();
                var packetType = (PacketType)reader.ReadByte();
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

                if (PacketTypes.TryGetValue(GetType(), out var packetType)) {
                    writer.Write((byte)packetType);
                } else {
                    Debug.Assert(this is UnknownPacket, "Packet should be an UnknownPacket.");
                }

                WriteToWriter(writer);

                var finalPosition = stream.Position;
                var packetLength = finalPosition - startPosition;

                /*
                 * Ideally we would have thrown this exception a long time ago, when the packet is actually being
                 * modified. Unfortunately, this is a major pain to implement properly.
                 */
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
