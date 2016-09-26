// <copyright file="ConsoleRegistrar.cs" company="natsnudasoft">
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
    using Extensions;
    using Properties;

    /// <summary>
    /// Provides functionality to register events to the <see cref="Console"/>.
    /// </summary>
    /// <seealso cref="IRegistrar" />
    internal sealed class ConsoleRegistrar : IRegistrar
    {
        private readonly IDateTimeProvider dateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleRegistrar"/> class.
        /// </summary>
        /// <param name="dateTimeProvider">The <see cref="DateTime"/> provider to use to
        /// provide the time of an event registration.</param>
        /// <exception cref="ArgumentNullException"><paramref name="dateTimeProvider"/> is
        /// <c>null</c>.</exception>
        public ConsoleRegistrar(IDateTimeProvider dateTimeProvider)
        {
            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(
                    nameof(dateTimeProvider),
                    Resources.ConsoleRegistrarDateTimeProviderNull);
            }

            this.dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc/>
        public void Register(Guid id, string text)
        {
            var registerTime = this.dateTimeProvider.Now;
            ConsoleExtensions.WriteColoredLine(
                FormattableString.Invariant(
                    $"{id}    {registerTime:yyyy-MM-dd HH:mm:ss.fff}    {text}"),
                ConsoleColor.DarkYellow);
        }
    }
}
