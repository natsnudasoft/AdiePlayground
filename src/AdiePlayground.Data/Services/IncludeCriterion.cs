// <copyright file="IncludeCriterion.cs" company="natsnudasoft">
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
    using System.Linq.Expressions;
    using Model;
    using Properties;

    /// <summary>
    /// Provides an eager loading criterion to be applied to a query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="ISearchCriterion{TEntity}" />
    internal sealed class IncludeCriterion<TEntity> : ISearchCriterion<TEntity>
        where TEntity : class, IModelEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IncludeCriterion{TEntity}"/> class.
        /// </summary>
        /// <param name="includePropertySelector">The property selector that selects the property
        /// to eager load.</param>
        /// <exception cref="ArgumentNullException">Thrown when includePropertySelector is null.
        /// </exception>
        public IncludeCriterion(Expression<Func<TEntity, object>> includePropertySelector)
        {
            if (includePropertySelector == null)
            {
                throw new ArgumentNullException(
                    nameof(includePropertySelector),
                    Resources.FilterCriterionPredicateInvalid);
            }

            this.IncludePropertySelector = includePropertySelector;
        }

        /// <summary>
        /// Gets the property selector that selects the property to eager load.
        /// </summary>
        public Expression<Func<TEntity, object>> IncludePropertySelector { get; }

        /// <inheritdoc/>
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            return query.Include(this.IncludePropertySelector);
        }
    }
}
