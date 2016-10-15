// <copyright file="StrategyExample.cs" company="natsnudasoft">
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

namespace AdiePlayground.Example
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Common.Extensions;
    using Common.Strategy;
    using Properties;

    /// <summary>
    /// Provides examples of using the Strategy pattern.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Example("strategy")]
    internal sealed class StrategyExample : IExample
    {
        private readonly SortStrategyResolver<string> strategyResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="StrategyExample"/> class.
        /// </summary>
        /// <param name="strategyResolver">The <see cref="SortStrategyResolver{T}"/> to use to
        /// resolve instances of <see cref="ISortStrategy{T}"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="strategyResolver"/> is
        /// <see langword="null"/>.</exception>
        public StrategyExample(SortStrategyResolver<string> strategyResolver)
        {
            if (strategyResolver == null)
            {
                throw new ArgumentNullException(nameof(strategyResolver));
            }

            this.strategyResolver = strategyResolver;
        }

        /// <inheritdoc/>
        public void Run(CancellationToken cancellationToken)
        {
            ConsoleExtensions.WriteColoredLine(Resources.StrategyExampleRunning, ConsoleColor.Cyan);
            var list = new List<string>
            {
                "furthest",
                "Hello",
                "Speak",
                "Correctness",
                "Inexperience",
                "Furthest",
                "Correctness",
                "speak"
            };
            var bubbleSortStrategy = this.strategyResolver.Resolve(SortType.BubbleSort);
            var quicksortStrategy = this.strategyResolver.Resolve(SortType.Quicksort);

            Console.WriteLine(Resources.StrategyExampleOriginalList);
            ConsoleExtensions.WriteColoredLine(
                string.Join(Environment.NewLine, list),
                ConsoleColor.DarkGray);
            Console.WriteLine();

            Console.WriteLine(Resources.StrategyExampleApplyingBubbleSort);
            var bubbleSortList = new List<string>(list);
            bubbleSortStrategy.Sort(bubbleSortList, StringComparer.OrdinalIgnoreCase);
            ConsoleExtensions.WriteColoredLine(
                string.Join(Environment.NewLine, bubbleSortList),
                ConsoleColor.DarkGray);
            Console.WriteLine();

            Console.WriteLine(Resources.StrategyExampleApplyingQuicksort);
            var quicksortList = new List<string>(list);
            quicksortStrategy.Sort(quicksortList, StringComparer.OrdinalIgnoreCase);
            ConsoleExtensions.WriteColoredLine(
                string.Join(Environment.NewLine, quicksortList),
                ConsoleColor.DarkGray);
            Console.WriteLine();
        }
    }
}
