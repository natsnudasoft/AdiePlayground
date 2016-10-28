// <copyright file="DbAsyncEnumerableStub.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;

    internal class DbAsyncEnumerableStub<TEntity> :
        EnumerableQuery<TEntity>,
        IDbAsyncEnumerable<TEntity>,
        IQueryable<TEntity>
    {
        public DbAsyncEnumerableStub(IEnumerable<TEntity> enumerable)
            : base(enumerable)
        {
        }

        public DbAsyncEnumerableStub(Expression expression)
            : base(expression)
        {
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new DbAsyncQueryProviderStub<TEntity>(this); }
        }

        public IDbAsyncEnumerator<TEntity> GetAsyncEnumerator()
        {
            return new DbAsyncEnumeratorStub<TEntity>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return this.GetAsyncEnumerator();
        }
    }
}
