// <copyright file="PagingCriterionTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.DataTests.Services
{
    using System;
    using System.Linq;
    using Data.Services;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="PagingCriterion{TEntity}"/> class.
    /// </summary>
    [TestFixture]
    public sealed class PagingCriterionTests
    {
        private const string ConstructorSkipCountParam = "skipCount";
        private const string ConstructorPageSizeParam = "pageSize";

        /// <summary>
        /// Tests the constructor with an invalid skip count.
        /// </summary>
        [Test]
        public void Constructor_InvalidSkipCount_ArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new PagingCriterion<TestEntity>(int.MinValue, 10));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorSkipCountParam));
        }

        /// <summary>
        /// Tests the constructor with an invalid page size.
        /// </summary>
        [Test]
        public void Constructor_InvalidPageSize_ArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new PagingCriterion<TestEntity>(0, int.MinValue));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorPageSizeParam));
        }

        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            const int SkipCount = 4;
            const int PageSize = 3;

            PagingCriterion<TestEntity> criterion = null;
            Assert.DoesNotThrow(
                () => criterion = new PagingCriterion<TestEntity>(SkipCount, PageSize));
            Assert.That(criterion.SkipCount, Is.EqualTo(SkipCount));
            Assert.That(criterion.PageSize, Is.EqualTo(PageSize));
        }

        /// <summary>
        /// Tests the apply method.
        /// </summary>
        [Test]
        public void Apply_DoesNotThrow()
        {
            const int SkipCount = 15;
            const int PageSize = 5;

            var queryData = TestData.DeepCopyTestEntityData().AsQueryable();
            var criterion = new PagingCriterion<TestEntity>(SkipCount, PageSize);

            Assert.DoesNotThrow(() => criterion.Apply(queryData));

            // We cannot mock extension methods so cannot verify which query method was called.
        }
    }
}
