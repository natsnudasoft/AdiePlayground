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

namespace AdiePlaygroundTests.Common.Command
{
    using System;
    using System.Globalization;
    using System.IO;
    using AdiePlayground.Common.Command;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConsoleRobotTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ConsoleRobot());
        }

        [Test]
        public void Move_WritesMessage()
        {
            const double MoveDistance = 20.75D;
            var expectedString = "Moved forwards 20.75 metres." + Environment.NewLine;
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

        [Test]
        public void Turn_WritesMessage()
        {
            const double TurnAngle = -0.25D;
            var expectedPattern = @"^Turned left \d+\.\d{2} degrees." + Environment.NewLine + "$";
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

        [Test]
        public void TurnDrillOn_WritesMessage()
        {
            var expectedString = "Turned drill on." + Environment.NewLine;
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

        [Test]
        public void TurnDrillOff_WritesMessage()
        {
            var expectedString = "Turned drill off." + Environment.NewLine;
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
