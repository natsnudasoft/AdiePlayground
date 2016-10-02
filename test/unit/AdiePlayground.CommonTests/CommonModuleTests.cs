// <copyright file="CommonModuleTests.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.Linq;
    using Autofac;
    using Common;
    using Common.Interceptor;
    using Common.Model;
    using Common.Observer;
    using Common.Strategy;
    using Common.Variance;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="CommonModule"/> class.
    /// </summary>
    [TestFixture]
    public sealed class CommonModuleTests
    {
        /// <summary>
        /// Tests the constructor with a valid connection string factory.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new CommonModule());
        }

        /// <summary>
        /// Tests the Load method registers all services in the Variance namespace.
        /// </summary>
        [Test]
        public void ModuleRegistered_VarianceServicesRegistered()
        {
            var commonModule = new CommonModule();
            var builder = new ContainerBuilder();
            builder.RegisterModule(commonModule);
            var container = builder.Build();

            var orangeInvariant = container.Resolve<IInvariant<Orange>>();
            var bananaCovariant = container.Resolve<ICovariant<Banana>>();
            var fruitContravariant = container.Resolve<IContravariant<Fruit>>();

            Assert.That(orangeInvariant, Is.Not.Null);
            Assert.That(bananaCovariant, Is.Not.Null);
            Assert.That(fruitContravariant, Is.Not.Null);
        }

        /// <summary>
        /// Tests the Load method registers all services in the Interceptor namespace.
        /// </summary>
        [Test]
        public void ModuleRegistered_InterceptorServicesRegistered()
        {
            var commonModule = new CommonModule();
            var builder = new ContainerBuilder();
            builder.RegisterModule(commonModule);
            var container = builder.Build();

            var dateTimeProvider = container.Resolve<IDateTimeProvider>();
            var guidProvider = container.Resolve<IGuidProvider>();
            var methodInvocationCounter = container.Resolve<MethodInvocationCounter>();
            var methodInvocationTimer = container.Resolve<MethodInvocationTimer>();
            var registrars = container.Resolve<IEnumerable<IRegistrar>>();
            var consoleInstrumentationReporter =
                container.Resolve<ConsoleInstrumentationReporter>();
            var instrumentationInterceptor = container.Resolve<InstrumentationInterceptor>();
            var messageBoard = container.Resolve<MessageBoard>();
            var messageBoardObserver = container.Resolve<IMessageBoardObserver>();
            var sortStrategies = container.Resolve<IEnumerable<ISortStrategy<int>>>();
            var sortStrategyResolver = container.Resolve<SortStrategyResolver<int>>();

            Assert.That(dateTimeProvider, Is.Not.Null);
            Assert.That(guidProvider, Is.Not.Null);
            Assert.That(methodInvocationCounter, Is.Not.Null);
            Assert.That(methodInvocationTimer, Is.Not.Null);
            Assert.That(registrars, Is.Not.Null);
            var registrarsList = registrars.ToList();
            Assert.That(registrarsList, Has.Count.EqualTo(1));
            Assert.That(registrarsList, Is.All.Not.Null);
            Assert.That(consoleInstrumentationReporter, Is.Not.Null);
            Assert.That(instrumentationInterceptor, Is.Not.Null);
            Assert.That(messageBoard, Is.Not.Null);
            Assert.That(messageBoardObserver, Is.Not.Null);
            Assert.That(sortStrategies, Is.Not.Null);
            var sortStrategiesList = sortStrategies.ToList();
            Assert.That(sortStrategiesList, Has.Count.EqualTo(2));
        }
    }
}
