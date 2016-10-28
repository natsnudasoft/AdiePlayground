// <copyright file="SortCriterionTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Data.Services
{
    using System;
    using System.Linq;
    using AdiePlayground.Data.Services;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SortCriterionTests
    {
        private const string ConstructorSortPropertySelectorParam = "sortPropertySelector";
        private const string ConstructorSortOrderParam = "sortOrder";

        [Test]
        public void Constructor_NullSortPropertySelector_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new SortCriterion<TestEntity, object>(null, SortOrder.Ascending));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorSortPropertySelectorParam));
        }

        [Test]
        public void Constructor_InvalidSortOrder_ArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new SortCriterion<TestEntity, int>(e => e.Id, (SortOrder)int.MinValue));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorSortOrderParam));
        }

        [Test]
        public void Constructor_DoesNotThrow(
            [Values(SortOrder.Ascending, SortOrder.Descending)] SortOrder sortOrder)
        {
            SortCriterion<TestEntity, int> criterion = null;
            Assert.DoesNotThrow(
                () => criterion = new SortCriterion<TestEntity, int>(e => e.Id, sortOrder));
            Assert.That(criterion.SortPropertySelector, Is.Not.Null);
            Assert.That(criterion.SortOrder, Is.EqualTo(sortOrder));
        }

        [Test]
        public void Apply_DoesNotThrow(
            [Values(SortOrder.Ascending, SortOrder.Descending)] SortOrder sortOrder)
        {
            var queryData = TestData.DeepCopyTestEntityData().AsQueryable();
            var criterion = new SortCriterion<TestEntity, int>(e => e.Id, sortOrder);

            Assert.DoesNotThrow(() => criterion.Apply(queryData));

            // We cannot mock extension methods so cannot verify which query method was called.
        }
    }
}
