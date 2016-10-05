// <copyright file="CommandExecutionManagerTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Command
{
    using System;
    using Common.Command;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="CommandExecutionManager"/> class.
    /// </summary>
    [TestFixture]
    public sealed class CommandExecutionManagerTests
    {
        private const string ExecuteCommandParam = "command";

        /// <summary>
        /// Tests the constructor with valid values.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new CommandExecutionManager());
        }

        /// <summary>
        /// Tests the ExecuteCommand method with a null command.
        /// </summary>
        [Test]
        public void ExecuteCommand_NullCommand_ArgumentNullException()
        {
            var commandExecutionManager = new CommandExecutionManager();
            var ex = Assert.Throws<ArgumentNullException>(
                () => commandExecutionManager.ExecuteCommand(null));
            Assert.That(ex.ParamName, Is.EqualTo(ExecuteCommandParam));
        }

        /// <summary>
        /// Tests the ExecuteCommand method.
        /// </summary>
        [Test]
        public void ExecuteCommand_ExecutesCommand()
        {
            var commandExecutionManager = new CommandExecutionManager();
            var commandMock = new Mock<ICommand>();

            commandExecutionManager.ExecuteCommand(commandMock.Object);

            commandMock.Verify(c => c.Execute(), Times.Once());
        }

        /// <summary>
        /// Tests the ExecuteCommand method.
        /// </summary>
        [Test]
        public void ExecuteCommand_ClearsUndoHistory()
        {
            var commandExecutionManager = new CommandExecutionManager();
            var commandMock = new Mock<ICommand>();
            commandExecutionManager.ExecuteCommand(commandMock.Object);
            commandExecutionManager.Undo();

            commandExecutionManager.ExecuteCommand(commandMock.Object);

            Assert.Throws<InvalidOperationException>(() => commandExecutionManager.Redo());
        }

        /// <summary>
        /// Tests the Undo method when there is nothing to undo.
        /// </summary>
        [Test]
        public void Undo_NothingToUndo_InvalidOperationException()
        {
            var commandExecutionManager = new CommandExecutionManager();
            Assert.Throws<InvalidOperationException>(() => commandExecutionManager.Undo());
        }

        /// <summary>
        /// Tests the Undo method.
        /// </summary>
        [Test]
        public void Undo_PerformsUndo()
        {
            var commandExecutionManager = new CommandExecutionManager();
            var commandMock = new Mock<ICommand>();
            commandExecutionManager.ExecuteCommand(commandMock.Object);

            commandExecutionManager.Undo();

            commandMock.Verify(c => c.Undo(), Times.Once());
        }

        /// <summary>
        /// Tests the Redo method when there is nothing to redo.
        /// </summary>
        [Test]
        public void Redo_NothingToRedo_InvalidOperationException()
        {
            var commandExecutionManager = new CommandExecutionManager();
            Assert.Throws<InvalidOperationException>(() => commandExecutionManager.Redo());
        }

        /// <summary>
        /// Tests the Redo method.
        /// </summary>
        [Test]
        public void Redo_PerformsRedo()
        {
            var commandExecutionManager = new CommandExecutionManager();
            var commandMock = new Mock<ICommand>();
            commandExecutionManager.ExecuteCommand(commandMock.Object);
            commandExecutionManager.Undo();

            commandExecutionManager.Redo();

            commandMock.Verify(c => c.Execute(), Times.Exactly(2));
        }
    }
}
