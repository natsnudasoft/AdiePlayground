// <copyright file="DataServicesModuleTests.cs" company="natsnudasoft">
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
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Data.Services;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="DataServicesModule"/> class.
    /// </summary>
    [TestFixture]
    public sealed class DataServicesModuleTests
    {
        private const string ConstructorFuncConnectionStringFactoryParam =
            "connectionStringFactoryValue";

        private const string ConstructorIConnectionStringFactoryParam =
            "connectionStringFactoryValue";

        /// <summary>
        /// Tests the constructor with a null connection string factory.
        /// </summary>
        [Test]
        public void Constructor_NullFuncConnectionStringFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DataServicesModule((Func<string>)null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorFuncConnectionStringFactoryParam));
        }

        /// <summary>
        /// Tests the constructor with a null connection string factory.
        /// </summary>
        [Test]
        public void Constructor_NullIConnectionStringFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DataServicesModule((IConnectionStringFactory)null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorIConnectionStringFactoryParam));
        }

        /// <summary>
        /// Tests the constructor with a valid connection string factory.
        /// </summary>
        [Test]
        public void Constructor_FuncConnectionStringFactory_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new DataServicesModule(() => string.Empty));
        }

        /// <summary>
        /// Tests the constructor with a valid connection string factory.
        /// </summary>
        [Test]
        public void Constructor_IConnectionStringFactory_DoesNotThrow()
        {
            var connectionStringFactory = new Mock<IConnectionStringFactory>();
            Assert.DoesNotThrow(() => new DataServicesModule(connectionStringFactory.Object));
        }
    }
}
