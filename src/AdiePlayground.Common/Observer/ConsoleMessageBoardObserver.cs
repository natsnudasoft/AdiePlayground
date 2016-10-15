// <copyright file="ConsoleMessageBoardObserver.cs" company="natsnudasoft">
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
    using Extensions;
    using static System.FormattableString;

    /// <summary>
    /// Provides an observer that writes its updates to the <see cref="Console"/>.
    /// </summary>
    /// <seealso cref="IMessageBoardObserver" />
    public sealed class ConsoleMessageBoardObserver : IMessageBoardObserver
    {
        private readonly Guid id;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleMessageBoardObserver"/> class.
        /// </summary>
        /// <param name="guidProvider">The <see cref="IGuidProvider"/> to use to generate the id of
        /// this <see cref="ConsoleMessageBoardObserver"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="guidProvider"/> is
        /// <see langword="null"/>.</exception>
        public ConsoleMessageBoardObserver(IGuidProvider guidProvider)
        {
            if (guidProvider == null)
            {
                throw new ArgumentNullException(nameof(guidProvider));
            }

            this.id = guidProvider.NewGuid();
        }

        /// <inheritdoc/>
        void IMessageBoardObserver.Update(IEnumerable<string> messages)
        {
            if (messages == null)
            {
                throw new ArgumentNullException(nameof(messages));
            }

            Console.WriteLine(
                Invariant($"{nameof(ConsoleMessageBoardObserver)} ({this.id}) update:"));
            ConsoleExtensions.WriteColoredLine(
                string.Join(Environment.NewLine, messages),
                ConsoleColor.DarkGray);
            Console.WriteLine();
        }
    }
}
