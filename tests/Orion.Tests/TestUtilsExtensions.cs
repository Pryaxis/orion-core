// Copyright (c) 2019 Pryaxis & Orion Contributors
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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using Microsoft.Xna.Framework;
using Orion.Packets;
using Orion.Utils;

namespace Orion {
    public static class TestUtilsExtensions {
        private static readonly IDictionary<Type, object> DefaultValues = new Dictionary<Type, object> {
            [typeof(bool)] = true,
            [typeof(sbyte)] = (sbyte)-100,
            [typeof(byte)] = (byte)100,
            [typeof(short)] = (short)-12345,
            [typeof(ushort)] = (ushort)12345,
            [typeof(int)] = -123456789,
            [typeof(uint)] = 123456789U,
            [typeof(long)] = -123456789101112L,
            [typeof(ulong)] = 123456789101112UL,
            [typeof(string)] = "test",
            [typeof(Color)] = new Color(111, 222, 333)
        };

        public static void Properties_GetSetShouldReflect(this EventArgs args, string fieldName) {
            // ReSharper disable once PossibleNullReferenceException
            var field = args.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
                            .GetValue(args);
            field.Should().NotBeNull();

            foreach (var property in args.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                var packetProperty = field.GetType().GetProperty(property.Name);
                if (packetProperty is null) continue;

                var propertyType = property.PropertyType;
                if (!DefaultValues.TryGetValue(propertyType, out var value)) {
                    if (propertyType.GetConstructor(Type.EmptyTypes) is null && !propertyType.IsValueType) continue;

                    value = Activator.CreateInstance(propertyType);
                }

                // Test getter.
                packetProperty.SetValue(field, value);
                property.GetValue(args).Should().Be(value);

                // Test setter, if applicable.
                if (!property.CanWrite) continue;
                property.SetValue(args, value);
                packetProperty.GetValue(field).Should().Be(value);
            }
        }

        public static void SetSimplePropertiesShouldMarkAsDirty(this IDirtiable dirtiable) {
            foreach (var property in dirtiable.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                if (property.SetMethod?.IsPublic != true) continue;

                var propertyType = property.PropertyType;
                if (!DefaultValues.TryGetValue(propertyType, out var value)) {
                    if (propertyType.GetConstructor(Type.EmptyTypes) is null && !propertyType.IsValueType) continue;

                    value = Activator.CreateInstance(propertyType);
                }

                property.SetValue(dirtiable, value);
                dirtiable.ShouldBeDirty();
            }
        }

        public static void ShouldBeDirty(this IDirtiable dirtiable) {
            dirtiable.IsDirty.Should().BeTrue();
            dirtiable.Clean();
            dirtiable.IsDirty.Should().BeFalse();
        }

        public static void ShouldDeserializeAndSerializeSamePacket(this byte[] bytes) {
            using var inStream = new MemoryStream(bytes);
            using var outStream = new MemoryStream();
            var packet = Packet.ReadFromStream(inStream, PacketContext.Server);
            packet.IsDirty.Should().BeFalse();

            packet.WriteToStream(outStream, PacketContext.Client);

            outStream.ToArray().Should().BeEquivalentTo(bytes);
        }
    }
}
