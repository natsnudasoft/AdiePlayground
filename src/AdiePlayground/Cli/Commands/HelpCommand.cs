// <copyright file="HelpCommand.cs" company="natsnudasoft">
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
    using Common.Extensions;
    using Metadata;
    using Properties;

    /// <summary>
    /// Represents an <see cref="ICommand"/> that will display help.
    /// </summary>
    /// <seealso cref="ICommand" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Command("Playground", "help", typeof(Resources), nameof(Resources.HelpCommandHelp))]
    internal sealed class HelpCommand : ICommand
    {
        private readonly CommandLoop commandLoop;
        private readonly CommandGroupMetadataFactory commandGroupMetadataFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/> class.
        /// </summary>
        /// <param name="commandLoop">The <see cref="CommandLoop"/> that this
        /// <see cref="HelpCommand"/> will use to get the current command group from.</param>
        /// <param name="commandGroupMetadataFactory">The
        /// <see cref="CommandGroupMetadataFactory"/> to use to create a collection of
        /// instances of <see cref="CommandMetadata"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandLoop"/>, or
        /// <paramref name="commandGroupMetadataFactory"/> is <see langword="null"/>.</exception>
        public HelpCommand(
            CommandLoop commandLoop,
            CommandGroupMetadataFactory commandGroupMetadataFactory)
        {
            ParameterValidation.IsNotNull(commandLoop, nameof(commandLoop));
            ParameterValidation.IsNotNull(
                commandGroupMetadataFactory,
                nameof(commandGroupMetadataFactory));

            this.commandLoop = commandLoop;
            this.commandGroupMetadataFactory = commandGroupMetadataFactory;
        }

        /// <summary>
        /// Gets the name of the <see cref="ICommand"/> that this <see cref="HelpCommand"/> will
        /// display help for.
        /// </summary>
        [CommandParameter(
            0,
            "command-name",
            typeof(Resources),
            nameof(Resources.HelpCommandCommandNameHelp),
            Required = false)]
        public string CommandName { get; private set; }

        /// <inheritdoc/>
        public void Execute(CancellationToken cancellationToken)
        {
            var commandGroupMetadata =
                this.commandGroupMetadataFactory(this.commandLoop.CurrentGroup);
            if (this.CommandName == null)
            {
                foreach (var commandMetadata in commandGroupMetadata.OrderBy(m => m.Name))
                {
                    WriteCommandHelp(commandMetadata);
                    Console.WriteLine();
                    Console.WriteLine();
                }
            }
            else
            {
                var commandMetadata = commandGroupMetadata.
                    SingleOrDefault(m => m.Name == this.CommandName);
                if (commandMetadata != null)
                {
                    WriteCommandHelp(commandMetadata);
                }
                else
                {
                    CommandUsage.WriteCommandNotFound(this.CommandName);
                }

                Console.WriteLine();
            }
        }

        private static void WriteCommandHelp(CommandMetadata commandMetadata)
        {
            const int CommandMetadataNamePad = 15;
            ConsoleExtensions.WriteColored(
                commandMetadata.Name.PadRight(CommandMetadataNamePad),
                ConsoleColor.Cyan);
            CommandUsage.WriteCommandHelp(commandMetadata);
        }
    }
}
