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
    using Model;

    /// <summary>
    /// Provides a sorting criterion to be applied to a query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TProperty">The type the property selector will return.</typeparam>
    /// <seealso cref="ISearchCriterion{TEntity}" />
    internal sealed class SortCriterion<TEntity, TProperty> : ISearchCriterion<TEntity>
        where TEntity : class, IModelEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SortCriterion{TEntity, TProperty}" />
        /// class.
        /// </summary>
        /// <param name="sortPropertySelector">The selector to choose the property to sort on.
        /// </param>
        /// <param name="sortOrder">The sort order.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sortPropertySelector"/> is
        /// <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="sortOrder"/> specified an
        /// invalid value.</exception>
        public SortCriterion(
            Expression<Func<TEntity, TProperty>> sortPropertySelector,
            SortOrder sortOrder)
        {
            if (sortPropertySelector == null)
            {
                throw new ArgumentNullException(nameof(sortPropertySelector));
            }

            switch (sortOrder)
            {
                case SortOrder.Ascending:
                case SortOrder.Descending:
                    this.SortOrder = sortOrder;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder));
            }

            this.SortPropertySelector = sortPropertySelector;
        }

        /// <summary>
        /// Gets the sort property selector that selects the property this criterion will search on.
        /// </summary>
        public Expression<Func<TEntity, TProperty>> SortPropertySelector { get; }

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
