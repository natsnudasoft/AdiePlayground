// <copyright file="ContextServiceTests.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Data.Services;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="ContextService"/> class.
    /// </summary>
    [TestFixture]
    public sealed class ContextServiceTests
    {
        private const string ConstructorDbContextScopeFactoryParam = "dbContextScopeFactory";

        private DbMockHelper dbMockHelper;
        private ContextService contextService;

        /// <summary>
        /// Sets up mocks before each test.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.dbMockHelper = new DbMockHelper();
            this.contextService = new ContextService(
                this.dbMockHelper.DbContextScopeFactoryMock.Object);
        }

        /// <summary>
        /// Tests the constructor with a null db context scope factory.
        /// </summary>
        [Test]
        public void Constructor_NullDbContextScopeFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ContextService(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorDbContextScopeFactoryParam));
        }

        /// <summary>
        /// Tests the BeginTransaction method.
        /// </summary>
        [Test]
        public void BeginTransaction_CreateTransactionAndDispose()
        {
            this.dbMockHelper.MockDbContextScopeFactory();

            using (var transaction = this.contextService.BeginTransaction())
            {
                Assert.That(transaction, Is.Not.Null);
            }

            this.dbMockHelper.DbContextScopeMock.Verify(m => m.Dispose(), Times.Once);
        }

        /// <summary>
        /// Tests the BeginReadOnlyTransaction method.
        /// </summary>
        [Test]
        public void BeginReadOnlyTransaction_CreateTransactionAndDispose()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();

            using (var transaction = this.contextService.BeginReadOnlyTransaction())
            {
                Assert.That(transaction, Is.Not.Null);
            }

            this.dbMockHelper.DbContextReadOnlyScopeMock.Verify(m => m.Dispose(), Times.Once);
        }

        /// <summary>
        /// Tests the FindAsync method when given a valid id.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_ValidId_ReturnCorrectEntityAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();
            const int ValidId = 1;

            var entity = await this.contextService
                .FindAsync<TestEntity>(ValidId)
                .ConfigureAwait(false);

            Assert.That(entity.Id, Is.EqualTo(ValidId));
        }

        /// <summary>
        /// Tests the FindAsync method when given an invalid id.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_InvalidId_ReturnNullAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();
            const int InvalidId = int.MinValue;

            var entity = await this.contextService
                .FindAsync<TestEntity>(InvalidId)
                .ConfigureAwait(false);

            Assert.That(entity, Is.Null);
        }

        /// <summary>
        /// Tests the FindAsync method with search criteria to return all entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_Criteria_ReturnAllEntitiesAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();
            var expectedEntities = TestData.DeepCopyTestEntityData();

            var entities = await this.contextService
                .FindAsync(new SearchQuery<TestEntity>()
                    .Sort(e => e.Id, SortOrder.Ascending))
                .ConfigureAwait(false);

            Assert.That(entities, Is.Not.Null);
            Assert.That(entities, Is.EqualTo(expectedEntities));
        }

        /// <summary>
        /// Tests the FindAsync method with search criteria that should match some entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_Criteria_ReturnFoundEntitiesAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();
            var expectedEntities = new[]
            {
                TestData.TestEntityData[6],
                TestData.TestEntityData[3],
                TestData.TestEntityData[5]
            };

            var entities = await this.contextService
                .FindAsync(new SearchQuery<TestEntity>()
                    .Filter(e => e.Property1.StartsWith("Giraffe"))
                    .Sort(e => e.Property2, SortOrder.Ascending))
                .ConfigureAwait(false);

            Assert.That(entities, Is.Not.Null);
            Assert.That(entities, Is.EqualTo(expectedEntities));
        }

        /// <summary>
        /// Tests the FindAsync method with search criteria and a selector that should match some
        /// entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_CriteriaAndSelector_ReturnFoundEntitiesAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();
            var expectedEntities = new[]
            {
                new { Select1 = TestData.TestEntityData[1].Property2 },
                new { Select1 = TestData.TestEntityData[0].Property2 },
                new { Select1 = TestData.TestEntityData[4].Property2 }
            };

            var entities = await this.contextService
                .FindAsync(
                    new SearchQuery<TestEntity>()
                        .Filter(e => e.Property1.StartsWith("Elephant"))
                        .Sort(e => e.Property2, SortOrder.Ascending),
                    e => new { Select1 = e.Property2 })
                .ConfigureAwait(false);

            Assert.That(entities, Is.Not.Null);
            Assert.That(entities, Is.EqualTo(expectedEntities));
        }

        /// <summary>
        /// Tests the FindAsync method with search criteria and an index selector that should match
        /// some entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_CriteriaAndIndexSelector_ReturnFoundEntitiesAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();
            var expectedEntities = new[]
            {
                new { Select1 = TestData.TestEntityData[1].Property2, I = 0 },
                new { Select1 = TestData.TestEntityData[0].Property2, I = 1 },
                new { Select1 = TestData.TestEntityData[4].Property2, I = 2 }
            };

            var entities = await this.contextService
                .FindAsync(
                    new SearchQuery<TestEntity>()
                        .Filter(e => e.Property1.StartsWith("Elephant"))
                        .Sort(e => e.Property2, SortOrder.Ascending),
                    (e, i) => new { Select1 = e.Property2, I = i })
                .ConfigureAwait(false);

            Assert.That(entities, Is.Not.Null);
            Assert.That(entities, Is.EqualTo(expectedEntities));
        }

        /// <summary>
        /// Tests the FindAsync method with paging criteria that should match some entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_CriteriaPaging_ReturnPagedEntitiesAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();
            var expectedEntities = new[]
            {
                TestData.TestEntityData[4],
                TestData.TestEntityData[5],
                TestData.TestEntityData[6]
            };

            var entities = await this.contextService
                .FindAsync(new SearchQuery<TestEntity>()
                    .Page(4, 3))
                .ConfigureAwait(false);

            Assert.That(entities, Is.Not.Null);
            Assert.That(entities, Is.EqualTo(expectedEntities));
        }

        /// <summary>
        /// Tests the FindAsync method with search criteria that should not match any entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_Criteria_ReturnNoEntitiesAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();
            this.dbMockHelper.MockDbSetQueryable();

            var entities = await this.contextService
                .FindAsync(
                    new SearchQuery<TestEntity>().Filter(e => e.Id == int.MaxValue))
                .ConfigureAwait(false);

            Assert.That(entities, Is.Not.Null);
            Assert.That(entities, Is.Empty);
        }

        /// <summary>
        /// Tests the AddAsync method when given an entity.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task AddAsync_AddsEntityAndSavesContextAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactory();
            this.dbMockHelper.MockDbSetAdd();
            var entityToAdd = new TestEntity();

            var affectedCount = await this.contextService
                .AddAsync(entityToAdd)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(1));
            this.dbMockHelper.DbSetMock.Verify(m => m.Add(It.IsAny<TestEntity>()), Times.Once);
            this.dbMockHelper.DbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the AddRangeAsync method when given a list of entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task AddRangeAsync_AddsEntitiesAndSavesContextAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactory();
            this.dbMockHelper.MockDbSetAddRange();
            var entitiesToAdd = Enumerable.Repeat(new TestEntity(), 10).ToArray();

            var affectedCount = await this.contextService
                .AddRangeAsync(entitiesToAdd)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(entitiesToAdd.Length));
            this.dbMockHelper.DbSetMock
                .Verify(m => m.AddRange(It.IsAny<IEnumerable<TestEntity>>()), Times.Once);
            this.dbMockHelper.DbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the RemoveAsync method when given an entity.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task RemoveAsync_RemovesEntityAndSavesContextAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactory();
            this.dbMockHelper.MockDbSetRemove();
            var entityToRemove = new TestEntity();

            var affectedCount = await this.contextService
                .RemoveAsync(entityToRemove)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(1));
            this.dbMockHelper.DbSetMock.Verify(m => m.Remove(It.IsAny<TestEntity>()), Times.Once);
            this.dbMockHelper.DbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the RemoveRangeAsync method when given a list of entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task RemoveRangeAsync_RemovesEntitiesAndSavesContextAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactory();
            this.dbMockHelper.MockDbSetRemoveRange();
            var entitiesToRemove = Enumerable.Repeat(new TestEntity(), 10).ToArray();

            var affectedCount = await this.contextService
                .RemoveRangeAsync(entitiesToRemove)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(entitiesToRemove.Length));
            this.dbMockHelper.DbSetMock
                .Verify(m => m.RemoveRange(It.IsAny<IEnumerable<TestEntity>>()), Times.Once);
            this.dbMockHelper.DbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }
    }
}
