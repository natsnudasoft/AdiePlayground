// <copyright file="CommonModule.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using Autofac;
    using Command;
    using Facade;
    using Interceptor;
    using Model;
    using Observer;
    using Strategy;
    using Variance;

    /// <summary>
    /// Provides Dependency Injection registration module for the <see cref="Common"/> namespace.
    /// </summary>
    /// <seealso cref="Module" />
    public sealed class CommonModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            LoadCommonNamespace(builder);
            LoadCommandNamespace(builder);
            LoadFacadeNamespace(builder);
            LoadInterceptorNamespace(builder);
            LoadObserverNamespace(builder);
            LoadStrategyNamespace(builder);
            LoadVarianceNamespace(builder);
        }

        private static void LoadCommonNamespace(ContainerBuilder builder)
        {
            builder.Register(c => new SystemDateTimeProvider()).As<IDateTimeProvider>();
            builder.Register(c => new SystemGuidProvider()).As<IGuidProvider>();
        }

        private static void LoadCommandNamespace(ContainerBuilder builder)
        {
            builder.Register(c => new CommandExecutionManager()).AsSelf();
            builder.Register(c => new ConsoleRobot()).As<IRobot>();
            builder
               .Register((c, p) => new MoveCommand(
                   p.Positional<IRobot>(0),
                   p.Positional<double>(1)))
               .Named<ICommand>("robot move");
            builder
               .Register((c, p) => new TurnCommand(
                   p.Positional<IRobot>(0),
                   p.Positional<double>(1)))
               .Named<ICommand>("robot turn");
            builder
               .Register((c, p) => new TurnDrillOnCommand(p.Positional<IRobot>(0)))
               .Named<ICommand>("robot drill on");
            builder
               .Register((c, p) => new TurnDrillOffCommand(p.Positional<IRobot>(0)))
               .Named<ICommand>("robot drill off");
            builder
                .Register<CommandFactory>(c =>
                {
                    var injectedContext = c.Resolve<IComponentContext>();
                    return (name, parameters) => injectedContext.ResolveNamed<ICommand>(
                        name,
                        parameters.Select((p, i) => new PositionalParameter(i, p)));
                })
                .AsSelf();
        }

        private static void LoadFacadeNamespace(ContainerBuilder builder)
        {
            builder.Register(c => new GoldMine()).AsSelf();
        }

        private static void LoadInterceptorNamespace(ContainerBuilder builder)
        {
            builder.Register(c => new MethodInvocationCounter()).AsSelf().SingleInstance();
            builder.Register(c => new MethodInvocationTimer()).AsSelf().SingleInstance();
            builder
                .Register(c => new ConsoleRegistrar(c.Resolve<IDateTimeProvider>()))
                .As<IRegistrar>();
            builder
                .Register(c => new ConsoleInstrumentationReporter(
                    c.Resolve<MethodInvocationCounter>(),
                    c.Resolve<MethodInvocationTimer>(),
                    c.Resolve<IDateTimeProvider>(),
                    c.Resolve<IGuidProvider>()))
                .AsSelf();
            builder
                .Register(c => new InstrumentationInterceptor(
                    c.Resolve<MethodInvocationCounter>(),
                    c.Resolve<MethodInvocationTimer>(),
                    c.Resolve<IEnumerable<IRegistrar>>(),
                    c.Resolve<IGuidProvider>()));
        }

        private static void LoadObserverNamespace(ContainerBuilder builder)
        {
            builder.Register(c => new MessageBoard()).AsSelf();
            builder
                .Register(c => new ConsoleMessageBoardObserver(
                    c.Resolve<IGuidProvider>()))
                .As<IMessageBoardObserver>();
        }

        private static void LoadStrategyNamespace(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(BubbleSortStrategy<>))
                .Keyed(SortType.BubbleSort, typeof(ISortStrategy<>))
                .As(typeof(ISortStrategy<>));
            builder
                .RegisterGeneric(typeof(QuicksortStrategy<>))
                .Keyed(SortType.Quicksort, typeof(ISortStrategy<>))
                .As(typeof(ISortStrategy<>));
            builder.RegisterGeneric(typeof(SortStrategyResolver<>)).AsSelf();
        }

        private static void LoadVarianceNamespace(ContainerBuilder builder)
        {
            builder.Register(c => new OrangeInvariant()).As<IInvariant<Orange>>();
            builder.Register(c => new BananaCovariant()).As<ICovariant<Banana>>();
            builder.Register(c => new FruitContravariant()).As<IContravariant<Fruit>>();
        }
    }
}
