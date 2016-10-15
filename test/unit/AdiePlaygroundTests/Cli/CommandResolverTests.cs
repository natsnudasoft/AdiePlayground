// <copyright file="CommandResolverTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Cli
{
    using System;
    using AdiePlayground.Cli;
    using AdiePlayground.Cli.Metadata;
    using Autofac.Core;
    using Autofac.Core.Registration;
    using Autofac.Features.Metadata;
    using Commands;
    using Metadata;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandResolverTests
    {
        private const string ConstructorCommandFactoryParam = "commandFactory";
        private const string ResolveCommandGroupParam = "commandGroup";
        private const string ResolveParsedCommandParam = "parsedCommand";

        private Mock<CommandFactory> commandFactoryMock;
        private Meta<ICommand, CommandMetadata> commandMeta;

        [SetUp]
        public void BeforeTest()
        {
            var commandMetadata = CommandMetadataHelper.GetCommandMetadata();
            this.commandMeta = new Meta<ICommand, CommandMetadata>(
                new CommandStub(),
                commandMetadata);
            this.commandFactoryMock = new Mock<CommandFactory>();
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.commandFactoryMock
                .Setup(f => f("ValidGroup", "NotRegisteredCommand"))
                .Throws(new ComponentNotRegisteredException(
                    new TypedService(typeof(CommandResolverTests))));
            this.commandFactoryMock
                .Setup(f => f("ValidGroup", "CannotResolveCommand"))
                .Throws(new DependencyResolutionException("Cannot resolve command."));
            this.commandFactoryMock
                .Setup(f => f("ValidGroup", "ValidCommand"))
                .Returns(() => this.commandMeta);
#pragma warning restore CC0031 // Check for null before calling a delegate
        }

        [Test]
        public void Constructor_NullCommandFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new CommandResolver(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCommandFactoryParam));
        }

        [Test]
        public void Resolve_NullCommandGroup_ArgumentNullException()
        {
            var commandResolver = this.CreateCommandResolver();
            var ex = Assert.Throws<ArgumentNullException>(() =>
                commandResolver.Resolve(null, this.CreateValidParsedCommand()));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveCommandGroupParam));
        }

        [Test]
        public void Resolve_EmptyCommandGroup_ArgumentNullException()
        {
            var commandResolver = this.CreateCommandResolver();
            var ex = Assert.Throws<ArgumentException>(() =>
                commandResolver.Resolve(string.Empty, this.CreateValidParsedCommand()));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveCommandGroupParam));
        }

        [Test]
        public void Resolve_NullParsedCommand_ArgumentNullException()
        {
            var commandResolver = this.CreateCommandResolver();
            var ex = Assert.Throws<ArgumentNullException>(() =>
                commandResolver.Resolve("ValidGroup", null));
            Assert.That(ex.ParamName, Is.EqualTo(ResolveParsedCommandParam));
        }

        [Test]
        public void Resolve_CommandNotFoundException()
        {
            var commandResolver = this.CreateCommandResolver();
            var ex = Assert.Throws<CommandNotFoundException>(() =>
                commandResolver.Resolve("ValidGroup", this.CreateNotRegisteredParsedCommand()));
            Assert.That(ex.CommandName, Is.EqualTo("NotRegisteredCommand"));
            Assert.That(ex.CommandArgs, Is.EqualTo(new[] { "Arg0", "Arg1" }));
        }

        [Test]
        public void Resolve_CommandResolveException()
        {
            var commandResolver = this.CreateCommandResolver();
            var ex = Assert.Throws<CommandResolveException>(() =>
                commandResolver.Resolve("ValidGroup", this.CreateCannotResolveParsedCommand()));
            Assert.That(ex.CommandName, Is.EqualTo("CannotResolveCommand"));
            Assert.That(ex.CommandArgs, Is.EqualTo(new[] { "Arg0", "Arg1" }));
        }

        [Test]
        public void Resolve_TooManyArguments_CommandResolveException()
        {
            var commandResolver = this.CreateCommandResolver();
            var ex = Assert.Throws<CommandResolveException>(() =>
                commandResolver.Resolve("ValidGroup", this.CreateTooManyArgumentsParsedCommand()));
            Assert.That(ex.CommandName, Is.EqualTo("ValidCommand"));
            Assert.That(ex.CommandArgs, Is.EqualTo(new[] { "Arg0", "Arg1", "Arg3" }));
        }

        [Test]
        public void Resolve_NotEnoughArguments_CommandResolveException()
        {
            var commandResolver = this.CreateCommandResolver();
            var ex = Assert.Throws<CommandResolveException>(() =>
                commandResolver.Resolve(
                    "ValidGroup",
                    this.CreateNotEnoughArgumentsParsedCommand()));
            Assert.That(ex.CommandName, Is.EqualTo("ValidCommand"));
            Assert.That(ex.CommandArgs, Is.EqualTo(new[] { "Arg0" }));
        }

        private CommandResolver CreateCommandResolver()
        {
            return new CommandResolver(this.commandFactoryMock.Object);
        }

        private ParsedCommand CreateValidParsedCommand()
        {
            return new ParsedCommand("ValidCommand", new[] { "Arg0", "Arg1" });
        }

        private ParsedCommand CreateNotRegisteredParsedCommand()
        {
            return new ParsedCommand("NotRegisteredCommand", new[] { "Arg0", "Arg1" });
        }

        private ParsedCommand CreateCannotResolveParsedCommand()
        {
            return new ParsedCommand("CannotResolveCommand", new[] { "Arg0", "Arg1" });
        }

        private ParsedCommand CreateTooManyArgumentsParsedCommand()
        {
            return new ParsedCommand("ValidCommand", new[] { "Arg0", "Arg1", "Arg3" });
        }

        private ParsedCommand CreateNotEnoughArgumentsParsedCommand()
        {
            return new ParsedCommand("ValidCommand", new[] { "Arg0" });
        }
    }
}
