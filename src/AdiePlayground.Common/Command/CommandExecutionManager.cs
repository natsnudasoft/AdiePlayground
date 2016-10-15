// <copyright file="CommandExecutionManager.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Command
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a class to execute instances of <see cref="ICommand"/> and manage a list of
    /// previously executed instances to allow undo/redo functionality.
    /// </summary>
    public sealed class CommandExecutionManager
    {
        private readonly LinkedList<ICommand> executedCommands = new LinkedList<ICommand>();
        private LinkedListNode<ICommand> previousCommand;
        private LinkedListNode<ICommand> previousUndo;

        /// <summary>
        /// Executes the specified <see cref="ICommand"/> and adds it to the undo list.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="command"/> is
        /// <see langword="null"/>.</exception>
        public void ExecuteCommand(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.Execute();
            while (this.executedCommands.Last != this.previousCommand)
            {
                this.executedCommands.RemoveLast();
            }

            this.previousCommand = this.executedCommands.AddLast(command);
            this.previousUndo = null;
        }

        /// <summary>
        /// Undo the current topmost <see cref="ICommand"/> in this
        /// <see cref="CommandExecutionManager"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">No operations to undo.</exception>
        public void Undo()
        {
            var undoCommand = this.previousCommand;
            if (undoCommand == null)
            {
                throw new InvalidOperationException("No operations to undo.");
            }

            undoCommand.Value.Undo();
            this.previousCommand = undoCommand.Previous;
            this.previousUndo = undoCommand;
        }

        /// <summary>
        /// Redo the most recent undo operation in this <see cref="CommandExecutionManager"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">No operations to redo.</exception>
        public void Redo()
        {
            var redoCommand = this.previousUndo;
            if (redoCommand == null)
            {
                throw new InvalidOperationException("No operations to redo.");
            }

            redoCommand.Value.Execute();
            this.previousCommand = redoCommand;
            this.previousUndo = redoCommand.Next;
        }
    }
}
