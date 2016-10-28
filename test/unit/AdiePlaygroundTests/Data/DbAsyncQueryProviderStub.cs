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

namespace AdiePlaygroundTests.Data
{
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    internal class DbAsyncQueryProviderStub<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider innerProvider;

        internal DbAsyncQueryProviderStub(IQueryProvider innerProvider)
        {
            this.innerProvider = innerProvider;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new DbAsyncEnumerableStub<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new DbAsyncEnumerableStub<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return this.innerProvider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return this.innerProvider.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(
            Expression expression,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(
            Expression expression,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(this.Execute<TResult>(expression));
        }
    }
}
