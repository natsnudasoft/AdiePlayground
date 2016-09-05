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
    using Properties;

    /// <summary>
    /// Provides a filtering criterion to be applied to a query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ISearchCriterion{TEntity}" />
    internal sealed class FilterCriterion<TEntity> : ISearchCriterion<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterCriterion{TEntity}" /> class.
        /// </summary>
        /// <param name="filterPredicate">The predicate that will be applied to a query.</param>
        /// <exception cref="ArgumentNullException">Thrown when filterPredicate is null.</exception>
        public FilterCriterion(Expression<Func<TEntity, bool>> filterPredicate)
        {
            if (filterPredicate == null)
            {
                throw new ArgumentNullException(
                    nameof(filterPredicate),
                    Resources.FilterCriterionPredicateInvalid);
            }

            this.FilterPredicate = filterPredicate;
        }

        /// <summary>
        /// Gets the predicate that will be applied to filter entities.
        /// </summary>
        public Expression<Func<TEntity, bool>> FilterPredicate { get; }

        /// <inheritdoc/>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            return query.Where(this.FilterPredicate);
        }
    }
}
