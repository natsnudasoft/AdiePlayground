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

    /// <summary>
    /// Represents a transaction for the underlying store.
    /// </summary>
    /// <remarks>
    /// Any service operations performed within this transaction will be applied together in a
    /// single operation. This means any failed operations will roll back the entire transaction.
    /// A <see cref="ServiceTransaction"/> cannot be created within a
    /// <see cref="ReadOnlyServiceTransaction"/>.
    /// </remarks>
    /// <seealso cref="IDisposable" />
    public sealed class ServiceTransaction : IDisposable
    {
        private readonly IDbContextScope dbContextScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceTransaction"/> class.
        /// </summary>
        /// <param name="dbContextScopeFactory">The <see cref="IDbContextScopeFactory"/> used to
        /// create instances of <see cref="IDbContextScope"/> as they are needed by context
        /// operations.</param>
        internal ServiceTransaction(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
            {
                throw new ArgumentNullException(nameof(dbContextScopeFactory));
            }

            this.dbContextScope = dbContextScopeFactory.Create();
        }

        /// <summary>
        /// Completes this transaction and commits any changes made as part of it.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the
        /// number of state entries modified in the underlying store.</returns>
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
