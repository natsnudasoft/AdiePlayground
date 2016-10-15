// <copyright file="ExampleCommandTests.cs" company="natsnudasoft">
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
    using Autofac.Features.Indexed;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ExampleCommandTests
    {
        private const string ConstructorExamplesParam = "examples";

        private Mock<IIndex<string, IExample>> examplesMock;
        private Mock<IExample> exampleMock;

        [SetUp]
        public void BeforeTest()
        {
            this.exampleMock = new Mock<IExample>();
            this.examplesMock = new Mock<IIndex<string, IExample>>();
            var example = this.exampleMock.Object;
            this.examplesMock
                .Setup(i => i.TryGetValue("Found", out example))
                .Returns(true);
            this.examplesMock
                .Setup(i => i.TryGetValue("NotFound", out example))
                .Returns(false);
        }

        [Test]
        public void Constructor_NullExamples_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new ExampleCommand(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorExamplesParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ExampleCommand(this.examplesMock.Object));
        }

        [Test]
        public void Execute_ExampleNotFound_WritesNotFound()
        {
            var exampleCommand = new ExampleCommand(this.examplesMock.Object);
            typeof(ExampleCommand)
                .GetProperty(nameof(ExampleCommand.ExampleName))
                .SetValue(exampleCommand, "NotFound");
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                exampleCommand.Execute(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(
                outputString,
                Is.EqualTo("Could not find example named 'NotFound'." + Environment.NewLine));
        }

        [Test]
        public void Execute_ExampleFound_RunsExample()
        {
            var exampleCommand = new ExampleCommand(this.examplesMock.Object);
            typeof(ExampleCommand)
                .GetProperty(nameof(ExampleCommand.ExampleName))
                .SetValue(exampleCommand, "Found");
            exampleCommand.Execute(CancellationToken.None);
            this.exampleMock.Verify(e => e.Run(It.IsAny<CancellationToken>()));
        }
    }
}
