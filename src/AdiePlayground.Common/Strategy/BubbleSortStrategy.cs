// <copyright file="BubbleSortStrategy.cs" company="natsnudasoft">
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
    /// Provides a sort strategy which uses the bubble sort algorithm. This is a stable sort.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="IList{T}"/> that this
    /// <see cref="SortStrategy{T}"/> will sort.</typeparam>
    /// <seealso cref="SortStrategy{T}" />
    internal sealed class BubbleSortStrategy<T> : SortStrategy<T>
    {
        /// <inheritdoc/>
        protected override SortType SortType => SortType.BubbleSort;

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Validated in abstract base.")]
        protected override void Sort(IList<T> list, IComparer<T> comparer)
        {
            for (int bubbleEnd = list.Count - 1; bubbleEnd > 0;)
            {
                var nextBubbleEnd = 0;
                for (int i = 0; i < bubbleEnd; ++i)
                {
                    if (comparer.Compare(list[i], list[i + 1]) > 0)
                    {
                        var tmp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = tmp;
                        nextBubbleEnd = i;
                    }
                }

                bubbleEnd = nextBubbleEnd;
            }
        }
    }
}
