// <copyright file="CommandLoopTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Threading;
    using AdiePlayground.Cli;
    using Cli;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandLoopTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorCommandResolverParam = "commandResolver";
        private const string ConstructorCommandGroupMetadataFactoryParam =
            "commandGroupMetadataFactory";
#pragma warning restore CC0021 // Use nameof

        private Mock<CommandFactory> commandFactoryMock;
        private Mock<CommandGroupMetadataFactory> commandGroupMetadataFactoryMock;
        private CommandResolver commandResolver;

        [SetUp]
        public void BeforeTest()
        {
            this.commandFactoryMock = new Mock<CommandFactory>();
            this.commandGroupMetadataFactoryMock = new Mock<CommandGroupMetadataFactory>();
            this.commandResolver = new CommandResolver(this.commandFactoryMock.Object);
        }

        [Test]
        public void Constructor_NullCommandResolver_ArgumentNullException()
        {
#pragma warning disable CC0022 // Should dispose object
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new CommandLoop(null, this.commandGroupMetadataFactoryMock.Object));
#pragma warning restore CC0022 // Should dispose object
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandResolverParam));
        }

        [Test]
        public void Constructor_NullCommandGroupMetadataFactory_ArgumentNullException()
        {
#pragma warning disable CC0022 // Should dispose object
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new CommandLoop(this.commandResolver, null));
#pragma warning restore CC0022 // Should dispose object
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandGroupMetadataFactoryParam));
        }

        [Test]
        public void CurrentGroup_ReturnsValue()
        {
            var commandLoop = this.CreateCommandLoop();
            var currentGroup = commandLoop.CurrentGroup;
            Assert.That(currentGroup, Is.EqualTo("CommandGroup"));
        }

        [Test]
        [Timeout(2000)]
        public void Run_CommandLoopAlreadyRunning_InvalidOperationException()
        {
            var commandLoop = this.CreateCommandLoop();
            var commandLoopRunningField = typeof(CommandLoop)
                .GetField(
                    "commandLoopRunning",
                    BindingFlags.Static | BindingFlags.NonPublic);
            commandLoopRunningField.SetValue(commandLoop, true);
            Assert.Throws<InvalidOperationException>(() => commandLoop.Run());
            commandLoopRunningField.SetValue(commandLoop, false);
        }

        [Test]
        [Timeout(2000)]
        public void Run_CommandResolveExceptionCommandFound_WritesInvalidArguments()
        {
            var commandLoop = this.CreateCommandLoop();
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.commandFactoryMock
                .Setup(f => f("CommandGroup", "exit"))
                .Callback(GetCommandLoopCancellationTokenSource(commandLoop).Cancel)
                .Throws(new CommandResolveException("exit", new[] { "Arg0" }));
            this.commandGroupMetadataFactoryMock
                .Setup(f => f("CommandGroup"))
                .Returns(new[]
                {
                    CommandFactoryHelper.CreateExitCommandMeta(commandLoop).Metadata
                });
#pragma warning restore CC0031 // Check for null before calling a delegate

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            using (var newIn = new StringReader("exit Arg0"))
            {
                var previousOut = Console.Out;
                var previousIn = Console.In;
                Console.SetOut(newOut);
                Console.SetIn(newIn);

                commandLoop.Run();

                Console.SetIn(previousIn);
                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("Invalid arguments on command 'exit'."));
            Assert.That(outputString, Does.Not.Contain("Exit help text."));
        }

        [Test]
        [Timeout(2000)]
        public void Run_CommandResolveExceptionCommandNotFound_WritesCommandNotFound()
        {
            var commandLoop = this.CreateCommandLoop();
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.commandFactoryMock
                .Setup(f => f("CommandGroup", "exit"))
                .Callback(GetCommandLoopCancellationTokenSource(commandLoop).Cancel)
                .Throws(new CommandResolveException("exit", new[] { "Arg0" }));
#pragma warning restore CC0031 // Check for null before calling a delegate

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            using (var newIn = new StringReader("exit Arg0"))
            {
                var previousOut = Console.Out;
                var previousIn = Console.In;
                Console.SetOut(newOut);
                Console.SetIn(newIn);

                commandLoop.Run();

                Console.SetIn(previousIn);
                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("Invalid command 'exit'."));
            Assert.That(outputString, Does.Not.Contain("Exit help text."));
        }

        [Test]
        [Timeout(2000)]
        public void Run_CommandNotFoundException_WritesCommandNotFound()
        {
            var commandLoop = this.CreateCommandLoop();
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.commandFactoryMock
                .Setup(f => f("CommandGroup", "exit"))
                .Callback(GetCommandLoopCancellationTokenSource(commandLoop).Cancel)
                .Throws(new CommandNotFoundException("exit", new[] { "Arg0" }));
#pragma warning restore CC0031 // Check for null before calling a delegate

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            using (var newIn = new StringReader("exit Arg0"))
            {
                var previousOut = Console.Out;
                var previousIn = Console.In;
                Console.SetOut(newOut);
                Console.SetIn(newIn);

                commandLoop.Run();

                Console.SetIn(previousIn);
                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("Invalid command 'exit'."));
            Assert.That(outputString, Does.Not.Contain("Exit help text."));
        }

        [Test]
        [Timeout(2000)]
        public void Run_ValidCommand()
        {
            var commandLoop = this.CreateCommandLoop();
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.commandFactoryMock
                .Setup(f => f("CommandGroup", "exit"))
                .Returns(() => CommandFactoryHelper.CreateExitCommandMeta(commandLoop));
#pragma warning restore CC0031 // Check for null before calling a delegate

            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            using (var newIn = new StringReader("exit"))
            {
                var previousOut = Console.Out;
                var previousIn = Console.In;
                Console.SetOut(newOut);
                Console.SetIn(newIn);

                commandLoop.Run();

                Console.SetIn(previousIn);
                Console.SetOut(previousOut);
            }
        }

        private static CancellationTokenSource GetCommandLoopCancellationTokenSource(
            CommandLoop commandLoop)
        {
            return (CancellationTokenSource)typeof(CommandLoop)
                .GetField(
                    "cancellationTokenSource",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(commandLoop);
        }

        private CommandLoop CreateCommandLoop()
        {
            return new CommandLoop(
                this.commandResolver,
                this.commandGroupMetadataFactoryMock.Object)
            {
                CurrentGroup = "CommandGroup"
            };
        }
    }
}