// <copyright file="TemplateMethodExampleTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Common.TemplateMethod;
    using AdiePlayground.Example;
    using Autofac.Features.Indexed;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class TemplateMethodExampleTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorConsoleWorkersParam = "consoleWorkers";
#pragma warning restore CC0021 // Use nameof

        private Mock<IIndex<string, ConsoleWorker>> consoleWorkersMock;

        [SetUp]
        public void BeforeTest()
        {
            var consoleWorkerMock = new Mock<ConsoleWorker> { CallBase = true };
            this.consoleWorkersMock = new Mock<IIndex<string, ConsoleWorker>>();
            this.consoleWorkersMock
                .SetupGet(i => i[It.IsAny<string>()])
                .Returns(consoleWorkerMock.Object);
        }

        [Test]
        public void Constructor_NullMessageBoard_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new TemplateMethodExample(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorConsoleWorkersParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new TemplateMethodExample(this.consoleWorkersMock.Object));
        }

        [Test]
        public void Run_RunsExample()
        {
            var templateMethodExample = new TemplateMethodExample(this.consoleWorkersMock.Object);

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                templateMethodExample.Run(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith("Running template method example."));
        }
    }
}
