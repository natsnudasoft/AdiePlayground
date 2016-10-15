// <copyright file="ExampleListCommandTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Cli.Commands;
    using AdiePlayground.Example;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ExampleListCommandTests
    {
        private const string ConstructorExampleMetadataCollectionFactoryParam =
            "exampleMetadataCollectionFactory";

        private Mock<ExampleMetadataCollectionFactory> exampleMetadataCollectionFactoryMock;

        [SetUp]
        public void BeforeTest()
        {
            this.exampleMetadataCollectionFactoryMock =
                new Mock<ExampleMetadataCollectionFactory>();
#pragma warning disable CC0031 // Check for null before calling a delegate
            this.exampleMetadataCollectionFactoryMock
                .Setup(f => f())
                .Returns(new[]
                {
                    new ExampleMetadata { Name = "Example1" },
                    new ExampleMetadata { Name = "Example2" }
                });
#pragma warning restore CC0031 // Check for null before calling a delegate
        }

        [Test]
        public void Constructor_NullExampleMetadataCollectionFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new ExampleListCommand(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorExampleMetadataCollectionFactoryParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
                new ExampleListCommand(this.exampleMetadataCollectionFactoryMock.Object));
        }

        [Test]
        public void Execute_WritesExampleNames()
        {
            var exampleListCommand =
                new ExampleListCommand(this.exampleMetadataCollectionFactoryMock.Object);
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                exampleListCommand.Execute(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(
                outputString,
                Is.EqualTo("Example1" + Environment.NewLine + "Example2" + Environment.NewLine));
        }
    }
}
