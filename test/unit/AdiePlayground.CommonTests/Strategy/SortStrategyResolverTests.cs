// <copyright file="SortStrategyResolverTests.cs" company="natsnudasoft">
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
    using System;
    using Common.Strategy;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="SortStrategyResolver{T}"/> class.
    /// </summary>
    [TestFixture]
    public sealed class SortStrategyResolverTests
    {
        private const string ConstructorSortStrategiesParam = "sortStrategies";
        private const string ResolveSortTypeParam = "sortType";

        private Mock<ISortStrategy<int>> sortStrategyMock;

        /// <summary>
        /// Sets up mocks before each test.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.sortStrategyMock = new Mock<ISortStrategy<int>>();
            this.sortStrategyMock.SetupGet(s => s.SortType).Returns(SortType.Quicksort);
        }

        /// <summary>
        /// Tests the constructor with a null sort strategies collection.
        /// </summary>
        [Test]
        public void Constructor_NullStrategies_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new SortStrategyResolver<int>(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorSortStrategiesParam));
        }

        /// <summary>
        /// Tests the constructor with valid values.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => this.CreateSortStrategyResolver());
        }

        /// <summary>
        /// Tests the Resolve method with an invalid sort type.
        /// </summary>
        [Test]
        public void Resolve_InvalidSortType_ArgumentOutOfRangeException()
        {
            var sortStrategyResolver = this.CreateSortStrategyResolver();

            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => sortStrategyResolver.Resolve((SortType)int.MinValue));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveSortTypeParam));
        }

        /// <summary>
        /// Tests the Resolve method with a sort type that can't be resolved.
        /// </summary>
        [Test]
        public void Resolve_CannotResolve_ArgumentException()
        {
            var sortStrategyResolver = this.CreateSortStrategyResolver();

            var ex = Assert.Throws<ArgumentException>(
                () => sortStrategyResolver.Resolve(SortType.BubbleSort));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveSortTypeParam));
        }

        /// <summary>
        /// Tests the Resolve method with a sort type that can be resolved.
        /// </summary>
        [Test]
        public void Resolve_ValidResolve()
        {
            var sortStrategyResolver = this.CreateSortStrategyResolver();

            var sortStrategy = sortStrategyResolver.Resolve(SortType.Quicksort);

            Assert.That(sortStrategy, Is.Not.Null);
            Assert.That(sortStrategy.SortType, Is.EqualTo(SortType.Quicksort));
        }

        private SortStrategyResolver<int> CreateSortStrategyResolver()
        {
            return new SortStrategyResolver<int>(new[] { this.sortStrategyMock.Object });
        }
    }
}
