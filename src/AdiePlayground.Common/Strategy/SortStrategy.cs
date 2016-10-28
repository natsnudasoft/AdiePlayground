// <copyright file="SortStrategy.cs" company="natsnudasoft">
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
    using System.Collections.Generic;

    /// <summary>
    /// Provides an abstract base class for sorting strategies.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="IList{T}"/> that this
    /// <see cref="SortStrategy{T}"/> will sort.</typeparam>
    /// <seealso cref="ISortStrategy{T}" />
    internal abstract class SortStrategy<T> : ISortStrategy<T>
    {
        /// <inheritdoc/>
        SortType ISortStrategy<T>.SortType => this.SortType;

        /// <summary>
        /// Gets the type of sorting algorithm this <see cref="SortStrategy{T}"/> uses.
        /// </summary>
        protected abstract SortType SortType { get; }

        /// <inheritdoc/>
        void ISortStrategy<T>.Sort(IList<T> list)
        {
            ParameterValidation.IsNotNull(list, nameof(list));

            this.Sort(list, Comparer<T>.Default);
        }

        /// <inheritdoc/>
        void ISortStrategy<T>.Sort(IList<T> list, IComparer<T> comparer)
        {
            ParameterValidation.IsNotNull(list, nameof(list));
            ParameterValidation.IsNotNull(comparer, nameof(comparer));

            this.Sort(list, comparer);
        }

        /// <summary>
        /// Sorts the specified <see cref="IList{T}"/> using the specified
        /// <see cref="IComparer{T}"/>; uses the sorting algorithm specified by this
        /// <see cref="ISortStrategy{T}"/>.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> to sort.</param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> to use when sorting.</param>
        protected abstract void Sort(IList<T> list, IComparer<T> comparer);
    }
}
