// <copyright file="DbAsyncQueryProviderStub.cs" company="natsnudasoft">
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

namespace AdiePlayground.DataTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Stub for <see cref="DbAsyncQueryProviderStub{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="System.Data.Entity.Infrastructure.IDbAsyncQueryProvider" />
    internal class DbAsyncQueryProviderStub<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider inner;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbAsyncQueryProviderStub{TEntity}"/> class.
        /// </summary>
        /// <param name="innerProvider">The inner provider.</param>
        internal DbAsyncQueryProviderStub(IQueryProvider innerProvider)
        {
            this.inner = innerProvider;
        }

        /// <inheritdoc/>
        public IQueryable CreateQuery(Expression expression)
        {
            return new DbAsyncEnumerableStub<TEntity>(expression);
        }

        /// <inheritdoc/>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new DbAsyncEnumerableStub<TElement>(expression);
        }

        /// <inheritdoc/>
        public object Execute(Expression expression)
        {
            return this.inner.Execute(expression);
        }

        /// <inheritdoc/>
        public TResult Execute<TResult>(Expression expression)
        {
            return this.inner.Execute<TResult>(expression);
        }

        /// <inheritdoc/>
        public Task<object> ExecuteAsync(
            Expression expression,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute(expression));
        }

        /// <inheritdoc/>
        public Task<TResult> ExecuteAsync<TResult>(
            Expression expression,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute<TResult>(expression));
        }
    }
}
