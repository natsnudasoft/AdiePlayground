// <copyright file="SortCriterion.cs" company="natsnudasoft">
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
    /// Provides a sorting criterion to be applied to a query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ISearchCriterion{TEntity}" />
    internal sealed class SortCriterion<TEntity> : ISearchCriterion<TEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortCriterion{TEntity}" /> class.
        /// </summary>
        /// <param name="sortPropertySelector">The selector to choose the property to sort on.
        /// </param>
        /// <param name="sortOrder">The sort order.</param>
        /// <exception cref="ArgumentNullException">Thrown when sortPropertySelector is null.
        /// </exception>
        /// <exception cref="ArgumentException">Thrown when sortOrder is invalid.</exception>
        public SortCriterion(
            Expression<Func<TEntity, object>> sortPropertySelector,
            SortOrder sortOrder)
        {
            if (sortPropertySelector == null)
            {
                throw new ArgumentNullException(
                    nameof(sortPropertySelector),
                    Resources.SortCriterionPropertySelectorInvalid);
            }

            if (sortOrder != SortOrder.Ascending && sortOrder != SortOrder.Descending)
            {
                throw new ArgumentException(
                    Resources.SortCriterionSortOrderInvalid,
                    nameof(sortOrder));
            }

            this.SortPropertySelector = sortPropertySelector;
            this.SortOrder = sortOrder;
        }

        /// <summary>
        /// Gets the sort property selector that selects the property this criterion will search on.
        /// </summary>
        public Expression<Func<TEntity, object>> SortPropertySelector { get; }

        /// <summary>
        /// Gets the sort order of this criterion.
        /// </summary>
        public SortOrder SortOrder { get; }

        /// <inheritdoc/>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (this.SortOrder == SortOrder.Ascending)
            {
                query = query.OrderBy(this.SortPropertySelector);
            }
            else if (this.SortOrder == SortOrder.Descending)
            {
                query = query.OrderByDescending(this.SortPropertySelector);
            }

            return query;
        }
    }
}
