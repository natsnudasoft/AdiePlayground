// <copyright file="MessageBoard.cs" company="natsnudasoft">
// Copyright (c) Adrian John Dunstan. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>

namespace AdiePlayground.Common.Observer
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an observable message board which observers can attach to and be notified of any
    /// changes in state.
    /// </summary>
    public sealed class MessageBoard
    {
        private readonly HashSet<string> messages = new HashSet<string>(StringComparer.Ordinal);
        private readonly List<IMessageBoardObserver> observers = new List<IMessageBoardObserver>();

        /// <summary>
        /// Adds a message to this <see cref="MessageBoard"/>.
        /// </summary>
        /// <param name="message">The message to add to this <see cref="MessageBoard"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is
        /// <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> is empty.</exception>
        public void AddMessage(string message)
        {
            ParameterValidation.IsNotNull(message, nameof(message));
            ParameterValidation.IsNotEmpty(message, nameof(message));

            if (this.messages.Add(message))
            {
                this.Notify();
            }
        }

        /// <summary>
        /// Removes a message from this <see cref="MessageBoard"/>.
        /// </summary>
        /// <param name="message">The message to remove from this <see cref="MessageBoard"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is
        /// <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="message"/> is empty.</exception>
        public void RemoveMessage(string message)
        {
            ParameterValidation.IsNotNull(message, nameof(message));
            ParameterValidation.IsNotEmpty(message, nameof(message));

            if (this.messages.Remove(message))
            {
                this.Notify();
            }
        }

        /// <summary>
        /// Attaches the specified observer to this <see cref="MessageBoard"/> so that it can
        /// receive notification updates.
        /// </summary>
        /// <param name="observer">The observer to attach to this <see cref="MessageBoard"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="observer"/> is
        /// <see langword="null"/>.</exception>
        public void Attach(IMessageBoardObserver observer)
        {
            ParameterValidation.IsNotNull(observer, nameof(observer));

            this.observers.Add(observer);
        }

        /// <summary>
        /// Detaches the specified observer from this <see cref="MessageBoard"/> so that it will no
        /// longer receive notification updates.
        /// </summary>
        /// <param name="observer">The observer to detach from this <see cref="MessageBoard"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="observer"/> is
        /// <see langword="null"/>.</exception>
        public void Detach(IMessageBoardObserver observer)
        {
            ParameterValidation.IsNotNull(observer, nameof(observer));

            this.observers.Remove(observer);
        }

        private void Notify()
        {
            foreach (var observer in this.observers)
            {
                observer.Update(this.messages);
            }
        }
    }
}
