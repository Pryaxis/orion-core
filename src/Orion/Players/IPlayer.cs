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

using System;
using Microsoft.Xna.Framework;
using Orion.Entities;
using Orion.Packets;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Orion.Packets.World;
using Orion.Utils;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    /// <summary>
    /// Represents a Terraria player.
    /// </summary>
    /// <remarks>
    /// Players are the users that join servers. They can freely alter the game state in many different ways. <para/>
    /// 
    /// There are two types of players:
    /// <list type="bullet">
    /// <item>
    /// <description>Players which are not active.</description>
    /// </item>
    /// <item>
    /// <description>Players which are active and on the server.</description>
    /// </item>
    /// </list>
    /// 
    /// Care must be taken to differentiate the two using the <see cref="IEntity.IsActive"/> property.
    /// </remarks>
    public interface IPlayer : IEntity, IWrapping<TerrariaPlayer> {
        /// <summary>
        /// Gets or sets the player's team.
        /// </summary>
        /// <value>THe player's teams.</value>
        PlayerTeam Team { get; set; }

        /// <summary>
        /// Gets the player's statistics.
        /// </summary>
        /// <value>The player's statistics.</value>
        IPlayerStats Stats { get; }

        /// <summary>
        /// Gets the player's inventory.
        /// </summary>
        /// <value>The player's inventory.</value>
        IPlayerInventory Inventory { get; }

        /// <summary>
        /// Sends a <paramref name="packet"/> to the player.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="packet"/> is <see langword="null"/>.</exception>
        /// <remarks>
        /// This method sends the packet asynchronously to the player. If the player is not active, then the method will
        /// silently return.
        /// </remarks>
        void SendPacket(Packet packet);
    }

    /// <summary>
    /// Provides extensions for the <see cref="IPlayer"/> interface.
    /// </summary>
    public static class PlayerExtensions {
        /// <summary>
        /// Disconnects the <paramref name="player"/> with a <paramref name="reason"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="reason">The reason.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="reason"/> are <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This method sends a <see cref="PlayerDisconnectPacket"/> to the player with the relevant properties filled
        /// in.
        /// </remarks>
        public static void Disconnect(this IPlayer player, string reason) {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            if (reason is null) {
                throw new ArgumentNullException(nameof(reason));
            }

            player.SendPacket(new PlayerDisconnectPacket { PlayerDisconnectReason = reason });
        }

        /// <summary>
        /// Sends a <paramref name="message"/> to the <paramref name="player"/> with the given <paramref name="color"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="message">The message.</param>
        /// <param name="color">The color. The alpha component is ignored.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="message"/> are <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This method sends a <see cref="ChatPacket"/> to the player with the relevant properties filled in.
        /// </remarks>
        public static void SendMessage(this IPlayer player, string message, Color color) {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            if (message is null) {
                throw new ArgumentNullException(nameof(message));
            }

            player.SendPacket(new ChatPacket {
                ChatColor = color,
                ChatLineWidth = -1,
                ChatText = message
            });
        }

        /// <summary>
        /// Sends a <paramref name="message"/> to the <paramref name="player"/> from <paramref name="fromPlayer"/> with
        /// the given <paramref name="color"/>. This results in overhead chat.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="fromPlayer">The player to receive the <paramref name="message"/> from.</param>
        /// <param name="message">The message.</param>
        /// <param name="color">The color. The alpha component is ignored.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/>, <paramref name="fromPlayer"/>, or <paramref name="message"/> are
        /// <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// This method sends a <see cref="ModulePacket"/> with a <see cref="ChatModule"/> to the player with the
        /// relevant properties filled in.
        /// </remarks>
        public static void SendMessageFrom(this IPlayer player, IPlayer fromPlayer, string message, Color color) {
            if (player is null) {
                throw new ArgumentNullException(nameof(player));
            }

            if (fromPlayer is null) {
                throw new ArgumentNullException(nameof(fromPlayer));
            }

            if (message is null) {
                throw new ArgumentNullException(nameof(message));
            }

            player.SendPacket(new ModulePacket {
                Module = new ChatModule {
                    ServerChattingPlayerIndex = (byte)fromPlayer.Index,
                    ServerChatText = message,
                    ServerChatColor = color
                }
            });
        }
    }
}
