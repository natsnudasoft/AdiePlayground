// <copyright file="BubbleSortStrategyTests.cs" company="natsnudasoft">
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
    /// Tests the <see cref="BubbleSortStrategy{T}"/> class.
    /// </summary>
    [TestFixture]
    public sealed class BubbleSortStrategyTests
    {
        private static readonly IEnumerable<IList<int>> Lists = new[]
        {
            new[] { 7, 4, 1, 8, 7, 3, 2, 9 },
            new[] { 50, -4, 0, int.MinValue, 44, -148, int.MaxValue, -5359 },
            new[] { 65, 41, 30, -195, 46277, 153, 408762, 44134 }
        };

        /// <summary>
        /// Tests the SortType property returns correctly.
        /// </summary>
        [Test]
        public void SortType_CorrectSortType()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new BubbleSortStrategy<int>();
            Assert.That(sortStrategyExplicit.SortType, Is.EqualTo(SortType.BubbleSort));
        }

        /// <summary>
        /// Tests the Sort method.
        /// </summary>
        /// <param name="list">The list to test.</param>
        [Test]
        public void Sort_SortsData([ValueSource(nameof(Lists))] IList<int> list)
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new BubbleSortStrategy<int>();
            sortStrategyExplicit.Sort(list);
            Assert.That(list, Is.Ordered);
        }
    }
}
