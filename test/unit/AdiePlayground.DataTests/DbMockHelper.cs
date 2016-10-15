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

    internal sealed class DbMockHelper
    {
        private int batchAffectedCount;

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

        public Mock<DbSet<TestEntity>> DbSetMock { get; private set; }

        public Mock<PlaygroundDbContext> PlaygroundDbContextMock { get; private set; }

        public Mock<IDbContextScope> DbContextScopeMock { get; private set; }

        public Mock<IDbContextReadOnlyScope> DbContextReadOnlyScopeMock { get; private set; }

        public Mock<IDbContextScopeFactory> DbContextScopeFactoryMock { get; private set; }

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

        public void MockDbContextScopeFactoryReadOnly()
        {
            this.DbContextReadOnlyScopeMock
                .Setup(m => m.DbContexts.Get<PlaygroundDbContext>())
                .Returns(this.PlaygroundDbContextMock.Object);

            this.DbContextScopeFactoryMock
                .Setup(m => m.CreateReadOnly(DbContextScopeOption.JoinExisting))
                .Returns(this.DbContextReadOnlyScopeMock.Object);
        }

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

        public void MockDbSetAdd()
        {
            this.DbSetMock
                .Setup(m => m.Add(It.IsAny<TestEntity>()))
                .Callback(() => ++this.batchAffectedCount);
        }

        public void MockDbSetAddRange()
        {
            this.DbSetMock
                .Setup(m => m.AddRange(It.IsAny<IEnumerable<TestEntity>>()))
                .Callback<IEnumerable<TestEntity>>(e => this.batchAffectedCount += e.Count());
        }

        public void MockDbSetRemove()
        {
            this.DbSetMock
                .Setup(m => m.Remove(It.IsAny<TestEntity>()))
                .Callback(() => ++this.batchAffectedCount);
        }

        public void MockDbSetRemoveRange()
        {
            this.DbSetMock
                .Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TestEntity>>()))
                .Callback<IEnumerable<TestEntity>>(e => this.batchAffectedCount += e.Count());
        }
    }
}
