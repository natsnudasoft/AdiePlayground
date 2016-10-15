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
    using Autofac.Features.Indexed;
    using Common;
    using Common.Command;
    using Common.Interceptor;
    using Common.Model;
    using Common.Observer;
    using Common.Strategy;
    using Common.Variance;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="CommonModule"/> class.
    /// </summary>
    [TestFixture]
    public sealed class CommonModuleTests
    {
        private static IContainer container;

        /// <summary>
        /// Builds the <see cref="IContainer"/> for all tests to use.
        /// </summary>
        [OneTimeSetUp]
        public static void BeforeAllTests()
        {
            var commonModule = new CommonModule();
            var builder = new ContainerBuilder();
            builder.RegisterModule(commonModule);
            container = builder.Build();
        }

        /// <summary>
        /// Tests the Load method registers all services in the Common namespace.
        /// </summary>
        [Test]
        public void ModuleRegistered_CommonServicesRegistered()
        {
            var dateTimeProvider = container.Resolve<IDateTimeProvider>();
            var guidProvider = container.Resolve<IGuidProvider>();

            Assert.That(dateTimeProvider, Is.Not.Null);
            Assert.That(guidProvider, Is.Not.Null);
        }

        /// <summary>
        /// Tests the Load method registers all services in the Command namespace.
        /// </summary>
        [Test]
        public void ModuleRegistered_CommandServicesRegistered()
        {
            var commandExecutionManager = container.Resolve<CommandExecutionManager>();
            var robot = container.Resolve<IRobot>();
            var robotMock = new Mock<IRobot>();
            var moveCommand = container.ResolveNamed<ICommand>(
                "robot move",
                new PositionalParameter(0, robotMock.Object),
                new PositionalParameter(1, 0D));
            var turnCommand = container.ResolveNamed<ICommand>(
                "robot turn",
                new PositionalParameter(0, robotMock.Object),
                new PositionalParameter(1, 0D));
            var drillOnCommand = container.ResolveNamed<ICommand>(
                "robot drill on",
                new PositionalParameter(0, robotMock.Object));
            var drillOffCommand = container.ResolveNamed<ICommand>(
                "robot drill off",
                new PositionalParameter(0, robotMock.Object));
            var commandFactory = container.Resolve<CommandFactory>();
            var commandFromFactory = commandFactory?.Invoke("robot move", robotMock.Object, 0D);

            Assert.That(commandExecutionManager, Is.Not.Null);
            Assert.That(robot, Is.Not.Null);
            Assert.That(moveCommand, Is.Not.Null);
            Assert.That(turnCommand, Is.Not.Null);
            Assert.That(drillOnCommand, Is.Not.Null);
            Assert.That(drillOffCommand, Is.Not.Null);
            Assert.That(commandFactory, Is.Not.Null);
            Assert.That(commandFromFactory, Is.Not.Null);
        }

        /// <summary>
        /// Tests the Load method registers all services in the Interceptor namespace.
        /// </summary>
        [Test]
        public void ModuleRegistered_InterceptorServicesRegistered()
        {
            var methodInvocationCounter = container.Resolve<MethodInvocationCounter>();
            var methodInvocationTimer = container.Resolve<MethodInvocationTimer>();
            var registrars = container.Resolve<IEnumerable<IRegistrar>>();
            var consoleInstrumentationReporter =
                container.Resolve<ConsoleInstrumentationReporter>();
            var instrumentationInterceptor = container.Resolve<InstrumentationInterceptor>();

            Assert.That(methodInvocationCounter, Is.Not.Null);
            Assert.That(methodInvocationTimer, Is.Not.Null);
            Assert.That(registrars, Is.Not.Null);
            var registrarsList = registrars.ToList();
            Assert.That(registrarsList, Has.Count.EqualTo(1));
            Assert.That(registrarsList, Is.All.Not.Null);
            Assert.That(consoleInstrumentationReporter, Is.Not.Null);
            Assert.That(instrumentationInterceptor, Is.Not.Null);
        }

        /// <summary>
        /// Tests the Load method registers all services in the Observer namespace.
        /// </summary>
        [Test]
        public void ModuleRegistered_ObserverServicesRegistered()
        {
            var messageBoard = container.Resolve<MessageBoard>();
            var messageBoardObserver = container.Resolve<IMessageBoardObserver>();

            Assert.That(messageBoard, Is.Not.Null);
            Assert.That(messageBoardObserver, Is.Not.Null);
        }

        /// <summary>
        /// Tests the Load method registers all services in the Strategy namespace.
        /// </summary>
        [Test]
        public void ModuleRegistered_StrategyServicesRegistered()
        {
            var sortStrategies = container.Resolve<IEnumerable<ISortStrategy<int>>>();
            var sortStrategiesKeyed = container.Resolve<IIndex<SortType, ISortStrategy<int>>>();
            var sortStrategyResolver = container.Resolve<SortStrategyResolver<int>>();

            Assert.That(sortStrategies, Is.Not.Null);
            var sortStrategiesList = sortStrategies.ToList();
            Assert.That(sortStrategiesList, Has.Count.EqualTo(2));
            Assert.That(sortStrategiesList, Is.All.Not.Null);
            Assert.That(sortStrategiesKeyed, Is.Not.Null);
            Assert.That(sortStrategiesKeyed[SortType.Quicksort], Is.Not.Null);
            Assert.That(sortStrategyResolver, Is.Not.Null);
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
    }
}
