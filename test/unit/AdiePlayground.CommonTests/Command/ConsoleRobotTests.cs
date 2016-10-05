// <copyright file="ConsoleRobotTests.cs" company="natsnudasoft">
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
    using System.Globalization;
    using System.IO;
    using Common.Command;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="ConsoleRobot"/> class.
    /// </summary>
    [TestFixture]
    public sealed class ConsoleRobotTests
    {
        /// <summary>
        /// Tests the constructor with valid values.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ConsoleRobot());
        }

        /// <summary>
        /// Tests the Move method.
        /// </summary>
        [Test]
        public void Move_WritesMessage()
        {
            const double MoveDistance = 20.75D;
            const string expectedString = "Moved forwards 20.75 metres.\r\n";
            var robot = new ConsoleRobot();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                robot.Move(MoveDistance);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(expectedString));
        }

        /// <summary>
        /// Tests the Turn method.
        /// </summary>
        [Test]
        public void Turn_WritesMessage()
        {
            const double TurnAngle = -0.25D;
            const string expectedPattern = @"^Turned left \d+\.\d{2} degrees.\r\n$";
            var robot = new ConsoleRobot();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                robot.Turn(TurnAngle);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Match(expectedPattern));
        }

        /// <summary>
        /// Tests the TurnDrillOn method.
        /// </summary>
        [Test]
        public void TurnDrillOn_WritesMessage()
        {
            const string expectedString = "Turned drill on.\r\n";
            var robot = new ConsoleRobot();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                robot.TurnDrillOn();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(expectedString));
        }

        /// <summary>
        /// Tests the TurnDrillOff method.
        /// </summary>
        [Test]
        public void TurnDrillOff_WritesMessage()
        {
            const string expectedString = "Turned drill off.\r\n";
            var robot = new ConsoleRobot();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                robot.TurnDrillOff();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(expectedString));
        }
    }
}
