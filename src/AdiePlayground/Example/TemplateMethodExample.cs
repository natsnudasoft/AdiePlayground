// <copyright file="TemplateMethodExample.cs" company="natsnudasoft">
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
    using System.Threading;
    using Autofac.Features.Indexed;
    using Common;
    using Common.Extensions;
    using Common.TemplateMethod;
    using Properties;

    /// <summary>
    /// Provides examples of using the Template Method pattern.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Example("template-method")]
    internal sealed class TemplateMethodExample : IExample
    {
        private readonly IIndex<string, ConsoleWorker> consoleWorkers;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateMethodExample"/> class.
        /// </summary>
        /// <param name="consoleWorkers">The keyed collection of available
        /// <see cref="ConsoleWorker"/> types.</param>
        /// <exception cref="ArgumentNullException"><paramref name="consoleWorkers"/> is
        /// <see langword="null"/>.</exception>
        public TemplateMethodExample(IIndex<string, ConsoleWorker> consoleWorkers)
        {
            ParameterValidation.IsNotNull(consoleWorkers, nameof(consoleWorkers));

            this.consoleWorkers = consoleWorkers;
        }

        /// <inheritdoc/>
        public void Run(CancellationToken cancellationToken)
        {
            ConsoleExtensions.WriteColoredLine(
                Resources.TemplateMethodExampleRunning,
                ConsoleColor.Cyan);
            Console.WriteLine();
            this.consoleWorkers["Architect"].PerformDailyRoutine();
            Console.WriteLine();
            this.consoleWorkers["Plumber"].PerformDailyRoutine();
            Console.WriteLine();
            this.consoleWorkers["ShopAssistant"].PerformDailyRoutine();
        }
    }
}
