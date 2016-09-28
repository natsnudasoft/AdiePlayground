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
    using Model;

    /// <summary>
    /// Provides methods for building up a search query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity this search query operates on.</typeparam>
    public interface ISearchQuery<TEntity>
        where TEntity : class, IModelEntity
    {
        /// <summary>
        /// Gets the criteria that will be evaluated when this <see cref="ISearchQuery{TEntity}"/>
        /// is applied.
        /// </summary>
        IEnumerable<ISearchCriterion<TEntity>> SearchCriteria { get; }

        /// <summary>
        /// Adds the specified filter criterion to this <see cref="ISearchQuery{TEntity}"/>.
        /// </summary>
        /// <param name="filterPredicate">The predicate to use when this filter criterion is
        /// applied.
        /// </param>
        /// <returns>The original <see cref="ISearchQuery{TEntity}"/> with this filter criterion
        /// added.</returns>
        ISearchQuery<TEntity> Filter(Expression<Func<TEntity, bool>> filterPredicate);

        /// <summary>
        /// Adds the specified eager loading criterion to this <see cref="ISearchQuery{TEntity}"/>.
        /// </summary>
        /// <param name="includePropertySelector">The property selector to use when this eager
        /// loading criterion is applied.</param>
        /// <returns>The original <see cref="ISearchQuery{TEntity}"/> with this eager loading
        /// criterion added.</returns>
        ISearchQuery<TEntity> Include(Expression<Func<TEntity, object>> includePropertySelector);

        /// <summary>
        /// Adds the specified sort criterion to this <see cref="ISearchQuery{TEntity}"/> with the
        /// default sort order of <see cref="SortOrder.Ascending"/>.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property returned by
        /// <paramref name="sortPropertySelector"/>.</typeparam>
        /// <param name="sortPropertySelector">The property selector to use when this sort criterion
        /// is applied.</param>
        /// <returns>The original <see cref="ISearchQuery{TEntity}"/> with this sort criterion
        /// added.</returns>
        ISearchQuery<TEntity> Sort<TProperty>(
            Expression<Func<TEntity, TProperty>> sortPropertySelector);

        /// <summary>
        /// Adds the specified sort criterion to this <see cref="ISearchQuery{TEntity}"/> with the
        /// specified sort order.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property returned by
        /// <paramref name="sortPropertySelector"/>.</typeparam>
        /// <param name="sortPropertySelector">The property selector to use when this sort criterion
        /// is applied.</param>
        /// <param name="sortOrder">The sort order to use when this sort criterion is applied.
        /// </param>
        /// <returns>The original <see cref="ISearchQuery{TEntity}"/> with this sort criterion
        /// added.</returns>
        ISearchQuery<TEntity> Sort<TProperty>(
            Expression<Func<TEntity, TProperty>> sortPropertySelector,
            SortOrder sortOrder);

        /// <summary>
        /// Adds the specified paging criterion to this <see cref="ISearchQuery{TEntity}"/>.
        /// </summary>
        /// <param name="skipCount">The number of entities to skip when this paging criterion is
        /// applied.</param>
        /// <param name="pageSize">The number of entities to take when this paging criterion is
        /// applied.</param>
        /// <returns>The original <see cref="ISearchQuery{TEntity}"/> with this paging criterion
        /// added.</returns>
        ISearchQuery<TEntity> Page(int skipCount, int pageSize);
    }
}
