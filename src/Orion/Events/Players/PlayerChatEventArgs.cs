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
using Orion.Packets.Modules;
using Orion.Players;

namespace Orion.Events.Players {
    /// <summary>
    /// Provides data for the <see cref="IPlayerService.PlayerChat"/> event.
    /// </summary>
    public sealed class PlayerChatEventArgs : PlayerEventArgs, ICancelable {
        private readonly ChatModule _module;

        /// <inheritdoc />
        public string? CancellationReason { get; set; }

        /// <inheritdoc cref="ChatModule.ClientChatCommand"/>
        public string ChatCommand {
            get => _module.ClientChatCommand;
            set => _module.ClientChatCommand = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <inheritdoc cref="ChatModule.ClientChatText"/>
        public string ChatText {
            get => _module.ClientChatText;
            set => _module.ClientChatText = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerChatEventArgs"/> class with the specified player and
        /// module.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <param name="module">The module.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="player"/> or <paramref name="module"/> are <see langword="null"/>.
        /// </exception>
        public PlayerChatEventArgs(IPlayer player, ChatModule module) : base(player) {
            _module = module ?? throw new ArgumentNullException(nameof(module));
        }
    }
}
