// <copyright file="DataModuleTests.cs" company="natsnudasoft">
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
    using Autofac;
    using Data;
    using Data.Services;
    using Mehdime.Entity;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class DataModuleTests
    {
        private const string ConstructorFuncConnectionStringFactoryParam =
            "connectionStringFactory";

        private const string ConstructorIConnectionStringFactoryParam =
            "connectionStringFactory";

        [Test]
        public void Constructor_NullFuncConnectionStringFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DataModule((Func<string>)null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorFuncConnectionStringFactoryParam));
        }

        [Test]
        public void Constructor_NullIConnectionStringFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DataModule((IConnectionStringFactory)null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorIConnectionStringFactoryParam));
        }

        [Test]
        public void Constructor_FuncConnectionStringFactory_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new DataModule(() => string.Empty));
        }

        [Test]
        public void Constructor_IConnectionStringFactory_DoesNotThrow()
        {
            var connectionStringFactory = new Mock<IConnectionStringFactory>();
            Assert.DoesNotThrow(() => new DataModule(connectionStringFactory.Object));
        }

        [Test]
        public void ModuleRegistered_ServicesRegistered()
        {
            var dataModule = new DataModule(() => string.Empty);
            var builder = new ContainerBuilder();
            builder.RegisterModule(dataModule);
            var container = builder.Build();

            var contextService = container.Resolve<ContextService>();
            var ambientDbContextLocator = container.Resolve<IAmbientDbContextLocator>();

            Assert.That(contextService, Is.Not.Null);
            Assert.That(ambientDbContextLocator, Is.Not.Null);
        }
    }
}
