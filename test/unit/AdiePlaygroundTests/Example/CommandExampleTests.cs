// <copyright file="CommandExampleTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Example
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using AdiePlayground.Common.Command;
    using AdiePlayground.Example;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandExampleTests
    {
        private const string ConstructorRobotParam = "robot";
        private const string ConstructorCommandFactoryParam = "commandFactory";
        private const string ConstructorCommandExecutionManagerParam = "commandExecutionManager";

        private Mock<IRobot> robotMock;
        private Mock<CommandFactory> commandFactoryMock;
        private Mock<ICommand> commandMock;

        [SetUp]
        public void BeforeTest()
        {
            this.robotMock = new Mock<IRobot>();
            this.commandMock = new Mock<ICommand>();
            this.commandFactoryMock = new Mock<CommandFactory>();
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.commandFactoryMock
                .Setup(f => f(It.IsAny<string>(), It.IsAny<object[]>()))
                .Returns(() => this.commandMock.Object);
#pragma warning restore CC0031 // Check for null before calling a delegate
        }

        [Test]
        public void Constructor_NullRobot_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandExample(
                    null,
                    this.commandFactoryMock.Object,
                    new CommandExecutionManager()));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorRobotParam));
        }

        [Test]
        public void Constructor_NullCommandFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandExample(
                    this.robotMock.Object,
                    null,
                    new CommandExecutionManager()));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandFactoryParam));
        }

        [Test]
        public void Constructor_NullCommandExecutionManager_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandExample(
                    this.robotMock.Object,
                    this.commandFactoryMock.Object,
                    null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandExecutionManagerParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => new CommandExample(
                    this.robotMock.Object,
                    this.commandFactoryMock.Object,
                    new CommandExecutionManager()));
        }

        [Test]
        public void Run_RunsExample()
        {
            var commandExample = new CommandExample(
                this.robotMock.Object,
                this.commandFactoryMock.Object,
                new CommandExecutionManager());

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                commandExample.Run(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith("Running command example."));
            this.commandMock.Verify(c => c.Execute(), Times.Exactly(6));
            this.commandMock.Verify(c => c.Undo(), Times.Exactly(3));
        }
    }
}
