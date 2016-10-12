// <copyright file="ExampleCommand.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli.Commands
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Autofac.Features.Indexed;
    using Common.Extensions;
    using Example;
    using Metadata;
    using Properties;

    /// <summary>
    /// Represents an <see cref="ICommand"/> that will run a named <see cref="IExample"/>.
    /// </summary>
    /// <seealso cref="ICommand" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Command("Playground", "example", typeof(Resources), nameof(Resources.ExampleCommandHelp))]
    internal sealed class ExampleCommand : ICommand
    {
        private readonly IIndex<string, IExample> examples;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleCommand"/> class.
        /// </summary>
        /// <param name="examples">The keyed collection of available <see cref="IExample"/> types.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="examples"/> is <c>null</c>.
        /// </exception>
        public ExampleCommand(IIndex<string, IExample> examples)
        {
            if (examples == null)
            {
                throw new ArgumentNullException(nameof(examples));
            }

            this.examples = examples;
        }

        /// <summary>
        /// Gets the name of the example this <see cref="ExampleCommand"/> will run.
        /// </summary>
        [CommandParameter(
            0,
            "example-name",
            typeof(Resources),
            nameof(Resources.ExampleCommandExampleNameHelp))]
        public string ExampleName { get; private set; }

        /// <inheritdoc/>
        public void Execute(CancellationToken cancellationToken)
        {
            IExample example;
            if (this.examples.TryGetValue(this.ExampleName, out example))
            {
                example.Run(cancellationToken);
            }
            else
            {
                WriteExampleNotFound(this.ExampleName);
            }
        }

        private static void WriteExampleNotFound(string exampleName)
        {
            ConsoleExtensions.WriteColoredLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.ExampleNotFound,
                    exampleName),
                ConsoleColor.Red);
        }
    }
}
