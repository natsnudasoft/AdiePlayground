﻿// <copyright file="ReadOnlyServiceTransactionTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Data.Services;
    using Mehdime.Entity;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ReadOnlyServiceTransactionTests
    {
        private const string ConstructorDbContextScopeFactoryParam = "dbContextScopeFactory";

        private DbMockHelper dbMockHelper;

        [SetUp]
        public void BeforeTest()
        {
            this.dbMockHelper = new DbMockHelper();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = "Testing exception (object should never be created)")]
        [Test]
        public void Constructor_NullDbContextScopeFactory_ArgumentNullException()
        {
#pragma warning disable CC0022 // Should dispose object
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ReadOnlyServiceTransaction(null));
#pragma warning restore CC0022 // Should dispose object
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorDbContextScopeFactoryParam));
        }

        [Test]
        public void Disposal_ObjectsCorrectlyDisposed()
        {
            this.dbMockHelper.MockDbContextScopeFactoryReadOnly();

            using (var transaction =
                new ReadOnlyServiceTransaction(this.dbMockHelper.DbContextScopeFactoryMock.Object))
            {
                Assert.That(transaction, Is.Not.Null);
            }

            this.dbMockHelper.DbContextScopeFactoryMock
                .Verify(m => m.CreateReadOnly(DbContextScopeOption.JoinExisting), Times.Once);
            this.dbMockHelper.DbContextReadOnlyScopeMock.Verify(m => m.Dispose(), Times.Once);
        }
    }
}
