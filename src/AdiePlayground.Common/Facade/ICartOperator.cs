// <copyright file="ICartOperator.cs" company="natsnudasoft">
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
    /// <summary>
    /// Provides an interface for a cart operator inside a gold mine.
    /// </summary>
    public interface ICartOperator
    {
        /// <summary>
        /// Moves a cart to a specified location inside a gold mine.
        /// </summary>
        /// <param name="location">The location to move the cart to.</param>
        void MoveTo(string location);

        /// <summary>
        /// Picks up some mined gold inside a gold mine.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = "PickUp",
            Justification = "Not a compound word.")]
        void PickUpGold();

        /// <summary>
        /// Deposits some gold collected from inside a gold mine.
        /// </summary>
        void DepositGold();
    }
}