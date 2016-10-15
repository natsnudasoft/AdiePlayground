// <copyright file="InterceptorExampleTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Common;
    using AdiePlayground.Common.Interceptor;
    using AdiePlayground.Example;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class InterceptorExampleTests
    {
        private const string ConstructorInstrumentationExampleParam = "instrumentationExample";
        private const string ConstructorConsoleInstrumentationReporterParam =
            "consoleInstrumentationReporter";

        private Mock<IInstrumentationExample> instrumentationExampleMock;
        private MethodInvocationCounter invocationCounter;
        private MethodInvocationTimer invocationTimer;
        private Mock<IDateTimeProvider> dateTimeProviderMock;
        private Mock<IGuidProvider> guidProviderMock;

        [SetUp]
        public void BeforeTest()
        {
            this.instrumentationExampleMock = new Mock<IInstrumentationExample>();
            this.invocationCounter = new MethodInvocationCounter();
            this.invocationTimer = new MethodInvocationTimer();
            this.dateTimeProviderMock = new Mock<IDateTimeProvider>();
            this.guidProviderMock = new Mock<IGuidProvider>();
        }

        [Test]
        public void Constructor_NullInstrumentationExample_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new InterceptorExample(
                    null,
                    this.CreateConsoleInstrumentationReporter()));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorInstrumentationExampleParam));
        }

        [Test]
        public void Constructor_NullConsoleInstrumentationReporter_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new InterceptorExample(
                    this.instrumentationExampleMock.Object,
                    null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorConsoleInstrumentationReporterParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => new InterceptorExample(
                    this.instrumentationExampleMock.Object,
                    this.CreateConsoleInstrumentationReporter()));
        }

        [Test]
        public void Run_RunsExample()
        {
            var interceptorExample = new InterceptorExample(
                this.instrumentationExampleMock.Object,
                this.CreateConsoleInstrumentationReporter());

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                interceptorExample.Run(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith("Running interception example."));
        }

        private ConsoleInstrumentationReporter CreateConsoleInstrumentationReporter()
        {
            return new ConsoleInstrumentationReporter(
                this.invocationCounter,
                this.invocationTimer,
                this.dateTimeProviderMock.Object,
                this.guidProviderMock.Object);
        }
    }
}
