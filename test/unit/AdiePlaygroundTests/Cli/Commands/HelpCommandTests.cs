// <copyright file="HelpCommandTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Cli.Commands
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using AdiePlayground.Cli;
    using AdiePlayground.Cli.Commands;
    using AdiePlayground.Cli.Metadata;
    using Moq;
    using NUnit.Framework;

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "Disposed in AfterTest")]
    [TestFixture]
    public sealed class HelpCommandTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorCommandLoopParam = "commandLoop";
        private const string ConstructorCommandGroupMetadataFactoryParam =
            "commandGroupMetadataFactory";
#pragma warning restore CC0021 // Use nameof

        private Mock<CommandGroupMetadataFactory> commandGroupMetadataFactoryMock;
        private CommandResolver commandResolver;
        private CommandLoop commandLoop;

        [SetUp]
        public void BeforeTest()
        {
            var commandFactoryMock = new Mock<CommandFactory>();
            this.commandGroupMetadataFactoryMock = new Mock<CommandGroupMetadataFactory>();
            this.commandResolver = new CommandResolver(commandFactoryMock.Object);
            this.commandLoop = new CommandLoop(
                this.commandResolver,
                this.commandGroupMetadataFactoryMock.Object);
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.commandGroupMetadataFactoryMock
                .Setup(f => f(this.commandLoop.CurrentGroup))
                .Returns(new[]
                {
                    new CommandMetadata
                    {
                        Group = this.commandLoop.CurrentGroup,
                        Name = "Command0",
                        HelpText = "This is some help text.",
                        ParametersMetadata = new[]
                        {
                            new CommandParameterMetadata
                            {
                                Index = 0,
                                Name = "Arg0",
                                HelpText = "This is some parameter help text."
                            }
                        }
                    },
                    new CommandMetadata
                    {
                        Group = this.commandLoop.CurrentGroup,
                        Name = "Command1",
                        HelpText = "This is some more help text.",
                        ParametersMetadata = new CommandParameterMetadata[0]
                    }
                });
#pragma warning restore CC0031 // Check for null before calling a delegate
        }

        [TearDown]
        public void AfterTest()
        {
            this.commandLoop.Dispose();
        }

        [Test]
        public void Constructor_NullCommandLoop_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new HelpCommand(null, this.commandGroupMetadataFactoryMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandLoopParam));
        }

        [Test]
        public void Constructor_NullCommandGroupMetadataFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new HelpCommand(this.commandLoop, null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandGroupMetadataFactoryParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => new HelpCommand(
                    this.commandLoop,
                    this.commandGroupMetadataFactoryMock.Object));
        }

        [Test]
        public void Execute_NullCommandName_WritesAllCommandsHelp()
        {
            var helpCommand = new HelpCommand(
                this.commandLoop,
                this.commandGroupMetadataFactoryMock.Object);
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                helpCommand.Execute(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("Command0"));
            Assert.That(outputString, Does.Contain("Command1"));
            Assert.That(outputString, Does.Contain("This is some help text."));
            Assert.That(outputString, Does.Contain("This is some more help text."));
            Assert.That(outputString, Does.Contain("Arg0"));
            Assert.That(outputString, Does.Contain("This is some parameter help text."));
        }

        [Test]
        public void Execute_CommandNotFound_WritesNotFound()
        {
            var helpCommand = new HelpCommand(
                this.commandLoop,
                this.commandGroupMetadataFactoryMock.Object);
            typeof(HelpCommand)
                .GetProperty(nameof(HelpCommand.CommandName))
                .SetValue(helpCommand, "NotFound");

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                helpCommand.Execute(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(
                outputString,
                Is.EqualTo(
                    "Invalid command 'NotFound'." +
                    Environment.NewLine +
                    Environment.NewLine));
        }

        [Test]
        public void Execute_CommandFound_WritesCommandHelp()
        {
            var helpCommand = new HelpCommand(
                this.commandLoop,
                this.commandGroupMetadataFactoryMock.Object);
            typeof(HelpCommand)
                .GetProperty(nameof(HelpCommand.CommandName))
                .SetValue(helpCommand, "Command0");
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                helpCommand.Execute(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("Command0"));
            Assert.That(outputString, Does.Not.Contain("Command1"));
            Assert.That(outputString, Does.Contain("This is some help text."));
            Assert.That(outputString, Does.Not.Contain("This is some more help text."));
            Assert.That(outputString, Does.Contain("Arg0"));
            Assert.That(outputString, Does.Contain("This is some parameter help text."));
        }
    }
}
