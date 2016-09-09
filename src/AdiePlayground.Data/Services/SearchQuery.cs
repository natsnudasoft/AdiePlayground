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
    /// Represents a search query with a list of criteria that can be applied to a query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ISearchQuery{TEntity}" />
    internal sealed class SearchQuery<TEntity> : ISearchQuery<TEntity>
        where TEntity : class, IModelEntity
    {
        private readonly List<ISearchCriterion<TEntity>> searchCriteria =
            new List<ISearchCriterion<TEntity>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchQuery{TEntity}"/> class.
        /// </summary>
        internal SearchQuery()
        {
        }

        /// <inheritdoc/>
        IEnumerable<ISearchCriterion<TEntity>> ISearchQuery<TEntity>.SearchCriteria =>
            this.searchCriteria;

        /// <inheritdoc/>
        ISearchQuery<TEntity> ISearchQuery<TEntity>.Filter(
            Expression<Func<TEntity, bool>> filterPredicate)
        {
            this.searchCriteria.Add(new FilterCriterion<TEntity>(filterPredicate));
            return this;
        }

        /// <inheritdoc/>
        ISearchQuery<TEntity> ISearchQuery<TEntity>.Include(
            Expression<Func<TEntity, object>> includePropertySelector)
        {
            this.searchCriteria.Add(new IncludeCriterion<TEntity>(includePropertySelector));
            return this;
        }

        /// <inheritdoc/>
        ISearchQuery<TEntity> ISearchQuery<TEntity>.Sort<TProperty>(
            Expression<Func<TEntity, TProperty>> sortPropertySelector)
        {
            this.searchCriteria.Add(
                new SortCriterion<TEntity, TProperty>(sortPropertySelector, SortOrder.Ascending));
            return this;
        }

        /// <inheritdoc/>
        ISearchQuery<TEntity> ISearchQuery<TEntity>.Sort<TProperty>(
            Expression<Func<TEntity, TProperty>> sortPropertySelector,
            SortOrder sortOrder)
        {
            this.searchCriteria.Add(
                new SortCriterion<TEntity, TProperty>(sortPropertySelector, sortOrder));
            return this;
        }

        /// <inheritdoc/>
        ISearchQuery<TEntity> ISearchQuery<TEntity>.Page(int skipCount, int pageSize)
        {
            this.searchCriteria.Add(new PagingCriterion<TEntity>(skipCount, pageSize));
            return this;
        }
    }
}
