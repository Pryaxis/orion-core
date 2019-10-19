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
        private Vector2 _position;
        private Vector2 _velocity;
        private ushort _targetIndex;
        private bool _horizontalDirection;
        private bool _verticalDirection;
        private bool _spriteDirection;
        private bool _isAtMaxHealth;
        private NpcType _npcType;
        private byte _numberOfHealthBytes;
        private int _health;
        private byte _releaserIndex;
        private DirtiableArray<float> _aiValues = new DirtiableArray<float>(TerrariaNpc.maxAI);

        /// <inheritdoc/>
        public override bool IsDirty => base.IsDirty || _aiValues.IsDirty;

        /// <inheritdoc/>
        public override PacketType Type => PacketType.NpcInfo;

        /// <summary>
        /// Gets or sets the NPC index.
        /// </summary>
        /// <value>The NPC index.</value>
        public short NpcIndex {
            get => _npcIndex;
            set {
                _npcIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's position. The components are pixels.
        /// </summary>
        /// <value>The NPC's position.</value>
        public Vector2 Position {
            get => _position;
            set {
                _position = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's velocity. The components are pixels per tick.
        /// </summary>
        /// <value>The NPC's index.</value>
        public Vector2 Velocity {
            get => _velocity;
            set {
                _velocity = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's target index.
        /// </summary>
        /// <value>The NPC's target index.</value>
        /// <remarks>
        /// If the target index is under <c>300</c>, then the NPC is targeting a player. If the target index is at least
        /// <c>300</c>, then the NPC is targeting an NPC with the index offset by <c>300</c>. <para/>
        /// 
        /// There appears to be a bug where Terraria defaults this value to <c>0</c> instead of <c>-1</c>. This property
        /// replicates that for accuracy.
        /// </remarks>
        public ushort TargetIndex {
            get => _targetIndex;
            set {
                _targetIndex = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the horizontal direction of the NPC.
        /// </summary>
        /// <value><see langword="true"/> if the NPC is facing right; otherwise, <see langword="false"/>.</value>
        public bool HorizontalDirection {
            get => _horizontalDirection;
            set {
                _horizontalDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the vertical direction of the NPC.
        /// </summary>
        /// <value><see langword="true"/> if the NPC is facing right; otherwise, <see langword="false"/>.</value>
        public bool VerticalDirection {
            get => _verticalDirection;
            set {
                _verticalDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets the NPC's AI values.
        /// </summary>
        /// <value>The NPC's AI values.</value>
        public IArray<float> AiValues => _aiValues;

        /// <summary>
        /// Gets or sets a value indicating the direction of the NPC's sprite.
        /// </summary>
        /// <value>
        /// <see langword="true"/> if the NPC's sprite is facing right; otherwise, <see langword="false"/>.
        /// </value>
        public bool SpriteDirection {
            get => _spriteDirection;
            set {
                _spriteDirection = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or set a value indicating whether the NPC is at maximum health.
        /// </summary>
        /// <value><see langword="true"/> if the NPC is at maximum health; otherwise, <see langword="false"/>.</value>
        public bool IsAtMaxHealth {
            get => _isAtMaxHealth;
            set {
                _isAtMaxHealth = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's type.
        /// </summary>
        /// <value>The NPC's type.</value>
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
        /// <value>The number of bytes that represent the NPC's health.</value>
        public byte NumberOfHealthBytes {
            get => _numberOfHealthBytes;
            set {
                _numberOfHealthBytes = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's health.
        /// </summary>
        /// <value>The NPC's health.</value>
        public int Health {
            get => _health;
            set {
                _health = value;
                _isDirty = true;
            }
        }

        /// <summary>
        /// Gets or sets the NPC's releaser's player index.
        /// </summary>
        /// <value>The NPC's releaser's player index.</value>
        public byte ReleaserIndex {
            get => _releaserIndex;
            set {
                _releaserIndex = value;
                _isDirty = true;
            }
        }

        /// <inheritdoc/>
        public override void Clean() {
            base.Clean();
            _aiValues.Clean();
        }

        private protected override void ReadFromReader(BinaryReader reader, PacketContext context) {
            _npcIndex = reader.ReadInt16();
            _position = reader.ReadVector2();
            _velocity = reader.ReadVector2();

            var targetIndex = reader.ReadUInt16();
            _targetIndex = targetIndex != ushort.MaxValue ? targetIndex : (ushort)0;

            Terraria.BitsByte header = reader.ReadByte();
            _horizontalDirection = header[0];
            _verticalDirection = header[1];

            var aiValues = new float[_aiValues.Count];
            if (header[2]) aiValues[0] = reader.ReadSingle();
            if (header[3]) aiValues[1] = reader.ReadSingle();
            if (header[4]) aiValues[2] = reader.ReadSingle();
            if (header[5]) aiValues[3] = reader.ReadSingle();

            _aiValues = new DirtiableArray<float>(aiValues);
            _spriteDirection = header[6];
            _isAtMaxHealth = header[7];

            _npcType = (NpcType)reader.ReadInt16();

            if (!_isAtMaxHealth) {
                _numberOfHealthBytes = reader.ReadByte();
                _health = _numberOfHealthBytes switch {
                    2 => reader.ReadInt16(),
                    4 => reader.ReadInt32(),
                    _ => reader.ReadSByte()
                };
            }

            if (_npcType.IsCatchable()) {
                _releaserIndex = reader.ReadByte();
            }
        }

        private protected override void WriteToWriter(BinaryWriter writer, PacketContext context) {
            writer.Write(_npcIndex);
            writer.Write(in _position);
            writer.Write(in _velocity);
            writer.Write(_targetIndex);

            Terraria.BitsByte header = 0;
            header[0] = _horizontalDirection;
            header[1] = _verticalDirection;
            header[2] = _aiValues[0] != 0;
            header[3] = _aiValues[1] != 0;
            header[4] = _aiValues[2] != 0;
            header[5] = _aiValues[3] != 0;
            header[6] = _spriteDirection;
            header[7] = _isAtMaxHealth;

            writer.Write(header);
            if (header[2]) writer.Write(_aiValues[0]);
            if (header[3]) writer.Write(_aiValues[1]);
            if (header[4]) writer.Write(_aiValues[2]);
            if (header[5]) writer.Write(_aiValues[3]);

            writer.Write((short)_npcType);

            if (!_isAtMaxHealth) {
                writer.Write(_numberOfHealthBytes);
                switch (_numberOfHealthBytes) {
                case 4:
                    writer.Write(_health);
                    break;
                case 2:
                    writer.Write((short)_health);
                    break;
                default:
                    writer.Write((byte)_health);
                    break;
                }
            }

            if (_npcType.IsCatchable()) {
                writer.Write(_releaserIndex);
            }
        }
    }
}
