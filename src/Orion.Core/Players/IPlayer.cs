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
using Orion.Core.Buffs;
using Orion.Core.Collections;
using Orion.Core.DataStructures;
using Orion.Core.Entities;
using Orion.Core.Packets;
using Orion.Core.Packets.Server;
using Orion.Core.Packets.World.Tiles;
using Orion.Core.World.Tiles;

namespace Orion.Core.Players {
    /// <summary>
    /// Represents a Terraria player.
    /// </summary>
    /// <remarks>
    /// Implementations are required to be thread-safe: i.e., each operation on the player should be atomic.
    /// </remarks>
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        /// <value>The player's health.</value>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum health.
        /// </summary>
        /// <value>The player's maximum health.</value>
        int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the player's mana.
        /// </summary>
        /// <value>The player's mana.</value>
        int Mana { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum mana.
        /// </summary>
        /// <value>The player's maximum mana.</value>
        int MaxMana { get; set; }

        /// <summary>
        /// Gets the player's buffs.
        /// </summary>
        /// <value>The player's buffs.</value>
        IArray<Buff> Buffs { get; }

        /// <summary>
        /// Gets or sets the player's difficulty.
        /// </summary>
        /// <value>The player's difficulty.</value>
        PlayerDifficulty Difficulty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is in PvP.
        /// </summary>
        /// <value><see langword="true"/> if the player is in PvP; otherwise, <see langword="false"/>.</value>
        bool IsInPvp { get; set; }

        /// <summary>
        /// Gets or sets the player's team.
        /// </summary>
        /// <value>The player's team.</value>
        PlayerTeam Team { get; set; }

        /// <summary>
        /// Sends the given <paramref name="packet"/> reference to the player.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="packet">The packet reference to send. <b>This must be on the stack!</b></param>
        void SendPacket<TPacket>(ref TPacket packet) where TPacket : struct, IPacket;
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayer"/> interface.
    /// </summary>
    public static class PlayerExtensions {
        /// <summary>
        /// Sends the given <paramref name="packet"/> to the <paramref name="player"/>. This "overload" is provided for
        /// convenience, but is slightly less efficient due to a struct copy.
        /// </summary>
        /// <typeparam name="TPacket">The type of packet.</typeparam>
        /// <param name="player">The player.</param>
        /// <param name="packet">The packet to send.</param>
        /// <exception cref="ArgumentNullException"><paramref name="player"/> is <see langword="null"/>.</exception>
        public static void SendPacket<TPacket>(this IPlayer player, TPacket packet) where TPacket : struct, IPacket {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            player.SendPacket(ref packet);
        }

        /// <summary>
        /// Disconnects the <paramref name="player"/> for the given <paramref name="reason"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="reason">The reason for the disconnect.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="reason"/> are <see langword="null"/>.
        /// </exception>
        public static void Disconnect(this IPlayer player, NetworkText reason) {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            if (reason is null) {
                throw new ArgumentNullException(nameof(reason));
            }

            var packet = new ServerDisconnectPacket { Reason = reason };
            player.SendPacket(ref packet);
        }

        /// <summary>
        /// Sends the given <paramref name="message"/> with the specified <paramref name="color"/> to the
        /// <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="color">The color to send the message as.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="message"/> are <see langword="null"/>.
        /// </exception>
        public static void SendMessage(this IPlayer player, NetworkText message, Color3 color) {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            if (message is null) {
                throw new ArgumentNullException(nameof(message));
            }

            var packet = new ServerChatPacket { Color = color, Message = message, LineWidth = -1 };
            player.SendPacket(ref packet);
        }

        /// <summary>
        /// Sends the given <paramref name="tiles"/> at the specified coordinates to the <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="x">The top-left tile's X coordinate.</param>
        /// <param name="y">The top-left tile's Y coordinate.</param>
        /// <param name="tiles">The tiles to send.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="tiles"/> are <see langword="null"/>.
        /// </exception>
        /// <exception cref="NotSupportedException"><paramref name="tiles"/> is not square.</exception>
        public static void SendTiles(this IPlayer player, int x, int y, ITileSlice tiles) {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            if (tiles is null) {
                throw new ArgumentNullException(nameof(tiles));
            }

            if (tiles.Width != tiles.Height) {
                // Not localized because this string is developer-facing.
                // TODO: implement this when the section packet is implemented.
                throw new NotSupportedException("Tiles is not square");
            }

            var packet = new TileSquarePacket { X = (short)x, Y = (short)y, Tiles = tiles };
            player.SendPacket(ref packet);
        }
    }
}
