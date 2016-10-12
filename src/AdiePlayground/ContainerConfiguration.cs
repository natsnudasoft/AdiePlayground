// <copyright file="ContainerConfiguration.cs" company="natsnudasoft">
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

namespace AdiePlayground
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Autofac;
    using Autofac.Extras.AttributeMetadata;
    using Autofac.Extras.DynamicProxy;
    using Autofac.Features.Metadata;
    using Cli;
    using Cli.Metadata;
    using Common;
    using Data;
    using Example;

    /// <summary>
    /// Provides configuration for the IoC container.
    /// </summary>
    internal static class ContainerConfiguration
    {
        /// <summary>
        /// Configures the IoC container.
        /// </summary>
        /// <returns>A configured IoC container.</returns>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AttributedMetadataModule>();
            builder.RegisterModule(new DataModule(new ConnectionStringFactory()));
            builder.RegisterModule(new CommonModule());
            RegisterCli(builder);
            RegisterExample(builder);
            return builder.Build();
        }

        private static void RegisterCli(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<ICommand>()
                .Where(t => t.IsDefined(typeof(CommandAttribute), false))
                .Named<ICommand>(t =>
                {
                    return t.GetCustomAttribute<CommandAttribute>(false).Group;
                })
                .Named<ICommand>(t =>
                {
                    var commandAttribute = t.GetCustomAttribute<CommandAttribute>(false);
                    return commandAttribute.Group + commandAttribute.Name;
                });
            builder
                .Register<CommandGroupMetadataFactory>(c =>
                {
                    var injectedContext = c.Resolve<IComponentContext>();
                    return groupName => injectedContext
                        .ResolveNamed<IEnumerable<Lazy<ICommand, CommandMetadata>>>(groupName)
                        .Select(l => l.Metadata)
                        .ToArray();
                })
                .AsSelf();
            builder
                .Register<CommandFactory>(c =>
                {
                    var injectedContext = c.Resolve<IComponentContext>();
                    return (groupName, commandName) => injectedContext
                        .ResolveNamed<Meta<ICommand, CommandMetadata>>(groupName + commandName);
                })
                .AsSelf();
            builder
                .Register(c => new CommandResolver(c.Resolve<CommandFactory>()))
                .AsSelf();
            builder
                .Register(c => new CommandLoop(
                    c.Resolve<CommandResolver>(),
                    c.Resolve<CommandGroupMetadataFactory>()))
                .AsSelf()
                .SingleInstance();
        }

        private static void RegisterExample(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<IExample>()
                .Where(t => t.IsDefined(typeof(ExampleAttribute)))
                .Named<IExample>(t => t.GetCustomAttribute<ExampleAttribute>().Name)
                .AsImplementedInterfaces();
            builder
                .Register<ExampleMetadataCollectionFactory>(c =>
                {
                    var injectedContext = c.Resolve<IComponentContext>();
                    return () => injectedContext
                        .Resolve<IEnumerable<Lazy<IExample, ExampleMetadata>>>()
                        .Select(m => m.Metadata);
                });
            builder
                .Register(c => new InstrumentationExample())
                .As<IInstrumentationExample>()
                .EnableInterfaceInterceptors();
        }
    }
}
