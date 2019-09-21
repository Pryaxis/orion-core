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

namespace Orion.Networking.Packets.Misc {
    /// <summary>
    /// Packet sent to the client to set an emote bubble.
    /// </summary>
    public sealed class EmoteBubblePacket : Packet {
        private int _emoteIndex;
        private byte _anchorType;
        private ushort _anchorIndex;
        private byte _emoteLifetime;
        private int _emoteEmotion;
        private ushort _emoteMetadata;

        /// <inheritdoc />
        public override PacketType Type => PacketType.EmoteBubble;

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
        public byte AnchorType {
            get => _anchorType;
            set {
                _anchorType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the anchor index.
        /// </summary>
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
        public byte EmoteLifetime {
            get => _emoteLifetime;
            set {
                _emoteLifetime = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the emote's emotion.
        /// </summary>

        // TODO: implement enum for this.
        public int EmoteEmotion {
            get => _emoteEmotion;
            set {
                _emoteEmotion = value;
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
            EmoteIndex = reader.ReadInt32();
            AnchorType = reader.ReadByte();
            if (AnchorType == byte.MaxValue) return;

            AnchorIndex = reader.ReadUInt16();
            EmoteLifetime = reader.ReadByte();
            EmoteEmotion = reader.ReadSByte();
            if (EmoteEmotion < 0) {
                EmoteMetadata = reader.ReadUInt16();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(EmoteIndex);
            writer.Write(AnchorType);
            if (AnchorType == byte.MaxValue) return;

            writer.Write(AnchorIndex);
            writer.Write(EmoteLifetime);
            writer.Write((sbyte)EmoteEmotion);
            if (EmoteEmotion < 0) {
                writer.Write(EmoteMetadata);
            }
        }
    }
}
