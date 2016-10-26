// <copyright file="ConsoleMessageBoardObserverTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Observer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using AdiePlayground.Common;
    using AdiePlayground.Common.Observer;
    using Moq;
    using NUnit.Framework;
    using static System.FormattableString;

    [TestFixture]
    public sealed class ConsoleMessageBoardObserverTests
    {
        private const string ConstructorGuidProviderParam = "guidProvider";
        private const string UpdateMessagesParam = "messages";
        private static readonly Guid Guid = new Guid("{CE1C9C0C-7BA5-42F2-BE63-E9EA1E41D2B3}");

        private Mock<IGuidProvider> guidProviderMock;

        [SetUp]
        public void BeforeTest()
        {
            this.guidProviderMock = new Mock<IGuidProvider>();
            this.guidProviderMock
                .Setup(g => g.NewGuid())
                .Returns(Guid);
        }

        [Test]
        public void Constructor_NullGuidProvider_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ConsoleMessageBoardObserver(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorGuidProviderParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => this.CreateConsoleMessageBoardObserver());
            this.guidProviderMock.Verify(g => g.NewGuid(), Times.Once());
        }

        [Test]
        public void Update_NullMessages_ArgumentNullException()
        {
            var observer = this.CreateConsoleMessageBoardObserver();
            var observerExplicit = (IMessageBoardObserver)observer;

            var ex = Assert.Throws<ArgumentNullException>(() => observerExplicit.Update(null));
            Assert.That(ex.ParamName, Is.EqualTo(UpdateMessagesParam));
        }

        [Test]
        public void Update_WritesState()
        {
            var messages = new List<string>
            {
                "Fridge for sale.",
                "Members for band wanted.",
                "Help wanted in local gadget shop."
            };
            var observer = this.CreateConsoleMessageBoardObserver();
            var observerExplicit = (IMessageBoardObserver)observer;

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                observerExplicit.Update(messages);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith(
                Invariant($"{nameof(ConsoleMessageBoardObserver)} ({Guid}) update:")));
            Assert.That(outputString, Does.Contain(messages[0]));
            Assert.That(outputString, Does.Contain(messages[1]));
            Assert.That(outputString, Does.Contain(messages[2]));
        }

        private ConsoleMessageBoardObserver CreateConsoleMessageBoardObserver()
        {
            return new ConsoleMessageBoardObserver(this.guidProviderMock.Object);
        }
    }
}
