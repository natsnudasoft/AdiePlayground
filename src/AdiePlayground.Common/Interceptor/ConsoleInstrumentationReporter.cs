// <copyright file="ConsoleInstrumentationReporter.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Interceptor
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Provides a class to report on the current state of instrumentation to the
    /// <see cref="Console"/>.
    /// </summary>
    public sealed class ConsoleInstrumentationReporter
    {
        private readonly MethodInvocationCounter invocationCounter;
        private readonly MethodInvocationTimer invocationTimer;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IGuidProvider guidProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleInstrumentationReporter"/> class.
        /// </summary>
        /// <param name="invocationCounter">The <see cref="MethodInvocationCounter"/> to report on.
        /// </param>
        /// <param name="invocationTimer">The <see cref="MethodInvocationTimer"/> to report on.
        /// </param>
        /// <param name="dateTimeProvider">The <see cref="IDateTimeProvider"/> to use to
        /// provide the report time.</param>
        /// <param name="guidProvider">The <see cref="IGuidProvider"/> to use to generate a
        /// report id.</param>
        /// <exception cref="ArgumentNullException"><paramref name="invocationCounter"/>,
        /// <paramref name="invocationTimer"/>, <paramref name="dateTimeProvider"/>, or
        /// <paramref name="guidProvider"/> is <see langword="null"/>.</exception>
        public ConsoleInstrumentationReporter(
            MethodInvocationCounter invocationCounter,
            MethodInvocationTimer invocationTimer,
            IDateTimeProvider dateTimeProvider,
            IGuidProvider guidProvider)
        {
            ParameterValidation.IsNotNull(invocationCounter, nameof(invocationCounter));
            ParameterValidation.IsNotNull(invocationTimer, nameof(invocationTimer));
            ParameterValidation.IsNotNull(dateTimeProvider, nameof(dateTimeProvider));
            ParameterValidation.IsNotNull(guidProvider, nameof(guidProvider));

            this.invocationCounter = invocationCounter;
            this.invocationTimer = invocationTimer;
            this.dateTimeProvider = dateTimeProvider;
            this.guidProvider = guidProvider;
        }

        /// <summary>
        /// Writes the current state of instrumentation to the <see cref="Console"/>.
        /// </summary>
        public void Report()
        {
            const int SeparatorCharCount = 83;
            var stopwatch = Stopwatch.StartNew();
            var instrumentation = this.invocationCounter.MethodCounts
                .Join(
                    this.invocationTimer.MethodTimes,
                    methodCount => methodCount.Key,
                    methodTime => methodTime.Key,
                    (methodCount, methodTime) => new
                    {
                        Method = methodCount.Key,
                        Count = methodCount.Value,
                        AverageTime = methodTime.Value.Average(t => t.Ticks)
                    })
                .OrderByDescending(a => a.AverageTime);
            var reportId = this.guidProvider.NewGuid();
            var reportTime = this.dateTimeProvider.Now;
            var sb = new StringBuilder();
            sb.AppendFormat(
                CultureInfo.InvariantCulture,
                "Console Registrar Report    {0}" + Environment.NewLine + "{1:yyyy-MM-dd HH:mm:ss}",
                reportId,
                reportTime);
            sb.AppendLine();
            sb.AppendLine(new string('-', SeparatorCharCount));
            sb.AppendFormat(
                CultureInfo.InvariantCulture,
                "{0,-50}{1,-16}{2,-17}",
                "Method name",
                "Method count",
                "Method time (avg)");
            sb.AppendLine();
            sb.AppendLine(new string('=', SeparatorCharCount));
            foreach (var instrumented in instrumentation)
            {
                sb.AppendFormat(
                    CultureInfo.InvariantCulture,
                    "{0,-50}{1,12}    {2,14:0.0000} ms",
                    instrumented.Method.Name + " (" + instrumented.Method.DeclaringType.Name + ")",
                    instrumented.Count,
                    instrumented.AverageTime / TimeSpan.TicksPerMillisecond);
                sb.AppendLine();
            }

            sb.AppendLine(new string('=', SeparatorCharCount));
            stopwatch.Stop();
            sb.AppendLine(string
                .Format(
                    CultureInfo.InvariantCulture,
                    "(Generated in {0:0.0000} ms)",
                    stopwatch.Elapsed.TotalMilliseconds)
                .PadLeft(SeparatorCharCount));
            Console.Write(sb.ToString());
        }
    }
}
