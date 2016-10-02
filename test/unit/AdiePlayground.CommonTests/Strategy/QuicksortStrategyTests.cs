// <copyright file="QuicksortStrategyTests.cs" company="natsnudasoft">
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
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="QuicksortStrategy{T}"/> class.
    /// </summary>
    [TestFixture]
    public sealed class QuicksortStrategyTests
    {
        private static readonly IEnumerable<IList<int>> Lists = new[]
        {
            new[] { 9, 3, 8, 3, 2, 5, 1, 7 },
            new[] { -58642, -44, int.MinValue, 7985, -44, int.MaxValue, -5 },
            new[] { 5896756, 4971, 1565, -1167, -524677, -486, 956711, -1865690 }
        };

        /// <summary>
        /// Tests the SortType property returns correctly.
        /// </summary>
        [Test]
        public void SortType_CorrectSortType()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new QuicksortStrategy<int>();
            Assert.That(sortStrategyExplicit.SortType, Is.EqualTo(SortType.Quicksort));
        }

        /// <summary>
        /// Tests the Sort method.
        /// </summary>
        /// <param name="list">The list to test.</param>
        [Test]
        public void Sort_SortsData([ValueSource(nameof(Lists))] IList<int> list)
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new QuicksortStrategy<int>();
            sortStrategyExplicit.Sort(list);
            Assert.That(list, Is.Ordered);
        }
    }
}
