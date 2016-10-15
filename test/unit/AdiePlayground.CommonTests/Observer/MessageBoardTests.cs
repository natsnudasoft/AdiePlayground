// <copyright file="MessageBoardTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Observer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common.Observer;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MessageBoardTests
    {
        private const string AddMessageMessageParam = "message";
        private const string RemoveMessageMessageParam = "message";
        private const string AttachObserverParam = "observer";
        private const string DetachObserverParam = "observer";

        [Test]
        public void AddMessage_NullMessage_ArgumentNullException()
        {
            var messageBoard = new MessageBoard();
            var ex = Assert.Throws<ArgumentNullException>(() => messageBoard.AddMessage(null));
            Assert.That(ex.ParamName, Is.EqualTo(AddMessageMessageParam));
        }

        [Test]
        public void AddMessage_EmptyMessage_ArgumentException()
        {
            var messageBoard = new MessageBoard();
            var ex = Assert.Throws<ArgumentException>(() => messageBoard.AddMessage(string.Empty));
            Assert.That(ex.ParamName, Is.EqualTo(AddMessageMessageParam));
        }

        [Test]
        public void AddMessage_ObserversNotified()
        {
            const string message = "Room for rent.";
            var messageBoard = new MessageBoard();
            var observerMock = new Mock<IMessageBoardObserver>();
            messageBoard.Attach(observerMock.Object);

            messageBoard.AddMessage(message);

            observerMock.Verify(
                o => o.Update(It.Is<IEnumerable<string>>(e => e.Single() == message)), Times.Once);
        }

        [Test]
        public void AddMessage_Duplicate_ObserversNotNotified()
        {
            const string message = "Looking for car share.";
            var messageBoard = new MessageBoard();
            var observerMock = new Mock<IMessageBoardObserver>();
            messageBoard.AddMessage(message);
            messageBoard.Attach(observerMock.Object);

            messageBoard.AddMessage(message);

            observerMock.Verify(
                o => o.Update(It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void RemoveMessage_NullMessage_ArgumentNullException()
        {
            var messageBoard = new MessageBoard();
            var ex = Assert.Throws<ArgumentNullException>(() => messageBoard.RemoveMessage(null));
            Assert.That(ex.ParamName, Is.EqualTo(RemoveMessageMessageParam));
        }

        [Test]
        public void RemoveMessage_EmptyMessage_ArgumentException()
        {
            var messageBoard = new MessageBoard();
            var ex = Assert.Throws<ArgumentException>(
                () => messageBoard.RemoveMessage(string.Empty));
            Assert.That(ex.ParamName, Is.EqualTo(RemoveMessageMessageParam));
        }

        [Test]
        public void RemoveMessage_ObserversNotified()
        {
            const string message1 = "Cat for sale.";
            const string message2 = "Dog for sale.";
            var messageBoard = new MessageBoard();
            var observerMock = new Mock<IMessageBoardObserver>();
            messageBoard.AddMessage(message1);
            messageBoard.AddMessage(message2);
            messageBoard.Attach(observerMock.Object);

            messageBoard.RemoveMessage(message1);

            observerMock.Verify(
                o => o.Update(It.Is<IEnumerable<string>>(e => e.Single() == message2)), Times.Once);
        }

        [Test]
        public void RemoveMessage_Nonexistent_ObserversNotNotified()
        {
            const string message = "Have you seen this man?";
            var messageBoard = new MessageBoard();
            var observerMock = new Mock<IMessageBoardObserver>();
            messageBoard.Attach(observerMock.Object);

            messageBoard.RemoveMessage(message);

            observerMock.Verify(
                o => o.Update(It.IsAny<IEnumerable<string>>()), Times.Never);
        }

        [Test]
        public void Attach_NullObserver_ArgumentNullException()
        {
            var messageBoard = new MessageBoard();
            var ex = Assert.Throws<ArgumentNullException>(() => messageBoard.Attach(null));
            Assert.That(ex.ParamName, Is.EqualTo(AttachObserverParam));
        }

        [Test]
        public void Attach_DoesNotThrow()
        {
            var messageBoard = new MessageBoard();
            var observerMock = new Mock<IMessageBoardObserver>();
            Assert.DoesNotThrow(() => messageBoard.Attach(observerMock.Object));
        }

        [Test]
        public void Detach_NullObserver_ArgumentNullException()
        {
            var messageBoard = new MessageBoard();
            var ex = Assert.Throws<ArgumentNullException>(() => messageBoard.Detach(null));
            Assert.That(ex.ParamName, Is.EqualTo(DetachObserverParam));
        }

        [Test]
        public void Detach_DoesNotThrow()
        {
            var messageBoard = new MessageBoard();
            var observerMock = new Mock<IMessageBoardObserver>();
            Assert.DoesNotThrow(() => messageBoard.Detach(observerMock.Object));
        }
    }
}
