// <copyright file="SortStrategyResolver.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Strategy
{
    using System;
    using System.Collections.Generic;
    using Autofac.Features.Indexed;
    using static System.FormattableString;

    /// <summary>
    /// Provides a class to resolve an <see cref="ISortStrategy{T}"/> based on a
    /// <see cref="SortType"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="IList{T}"/> that the resolved
    /// <see cref="ISortStrategy{T}"/> will sort.</typeparam>
    public sealed class SortStrategyResolver<T>
    {
        private readonly IIndex<SortType, ISortStrategy<T>> sortStrategies;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortStrategyResolver{T}"/> class.
        /// </summary>
        /// <param name="sortStrategies">The keyed collection of available
        /// <see cref="ISortStrategy{T}"/> types.</param>
        /// <exception cref="ArgumentNullException"><paramref name="sortStrategies"/> is
        /// <see langword="null"/>.</exception>
        public SortStrategyResolver(IIndex<SortType, ISortStrategy<T>> sortStrategies)
        {
            ParameterValidation.IsNotNull(sortStrategies, nameof(sortStrategies));

            this.sortStrategies = sortStrategies;
        }

        /// <summary>
        /// Resolves an instance of an <see cref="ISortStrategy{T}"/> from the specified
        /// <see cref="SortType"/>.
        /// </summary>
        /// <param name="sortType">The <see cref="SortType"/> of the instance to resolve.</param>
        /// <returns>The resolved <see cref="ISortStrategy{T}"/>.</returns>
        /// <exception cref="ArgumentException"><paramref name="sortType"/> could not resolve a
        /// valid <see cref="ISortStrategy{T}"/>.</exception>
        public ISortStrategy<T> Resolve(SortType sortType)
        {
            ISortStrategy<T> sortStrategy;
            if (!this.sortStrategies.TryGetValue(sortType, out sortStrategy))
            {
                throw new ArgumentException(
                    Invariant($"Sorting strategy not found."),
                    nameof(sortType));
            }

            return sortStrategy;
        }
    }
}
