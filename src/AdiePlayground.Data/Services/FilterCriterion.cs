// <copyright file="FilterCriterion.cs" company="natsnudasoft">
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
    using System.Linq;
    using System.Linq.Expressions;
    using Model;

    /// <summary>
    /// Provides a criterion to be applied to a query which will filter entities based on a given
    /// predicate.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity the query this filter will be applied to
    /// works on.</typeparam>
    /// <seealso cref="ISearchCriterion{TEntity}" />
    internal sealed class FilterCriterion<TEntity> : ISearchCriterion<TEntity>
        where TEntity : class, IModelEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCriterion{TEntity}" /> class.
        /// </summary>
        /// <param name="filterPredicate">The predicate to use when this
        /// <see cref="FilterCriterion{TEntity}"/> is applied.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filterPredicate"/> is
        /// <c>null</c>.</exception>
        public FilterCriterion(Expression<Func<TEntity, bool>> filterPredicate)
        {
            if (filterPredicate == null)
            {
                throw new ArgumentNullException(nameof(filterPredicate));
            }

            this.FilterPredicate = filterPredicate;
        }

        /// <summary>
        /// Gets the predicate used by this <see cref="FilterCriterion{TEntity}"/>.
        /// </summary>
        public Expression<Func<TEntity, bool>> FilterPredicate { get; }

        /// <inheritdoc/>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            return query.Where(this.FilterPredicate);
        }
    }
}
