// <copyright file="ExitCommand.cs" company="natsnudasoft">
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
    using System.Threading;
    using Common;
    using Metadata;
    using Properties;

    /// <summary>
    /// Represents an <see cref="ICommand"/> that will cause the current <see cref="CommandLoop"/>
    /// to exit.
    /// </summary>
    /// <seealso cref="ICommand" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Command("Playground", "exit", typeof(Resources), nameof(Resources.ExitCommandHelp))]
    internal sealed class ExitCommand : ICommand
    {
        private readonly CommandLoop commandLoop;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExitCommand"/> class.
        /// </summary>
        /// <param name="commandLoop">The <see cref="CommandLoop"/> that this
        /// <see cref="ExitCommand"/> will cause to exit.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandLoop"/> is
        /// <see langword="null"/>.</exception>
        public ExitCommand(CommandLoop commandLoop)
        {
            ParameterValidation.IsNotNull(commandLoop, nameof(commandLoop));

            this.commandLoop = commandLoop;
        }

        /// <inheritdoc/>
        public void Execute(CancellationToken cancellationToken)
        {
            this.commandLoop.Exit();
        }
    }
}
