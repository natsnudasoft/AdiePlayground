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
    using Autofac;
    using Common.Model;
    using Common.Variance;
    using Data.Services;
    using Example;

    /// <summary>
    /// Provides configuration for the IoC container.
    /// </summary>
    public static class ContainerConfiguration
    {
        /// <summary>
        /// Configures the IoC container.
        /// </summary>
        /// <returns>A configured IoC container.</returns>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DataServicesModule(new ConnectionStringFactory()));
            builder.RegisterModule(new VarianceModule());
            RegisterVariance(builder);
            return builder.Build();
        }

        private static void RegisterVariance(ContainerBuilder builder)
        {
            builder
                .Register(c => new VarianceExample(
                    c.Resolve<IInvariant<Orange>>(),
                    c.Resolve<ICovariant<Banana>>(),
                    c.Resolve<IContravariant<Fruit>>()))
                .AsSelf();
        }
    }
}
