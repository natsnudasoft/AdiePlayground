// <copyright file="ExitCommandTests.cs" company="natsnudasoft">
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
    using System.Reflection;
    using System.Threading;
    using AdiePlayground.Cli;
    using AdiePlayground.Cli.Commands;
    using Moq;
    using NUnit.Framework;

    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design",
        "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "Disposed in AfterTest")]
    [TestFixture]
    public sealed class ExitCommandTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorCommandLoopParam = "commandLoop";
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
                new ExitCommand(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandLoopParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ExitCommand(this.commandLoop));
        }

        [Test]
        public void Execute_CancelsToken()
        {
            var cancellationTokenSource = (CancellationTokenSource)typeof(CommandLoop)
                .GetField(
                    "cancellationTokenSource",
                    BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(this.commandLoop);
            var exitCommand = new ExitCommand(this.commandLoop);

            exitCommand.Execute(CancellationToken.None);

            Assert.That(cancellationTokenSource.IsCancellationRequested, Is.True);
        }
    }
}
