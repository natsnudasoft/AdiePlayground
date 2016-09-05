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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Data.Services;
    using Mehdime.Entity;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="ContextService"/> class.
    /// </summary>
    [TestFixture]
    public sealed class ContextServiceTests
    {
        private const string ConstructorDbContextScopeFactoryParam = "dbContextScopeFactoryValue";

        private int batchAffectedCount;
        private Mock<DbSet<TestEntity>> dbSetMock;
        private Mock<PlaygroundDbContext> playgroundDbContextMock;
        private Mock<IDbContextScope> dbContextScopeMock;
        private Mock<IDbContextReadOnlyScope> dbContextReadOnlyScopeMock;
        private Mock<IDbContextScopeFactory> dbContextScopeFactoryMock;
        private IContextService contextService;

        /// <summary>
        /// Sets up mocks before each test.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.batchAffectedCount = 0;
            this.dbSetMock = new Mock<DbSet<TestEntity>>();
            this.playgroundDbContextMock = new Mock<PlaygroundDbContext>();
            this.dbContextScopeMock = new Mock<IDbContextScope>();
            this.dbContextReadOnlyScopeMock = new Mock<IDbContextReadOnlyScope>();
            this.dbContextScopeFactoryMock = new Mock<IDbContextScopeFactory>();

            this.playgroundDbContextMock
                .Setup(m => m.Set<TestEntity>())
                .Returns(this.dbSetMock.Object);

            this.contextService = new ContextService(this.dbContextScopeFactoryMock.Object);
        }

        /// <summary>
        /// Tests the constructor with a null db context scope factory.
        /// </summary>
        [Test]
        public void Constructor_NullDbContextScopeFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ContextService(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorDbContextScopeFactoryParam));
            Assert.That(
                ex.Message,
                Does.StartWith(Data.Properties.Resources.DbContextScopeFactoryInvalid));
        }

        /// <summary>
        /// Tests the FindAsync method when given a valid id.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task FindAsync_ValidId_ReturnCorrectEntityAsync()
        {
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();
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
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();
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
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();
            var expectedEntities = TestData.DeepCopyTestEntityData();

            var entities = await this.contextService
                .FindAsync(SearchQuery.Create<TestEntity>()
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
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();
            var expectedEntities = new[]
            {
                TestData.TestEntityData[6],
                TestData.TestEntityData[3],
                TestData.TestEntityData[5]
            };

            var entities = await this.contextService
                .FindAsync(SearchQuery.Create<TestEntity>()
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
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();
            var expectedEntities = new[]
            {
                new { Select1 = TestData.TestEntityData[1].Property2 },
                new { Select1 = TestData.TestEntityData[0].Property2 },
                new { Select1 = TestData.TestEntityData[4].Property2 }
            };

            var entities = await this.contextService
                .FindAsync(
                    SearchQuery.Create<TestEntity>()
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
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();
            var expectedEntities = new[]
            {
                new { Select1 = TestData.TestEntityData[1].Property2, I = 0 },
                new { Select1 = TestData.TestEntityData[0].Property2, I = 1 },
                new { Select1 = TestData.TestEntityData[4].Property2, I = 2 }
            };

            var entities = await this.contextService
                .FindAsync(
                    SearchQuery.Create<TestEntity>()
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
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();
            var expectedEntities = new[]
            {
                TestData.TestEntityData[4],
                TestData.TestEntityData[5],
                TestData.TestEntityData[6]
            };

            var entities = await this.contextService
                .FindAsync(SearchQuery.Create<TestEntity>()
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
            this.SetUpReadOnlyContextScopeMock();
            this.SetUpQueryDataMock();

            var entities = await this.contextService
                .FindAsync(
                    SearchQuery.Create<TestEntity>().Filter(e => e.Id == int.MaxValue))
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
            this.SetUpContextScopeMock();
            this.SetUpSetAddMock();
            var entityToAdd = new TestEntity();

            var affectedCount = await this.contextService
                .AddAsync(entityToAdd)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(1));
            this.dbSetMock.Verify(m => m.Add(It.IsAny<TestEntity>()), Times.Once);
            this.dbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the AddRangeAsync method when given a list of entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task AddRangeAsync_AddsEntitiesAndSavesContextAsync()
        {
            this.SetUpContextScopeMock();
            this.SetUpSetAddRangeMock();
            var entitiesToAdd = Enumerable.Repeat(new TestEntity(), 10).ToArray();

            var affectedCount = await this.contextService
                .AddRangeAsync(entitiesToAdd)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(entitiesToAdd.Length));
            this.dbSetMock
                .Verify(m => m.AddRange(It.IsAny<IEnumerable<TestEntity>>()), Times.Once);
            this.dbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the RemoveAsync method when given an entity.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task RemoveAsync_RemovesEntityAndSavesContextAsync()
        {
            this.SetUpContextScopeMock();
            this.SetUpSetRemoveMock();
            var entityToRemove = new TestEntity();

            var affectedCount = await this.contextService
                .RemoveAsync(entityToRemove)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(1));
            this.dbSetMock.Verify(m => m.Remove(It.IsAny<TestEntity>()), Times.Once);
            this.dbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        /// <summary>
        /// Tests the RemoveRangeAsync method when given a list of entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task RemoveRangeAsync_RemovesEntitiesAndSavesContextAsync()
        {
            this.SetUpContextScopeMock();
            this.SetUpSetRemoveRangeMock();
            var entitiesToRemove = Enumerable.Repeat(new TestEntity(), 10).ToArray();

            var affectedCount = await this.contextService
                .RemoveRangeAsync(entitiesToRemove)
                .ConfigureAwait(false);

            Assert.That(affectedCount, Is.EqualTo(entitiesToRemove.Length));
            this.dbSetMock
                .Verify(m => m.RemoveRange(It.IsAny<IEnumerable<TestEntity>>()), Times.Once);
            this.dbContextScopeMock
                .Verify(m => m.SaveChangesAsync(), Times.Once);
        }

        private void SetUpContextScopeMock()
        {
            this.dbContextScopeMock
                .Setup(m => m.DbContexts.Get<PlaygroundDbContext>())
                .Returns(this.playgroundDbContextMock.Object);
            this.dbContextScopeMock
                .Setup(m => m.SaveChangesAsync())
                .Returns(() => Task.FromResult(this.batchAffectedCount))
                .Callback(() => this.batchAffectedCount = 0);

            this.dbContextScopeFactoryMock
                .Setup(m => m.Create(DbContextScopeOption.JoinExisting))
                .Returns(this.dbContextScopeMock.Object);
        }

        private void SetUpReadOnlyContextScopeMock()
        {
            this.dbContextReadOnlyScopeMock
                .Setup(m => m.DbContexts.Get<PlaygroundDbContext>())
                .Returns(this.playgroundDbContextMock.Object);

            this.dbContextScopeFactoryMock
                .Setup(m => m.CreateReadOnly(DbContextScopeOption.JoinExisting))
                .Returns(this.dbContextReadOnlyScopeMock.Object);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Maintainability",
            "CA1506:AvoidExcessiveClassCoupling",
            Justification = "Necessary mocking.")]
        private void SetUpQueryDataMock()
        {
            var queryData = TestData.DeepCopyTestEntityData().AsQueryable();

            this.dbSetMock
                .As<IDbAsyncEnumerable<TestEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(() => new DbAsyncEnumeratorStub<TestEntity>(queryData.GetEnumerator()));
            this.dbSetMock
                .As<IQueryable<TestEntity>>()
                .SetupGet(m => m.Provider)
                .Returns(() => new DbAsyncQueryProviderStub<TestEntity>(queryData.Provider));
            this.dbSetMock
                .As<IQueryable<TestEntity>>()
                .SetupGet(m => m.Expression)
                .Returns(() => queryData.Expression);
            this.dbSetMock
                .As<IQueryable<TestEntity>>()
                .SetupGet(m => m.ElementType)
                .Returns(() => queryData.ElementType);
            this.dbSetMock
                .As<IQueryable<TestEntity>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => queryData.GetEnumerator());
            this.dbSetMock
                .Setup(m => m.FindAsync(It.IsAny<int>()))
                .Returns<object>(
                    o => this.dbSetMock.Object
                        .AsQueryable()
                        .FirstOrDefaultAsync(e => e.Id == (int)((object[])o)[0]));
        }

        private void SetUpSetAddMock()
        {
            this.dbSetMock
                .Setup(m => m.Add(It.IsAny<TestEntity>()))
                .Callback(() => ++this.batchAffectedCount);
        }

        private void SetUpSetAddRangeMock()
        {
            this.dbSetMock
                .Setup(m => m.AddRange(It.IsAny<IEnumerable<TestEntity>>()))
                .Callback<IEnumerable<TestEntity>>(e => this.batchAffectedCount += e.Count());
        }

        private void SetUpSetRemoveMock()
        {
            this.dbSetMock
                .Setup(m => m.Remove(It.IsAny<TestEntity>()))
                .Callback(() => ++this.batchAffectedCount);
        }

        private void SetUpSetRemoveRangeMock()
        {
            this.dbSetMock
                .Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TestEntity>>()))
                .Callback<IEnumerable<TestEntity>>(e => this.batchAffectedCount += e.Count());
        }
    }
}
