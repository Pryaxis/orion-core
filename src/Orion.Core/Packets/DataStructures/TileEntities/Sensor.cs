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
using System.Runtime.InteropServices;
using Orion.Core.Utils;
using Orion.Core.World.TileEntities;

namespace Orion.Core.Packets.DataStructures.TileEntities
{
    /// <summary>
    /// Represents a serializable sensor.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 2)]
    public sealed class Sensor : SerializableTileEntity
    {
        [FieldOffset(0)] private byte _bytes;  // Used to obtain an interior reference.

        /// <inheritdoc/>
        public override TileEntityId Id => TileEntityId.Sensor;

        /// <summary>
        /// Gets or sets the sensor's type.
        /// </summary>
        /// <value>The sensor's type.</value>
        [field: FieldOffset(0)] public SensorType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the sensor is activated.
        /// </summary>
        /// <value><see langword="true"/> if the sensor is activated; otherwise, <see langword="false"/>.</value>
        [field: FieldOffset(1)] public bool IsActivated { get; set; }

        /// <inheritdoc/>
        protected override int ReadBody(Span<byte> span) => span.Read(ref _bytes, 2);

        /// <inheritdoc/>
        protected override int WriteBody(Span<byte> span) => span.Write(ref _bytes, 2);
    }
}
