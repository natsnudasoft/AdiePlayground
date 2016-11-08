// <copyright file="ContainerConfigurationTests.cs" company="natsnudasoft">
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
    using AdiePlayground;
    using AdiePlayground.Cli;
    using AdiePlayground.Cli.Commands;
    using AdiePlayground.Example;
    using Autofac;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ContainerConfigurationTests
    {
        private static IContainer container;

        [OneTimeSetUp]
        public static void BeforeAllTests()
        {
            container = ContainerConfiguration.Configure();
        }

        [Test]
        public void ContainerConfigured_CliServicesRegistered()
        {
            var commandFactory = container.Resolve<CommandFactory>();
            var commandGroupMetadataFactory = container.Resolve<CommandGroupMetadataFactory>();
            var command = commandFactory(CommandUsage.DefaultCommandGroup, "exit");
            var commandGroupMetadata = commandGroupMetadataFactory(
                CommandUsage.DefaultCommandGroup);
            var commandResolver = container.Resolve<CommandResolver>();
            var exampleCommand = commandResolver.Resolve(
                CommandUsage.DefaultCommandGroup,
                new ParsedCommand("example", new[] { "interceptor" }));
            var commandLoop = container.Resolve<CommandLoop>();

            Assert.That(commandFactory, Is.Not.Null);
            Assert.That(commandGroupMetadataFactory, Is.Not.Null);
            Assert.That(command, Is.Not.Null);
            Assert.That(commandGroupMetadata, Is.Not.Null);
            Assert.That(commandResolver, Is.Not.Null);
            var exampleCommandCasted = exampleCommand as ExampleCommand;
            Assert.That(exampleCommandCasted, Is.Not.Null);
            Assert.That(exampleCommandCasted.ExampleName, Is.EqualTo("interceptor"));
            Assert.That(commandLoop, Is.Not.Null);
        }

        [Test]
        public void ContainerConfigured_ExampleServicesRegistered()
        {
            var example = container.ResolveNamed<IExample>("strategy");
            var exampleMetadataCollectionFactory =
                container.Resolve<ExampleMetadataCollectionFactory>();
            var exampleMetadataCollection = exampleMetadataCollectionFactory?.Invoke();
            var instrumentationExample = container.Resolve<IInstrumentationExample>();

            Assert.That(example, Is.Not.Null);
            Assert.That(exampleMetadataCollectionFactory, Is.Not.Null);
            Assert.That(exampleMetadataCollection, Is.Not.Null);
            Assert.That(instrumentationExample, Is.Not.Null);
        }
    }
}
