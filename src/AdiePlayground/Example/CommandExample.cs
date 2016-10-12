// <copyright file="CommandExample.cs" company="natsnudasoft">
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
    using System.Globalization;
    using System.Threading;
    using Common.Command;
    using Common.Extensions;
    using Properties;

    /// <summary>
    /// Provides examples of using the Command pattern.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Example("command")]
    internal sealed class CommandExample : IExample
    {
        private readonly IRobot robot;
        private readonly CommandFactory commandFactory;
        private readonly CommandExecutionManager commandExecutionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandExample"/> class.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> to use in this example.</param>
        /// <param name="commandFactory">The <see cref="CommandFactory"/> used to create instances
        /// of <see cref="ICommand"/>.</param>
        /// <param name="commandExecutionManager">The <see cref="CommandExecutionManager"/> used
        /// to manage execution of instances of <see cref="ICommand"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="robot"/>,
        /// <paramref name="commandFactory"/>, or <paramref name="commandExecutionManager"/> is
        /// <c>null</c>.</exception>
        public CommandExample(
            IRobot robot,
            CommandFactory commandFactory,
            CommandExecutionManager commandExecutionManager)
        {
            if (robot == null)
            {
                throw new ArgumentNullException(nameof(robot));
            }

            if (commandFactory == null)
            {
                throw new ArgumentNullException(nameof(commandFactory));
            }

            if (commandExecutionManager == null)
            {
                throw new ArgumentNullException(nameof(commandExecutionManager));
            }

            this.robot = robot;
            this.commandFactory = commandFactory;
            this.commandExecutionManager = commandExecutionManager;
        }

        /// <inheritdoc/>
        public void Run(CancellationToken cancellationToken)
        {
            const int CommandPad = 35;
            const double MoveRobot1 = 5D;
            const double TurnRobot1 = 0.349065850398D;
            const double MoveRobot2 = 15D;
            const double MoveRobot3 = -10D;

            ConsoleExtensions.WriteColoredLine(Resources.CommandExampleRunning, ConsoleColor.Cyan);

            Console.Write(string.Format(
                CultureInfo.InvariantCulture,
                Resources.CommandExampleRobotMove,
                MoveRobot1).PadRight(CommandPad));
            this.commandExecutionManager.ExecuteCommand(
                this.commandFactory("robot move", this.robot, MoveRobot1));

            Console.Write(string.Format(
                CultureInfo.InvariantCulture,
                Resources.CommandExampleRobotTurn,
                TurnRobot1).PadRight(CommandPad));
            this.commandExecutionManager.ExecuteCommand(
                this.commandFactory("robot turn", this.robot, TurnRobot1));

            Console.Write(string.Format(
                CultureInfo.InvariantCulture,
                Resources.CommandExampleRobotMove,
                MoveRobot2).PadRight(CommandPad));
            this.commandExecutionManager.ExecuteCommand(
                this.commandFactory("robot move", this.robot, MoveRobot2));

            Console.Write(Resources.CommandExampleUndo.PadRight(CommandPad));
            this.commandExecutionManager.Undo();

            Console.Write(Resources.CommandExampleUndo.PadRight(CommandPad));
            this.commandExecutionManager.Undo();

            Console.Write(Resources.CommandExampleRedo.PadRight(CommandPad));
            this.commandExecutionManager.Redo();

            Console.Write(string.Format(
                CultureInfo.InvariantCulture,
                Resources.CommandExampleRobotMove,
                MoveRobot3).PadRight(CommandPad));
            this.commandExecutionManager.ExecuteCommand(
                this.commandFactory("robot move", this.robot, MoveRobot3));

            Console.Write(Resources.CommandExampleRobotDrillOn.PadRight(CommandPad));
            this.commandExecutionManager.ExecuteCommand(
                this.commandFactory("robot drill on", this.robot));

            Console.Write(Resources.CommandExampleUndo.PadRight(CommandPad));
            this.commandExecutionManager.Undo();

            Console.WriteLine();
        }
    }
}
