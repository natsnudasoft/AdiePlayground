// <copyright file="ObserverExampleTests.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using AdiePlayground.Common.Observer;
    using AdiePlayground.Example;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ObserverExampleTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorMessageBoardParam = "messageBoard";
        private const string ConstructorObserverFactoryParam = "observerFactory";
#pragma warning restore CC0021 // Use nameof

        private MessageBoard messageBoard;
        private Func<IMessageBoardObserver> messageBoardObserverFactory;

        [SetUp]
        public void BeforeTest()
        {
            this.messageBoard = new MessageBoard();
            var messageBoardObserverMock1 = new Mock<IMessageBoardObserver>();
            var messageBoardObserverMock2 = new Mock<IMessageBoardObserver>();
            var messageBoardObserverQueue = new Queue<IMessageBoardObserver>(
                new[]
                {
                    messageBoardObserverMock1.Object,
                    messageBoardObserverMock2.Object
                });
            this.messageBoardObserverFactory = () => messageBoardObserverQueue.Dequeue();
        }

        [Test]
        public void Constructor_NullMessageBoard_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ObserverExample(
                    null,
                    this.messageBoardObserverFactory));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorMessageBoardParam));
        }

        [Test]
        public void Constructor_NullObserverFactory_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ObserverExample(
                    this.messageBoard,
                    null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorObserverFactoryParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => new ObserverExample(
                    this.messageBoard,
                    this.messageBoardObserverFactory));
        }

        [Test]
        public void Run_RunsExample()
        {
            var observerExample = new ObserverExample(
                this.messageBoard,
                this.messageBoardObserverFactory);

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                observerExample.Run(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith("Running observer example."));
        }
    }
}
