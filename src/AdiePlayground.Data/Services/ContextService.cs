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
    using Properties;

    /// <summary>
    /// Provides a base for default actions on a database entity. This class can be overridden to
    /// provide more advanced or specific common operations for entities.
    /// </summary>
    /// <seealso cref="IContextService" />
    internal class ContextService : IContextService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContextService"/> class using the
        /// specified database context scope factory.
        /// </summary>
        /// <param name="dbContextScopeFactoryValue">The database context scope factory.</param>
        public ContextService(IDbContextScopeFactory dbContextScopeFactoryValue)
        {
            if (dbContextScopeFactoryValue == null)
            {
                throw new ArgumentNullException(
                    nameof(dbContextScopeFactoryValue),
                    Resources.DbContextScopeFactoryInvalid);
            }

            this.DbContextScopeFactory = dbContextScopeFactoryValue;
        }

        /// <summary>
        /// Gets the database context scope factory.
        /// </summary>
        protected IDbContextScopeFactory DbContextScopeFactory { get; }

        /// <inheritdoc/>
        public IServiceTransaction BeginTransaction()
        {
            return new ServiceTransaction(this.DbContextScopeFactory);
        }

        /// <inheritdoc/>
        public IReadOnlyServiceTransaction BeginReadOnlyTransaction()
        {
            return new ReadOnlyServiceTransaction(this.DbContextScopeFactory);
        }

        /// <inheritdoc/>
        public async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.CreateReadOnly())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                return await context.Set<TEntity>().FindAsync(id).ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(
            ISearchQuery<TEntity> searchQuery)
            where TEntity : class, IModelEntity
        {
            using (var scope = this.DbContextScopeFactory.CreateReadOnly())
            {
                var context = scope.DbContexts.Get<PlaygroundDbContext>();
                var query = BuildSearchQuery(context.Set<TEntity>(), searchQuery);
                return await query.ToListAsync().ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> FindAsync<TEntity, TResult>(
            ISearchQuery<TEntity> searchQuery,
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

        /// <inheritdoc/>
        public async Task<IEnumerable<TResult>> FindAsync<TEntity, TResult>(
            ISearchQuery<TEntity> searchQuery,
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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
            ISearchQuery<TEntity> searchQuery)
        {
            foreach (var criterion in searchQuery.SearchCriteria)
            {
                query = criterion.Apply(query);
            }

            return query;
        }
    }
}
