// <copyright file="ConnectionStringDbContextFactoryTests.cs" company="natsnudasoft">
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
    using Data.Services;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConnectionStringDbContextFactoryTests
    {
        private const string ConstructorConnectionStringFactoryParam =
            "connectionStringFactory";

        [Test]
        public void Constructor_NullConnectionStringFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ConnectionStringDbContextFactory(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorConnectionStringFactoryParam));
        }

        [Test]
        public void Constructor_ConnectionStringFactory_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ConnectionStringDbContextFactory(() => string.Empty));
        }
    }
}
