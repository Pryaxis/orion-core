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
using Orion.Entities;

namespace Orion.Networking.Packets.Entities {
    /// <summary>
    /// Packet sent from the server to the client to set emote information.
    /// </summary>
    [PublicAPI]
    public sealed class EmoteInfoPacket : Packet {
        private int _emoteIndex;
        private EmoteAnchorType _anchorType;
        private ushort _anchorEntityIndex;
        private byte _emoteLifetime;
        private EmoteType _emoteType;
        private ushort _emoteMetadata;

        /// <inheritdoc />
        public override PacketType Type => PacketType.EmoteInfo;

        /// <summary>
        /// Gets or sets the emote index.
        /// </summary>
        public int EmoteIndex {
            get => _emoteIndex;
            set {
                _emoteIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the anchor type.
        /// </summary>
        public EmoteAnchorType AnchorType {
            get => _anchorType;
            set {
                _anchorType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the anchor's entity index.
        /// </summary>
        public ushort AnchorEntityIndex {
            get => _anchorEntityIndex;
            set {
                _anchorEntityIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's lifetime.
        /// </summary>
        public byte EmoteLifetime {
            get => _emoteLifetime;
            set {
                _emoteLifetime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's type.
        /// </summary>
        public EmoteType EmoteType {
            get => _emoteType;
            set {
                _emoteType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's metadata.
        /// </summary>
        public ushort EmoteMetadata {
            get => _emoteMetadata;
            set {
                _emoteMetadata = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={EmoteIndex}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _emoteIndex = reader.ReadInt32();
            _anchorType = (EmoteAnchorType)reader.ReadByte();
            if (_anchorType == EmoteAnchorType.Remove) return;

            _anchorEntityIndex = reader.ReadUInt16();
            _emoteLifetime = reader.ReadByte();

            _emoteType = (EmoteType)reader.ReadByte();

            // NOTE: this is never possible and is a bug with Terraria.
            if (_emoteType < 0) {
                _emoteMetadata = reader.ReadUInt16();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_emoteIndex);
            writer.Write((byte)_anchorType);
            if (_anchorType == EmoteAnchorType.Remove) return;

            writer.Write(_anchorEntityIndex);
            writer.Write(_emoteLifetime);

            writer.Write((byte)_emoteType);
            if (_emoteType < 0) {
                writer.Write(_emoteMetadata);
            }
        }
    }
}
