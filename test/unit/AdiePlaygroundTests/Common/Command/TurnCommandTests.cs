// <copyright file="TurnCommandTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Command
{
    using AdiePlayground.Common.Command;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class TurnCommandTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
        {
            var robotMock = new Mock<IRobot>();
            Assert.DoesNotThrow(() => new TurnCommand(robotMock.Object, 0D));
        }

        [Test]
        public void Execute_CallsCorrectMethod()
        {
            const double TurnAngle = -2.75D;
            var robotMock = new Mock<IRobot>();
            var turnCommand = new TurnCommand(robotMock.Object, TurnAngle);

            turnCommand.Execute();

            robotMock.Verify(c => c.Turn(It.Is<double>(d => d == TurnAngle)), Times.Once());
        }

        [Test]
        public void Undo_CallsCorrectMethod()
        {
            const double TurnAngle = 1.25D;
            var robotMock = new Mock<IRobot>();
            var turnCommand = new TurnCommand(robotMock.Object, TurnAngle);
            turnCommand.Execute();

            turnCommand.Undo();

            robotMock.Verify(c => c.Turn(It.Is<double>(d => d == -TurnAngle)), Times.Once());
        }
    }
}
