﻿// <copyright file="SortStrategyTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Strategy
{
    using System;
    using System.Collections.Generic;
    using AdiePlayground.Common.Strategy;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SortStrategyTests
    {
        private const string SortListParam = "list";
        private const string SortComparerParam = "comparer";

        [Test]
        public void SortType_CorrectSortType()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new SortStrategyStub();
            Assert.That(sortStrategyExplicit.SortType, Is.EqualTo(SortType.Quicksort));
        }

        [Test]
        public void Sort_DefaultComparerNullList_ArgumentNullException()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new SortStrategyStub();
            var ex = Assert.Throws<ArgumentNullException>(() => sortStrategyExplicit.Sort(null));
            Assert.That(ex.ParamName, Is.EqualTo(SortListParam));
        }

        [Test]
        public void Sort_DefaultComparer_DoesNotThrow()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new SortStrategyStub();
            Assert.DoesNotThrow(() => sortStrategyExplicit.Sort(new[] { 0 }));
        }

        [Test]
        public void Sort_NullList_ArgumentNullException()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new SortStrategyStub();
            var ex = Assert.Throws<ArgumentNullException>(
                () => sortStrategyExplicit.Sort(null, Comparer<int>.Default));
            Assert.That(ex.ParamName, Is.EqualTo(SortListParam));
        }

        [Test]
        public void Sort_NullComparer_ArgumentNullException()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new SortStrategyStub();
            var ex = Assert.Throws<ArgumentNullException>(
                () => sortStrategyExplicit.Sort(new[] { 0 }, null));
            Assert.That(ex.ParamName, Is.EqualTo(SortComparerParam));
        }

        [Test]
        public void Sort_DoesNotThrow()
        {
            var sortStrategyExplicit = (ISortStrategy<int>)new SortStrategyStub();
            Assert.DoesNotThrow(
                () => sortStrategyExplicit.Sort(new[] { 0 }, Comparer<int>.Default));
        }
    }
}
