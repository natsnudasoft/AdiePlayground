// <copyright file="RobotCommandTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Command
{
    using System;
    using Common.Command;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="RobotCommand"/> class.
    /// </summary>
    [TestFixture]
    public sealed class RobotCommandTests
    {
        private const string ConstructorRobotParam = "robot";

        /// <summary>
        /// Tests the constructor with a null robot.
        /// </summary>
        [Test]
        public void Constructor_NullRobot_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new RobotCommandStub(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorRobotParam));
        }

        /// <summary>
        /// Tests the constructor with valid values.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            var robotMock = new Mock<IRobot>();
            Assert.DoesNotThrow(() => new RobotCommandStub(robotMock.Object));
        }

        /// <summary>
        /// Tests the Robot property.
        /// </summary>
        [Test]
        public void Robot_GetsRobot()
        {
            var robotMock = new Mock<IRobot>();
            var robotCommand = new RobotCommandStub(robotMock.Object);
            Assert.That(robotCommand.Robot, Is.EqualTo(robotMock.Object));
        }
    }
}
