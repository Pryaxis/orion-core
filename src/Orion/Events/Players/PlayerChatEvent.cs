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
using Destructurama.Attributed;
using Orion.Packets.Modules;
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// An event that occurs when a player chats.
    /// </summary>
    [Event("player-chat")]
    public sealed class PlayerChatEvent : PlayerEvent, ICancelable {
        /// <summary>
        /// Gets the command.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text { get; }

        /// <inheritdoc/>
        [NotLogged] public string? CancellationReason { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerChatEvent"/> class with the specified
        /// <paramref name="player"/>, <paramref name="command"/>, and <paramref name="text"/>.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="command">The command.</param>
        /// <param name="text">The text.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/>, <paramref name="command"/>, or <paramref name="text"/> are
        /// <see langword="null"/>.
        /// </exception>
        public PlayerChatEvent(IPlayer player, string command, string text) : base(player) {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }
    }
}
