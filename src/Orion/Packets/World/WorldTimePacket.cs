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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using JetBrains.Annotations;

namespace Orion.Packets.World {
    /// <summary>
    /// Packet sent to set the time. This is currently not naturally sent.
    /// </summary>
    [PublicAPI]
    public sealed class WorldTimePacket : Packet {
        private bool _isDaytime;
        private int _time;
        private short _sunY;
        private short _moonY;

        /// <inheritdoc />
        public override PacketType Type => PacketType.WorldTime;

        /// <summary>
        /// Gets or sets a value indicating whether it is daytime.
        /// </summary>
        public bool IsDaytime {
            get => _isDaytime;
            set {
                _isDaytime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        public int Time {
            get => _time;
            set {
                _time = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the sun's Y position.
        /// </summary>
        public short SunY {
            get => _sunY;
            set {
                _sunY = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the moon's Y position.
        /// </summary>
        public short MoonY {
            get => _moonY;
            set {
                _moonY = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[{Time} @ {(IsDaytime ? "day" : "night")}, ...]";

        /// <inheritdoc />
        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _isDaytime = reader.ReadBoolean();
            _time = reader.ReadInt32();
            _sunY = reader.ReadInt16();
            _moonY = reader.ReadInt16();
        }

        /// <inheritdoc />
        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_isDaytime);
            writer.Write(_time);
            writer.Write(_sunY);
            writer.Write(_moonY);
        }
    }
}
