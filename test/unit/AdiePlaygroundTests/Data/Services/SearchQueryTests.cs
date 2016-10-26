// <copyright file="SearchQueryTests.cs" company="natsnudasoft">
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
    using System.Linq;
    using AdiePlayground.Data.Services;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SearchQueryTests
    {
        [Test]
        public void SearchQuery_FilterSortAndPage_ReturnsCorrectEntities()
        {
            var query = TestData.DeepCopyTestEntityData().AsQueryable();
            var expectedEntities = new[]
            {
                TestData.TestEntityData[9],
                TestData.TestEntityData[2]
            };

            var searchQuery = new SearchQuery<TestEntity>()
                .Filter(e => e.Property1.EndsWith("one."))
                .Sort(e => e.Property2, SortOrder.Descending)
                .Page(1, 2);
            foreach (var criterion in searchQuery.SearchCriteria)
            {
                query = criterion.Apply(query);
            }

            var entities = query.ToArray();

            Assert.That(entities, Is.Not.Null);
            Assert.That(entities, Is.EqualTo(expectedEntities));
        }

        [Test]
        public void SearchQuery_Include_DoesNotThrow()
        {
            // We can not properly test Include in a unit test.
            var query = TestData.DeepCopyTestEntityData().AsQueryable();
            var searchQuery = new SearchQuery<TestEntity>()
                .Sort(e => e.Id)
                .Include(e => e.Property2);
            foreach (var criterion in searchQuery.SearchCriteria)
            {
                query = criterion.Apply(query);
            }

            var entities = query.ToArray();
            Assert.That(entities, Is.Not.Null);
        }
    }
}
