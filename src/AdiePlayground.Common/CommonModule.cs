﻿// <copyright file="CommonModule.cs" company="natsnudasoft">
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
    using Autofac;
    using Interceptor;
    using Model;
    using Variance;

    /// <summary>
    /// Provides Dependency Injection registration module for
    /// <see cref="Common"/>.
    /// </summary>
    /// <seealso cref="Module" />
    public sealed class CommonModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            LoadVarianceNamespace(builder);
            LoadInterceptorNamespace(builder);
        }

        private static void LoadVarianceNamespace(ContainerBuilder builder)
        {
            builder.Register(c => new OrangeInvariant()).As<IInvariant<Orange>>();
            builder.Register(c => new BananaCovariant()).As<ICovariant<Banana>>();
            builder.Register(c => new FruitContravariant()).As<IContravariant<Fruit>>();
        }

        private static void LoadInterceptorNamespace(ContainerBuilder builder)
        {
            builder
                .Register(c => new SystemDateTimeProvider())
                .As<IDateTimeProvider>();
            builder
                .Register(c => new SystemGuidProvider())
                .As<IGuidProvider>();
            builder
                .Register(c => new MethodInvocationCounter())
                .AsSelf()
                .SingleInstance();
            builder
                .Register(c => new MethodInvocationTimer())
                .AsSelf()
                .SingleInstance();
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
    }
}