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
using Destructurama.Attributed;
using Orion.Packets.Modules;
using Orion.Players;
using Orion.Utils;

namespace Orion.Events.Players {
    /// <summary>
    /// An event that occurs when a player chats. This event can be canceled and modified.
    /// </summary>
    [Event("player-chat")]
    public sealed class PlayerChatEvent : PlayerEvent, ICancelable, IDirtiable {
        private readonly ChatModule _module;

        /// <inheritdoc/>
        [NotLogged]
        public bool IsDirty => _module.IsDirty;

        /// <inheritdoc/>
        [NotLogged]
        public string? CancellationReason { get; set; }

        /// <summary>
        /// Gets or sets the player's chat command.
        /// </summary>
        /// <value>The player's chat command.</value>
        /// <remarks>
        /// The command indicates what to interpret the message as. For example, a <c>Say</c> command will send the
        /// message to everyone and an <c>Emote</c> command will send a third-person message to everyone.
        /// </remarks>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Command {
            get => _module.ClientCommand;
            set => _module.ClientCommand = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Gets or sets the player's chat text.
        /// </summary>
        /// <value>The player's chat text.</value>
        /// <exception cref="ArgumentNullException"><paramref name="value"/> is <see langword="null"/>.</exception>
        public string Text {
            get => _module.ClientText;
            set => _module.ClientText = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerChatEvent"/> class with the specified player and module.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="module">The module.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="module"/> are <see langword="null"/>.
        /// </exception>
        public PlayerChatEvent(IPlayer player, ChatModule module) : base(player) {
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }

        /// <inheritdoc/>
        public void Clean() => _module.Clean();
    }
}
