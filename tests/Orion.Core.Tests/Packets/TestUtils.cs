// Copyright (c) 2020 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Orion.Core.Packets.DataStructures.Modules;
using Orion.Core.Packets.DataStructures.TileEntities;
using Xunit;

namespace Orion.Core.Packets
{
    public static class TestUtils
    {
        /// <summary>
        /// Reads a packet from the given <paramref name="span"/> in the specified <paramref name="context"/>,
        /// performing round trip checks.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <returns>The resulting packet.</returns>
        public static TPacket ReadPacket<TPacket>(Span<byte> span, PacketContext context) where TPacket : IPacket
        {
            var otherContext = context == PacketContext.Server ? PacketContext.Client : PacketContext.Server;

            var packet = Read(span);
            var bytes = Write(packet);
            var packet2 = Read(bytes);
            var bytes2 = Write(packet2);
            Assert.Equal(bytes, bytes2);

            return packet;

            TPacket Read(Span<byte> span)
            {
                var packetLength = Unsafe.ReadUnaligned<ushort>(ref span[0]);
                var packetId = (PacketId)span[2];
                var type = packetId.Type();

                TPacket packet;
                if (packetId == PacketId.Module)
                {
                    var moduleId = Unsafe.ReadUnaligned<ModuleId>(ref span[3]);
                    packet =
                        (TPacket)Activator.CreateInstance(typeof(ModulePacket<>).MakeGenericType(moduleId.Type()))!;
                }
                else
                {
                    packet = type == typeof(UnknownPacket)
                       ? (TPacket)(object)new UnknownPacket(span.Length - 3, packetId)
                       : (TPacket)Activator.CreateInstance(type)!;
                }

                var length = packet.ReadBody(span[3..], context);
                Assert.Equal(span.Length - 3, length);

                return packet;
            }

            byte[] Write(TPacket packet)
            {
                var otherContext = context == PacketContext.Server ? PacketContext.Client : PacketContext.Server;

                var bytes = new byte[ushort.MaxValue];
                var length = packet.Write(bytes, otherContext);
                return bytes[..length];
            }
        }

        /// <summary>
        /// Reads a module from the given <paramref name="span"/> in the specified <paramref name="context"/>,
        /// performing round trip checks.
        /// </summary>
        /// <typeparam name="TModule">The type of module.</typeparam>
        /// <param name="span">The span to read from.</param>
        /// <param name="context">The packet context to use when reading.</param>
        /// <returns>The resulting module.</returns>
        public static TModule ReadModule<TModule>(Span<byte> span, PacketContext context) where TModule : IModule
        {
            var otherContext = context == PacketContext.Server ? PacketContext.Client : PacketContext.Server;

            var module = Read(span, context);
            var bytes = Write(module, otherContext);
            var module2 = Read(bytes, context);
            var bytes2 = Write(module2, otherContext);
            Assert.Equal(bytes, bytes2);

            return module;

            static TModule Read(Span<byte> span, PacketContext context)
            {
                var moduleId = Unsafe.ReadUnaligned<ModuleId>(ref span[0]);
                var type = moduleId.Type();

                var module = type == typeof(UnknownModule)
                    ? (TModule)(object)new UnknownModule(span.Length - 2, moduleId)
                    : (TModule)Activator.CreateInstance(type)!;

                var length = module.ReadBody(span[2..], context);
                Assert.Equal(span.Length - 2, length);

                return module;
            }

            static byte[] Write(TModule module, PacketContext context)
            {
                var bytes = new byte[ushort.MaxValue];
                var length = module.Write(bytes, context);
                return bytes[..length];
            }
        }

        /// <summary>
        /// Reads a serializable tile entity from the given <paramref name="span"/>, performing round trip checks.
        /// </summary>
        /// <typeparam name="TTileEntity">The type of serializable tile entity.</typeparam>
        /// <param name="span">The span to read from.</param>
        /// <param name="includeIndex">Whether to include the tile entity's index.</param>
        /// <returns>The tile entity.</returns>
        public static TTileEntity ReadTileEntity<TTileEntity>(Span<byte> span, bool includeIndex)
            where TTileEntity : SerializableTileEntity
        {
            var tileEntity = Read(span);
            var bytes = Write(tileEntity);
            var tileEntity2 = Read(bytes);
            var bytes2 = Write(tileEntity2);
            Assert.Equal(bytes, bytes2);

            return tileEntity;

            TTileEntity Read(Span<byte> span)
            {
                var length = SerializableTileEntity.Read(span, includeIndex, out var tileEntity);
                Assert.Equal(span.Length, length);
                return (TTileEntity)tileEntity;
            }

            byte[] Write(TTileEntity tileEntity)
            {
                var bytes = new byte[ushort.MaxValue];
                var length = tileEntity.Write(bytes, includeIndex);
                return bytes[..length];
            }
        }

        /// <summary>
        /// Tests a packet round trip by reading, writing, re-reading, and then re-writing the packet, comparing the
        /// written byte sequences.
        /// </summary>
        /// <param name="span">The span to read from, initially.</param>
        /// <param name="context">The packet context to use.</param>
        public static void RoundTripPacket(Span<byte> span, PacketContext context)
        {
            var otherContext = context == PacketContext.Server ? PacketContext.Client : PacketContext.Server;

            // Read the packet.
            IPacket.Read(span, context, out var packet);

            // Write the packet.
            var bytes = new byte[ushort.MaxValue];
            var packetLength = packet.Write(bytes, otherContext);

            // Read the packet again.
            IPacket.Read(span, context, out var packet2);

            // Write the packet again.
            var bytes2 = new byte[ushort.MaxValue];
            var packetLength2 = packet2.Write(bytes2, otherContext);

            Assert.Equal(packetLength, packetLength2);
            for (var i = 0; i < packetLength; ++i)
            {
                Assert.True(bytes[i] == bytes2[i], $"Expected: {bytes[i]}\nActual:   {bytes2[i]}\n  at position {i}");
            }
        }
    }
}
