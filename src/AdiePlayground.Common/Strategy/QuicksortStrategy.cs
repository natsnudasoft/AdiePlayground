// <copyright file="QuicksortStrategy.cs" company="natsnudasoft">
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
    /// Provides a sort strategy which uses the quicksort algorithm. This is an unstable sort.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="IList{T}"/> that this
    /// <see cref="SortStrategy{T}"/> will sort.</typeparam>
    /// <seealso cref="SortStrategy{T}" />
    internal sealed class QuicksortStrategy<T> : SortStrategy<T>
    {
        /// <inheritdoc/>
        protected override SortType SortType => SortType.Quicksort;

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Validated in abstract base.")]
        protected override void Sort(IList<T> list, IComparer<T> comparer)
        {
            Quicksort(list, comparer, 0, list.Count - 1);
        }

        private static void Quicksort(IList<T> list, IComparer<T> comparer, int left, int right)
        {
            if (left < right)
            {
                var p = Partition(list, comparer, left, right);
                Quicksort(list, comparer, left, p);
                Quicksort(list, comparer, p + 1, right);
            }
        }

        private static int Partition(IList<T> list, IComparer<T> comparer, int left, int right)
        {
            var pivot = list[left + ((right - left) / 2)];
            var i = left - 1;
            var j = right + 1;
            while (true)
            {
                do
                {
                    ++i;
                }
                while (comparer.Compare(list[i], pivot) < 0);

                do
                {
                    --j;
                }
                while (comparer.Compare(list[j], pivot) > 0);

                if (i >= j)
                {
                    break;
                }

                var tmp = list[i];
                list[i] = list[j];
                list[j] = tmp;
            }

            return j;
        }
    }
}
