// <copyright file="ConsoleInstrumentationReporterTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Interceptor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using AdiePlayground.Common;
    using AdiePlayground.Common.Interceptor;
    using Moq;
    using NUnit.Framework;
    using static System.FormattableString;

    [TestFixture]
    public sealed class ConsoleInstrumentationReporterTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorInvocationCounterParam = "invocationCounter";
        private const string ConstructorInvocationTimerParam = "invocationTimer";
        private const string ConstructorDateTimeProviderParam = "dateTimeProvider";
        private const string ConstructorGuidProviderParam = "guidProvider";
#pragma warning restore CC0021 // Use nameof
        private const long InvocationTime = 501234;
        private const long DateTimeTicks = 636102623587151400;
        private static readonly Guid Guid = new Guid("{310EE18B-38E8-4451-8C55-E5C9102FDC4A}");

        private MethodInvocationCounter invocationCounter;
        private MethodInvocationTimer invocationTimer;
        private Mock<IDateTimeProvider> dateTimeProviderMock;
        private Mock<IGuidProvider> guidProviderMock;

        [SetUp]
        public void BeforeTest()
        {
            this.invocationCounter = new MethodInvocationCounter();
            this.invocationTimer = new MethodInvocationTimer();
            this.dateTimeProviderMock = new Mock<IDateTimeProvider>();
            this.guidProviderMock = new Mock<IGuidProvider>();
            var currentMethod = MethodBase.GetCurrentMethod() as MethodInfo;
            this.invocationCounter.IncrementInvocationCount(currentMethod);
            this.invocationTimer
                .AddInvocationTime(currentMethod, TimeSpan.FromTicks(InvocationTime));
            this.dateTimeProviderMock
                .SetupGet(d => d.Now)
                .Returns(new DateTime(DateTimeTicks));
            this.guidProviderMock
                .Setup(g => g.NewGuid())
                .Returns(Guid);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = "CounterArgument",
            Justification = "This is not a casing exception.")]
        [Test]
        public void Constructor_NullInvocationCounter_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConsoleInstrumentationReporter(
                null,
                this.invocationTimer,
                this.dateTimeProviderMock.Object,
                this.guidProviderMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorInvocationCounterParam));
        }

        [Test]
        public void Constructor_NullInvocationTimer_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConsoleInstrumentationReporter(
                this.invocationCounter,
                null,
                this.dateTimeProviderMock.Object,
                this.guidProviderMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorInvocationTimerParam));
        }

        [Test]
        public void Constructor_NullDateTimeProvider_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConsoleInstrumentationReporter(
                this.invocationCounter,
                this.invocationTimer,
                null,
                this.guidProviderMock.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorDateTimeProviderParam));
        }

        [Test]
        public void Constructor_NullGuidProvider_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConsoleInstrumentationReporter(
                this.invocationCounter,
                this.invocationTimer,
                this.dateTimeProviderMock.Object,
                null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorGuidProviderParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => this.CreateConsoleInstrumentationReporter());
        }

        [Test]
        public void Report_GeneratesReport()
        {
            var consoleInstrumentationReporter = this.CreateConsoleInstrumentationReporter();
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleInstrumentationReporter.Report();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            this.dateTimeProviderMock.VerifyGet(d => d.Now, Times.Once());
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
            Assert.That(outputString, Does.StartWith(
                Invariant($"Console Registrar Report    {Guid}")));
            Assert.That(outputString, Does.Contain("(Generated in "));
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
