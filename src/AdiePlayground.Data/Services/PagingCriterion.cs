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
    using Properties;

    /// <summary>
    /// Provides a paging criterion to be applied to a query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ISearchCriterion{TEntity}" />
    internal sealed class PagingCriterion<TEntity> : ISearchCriterion<TEntity>
        where TEntity : class, IModelEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingCriterion{TEntity}" /> class.
        /// </summary>
        /// <param name="skipCount">The number of entities to skip.</param>
        /// <param name="pageSize">Number of entities on the page.</param>
        /// <exception cref="ArgumentException">Thrown when skipCount or pageSize is invalid.
        /// </exception>
        public PagingCriterion(int skipCount, int pageSize)
        {
            if (skipCount < 0)
            {
                throw new ArgumentException(
                    Resources.PagingCriterionSkipCountInvalid,
                    nameof(skipCount));
            }

            if (pageSize < 1)
            {
                throw new ArgumentException(
                    Resources.PagingCriterionPageSizeInvalid,
                    nameof(pageSize));
            }

            this.SkipCount = skipCount;
            this.PageSize = pageSize;
        }

        /// <summary>
        /// Gets the number of entities to skip.
        /// </summary>
        public int SkipCount { get; }

        /// <summary>
        /// Gets the number of entities to retrieve for the page.
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
