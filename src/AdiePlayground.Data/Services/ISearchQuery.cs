// <copyright file="ISearchQuery.cs" company="natsnudasoft">
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

    /// <summary>
    /// Provides methods for building up a search query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity this search query operates on.</typeparam>
    public interface ISearchQuery<TEntity>
    {
        /// <summary>
        /// Gets the search criteria that will be applied to the query when it is evaluated.
        /// </summary>
        IEnumerable<ISearchCriterion<TEntity>> SearchCriteria { get; }

        /// <summary>
        /// Creates the specified filter criterion that will be applied to the query when it is
        /// evaluated.
        /// </summary>
        /// <param name="filterPredicate">The filter that will be applied to the query.</param>
        /// <returns>The criterion to be applied when the query is evaluated.</returns>
        ISearchQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filterPredicate);

        /// <summary>
        /// Creates the specified eager loading criterion that will be applied to the query when it
        /// is evaluated.
        /// </summary>
        /// <param name="includePropertySelector">The property selector that selects the property
        /// that will be eagerly loaded in the query.</param>
        /// <returns>The criterion to be applied when the query is evaluated.</returns>
        ISearchQuery<TEntity> Include(Expression<Func<TEntity, object>> includePropertySelector);

        /// <summary>
        /// Creates the specified sort criterion that will be applied to the query when it is
        /// evaluated.
        /// </summary>
        /// <param name="sortPropertySelector">The sort that will be applied to the query.</param>
        /// <param name="sortOrder">The sort order that will be applied to the query.</param>
        /// <returns>The criterion to be applied when the query is evaluated.</returns>
        ISearchQuery<TEntity> Sort(
            Expression<Func<TEntity, object>> sortPropertySelector,
            SortOrder sortOrder);

        /// <summary>
        /// Creates the specified paging criterion that will be applied to the query when it is
        /// evaluated.
        /// </summary>
        /// <param name="skipCount">The number of entities to skip when the criterion is applied to
        /// the query.</param>
        /// <param name="pageSize">The size of the page that will be applied to the query.</param>
        /// <returns>The criterion to be applied when the query is evaluated.</returns>
        ISearchQuery<TEntity> Page(int skipCount, int pageSize);
    }
}
