// <copyright file="CommandLoop.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli
{
    using System;
    using System.Linq;
    using System.Threading;
    using Common.Extensions;
    using Metadata;
    using Properties;

    /// <summary>
    /// Manages a command loop able to parse and process input from the <see cref="Console"/> and
    /// execute instances of <see cref="ICommand"/> resolved from the processed input.
    /// </summary>
    /// <seealso cref="IDisposable" />
    internal sealed class CommandLoop : IDisposable
    {
        private static readonly object CommandLoopLock = new object();
        private static bool commandLoopRunning;

        private readonly CommandResolver commandResolver;
        private readonly CommandGroupMetadataFactory commandGroupMetadataFactory;
        private readonly CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLoop"/> class.
        /// </summary>
        /// <param name="commandResolver">The <see cref="CommandResolver"/> used to resolve
        /// instances of <see cref="ICommand"/>.</param>
        /// <param name="commandGroupMetadataFactory">The <see cref="CommandGroupMetadataFactory"/>
        /// used to retrieve instances of <see cref="CommandMetadata"/> from a group.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandResolver"/>, or
        /// <paramref name="commandGroupMetadataFactory"/> is <see langword="null"/>.
        /// </exception>
        public CommandLoop(
            CommandResolver commandResolver,
            CommandGroupMetadataFactory commandGroupMetadataFactory)
        {
            if (commandResolver == null)
            {
                throw new ArgumentNullException(nameof(commandResolver));
            }

            if (commandGroupMetadataFactory == null)
            {
                throw new ArgumentNullException(nameof(commandGroupMetadataFactory));
            }

            this.commandResolver = commandResolver;
            this.commandGroupMetadataFactory = commandGroupMetadataFactory;
            this.cancellationTokenSource = new CancellationTokenSource();
        }

        /// <summary>
        /// Gets or sets the current group that this <see cref="CommandLoop"/> uses to resolve
        /// instances of <see cref="ICommand"/>.
        /// </summary>
        public string CurrentGroup { get; set; } = CommandUsage.DefaultCommandGroup;

        /// <summary>
        /// Starts running this <see cref="CommandLoop"/> so that it can begin processing input.
        /// </summary>
        public void Run()
        {
            ConsoleCancelEventHandler onConsoleCancelKeyPress = (sender, e) =>
            {
                this.cancellationTokenSource.Cancel();
            };
            BeginRun();
            Console.CancelKeyPress += onConsoleCancelKeyPress;
            try
            {
                ConsoleExtensions.WriteColoredLine(Resources.WelcomeMessage, ConsoleColor.Green);
                Console.WriteLine();
                string commandString;
                while (true)
                {
                    if (this.cancellationTokenSource.IsCancellationRequested)
                    {
                        break;
                    }

                    var commandGroup = this.CurrentGroup;
                    Console.Write(Resources.CommandPrompt);
                    commandString = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(commandString))
                    {
                        var parsedCommand = CommandStringParser.Parse(commandString);
                        var command = this.ResolveCommand(commandGroup, parsedCommand);
                        if (command != null)
                        {
                            command.Execute(this.cancellationTokenSource.Token);
                        }
                    }

                    Console.WriteLine();
                }
            }
            finally
            {
                Console.CancelKeyPress -= onConsoleCancelKeyPress;
                EndRun();
            }
        }

        /// <summary>
        /// Signals this <see cref="CommandLoop"/> to stop processing input.
        /// </summary>
        public void Exit()
        {
            this.cancellationTokenSource.Cancel();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private static void BeginRun()
        {
            lock (CommandLoopLock)
            {
                if (commandLoopRunning)
                {
                    throw new InvalidOperationException("A command loop is already running.");
                }

                commandLoopRunning = true;
            }
        }

        private static void EndRun()
        {
            lock (CommandLoopLock)
            {
                commandLoopRunning = false;
            }
        }

        private ICommand ResolveCommand(
            string commandGroup,
            ParsedCommand parsedCommand)
        {
            ICommand command = null;
            try
            {
                command = this.commandResolver.Resolve(commandGroup, parsedCommand);
            }
            catch (CommandResolveException ex)
            {
                var commandMetadata = this.commandGroupMetadataFactory(commandGroup)
                    .FirstOrDefault(m => m.Name == parsedCommand.Name);
                if (commandMetadata != null)
                {
                    CommandUsage.WriteCommandResolveFailed(ex.CommandName);
                    CommandUsage.WriteCommandUsage(commandMetadata);
                }
                else
                {
                    CommandUsage.WriteCommandNotFound(ex.CommandName);
                }
            }
            catch (CommandNotFoundException ex)
            {
                CommandUsage.WriteCommandNotFound(ex.CommandName);
            }

            return command;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.cancellationTokenSource.Dispose();
            }
        }
    }
}
