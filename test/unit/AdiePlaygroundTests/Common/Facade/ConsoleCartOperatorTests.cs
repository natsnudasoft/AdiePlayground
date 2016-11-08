// <copyright file="ConsoleCartOperatorTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Facade
{
    using System;
    using System.Globalization;
    using System.IO;
    using AdiePlayground.Common.Facade;
    using NUnit.Framework;
    using static System.FormattableString;

    [TestFixture]
    public sealed class ConsoleCartOperatorTests
    {
        [Test]
        public void MoveTo_WritesMessage()
        {
            const string location = "TestLocation";
            var expectedString = Invariant($"Cart operator moves to {location}.")
                + Environment.NewLine;
            var consoleCartOperator = new ConsoleCartOperator();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleCartOperator.MoveTo(location);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(expectedString));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = "PickUp",
            Justification = "Not a compound word.")]
        [Test]
        public void PickUpGold_WritesMessage()
        {
            var expectedString = "Cart operator picks up some gold." + Environment.NewLine;
            var consoleCartOperator = new ConsoleCartOperator();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleCartOperator.PickUpGold();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(expectedString));
        }

        [Test]
        public void DepositGold_WritesMessage()
        {
            var expectedString = "Cart operator deposits some gold." + Environment.NewLine;
            var consoleCartOperator = new ConsoleCartOperator();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleCartOperator.DepositGold();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(expectedString));
        }
    }
}
