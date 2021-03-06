﻿// <copyright file="ReadOnlyServiceTransaction.cs" company="natsnudasoft">
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
    using Common;
    using Mehdime.Entity;

    /// <summary>
    /// Represents a read-only transaction on the underlying store.
    /// </summary>
    /// <remarks>
    /// Any service operations performed within this transaction will be applied together in a
    /// single operation. This means any failed operations will roll back the entire transaction.
    /// Read-only transactions will not track changes to entities unless they are created within
    /// a regular <see cref="ServiceTransaction"/>.
    /// </remarks>
    /// <seealso cref="IDisposable" />
    public sealed class ReadOnlyServiceTransaction : IDisposable
    {
        private readonly IDbContextReadOnlyScope dbContextReadOnlyScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyServiceTransaction"/> class.
        /// </summary>
        /// <param name="dbContextScopeFactory">The <see cref="IDbContextScopeFactory"/> used to
        /// create instances of <see cref="IDbContextScope"/> as they are needed by context
        /// operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContextScopeFactory"/> is
        /// <see langword="null"/>.</exception>
        internal ReadOnlyServiceTransaction(IDbContextScopeFactory dbContextScopeFactory)
        {
            ParameterValidation.IsNotNull(dbContextScopeFactory, nameof(dbContextScopeFactory));

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
