// <copyright file="SortStrategyStub.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Strategy
{
    using System.Collections.Generic;
    using Common.Strategy;

    /// <summary>
    /// Provides a stub for the <see cref="SortStrategy{T}"/> class.
    /// </summary>
    /// <seealso cref="SortStrategy{T}" />
    internal sealed class SortStrategyStub : SortStrategy<int>
    {
        /// <inheritdoc/>
        protected override SortType SortType => SortType.Quicksort;

        /// <inheritdoc/>
        protected override void Sort(IList<int> list, IComparer<int> comparer)
        {
        }
    }
}
