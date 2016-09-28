// <copyright file="SearchQuery.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Model;

    /// <summary>
    /// Represents a search query that can be built up and applied to operations that work on a
    /// context in the underlying store.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity this search query operates on.</typeparam>
    public sealed class SearchQuery<TEntity>
        where TEntity : class, IModelEntity
    {
        private readonly List<ISearchCriterion<TEntity>> searchCriteria =
            new List<ISearchCriterion<TEntity>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchQuery{TEntity}"/> class which can be
        /// used to build up a search query.
        /// </summary>
        public SearchQuery()
        {
        }

        /// <summary>
        /// Gets the criteria that will be evaluated when this <see cref="SearchQuery{TEntity}"/>
        /// is applied.
        /// </summary>
        internal IEnumerable<ISearchCriterion<TEntity>> SearchCriteria => this.searchCriteria;

        /// <summary>
        /// Adds the specified filter criterion to this <see cref="SearchQuery{TEntity}"/>.
        /// </summary>
        /// <param name="filterPredicate">The predicate to use when this filter criterion is
        /// applied.
        /// </param>
        /// <returns>This <see cref="SearchQuery{TEntity}"/> with the specified filter criterion
        /// added.</returns>
        public SearchQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filterPredicate)
        {
            this.searchCriteria.Add(new FilterCriterion<TEntity>(filterPredicate));
            return this;
        }

        /// <summary>
        /// Adds the specified eager loading criterion to this <see cref="SearchQuery{TEntity}"/>.
        /// </summary>
        /// <param name="includePropertySelector">The property selector to use when this eager
        /// loading criterion is applied.</param>
        /// <returns>This <see cref="SearchQuery{TEntity}"/> with the specified eager loading
        /// criterion added.</returns>
        public SearchQuery<TEntity> Include(
            Expression<Func<TEntity, object>> includePropertySelector)
        {
            this.searchCriteria.Add(new IncludeCriterion<TEntity>(includePropertySelector));
            return this;
        }

        /// <summary>
        /// Adds the specified sort criterion to this <see cref="SearchQuery{TEntity}"/> with the
        /// default sort order of <see cref="SortOrder.Ascending"/>.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property returned by
        /// <paramref name="sortPropertySelector"/>.</typeparam>
        /// <param name="sortPropertySelector">The property selector to use when this sort criterion
        /// is applied.</param>
        /// <returns>This <see cref="SearchQuery{TEntity}"/> with the specified sort criterion
        /// added.</returns>
        public SearchQuery<TEntity> Sort<TProperty>(
            Expression<Func<TEntity, TProperty>> sortPropertySelector)
        {
            this.searchCriteria.Add(
                new SortCriterion<TEntity, TProperty>(sortPropertySelector, SortOrder.Ascending));
            return this;
        }

        /// <summary>
        /// Adds the specified sort criterion to this <see cref="SearchQuery{TEntity}"/> with the
        /// specified sort order.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property returned by
        /// <paramref name="sortPropertySelector"/>.</typeparam>
        /// <param name="sortPropertySelector">The property selector to use when this sort criterion
        /// is applied.</param>
        /// <param name="sortOrder">The sort order to use when this sort criterion is applied.
        /// </param>
        /// <returns>This <see cref="SearchQuery{TEntity}"/> with the specified sort criterion
        /// added.</returns>
        public SearchQuery<TEntity> Sort<TProperty>(
            Expression<Func<TEntity, TProperty>> sortPropertySelector,
            SortOrder sortOrder)
        {
            this.searchCriteria.Add(
                new SortCriterion<TEntity, TProperty>(sortPropertySelector, sortOrder));
            return this;
        }

        /// <summary>
        /// Adds the specified paging criterion to this <see cref="SearchQuery{TEntity}"/>.
        /// </summary>
        /// <param name="skipCount">The number of entities to skip when this paging criterion is
        /// applied.</param>
        /// <param name="pageSize">The number of entities to take when this paging criterion is
        /// applied.</param>
        /// <returns>This <see cref="SearchQuery{TEntity}"/> with the specified paging criterion
        /// added.</returns>
        public SearchQuery<TEntity> Page(int skipCount, int pageSize)
        {
            this.searchCriteria.Add(new PagingCriterion<TEntity>(skipCount, pageSize));
            return this;
        }
    }
}
