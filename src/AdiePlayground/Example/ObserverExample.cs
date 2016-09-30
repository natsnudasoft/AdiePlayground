// <copyright file="ObserverExample.cs" company="natsnudasoft">
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

namespace AdiePlayground.Example
{
    using System;
    using Common.Extensions;
    using Common.Observer;
    using Properties;

    /// <summary>
    /// Provides examples of using the Observer pattern.
    /// </summary>
    public sealed class ObserverExample
    {
        private readonly MessageBoard messageBoard;
        private readonly Func<IMessageBoardObserver> observerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObserverExample"/> class.
        /// </summary>
        /// <param name="messageBoard">The <see cref="MessageBoard"/> to use in this example.
        /// </param>
        /// <param name="observerFactory">The observer factory used to create new
        /// <see cref="IMessageBoardObserver"/> instances.</param>
        /// <exception cref="ArgumentNullException"><paramref name="messageBoard"/>, or
        /// <paramref name="observerFactory"/> is <c>null</c>.</exception>
        public ObserverExample(
            MessageBoard messageBoard,
            Func<IMessageBoardObserver> observerFactory)
        {
            if (messageBoard == null)
            {
                throw new ArgumentNullException(nameof(messageBoard));
            }

            if (observerFactory == null)
            {
                throw new ArgumentNullException(nameof(observerFactory));
            }

            this.messageBoard = messageBoard;
            this.observerFactory = observerFactory;
        }

        /// <summary>
        /// Runs the example.
        /// </summary>
        public void RunExample()
        {
            ConsoleExtensions.WriteColoredLine(Resources.ObserverExampleRunning, ConsoleColor.Cyan);
            var observer1 = this.observerFactory();
            var observer2 = this.observerFactory();
            this.messageBoard.AddMessage(Resources.ObserverExampleMessage1);
            this.messageBoard.AddMessage(Resources.ObserverExampleMessage2);
            this.messageBoard.Attach(observer1);
            this.messageBoard.AddMessage(Resources.ObserverExampleMessage3);
            this.messageBoard.Attach(observer2);
            this.messageBoard.RemoveMessage(Resources.ObserverExampleMessage1);
        }
    }
}
