// <copyright file="ExampleListCommand.cs" company="natsnudasoft">
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
    using System.Linq;
    using System.Threading;
    using Common;
    using Example;
    using Metadata;
    using Properties;

    /// <summary>
    /// Represents an <see cref="ICommand"/> that will list available named types of
    /// <see cref="IExample"/>.
    /// </summary>
    /// <seealso cref="ICommand" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Command(
        "Playground",
        "example-list",
        typeof(Resources),
        nameof(Resources.ExampleListCommandHelp))]
    internal sealed class ExampleListCommand : ICommand
    {
        private readonly ExampleMetadataCollectionFactory exampleMetadataCollectionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleListCommand"/> class.
        /// </summary>
        /// <param name="exampleMetadataCollectionFactory">The
        /// <see cref="ExampleMetadataCollectionFactory"/> to use to create a collection of
        /// instances of <see cref="ExampleMetadata"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="exampleMetadataCollectionFactory"/> is <see langword="null"/>.
        /// </exception>
        public ExampleListCommand(ExampleMetadataCollectionFactory exampleMetadataCollectionFactory)
        {
            ParameterValidation.IsNotNull(
                exampleMetadataCollectionFactory,
                nameof(exampleMetadataCollectionFactory));

            this.exampleMetadataCollectionFactory = exampleMetadataCollectionFactory;
        }

        /// <inheritdoc/>
        public void Execute(CancellationToken cancellationToken)
        {
            foreach (var exampleMetadata in this.exampleMetadataCollectionFactory()
                .OrderBy(m => m.Name))
            {
                Console.WriteLine(exampleMetadata.Name);
            }
        }
    }
}
