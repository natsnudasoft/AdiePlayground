// <copyright file="ISearchCriterion.cs" company="natsnudasoft">
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
    using System.Linq;
    using Model;

    /// <summary>
    /// Provides an interface for various search criterion which will be applied to a query.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity the query this filter will be applied to
    /// works on.</typeparam>
    public interface ISearchCriterion<TEntity>
        where TEntity : class, IModelEntity
    {
        /// <summary>
        /// Applies this criterion to the specified query.
        /// </summary>
        /// <param name="query">The query to apply this criterion to.</param>
        /// <returns>The specified query with this criterion applied.</returns>
        IQueryable<TEntity> Apply(IQueryable<TEntity> query);
    }
}
