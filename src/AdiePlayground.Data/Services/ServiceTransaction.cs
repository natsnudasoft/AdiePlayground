// <copyright file="ServiceTransaction.cs" company="natsnudasoft">
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
    using System.Threading.Tasks;
    using Mehdime.Entity;
    using Properties;

    /// <summary>
    /// Represents a transaction for the context and underlying store. Any service operations
    /// performed within this transaction will be applied together in a single operation.
    /// </summary>
    /// <seealso cref="IServiceTransaction" />
    /// <seealso cref="IDisposable" />
    internal sealed class ServiceTransaction : IServiceTransaction, IDisposable
    {
        private readonly IDbContextScope dbContextScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTransaction"/> class.
        /// </summary>
        /// <param name="dbContextScopeFactoryValue">A factory to create a database context scope.
        /// </param>
        public ServiceTransaction(IDbContextScopeFactory dbContextScopeFactoryValue)
        {
            if (dbContextScopeFactoryValue == null)
            {
                throw new ArgumentNullException(
                    nameof(dbContextScopeFactoryValue),
                    Resources.DbContextScopeFactoryInvalid);
            }

            this.dbContextScope = dbContextScopeFactoryValue.Create();
        }

        /// <inheritdoc/>
        public async Task<int> CompleteAsync()
        {
            return await this.dbContextScope.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.dbContextScope.Dispose();
            }
        }
    }
}
