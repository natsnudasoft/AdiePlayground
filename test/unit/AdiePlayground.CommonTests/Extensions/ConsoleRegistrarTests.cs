// <copyright file="ConsoleRegistrarTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Extensions
{
    using System;
    using System.Globalization;
    using System.IO;
    using Common;
    using Common.Interceptor;
    using Moq;
    using NUnit.Framework;
    using static System.FormattableString;

    /// <summary>
    /// Tests the <see cref="ConsoleRegistrar"/> class.
    /// </summary>
    [TestFixture]
    public sealed class ConsoleRegistrarTests
    {
        private const string ConstructorDateTimeProviderParam = "dateTimeProvider";
        private const long DateTimeTicks = 636102623587144700;
        private static readonly Guid Guid = new Guid("{0863A11F-A1E2-4C06-816F-3AC5CBBBAE39}");

        private Mock<IDateTimeProvider> dateTimeProviderMock;

        /// <summary>
        /// Sets up mocks before each test.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            this.dateTimeProviderMock = new Mock<IDateTimeProvider>();
            this.dateTimeProviderMock
                .SetupGet(d => d.Now)
                .Returns(new DateTime(DateTimeTicks));
        }

        /// <summary>
        /// Tests the constructor with a null date time provider.
        /// </summary>
        [Test]
        public void Constructor_NullDateTimeProvider_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new ConsoleRegistrar(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorDateTimeProviderParam));
        }

        /// <summary>
        /// Tests the constructor with valid values.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ConsoleRegistrar(this.dateTimeProviderMock.Object));
        }

        /// <summary>
        /// Tests the Register method.
        /// </summary>
        [Test]
        public void Register_WritesToRegister()
        {
            const string RegistrationItem = "This is a registration.";
            var registrar = new ConsoleRegistrar(this.dateTimeProviderMock.Object);
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                registrar.Register(Guid, RegistrationItem);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith(Invariant($"{Guid}")));
            Assert.That(outputString, Does.EndWith(RegistrationItem + Environment.NewLine));
        }
    }
}
