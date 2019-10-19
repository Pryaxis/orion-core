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

using System.IO;
using Orion.Entities;

namespace Orion.Packets.Entities {
    /// <summary>
    /// Packet sent from the server to the client to set emote information.
    /// </summary>
    public sealed class EmoteInfoPacket : Packet {
        private int _emoteIndex;
        private EmoteAnchorType _anchorType;
        private ushort _anchorIndex;
        private byte _lifetime;
        private EmoteType _emoteType;
        private ushort _metadata;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.EmoteInfo;

        /// <summary>
        /// Gets or sets the emote index.
        /// </summary>
        /// <value>The emote index.</value>
        public int EmoteIndex {
            get => _emoteIndex;
            set {
                _emoteIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's anchor type.
        /// </summary>
        /// <value>The emote's anchor type.</value>
        public EmoteAnchorType AnchorType {
            get => _anchorType;
            set {
                _anchorType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's anchor index.
        /// </summary>
        /// <value>The emote's anchor index.</value>
        public ushort AnchorIndex {
            get => _anchorIndex;
            set {
                _anchorIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's lifetime.
        /// </summary>
        /// <value>The emote's lifetime.</value>
        public byte Lifetime {
            get => _lifetime;
            set {
                _lifetime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's type.
        /// </summary>
        /// <value>The emote's type.</value>
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
        /// <value>The emote's metadata.</value>
        public ushort Metadata {
            get => _metadata;
            set {
                _metadata = value;
                _isDirty = true;
            }
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _emoteIndex = reader.ReadInt32();
            _anchorType = (EmoteAnchorType)reader.ReadByte();
            if (_anchorType == EmoteAnchorType.None) {
                return;
            }

            _anchorIndex = reader.ReadUInt16();
            _lifetime = reader.ReadByte();
            _emoteType = (EmoteType)reader.ReadByte();

            // NOTE: this is never possible and is a bug with Terraria.
            if (_emoteType < 0) {
                _metadata = reader.ReadUInt16();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_emoteIndex);
            writer.Write((byte)_anchorType);
            if (_anchorType == EmoteAnchorType.None) {
                return;
            }

            writer.Write(_anchorIndex);
            writer.Write(_lifetime);

            writer.Write((byte)_emoteType);
            if (_emoteType < 0) {
                writer.Write(_metadata);
            }
        }
    }
}
