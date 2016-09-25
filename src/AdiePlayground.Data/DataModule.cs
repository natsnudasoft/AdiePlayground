// <copyright file="DataModule.cs" company="natsnudasoft">
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

namespace AdiePlayground.Data
{
    using System;
    using Autofac;
    using Mehdime.Entity;
    using Properties;
    using Services;

    /// <summary>
    /// Provides Dependency Injection registration module for
    /// <see cref="AdiePlayground.Data"/>.
    /// </summary>
    /// <seealso cref="Module" />
    public sealed class DataModule : Module
    {
        private readonly Func<string> connectionStringFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModule"/> class which will use
        /// the specified connection string factory.
        /// </summary>
        /// <param name="connectionStringFactory">The connection string factory.</param>
        public DataModule(Func<string> connectionStringFactory)
        {
            if (connectionStringFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(connectionStringFactory),
                    Resources.ConnectionStringFactoryInvalid);
            }

            this.connectionStringFactory = connectionStringFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataModule"/> class which will use
        /// the specified connection string factory.
        /// </summary>
        /// <param name="connectionStringFactory">The connection string factory.</param>
        public DataModule(IConnectionStringFactory connectionStringFactory)
        {
            if (connectionStringFactory == null)
            {
                throw new ArgumentNullException(
                    nameof(connectionStringFactory),
                    Resources.ConnectionStringFactoryInvalid);
            }

            this.connectionStringFactory = connectionStringFactory.CreateConnectionString;
        }

        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // Register DbContext objects
            builder
                .Register(c => new ConnectionStringDbContextFactory(this.connectionStringFactory))
                .As<IDbContextFactory>();
            builder
                .Register(c => new DbContextScopeFactory(c.Resolve<IDbContextFactory>()))
                .As<IDbContextScopeFactory>();
            builder
                .Register(c => new AmbientDbContextLocator())
                .As<IAmbientDbContextLocator>();

            // Register context services
            builder
                .Register(c => new ContextService(c.Resolve<IDbContextScopeFactory>()))
                .As<IContextService>();
        }
    }
}
