// <copyright file="ReadOnlyServiceTransaction.cs" company="natsnudasoft">
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
    using Mehdime.Entity;

    /// <summary>
    /// Represents a read-only transaction for the context and underlying store. Any service
    /// operations performed within this transaction will be applied together in a single operation.
    /// </summary>
    /// <seealso cref="IReadOnlyServiceTransaction" />
    /// <seealso cref="IDisposable" />
    internal sealed class ReadOnlyServiceTransaction : IReadOnlyServiceTransaction, IDisposable
    {
        private readonly IDbContextReadOnlyScope dbContextReadOnlyScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyServiceTransaction"/> class.
        /// </summary>
        /// <param name="dbContextScopeFactory">The <see cref="IDbContextScopeFactory"/> used to
        /// create instances of <see cref="IDbContextScope"/> as they are needed by context
        /// operations.</param>
        public ReadOnlyServiceTransaction(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
            {
                throw new ArgumentNullException(nameof(dbContextScopeFactory));
            }

            this.dbContextReadOnlyScope = dbContextScopeFactory.CreateReadOnly();
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
                this.dbContextReadOnlyScope.Dispose();
            }
        }
    }
}
