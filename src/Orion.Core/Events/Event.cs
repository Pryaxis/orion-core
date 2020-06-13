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

namespace Orion.Core.Events {
    /// <summary>
    /// Provides the base class for an event, the main form of communication between Orion and its plugins.
    /// </summary>
    [Event("event")]
    public abstract class Event {
        private string? _cancellationReason;

        /// <summary>
        /// Gets a value indicating whether the event is canceled.
        /// </summary>
        /// <value><see langword="true"/> if the event is canceled; otherwise, <see langword="false"/>.</value>
        [NotLogged]
        public bool IsCanceled => _cancellationReason != null;

        /// <summary>
        /// Gets the event's cancellation reason. This is only applicable if the event is canceled.
        /// </summary>
        /// <value>The event's cancellation reason.</value>
        /// <exception cref="InvalidOperationException">The event is not canceled.</exception>
        [NotLogged]
        public string CancellationReason =>
            // Not localized because this string is developer-facing.
            _cancellationReason ?? throw new InvalidOperationException("Event is not canceled");

        /// <summary>
        /// Cancels the event, optionally with the given <paramref name="reason"/>.
        /// </summary>
        /// <param name="reason">The cancellation reason.</param>
        /// <exception cref="ArgumentNullException"><paramref name="reason"/> is <see langword="null"/>.</exception>
        public void Cancel(string reason = "") {
            _cancellationReason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        /// <summary>
        /// Uncancels the event. This is only applicable if the event is canceled.
        /// </summary>
        /// <exception cref="InvalidOperationException">The event is not canceled.</exception>
        public void Uncancel() {
            if (_cancellationReason is null) {
                // Not localized because this string is developer-facing.
                throw new InvalidOperationException("Event is not canceled");
            }

            _cancellationReason = null;
        }
    }
}
