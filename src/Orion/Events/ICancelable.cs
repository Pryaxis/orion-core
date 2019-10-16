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
using System.Diagnostics.Contracts;

namespace Orion.Events {
    /// <summary>
    /// Represents something that can be canceled.
    /// </summary>
    /// <remarks>
    /// Many types of event arguments implement this interface to allow cancellation of the event.
    /// </remarks>
    public interface ICancelable {
        /// <summary>
        /// Gets or sets the cancellation reason. If <see langword="null"/>, then the object is not canceled.
        /// </summary>
        /// <value>The cancellation reason.</value>
        /// <remarks>
        /// This property allows consumers of the <see cref="ICancelable"/> to determine why the object is canceleed. In
        /// some cases, a consumer may uncancel the event.
        /// </remarks>
        string? CancellationReason { get; set; }
    }

    /// <summary>
    /// Provides extensions for the <see cref="ICancelable"/> interface.
    /// </summary>
    public static class CancelableExtensions {
        /// <summary>
        /// Returns a value indicating whether the <paramref name="cancelable"/> is canceled.
        /// </summary>
        /// <param name="cancelable">The cancelable object.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="cancelable"/> is canceled; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="cancelable"/> is <see langword="null"/>.</exception>
        [Pure]
        public static bool IsCanceled(this ICancelable cancelable) {
            if (cancelable is null) {
                throw new ArgumentNullException(nameof(cancelable));
            }

            return cancelable.CancellationReason != null;
        }

        /// <summary>
        /// Cancels the object, optionally with a <paramref name="reason"/>.
        /// </summary>
        /// <param name="cancelable">The cancelable object.</param>
        /// <param name="reason">The reason.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="cancelable"/> or <paramref name="reason"/> are <see langword="null"/>.
        /// </exception>
        /// <remarks>
        /// Reasons should always be provided if possible, as they allow consumers to learn why the cancellation was
        /// requested.
        /// </remarks>
        public static void Cancel(this ICancelable cancelable, string reason = "") {
            if (cancelable is null) {
                throw new ArgumentNullException(nameof(cancelable));
            }

            cancelable.CancellationReason = reason ?? throw new ArgumentNullException(nameof(reason));
        }

        /// <summary>
        /// Uncancels the <paramref name="cancelable"/>.
        /// </summary>
        /// <param name="cancelable">The cancelable object.</param>
        /// <exception cref="ArgumentNullException"><paramref name="cancelable"/> is <see langword="null"/>.</exception>
        public static void Uncancel(this ICancelable cancelable) {
            if (cancelable is null) {
                throw new ArgumentNullException(nameof(cancelable));
            }

            cancelable.CancellationReason = null;
        }
    }
}
