// <copyright file="ISortStrategy.cs" company="natsnudasoft">
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
    /// Represents a sort strategy containing a sorting algorithm that can be used to sort an
    /// indexed collection.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="IList{T}"/> that this
    /// <see cref="ISortStrategy{T}"/> will sort.</typeparam>
    public interface ISortStrategy<T>
    {
        /// <summary>
        /// Gets the type of sorting algorithm this <see cref="ISortStrategy{T}"/> uses.
        /// </summary>
        SortType SortType { get; }

        /// <summary>
        /// Sorts the specified <see cref="IList{T}"/>; uses the sorting algorithm specified by this
        /// <see cref="ISortStrategy{T}"/>.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> to sort.</param>
        void Sort(IList<T> list);

        /// <summary>
        /// Sorts the specified <see cref="IList{T}"/> using the specified
        /// <see cref="IComparer{T}"/>; uses the sorting algorithm specified by this
        /// <see cref="ISortStrategy{T}"/>.
        /// </summary>
        /// <param name="list">The <see cref="IList{T}"/> to sort.</param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> to use when sorting.</param>
        void Sort(IList<T> list, IComparer<T> comparer);
    }
}
