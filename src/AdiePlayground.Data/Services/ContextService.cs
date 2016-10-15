// <copyright file="ContextService.cs" company="natsnudasoft">
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
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Mehdime.Entity;
    using Model;

    /// <summary>
    /// Provides a base for a set of common actions to be performed on the underlying store. This
    /// class can be overridden to provide more specific operations for a context.
    /// </summary>
    public class ContextService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextService"/> class.
        /// </summary>
        /// <param name="dbContextScopeFactory">The <see cref="IDbContextScopeFactory"/> used to
        /// create instances of <see cref="IDbContextScope"/> as they are needed by context
        /// operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dbContextScopeFactory"/> is
        /// <see langword="null"/>.</exception>
        public ContextService(IDbContextScopeFactory dbContextScopeFactory)
        {
            if (dbContextScopeFactory == null)
            {
                throw new ArgumentNullException(nameof(dbContextScopeFactory));
            }

            this.DbContextScopeFactory = dbContextScopeFactory;
        }

        /// <summary>
        /// Gets the <see cref="IDbContextScopeFactory"/> used to create instances of
        /// <see cref="IDbContextScope"/> as they are needed by context operations.
        /// </summary>
        protected IDbContextScopeFactory DbContextScopeFactory { get; }

        /// <summary>
        /// Creates a new transaction for the underlying store. This instance should be disposed to
        /// correctly end the transaction.
        /// </summary>
        /// <returns>A new <see cref="ServiceTransaction"/>.</returns>
        public ServiceTransaction BeginTransaction()
        {
            return new ServiceTransaction(this.DbContextScopeFactory);
        }

        /// <summary>
        /// Creates a new read-only transaction for the underlying store. This instance should be
        /// disposed to correctly end the transaction.
        /// </summary>
        /// <returns>A new <see cref="ReadOnlyServiceTransaction"/>.</returns>
        public ReadOnlyServiceTransaction BeginReadOnlyTransaction()
        {
            return new ReadOnlyServiceTransaction(this.DbContextScopeFactory);
        }

        /// <summary>
        /// Retrieves the entity with the specified id from the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <param name="id">The id to find on an entity.</param>
        /// <returns>
        /// An entity of type <typeparamref name="TEntity" /> if an entity with the specified id is
        /// found; otherwise, <see langword="null"/>.
        /// </returns>
        public async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.CreateReadOnly())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                return await context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the entities that match the specified search criteria from the underlying
        /// store.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <param name="searchQuery">The search criteria to apply.</param>
        /// <returns>All entities of type <typeparamref name="TEntity"/> that match the specified
        /// search criteria.</returns>
        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(
            SearchQuery<TEntity> searchQuery)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.CreateReadOnly())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                var query = BuildSearchQuery(context.Set<TEntity>(), searchQuery);
                return await query.ToListAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the entities that match the specified search criteria from the underlying
        /// store and projects them into the form of the specified selector.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <typeparam name="TResult">The type of the new form the entities are projected to.
        /// </typeparam>
        /// <param name="searchQuery">The search criteria to apply.</param>
        /// <param name="selector">The selector to specify the new form to project entities to.
        /// </param>
        /// <returns>All entities of type <typeparamref name="TEntity"/> that match the specified
        /// search criteria, projected into the specified form.</returns>
        public async Task<IEnumerable<TResult>> FindAsync<TEntity, TResult>(
            SearchQuery<TEntity> searchQuery,
            Expression<Func<TEntity, TResult>> selector)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.CreateReadOnly())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                var query = BuildSearchQuery(context.Set<TEntity>(), searchQuery);
                return await query.Select(selector).ToListAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Retrieves the entities that match the specified search criteria from the underlying
        /// store and projects them into the form of the specified selector incorporating the
        /// current index.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <typeparam name="TResult">The type of the new form the entities are projected to.
        /// </typeparam>
        /// <param name="searchQuery">The search criteria to apply.</param>
        /// <param name="selector">The selector to specify the new form to project entities to.
        /// Incorporates the current index.
        /// </param>
        /// <returns>All entities of type <typeparamref name="TEntity"/> that match the specified
        /// search criteria, projected into the specified form.</returns>
        public async Task<IEnumerable<TResult>> FindAsync<TEntity, TResult>(
            SearchQuery<TEntity> searchQuery,
            Expression<Func<TEntity, int, TResult>> selector)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.CreateReadOnly())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                var query = BuildSearchQuery(context.Set<TEntity>(), searchQuery);
                return await query.Select(selector).ToListAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Adds the specified entity to a context in the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The number of state entries modified in the underlying store.</returns>
        public async Task<int> AddAsync<TEntity>(TEntity entity)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.Create())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                context.Set<TEntity>().Add(entity);
                return await scope.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Adds the specified entities to a context in the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <param name="entities">The entities to add.</param>
        /// <returns>The number of state entries modified in the underlying store.</returns>
        public async Task<int> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.Create())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                context.Set<TEntity>().AddRange(entities);
                return await scope.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Removes the specified entity from a context in the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>The number of state entries modified in the underlying store.</returns>
        public async Task<int> RemoveAsync<TEntity>(TEntity entity)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.Create())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                context.Set<TEntity>().Remove(entity);
                return await scope.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Removes the specified entities from a context in the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">The entity type of the context to perform this operation on.
        /// </typeparam>
        /// <param name="entities">The entities to remove.</param>
        /// <returns>The number of state entries modified in the underlying store.</returns>
        public async Task<int> RemoveRangeAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.Create())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                context.Set<TEntity>().RemoveRange(entities);
                return await scope.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        private static IQueryable<TEntity> BuildSearchQuery<TEntity>(
            IQueryable<TEntity> query,
            SearchQuery<TEntity> searchQuery)
            where TEntity : class, IModelEntity
        {
            foreach (var criterion in searchQuery.SearchCriteria)
            {
                query = criterion.Apply(query);
            }

            return query;
        }
    }
}
