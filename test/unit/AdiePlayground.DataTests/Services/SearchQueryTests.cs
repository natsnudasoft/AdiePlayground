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

namespace AdiePlayground.DataTests.Services
{
    using System.Linq;
    using Data.Services;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="SearchQuery"/> class.
    /// </summary>
    [TestFixture]
    public sealed class SearchQueryTests
    {
        /// <summary>
        /// Tests the search query filter, sort and page.
        /// </summary>
        [Test]
        public void SearchQuery_FilterSortAndPage_ReturnsCorrectEntities()
        {
            var query = TestData.DeepCopyTestEntityData().AsQueryable();
            var expectedEntities = new[]
            {
                TestData.TestEntityData[9],
                TestData.TestEntityData[2]
            };

            var searchQuery = SearchQuery.Create<TestEntity>()
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
    }
}
