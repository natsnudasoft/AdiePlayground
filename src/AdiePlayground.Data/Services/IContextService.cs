// <copyright file="IContextService.cs" company="natsnudasoft">
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
    using System.Threading.Tasks;
    using Model;

    /// <summary>
    /// Provides default actions on a specified database entity.
    /// </summary>
    public interface IContextService
    {
        /// <summary>
        /// Creates a new transaction for the context and underlying store. This transaction should
        /// be disposed when it is finished with.
        /// </summary>
        /// <returns>A new <see cref="IServiceTransaction"/>.</returns>
        IServiceTransaction BeginTransaction();

        /// <summary>
        /// Creates a new read-only transaction for the context and underlying store. This
        /// transaction should be disposed when it is finished with.
        /// </summary>
        /// <returns>A new <see cref="IReadOnlyServiceTransaction"/>.</returns>
        IReadOnlyServiceTransaction BeginReadOnlyTransaction();

        /// <summary>
        /// Gets the entity with the specified id from the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="id">The id.</param>
        /// <returns>
        /// An entity of type <typeparamref name="TEntity" /> if the specified id is found;
        /// otherwise, <c>null</c>.
        /// </returns>
        Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class, IModelEntity;

        /// <summary>
        /// Applies the specified search criteria to the underlying store and returns the entities
        /// that match.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="searchQuery">The search criteria to apply.</param>
        /// <returns>All entities of type <typeparamref name="TEntity"/> that match the specified
        /// criteria.</returns>
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(ISearchQuery<TEntity> searchQuery)
            where TEntity : class, IModelEntity;

        /// <summary>
        /// Applies the specified search criteria to the underlying store and returns the entities
        /// that match. The returned entities are projected into the form of the specified selector.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <typeparam name="TResult">The type of the new form the entities are projected to.
        /// </typeparam>
        /// <param name="searchQuery">The search criteria to apply.</param>
        /// <param name="selector">The selector to specify the new form to project entities to.
        /// </param>
        /// <returns>All entities that match the specified criteria, projected into the form
        /// specified as <typeparamref name="TResult"/>.</returns>
        Task<IEnumerable<TResult>> FindAsync<TEntity, TResult>(
            ISearchQuery<TEntity> searchQuery,
            Expression<Func<TEntity, TResult>> selector)
            where TEntity : class, IModelEntity;

        /// <summary>
        /// Applies the specified search criteria to the underlying store and returns the entities
        /// that match. The returned entities are projected into the form of the specified selector.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <typeparam name="TResult">The type of the new form the entities are projected to.
        /// </typeparam>
        /// <param name="searchQuery">The search criteria to apply.</param>
        /// <param name="selector">The selector to specify the new form to project entities to.
        /// Incorporates the current index.
        /// </param>
        /// <returns>All entities that match the specified criteria, projected into the form
        /// specified as <typeparamref name="TResult"/>.</returns>
        Task<IEnumerable<TResult>> FindAsync<TEntity, TResult>(
            ISearchQuery<TEntity> searchQuery,
            Expression<Func<TEntity, int, TResult>> selector)
            where TEntity : class, IModelEntity;

        /// <summary>
        /// Adds the specified entity to the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The number of state entries written to the underlying store.</returns>
        Task<int> AddAsync<TEntity>(TEntity entity)
            where TEntity : class, IModelEntity;

        /// <summary>
        /// Adds the specified entities to the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="entities">The entities to add.</param>
        /// <returns>The number of state entries written to the underlying store.</returns>
        Task<int> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IModelEntity;

        /// <summary>
        /// Removes the specified entity from the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>The number of state entries written to the underlying store.</returns>
        Task<int> RemoveAsync<TEntity>(TEntity entity)
            where TEntity : class, IModelEntity;

        /// <summary>
        /// Removes the specified entities from the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities.</typeparam>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>The number of state entries written to the underlying store.</returns>
        Task<int> RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IModelEntity;
    }
}
