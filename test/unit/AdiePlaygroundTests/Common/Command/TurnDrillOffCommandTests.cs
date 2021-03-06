﻿// <copyright file="TurnDrillOffCommandTests.cs" company="natsnudasoft">
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
    public sealed class TurnDrillOffCommandTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
        {
            var robotMock = new Mock<IRobot>();
            Assert.DoesNotThrow(() => new TurnDrillOffCommand(robotMock.Object));
        }

        [Test]
        public void Execute_CallsCorrectMethod()
        {
            var robotMock = new Mock<IRobot>();
            var turnDrillOffCommand = new TurnDrillOffCommand(robotMock.Object);

            turnDrillOffCommand.Execute();

            robotMock.Verify(c => c.TurnDrillOff(), Times.Once());
        }

        [Test]
        public void Undo_CallsCorrectMethod()
        {
            var robotMock = new Mock<IRobot>();
            var turnDrillOffCommand = new TurnDrillOffCommand(robotMock.Object);
            turnDrillOffCommand.Execute();

            turnDrillOffCommand.Undo();

            robotMock.Verify(c => c.TurnDrillOn(), Times.Once());
        }
    }
}
