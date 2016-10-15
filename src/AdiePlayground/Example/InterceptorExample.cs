// <copyright file="InterceptorExample.cs" company="natsnudasoft">
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
    using System.Threading.Tasks;
    using Common.Extensions;
    using Common.Interceptor;
    using Properties;

    /// <summary>
    /// Provides an example of interception.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Example("interceptor")]
    internal sealed class InterceptorExample : IExample
    {
        private readonly IInstrumentationExample instrumentationExample;
        private readonly ConsoleInstrumentationReporter consoleInstrumentationReporter;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterceptorExample" /> class.
        /// </summary>
        /// <param name="instrumentationExample">The instrumentation example that will be
        /// intercepted.</param>
        /// <param name="consoleInstrumentationReporter">The console instrumentation
        /// reporter.</param>
        /// <exception cref="ArgumentNullException"><paramref name="instrumentationExample"/>, or
        /// <paramref name="consoleInstrumentationReporter"/> is <see langword="null"/>.</exception>
        public InterceptorExample(
            IInstrumentationExample instrumentationExample,
            ConsoleInstrumentationReporter consoleInstrumentationReporter)
        {
            if (instrumentationExample == null)
            {
                throw new ArgumentNullException(nameof(instrumentationExample));
            }

            if (consoleInstrumentationReporter == null)
            {
                throw new ArgumentNullException(nameof(consoleInstrumentationReporter));
            }

            this.instrumentationExample = instrumentationExample;
            this.consoleInstrumentationReporter = consoleInstrumentationReporter;
        }

        /// <inheritdoc/>
        public void Run(CancellationToken cancellationToken)
        {
            const int IncrementCount = 5;
            const int RegisterDelay = 50;

            ConsoleExtensions
                .WriteColoredLine(Resources.InterceptionExampleRunning, ConsoleColor.Cyan);

            // We have only set up the interceptor on the interface so the calls the class makes
            // to its own members are not intercepted.
            var task1 = this.instrumentationExample.IncrementValueAsync();
            for (int i = 0; i < IncrementCount; ++i)
            {
                this.instrumentationExample.IncrementValue();
            }

            var task2 = this.instrumentationExample.MultiplyValueAsync(
                this.instrumentationExample.Value);
            this.instrumentationExample.Value = 0;
            Task.WaitAll(task1, task2);
            Task.Delay(RegisterDelay).Wait();
            Console.WriteLine();
            this.consoleInstrumentationReporter.Report();
            Console.WriteLine();
        }
    }
}
