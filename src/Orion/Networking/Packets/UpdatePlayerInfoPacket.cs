using System;
using System.IO;
using Microsoft.Xna.Framework;
using Orion.Networking.Packets.Extensions;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Packet sent to provide information about a player.
    /// </summary>
    public sealed class UpdatePlayerInfoPacket : TerrariaPacket {
        private string _name = "";

        /// <inheritdoc />
        public override bool IsSentToClient => true;

        /// <inheritdoc />
        public override bool IsSentToServer => true;

        /// <inheritdoc />
        public override TerrariaPacketType Type => TerrariaPacketType.UpdatePlayerInfo;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        public byte PlayerIndex { get; set; }

        /// <summary>
        /// Gets or sets the player's skin type.
        /// </summary>
        public byte SkinType { get; set; }

        /// <summary>
        /// Gets or sets the player's hair type.
        /// </summary>
        public byte HairType { get; set; }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <c>null</c>.</exception>
        public string Name {
            get => _name;
            set => _name = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the player's hair dye.
        /// </summary>
        public byte HairDye { get; set; }

        /// <summary>
        /// Gets or sets the player's hidden visuals flags.
        /// </summary>
        public ushort HiddenVisualsFlags { get; set; }

        /// <summary>
        /// Gets or sets the player's hidden misc flags.
        /// </summary>
        public byte HiddenMiscFlags { get; set; }

        /// <summary>
        /// Gets or sets the player's hair color.
        /// </summary>
        public Color HairColor { get; set; }

        /// <summary>
        /// Gets or sets the player's skin color.
        /// </summary>
        public Color SkinColor { get; set; }

        /// <summary>
        /// Gets or sets the player's eye color.
        /// </summary>
        public Color EyeColor { get; set; }

        /// <summary>
        /// Gets or sets the player's shirt color.
        /// </summary>
        public Color ShirtColor { get; set; }

        /// <summary>
        /// Gets or sets the player's undershirt color.
        /// </summary>
        public Color UndershirtColor { get; set; }

        /// <summary>
        /// Gets or sets the player's pants color.
        /// </summary>
        public Color PantsColor { get; set; }

        /// <summary>
        /// Gets or sets the player's shoe color.
        /// </summary>
        public Color ShoeColor { get; set; }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        public byte Difficulty { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
            SkinType = reader.ReadByte();
            HairType = reader.ReadByte();
            _name = reader.ReadString();
            HairDye = reader.ReadByte();
            HiddenVisualsFlags = reader.ReadUInt16();
            HiddenMiscFlags = reader.ReadByte();
            HairColor = reader.ReadColor();
            SkinColor = reader.ReadColor();
            EyeColor = reader.ReadColor();
            ShirtColor = reader.ReadColor();
            UndershirtColor = reader.ReadColor();
            PantsColor = reader.ReadColor();
            ShoeColor = reader.ReadColor();
            Difficulty = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
            writer.Write(SkinType);
            writer.Write(HairType);
            writer.Write(Name);
            writer.Write(HairDye);
            writer.Write(HiddenVisualsFlags);
            writer.Write(HiddenMiscFlags);
            writer.Write(HairColor);
            writer.Write(SkinColor);
            writer.Write(EyeColor);
            writer.Write(ShirtColor);
            writer.Write(UndershirtColor);
            writer.Write(PantsColor);
            writer.Write(ShoeColor);
            writer.Write(Difficulty);
        }
    }
}
