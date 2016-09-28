// <copyright file="PagingCriterion.cs" company="natsnudasoft">
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
    using System.Linq;
    using Model;

    /// <summary>
    /// Provides a criterion to be applied to a query which will select only those entities that
    /// match a paging specification.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity the query this filter will be applied to
    /// works on.</typeparam>
    /// <seealso cref="ISearchCriterion{TEntity}" />
    internal sealed class PagingCriterion<TEntity> : ISearchCriterion<TEntity>
        where TEntity : class, IModelEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingCriterion{TEntity}" /> class.
        /// </summary>
        /// <param name="skipCount">The number of entities to skip when this
        /// <see cref="PagingCriterion{TEntity}"/> is applied.</param>
        /// <param name="pageSize">The number of entities to take when this
        /// <see cref="PagingCriterion{TEntity}"/> is applied.</param>
        /// <exception cref="ArgumentException"><para><paramref name="skipCount"/> is less then 0.
        /// </para><para>-or-</para><para><paramref name="pageSize"/> is less than 1.</para>
        /// </exception>
        public PagingCriterion(int skipCount, int pageSize)
        {
            if (skipCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(skipCount),
                    skipCount,
                    "Value must be greater than or equal to 0.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(pageSize),
                    pageSize,
                    "Value must be greater than 0.");
            }

            this.SkipCount = skipCount;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// Gets the number of entities to skip to reach this page.
        /// </summary>
        public int SkipCount { get; }

        /// <summary>
        /// Gets the number of entities to take for this page.
        /// </summary>
        public int PageSize { get; }

        /// <inheritdoc/>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            // Using the lambda versions allows cached queries.
            return query.Skip(() => this.SkipCount).Take(() => this.PageSize);
        }
    }
}
