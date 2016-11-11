// <copyright file="ConsoleArchitectTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.TemplateMethod
{
    using System;
    using System.Globalization;
    using System.IO;
    using AdiePlayground.Common.TemplateMethod;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConsoleArchitectTests
    {
        [Test]
        public void EatBreakfast_WritesMessage()
        {
            var consoleArchitect = new ConsoleArchitect();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleArchitect.EatBreakfast();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain(" has a banana for breakfast."));
        }

        [Test]
        public void Work_WritesMessage()
        {
            var consoleArchitect = new ConsoleArchitect();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleArchitect.Work();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain(" designs a building."));
        }

        [Test]
        public void Relax_WritesMessage()
        {
            var consoleArchitect = new ConsoleArchitect();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleArchitect.Relax();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain(" reads a book."));
        }
    }
}
