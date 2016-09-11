// <copyright file="VarianceModuleTests.cs" company="natsnudasoft">
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
    using Autofac;
    using Common.Model;
    using Common.Variance;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="VarianceModule"/> class.
    /// </summary>
    [TestFixture]
    public sealed class VarianceModuleTests
    {
        /// <summary>
        /// Tests the constructor with a valid connection string factory.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new VarianceModule());
        }

        /// <summary>
        /// Tests the Load method registers all services.
        /// </summary>
        [Test]
        public void ModuleRegistered_ServicesRegistered()
        {
            var varianceModule = new VarianceModule();
            var builder = new ContainerBuilder();
            builder.RegisterModule(varianceModule);
            var container = builder.Build();

            var orangeInvariant = container.Resolve<IInvariant<Orange>>();
            var bananaCovariant = container.Resolve<ICovariant<Banana>>();
            var fruitContravariant = container.Resolve<IContravariant<Fruit>>();

            Assert.That(orangeInvariant, Is.Not.Null);
            Assert.That(bananaCovariant, Is.Not.Null);
            Assert.That(fruitContravariant, Is.Not.Null);
        }
    }
}
