// <copyright file="ConsoleCartOperator.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Facade
{
    using System;
    using Extensions;
    using static System.FormattableString;

    /// <summary>
    /// Provides an implementation of <see cref="ICartOperator"/> that simply outputs to the
    /// <see cref="Console"/> .
    /// </summary>
    /// <seealso cref="ICartOperator" />
    internal sealed class ConsoleCartOperator : ICartOperator
    {
        /// <inheritdoc/>
        public void MoveTo(string location)
        {
            ConsoleExtensions.WriteColoredLine(
                Invariant($"Cart operator moves to {location}."),
                ConsoleColor.DarkGray);
        }

        /// <inheritdoc/>
        public void PickUpGold()
        {
            ConsoleExtensions.WriteColoredLine(
                "Cart operator picks up some gold.",
                ConsoleColor.DarkGray);
        }

        /// <inheritdoc/>
        public void DepositGold()
        {
            ConsoleExtensions.WriteColoredLine(
                "Cart operator deposits some gold.",
                ConsoleColor.DarkGray);
        }
    }
}
