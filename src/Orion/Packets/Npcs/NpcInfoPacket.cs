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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Npcs;
using Orion.Packets.Extensions;
using Orion.Utils;
using TerrariaNpc = Terraria.NPC;

namespace Orion.Packets.Npcs {
    /// <summary>
    /// Packet sent from the server to the client to set NPC information.
    /// </summary>
    public sealed class NpcInfoPacket : Packet {
        private short _npcIndex;
        private Vector2 _npcPosition;
        private Vector2 _npcVelocity;
        private ushort _npcTargetIndex;
        private bool _npcHorizontalDirection;
        private bool _npcVerticalDirection;
        private bool _npcSpriteDirection;
        private bool _isNpcAtMaxHealth;
        private NpcType _npcType;
        private byte _npcNumberOfHealthBytes;
        private int _npcHealth;
        private byte _npcReleaserPlayerIndex;
        private readonly DirtiableArray<float> _npcAiValues = new DirtiableArray<float>(TerrariaNpc.maxAI);

        /// <inheritdoc />
        public override bool IsDirty => base.IsDirty || _npcAiValues.IsDirty;

        /// <inheritdoc />
        public override PacketType Type => PacketType.NpcInfo;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's position. The components are pixel-based.
        /// </summary>
        public Vector2 NpcPosition {
            get => _npcPosition;
            set {
                _npcPosition = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's velocity. The components are pixel-based.
        /// </summary>
        public Vector2 NpcVelocity {
            get => _npcVelocity;
            set {
                _npcVelocity = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's target index.
        /// </summary>
        // TODO: convert this to a specific type
        public ushort NpcTargetIndex {
            get => _npcTargetIndex;
            set {
                _npcTargetIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the horizontal direction of the NPC.
        /// </summary>
        public bool NpcHorizontalDirection {
            get => _npcHorizontalDirection;
            set {
                _npcHorizontalDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the vertical direction of the NPC.
        /// </summary>
        public bool NpcVerticalDirection {
            get => _npcVerticalDirection;
            set {
                _npcVerticalDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets the NPC's AI values.
        /// </summary>
        public IArray<float> NpcAiValues => _npcAiValues;

        /// <summary>
        /// Gets or sets a value indicating the direction of the NPC sprite.
        /// </summary>
        public bool NpcSpriteDirection {
            get => _npcSpriteDirection;
            set {
                _npcSpriteDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or set a value indicating whether the NPC is at maximum health.
        /// </summary>
        public bool IsNpcAtMaxHealth {
            get => _isNpcAtMaxHealth;
            set {
                _isNpcAtMaxHealth = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's type.
        /// </summary>
        public NpcType NpcType {
            get => _npcType;
            set {
                _npcType = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the number of bytes that represent the NPC's health. Values are 1, 2, or 4.
        /// </summary>
        public byte NpcNumberOfHealthBytes {
            get => _npcNumberOfHealthBytes;
            set {
                _npcNumberOfHealthBytes = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's health.
        /// </summary>
        public int NpcHealth {
            get => _npcHealth;
            set {
                _npcHealth = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's releaser player index.
        /// </summary>
        public byte NpcReleaserPlayerIndex {
            get => _npcReleaserPlayerIndex;
            set {
                _npcReleaserPlayerIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc />
        public override void Clean() {
            base.Clean();
            _npcAiValues.Clean();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}[#={NpcIndex}, {NpcType} @ {NpcPosition}, ...]";

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _npcPosition = reader.ReadVector2();
            _npcVelocity = reader.ReadVector2();

            var targetIndex = reader.ReadUInt16();
            _npcTargetIndex = targetIndex != ushort.MaxValue ? targetIndex : (ushort)0;

            Terraria.BitsByte header = reader.ReadByte();
            _npcHorizontalDirection = header[0];
            _npcVerticalDirection = header[1];
            if (header[2]) _npcAiValues._array[0] = reader.ReadSingle();
            if (header[3]) _npcAiValues._array[1] = reader.ReadSingle();
            if (header[4]) _npcAiValues._array[2] = reader.ReadSingle();
            if (header[5]) _npcAiValues._array[3] = reader.ReadSingle();
            _npcSpriteDirection = header[6];
            _isNpcAtMaxHealth = header[7];

            _npcType = (NpcType)reader.ReadInt16();

            if (!_isNpcAtMaxHealth) {
                _npcNumberOfHealthBytes = reader.ReadByte();
                _npcHealth = _npcNumberOfHealthBytes switch {
                    2 => reader.ReadInt16(),
                    4 => reader.ReadInt32(),
                    _ => reader.ReadSByte()
                };
            }

            if (_npcType.IsCatchable()) {
                _npcReleaserPlayerIndex = reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(_npcPosition);
            writer.Write(_npcVelocity);
            writer.Write(_npcTargetIndex);

            Terraria.BitsByte header = 0;
            header[0] = _npcHorizontalDirection;
            header[1] = _npcVerticalDirection;
            header[2] = _npcAiValues._array[0] != 0;
            header[3] = _npcAiValues._array[1] != 0;
            header[4] = _npcAiValues._array[2] != 0;
            header[5] = _npcAiValues._array[3] != 0;
            header[6] = _npcSpriteDirection;
            header[7] = _isNpcAtMaxHealth;

            writer.Write(header);
            if (header[2]) writer.Write(_npcAiValues._array[0]);
            if (header[3]) writer.Write(_npcAiValues._array[1]);
            if (header[4]) writer.Write(_npcAiValues._array[2]);
            if (header[5]) writer.Write(_npcAiValues._array[3]);

            writer.Write((short)_npcType);

            if (!_isNpcAtMaxHealth) {
                writer.Write(_npcNumberOfHealthBytes);
                switch (_npcNumberOfHealthBytes) {
                case 4:
                    writer.Write(_npcHealth);
                    break;
                case 2:
                    writer.Write((short)_npcHealth);
                    break;
                default:
                    writer.Write((byte)_npcHealth);
                    break;
                }
            }

            if (_npcType.IsCatchable()) {
                writer.Write(_npcReleaserPlayerIndex);
            }
        }
    }
}
