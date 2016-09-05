// <copyright file="ConnectionStringDbContextFactory.cs" company="natsnudasoft">
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

namespace AdiePlayground.Data.Services
{
    using System;
    using System.Data.Entity;
    using Mehdime.Entity;

    /// <summary>
    /// Provides a default factory for creating a <see cref="DbContext"/> which requires a
    /// connection string parameter.
    /// </summary>
    /// <seealso cref="IDbContextFactory" />
    internal sealed class ConnectionStringDbContextFactory : IDbContextFactory
    {
        private readonly Func<string> connectionStringFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionStringDbContextFactory" /> class
        /// which will use the specified connection string factory.
        /// </summary>
        /// <param name="connectionStringFactoryValue">The connection string factory.</param>
        /// <exception cref="ArgumentNullException">A <code>null</code> connection string factory
        /// was supplied.</exception>
        public ConnectionStringDbContextFactory(Func<string> connectionStringFactoryValue)
        {
            if (connectionStringFactoryValue == null)
            {
                throw new ArgumentNullException(
                    nameof(connectionStringFactoryValue),
                    Properties.Resources.ConnectionStringFactoryInvalid);
            }

            this.connectionStringFactory = connectionStringFactoryValue;
        }

        /// <inheritdoc/>
        TDbContext IDbContextFactory.CreateDbContext<TDbContext>()
        {
            return (TDbContext)Activator.CreateInstance(
                typeof(TDbContext),
                this.connectionStringFactory());
        }
    }
}
