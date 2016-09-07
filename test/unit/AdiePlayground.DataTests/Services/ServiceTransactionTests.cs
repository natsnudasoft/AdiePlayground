// <copyright file="ServiceTransactionTests.cs" company="natsnudasoft">
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
    using System.Threading.Tasks;
    using Data.Services;
    using Mehdime.Entity;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="ServiceTransaction"/> class.
    /// </summary>
    [TestFixture]
    public sealed class ServiceTransactionTests
    {
        private const string ConstructorDbContextScopeFactoryParam = "dbContextScopeFactoryValue";

        private DbMockHelper dbMockHelper;
        private IContextService contextService;

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "Testing exception (object should never be created)")]
        public void Constructor_NullDbContextScopeFactory_ArgumentNullException()
        {
#pragma warning disable CC0022 // Should dispose object
            var ex = Assert.Throws<ArgumentNullException>(() => new ServiceTransaction(null));
#pragma warning restore CC0022 // Should dispose object
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorDbContextScopeFactoryParam));
        }

        /// <summary>
        /// Tests proper disposal.
        /// </summary>
        [Test]
        public void Disposal_ObjectsCorrectlyDisposed()
        {
            this.dbMockHelper.MockDbContextScopeFactory();

            using (var transaction =
                new ServiceTransaction(this.dbMockHelper.DbContextScopeFactoryMock.Object))
            {
                Assert.That(transaction, Is.Not.Null);
            }

            this.dbMockHelper.DbContextScopeFactoryMock
                .Verify(m => m.Create(DbContextScopeOption.JoinExisting), Times.Once);
            this.dbMockHelper.DbContextScopeMock.Verify(m => m.Dispose(), Times.Once);
        }

        /// <summary>
        /// Tests the CompleteAsync method.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        [Test]
        public async Task CompleteAsync_SaveChangesAsyncCalledAsync()
        {
            this.dbMockHelper.MockDbContextScopeFactory();

            using (var transaction =
                new ServiceTransaction(this.dbMockHelper.DbContextScopeFactoryMock.Object))
            {
                await transaction.CompleteAsync().ConfigureAwait(false);
            }

            this.dbMockHelper.DbContextScopeMock.Verify(m => m.SaveChangesAsync(), Times.Once);
        }
    }
}
