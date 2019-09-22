// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.IO;
using System.Reflection;
using FluentAssertions;
using Orion.Events;
using Orion.Networking.Packets;
using Orion.Utils;

namespace Orion {
    public static class TestUtilsExtensions {
        public static void ShouldHaveDefaultablePropertiesMarkAsDirty(this IDirtiable dirtiable) {
            // Use reflection to check all properties with types that have default constructors. This is pretty terrible
            // if we don't do this...
            foreach (var property in dirtiable.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)) {
                if (property.SetMethod?.IsPublic != true) continue;

                var propertyType = property.PropertyType;
                if (propertyType.GetConstructor(Type.EmptyTypes) == null && !propertyType.IsValueType) continue;

                property.SetValue(dirtiable, Activator.CreateInstance(propertyType));
                dirtiable.ShouldBeDirty();
            }
        }

        public static void ShouldBeDirty(this IDirtiable dirtiable) {
            dirtiable.IsDirty.Should().BeTrue();
            dirtiable.Clean();
            dirtiable.IsDirty.Should().BeFalse();
        }

        public static void ShouldDeserializeAndSerializeSamePacket(this byte[] bytes) {
            using (var inStream = new MemoryStream(bytes))
            using (var outStream = new MemoryStream()) {
                var packet = Packet.ReadFromStream(inStream, PacketContext.Server);
                packet.IsDirty.Should().BeFalse();

                packet.WriteToStream(outStream, PacketContext.Client);

                outStream.ToArray().Should().BeEquivalentTo(bytes);
            }
        }
    }
}
