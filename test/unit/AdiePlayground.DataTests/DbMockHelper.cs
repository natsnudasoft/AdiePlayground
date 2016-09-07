// <copyright file="DbMockHelper.cs" company="natsnudasoft">
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

namespace AdiePlayground.DataTests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using Mehdime.Entity;
    using Moq;

    /// <summary>
    /// Provides helper methods for setting up mocks relating to DbContext etc.
    /// </summary>
    internal sealed class DbMockHelper
    {
        private int batchAffectedCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbMockHelper"/> class.
        /// </summary>
        public DbMockHelper()
        {
            this.batchAffectedCount = 0;
            this.DbSetMock = new Mock<DbSet<TestEntity>>();
            this.PlaygroundDbContextMock = new Mock<PlaygroundDbContext>();
            this.DbContextScopeMock = new Mock<IDbContextScope>();
            this.DbContextReadOnlyScopeMock = new Mock<IDbContextReadOnlyScope>();
            this.DbContextScopeFactoryMock = new Mock<IDbContextScopeFactory>();

            this.PlaygroundDbContextMock
                .Setup(m => m.Set<TestEntity>())
                .Returns(this.DbSetMock.Object);
        }

        /// <summary>
        /// Gets the database set mock.
        /// </summary>
        public Mock<DbSet<TestEntity>> DbSetMock { get; private set; }

        /// <summary>
        /// Gets the playground database context mock.
        /// </summary>
        public Mock<PlaygroundDbContext> PlaygroundDbContextMock { get; private set; }

        /// <summary>
        /// Gets the database context scope mock.
        /// </summary>
        public Mock<IDbContextScope> DbContextScopeMock { get; private set; }

        /// <summary>
        /// Gets the database context read only scope mock.
        /// </summary>
        public Mock<IDbContextReadOnlyScope> DbContextReadOnlyScopeMock { get; private set; }

        /// <summary>
        /// Gets the database context scope factory mock.
        /// </summary>
        public Mock<IDbContextScopeFactory> DbContextScopeFactoryMock { get; private set; }

        /// <summary>
        /// Wraps a given mock around the given query data.
        /// </summary>
        /// <param name="queryMock">The query mock.</param>
        /// <param name="queryData">The query data.</param>
        public static void MockIQueryable(
            Mock<IQueryable<TestEntity>> queryMock,
            IQueryable<TestEntity> queryData)
        {
            queryMock
                .SetupGet(m => m.Provider)
                .Returns(() => new DbAsyncQueryProviderStub<TestEntity>(queryData.Provider));
            queryMock
                .SetupGet(m => m.Expression)
                .Returns(() => queryData.Expression);
            queryMock
                .SetupGet(m => m.ElementType)
                .Returns(() => queryData.ElementType);
            queryMock
                .Setup(m => m.GetEnumerator())
                .Returns(() => queryData.GetEnumerator());
        }

        /// <summary>
        /// Mocks the database context scope factory methods.
        /// </summary>
        public void MockDbContextScopeFactory()
        {
            this.DbContextScopeMock
                .Setup(m => m.DbContexts.Get<PlaygroundDbContext>())
                .Returns(this.PlaygroundDbContextMock.Object);
            this.DbContextScopeMock
                .Setup(m => m.SaveChangesAsync())
                .Returns(() => Task.FromResult(this.batchAffectedCount))
                .Callback(() => this.batchAffectedCount = 0);

            this.DbContextScopeFactoryMock
                .Setup(m => m.Create(DbContextScopeOption.JoinExisting))
                .Returns(this.DbContextScopeMock.Object);
        }

        /// <summary>
        /// Mocks the database context scope factory read only methods.
        /// </summary>
        public void MockDbContextScopeFactoryReadOnly()
        {
            this.DbContextReadOnlyScopeMock
                .Setup(m => m.DbContexts.Get<PlaygroundDbContext>())
                .Returns(this.PlaygroundDbContextMock.Object);

            this.DbContextScopeFactoryMock
                .Setup(m => m.CreateReadOnly(DbContextScopeOption.JoinExisting))
                .Returns(this.DbContextReadOnlyScopeMock.Object);
        }

        /// <summary>
        /// Mocks the database set <see cref="IQueryable{TestEntity}"/> methods.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Maintainability",
            "CA1506:AvoidExcessiveClassCoupling",
            Justification = "Necessary mocking.")]
        public void MockDbSetQueryable()
        {
            var queryData = TestData.DeepCopyTestEntityData().AsQueryable();

            this.DbSetMock
                .As<IDbAsyncEnumerable<TestEntity>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(() => new DbAsyncEnumeratorStub<TestEntity>(queryData.GetEnumerator()));

            MockIQueryable(this.DbSetMock.As<IQueryable<TestEntity>>(), queryData);

            this.DbSetMock
                .Setup(m => m.FindAsync(It.IsAny<int>()))
                .Returns<object>(
                    o => this.DbSetMock.Object
                        .AsQueryable()
                        .FirstOrDefaultAsync(e => e.Id == (int)((object[])o)[0]));
        }

        /// <summary>
        /// Mocks the database set add method.
        /// </summary>
        public void MockDbSetAdd()
        {
            this.DbSetMock
                .Setup(m => m.Add(It.IsAny<TestEntity>()))
                .Callback(() => ++this.batchAffectedCount);
        }

        /// <summary>
        /// Mocks the database set add range method.
        /// </summary>
        public void MockDbSetAddRange()
        {
            this.DbSetMock
                .Setup(m => m.AddRange(It.IsAny<IEnumerable<TestEntity>>()))
                .Callback<IEnumerable<TestEntity>>(e => this.batchAffectedCount += e.Count());
        }

        /// <summary>
        /// Mocks the database set remove method.
        /// </summary>
        public void MockDbSetRemove()
        {
            this.DbSetMock
                .Setup(m => m.Remove(It.IsAny<TestEntity>()))
                .Callback(() => ++this.batchAffectedCount);
        }

        /// <summary>
        /// Mocks the database set remove range method.
        /// </summary>
        public void MockDbSetRemoveRange()
        {
            this.DbSetMock
                .Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TestEntity>>()))
                .Callback<IEnumerable<TestEntity>>(e => this.batchAffectedCount += e.Count());
        }
    }
}
