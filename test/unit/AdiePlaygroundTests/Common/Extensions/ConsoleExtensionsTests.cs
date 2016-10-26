// <copyright file="ConsoleExtensionsTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Extensions
{
    using System;
    using System.Globalization;
    using System.IO;
    using AdiePlayground.Common.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ConsoleExtensionsTests
    {
        [Test]
        public void WriteColoredLine_WritesLine()
        {
            const string LineToWrite = "This is a test.";
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                ConsoleExtensions.WriteColoredLine(LineToWrite, ConsoleColor.Red);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(LineToWrite + Environment.NewLine));
        }

        [Test]
        public void WriteColored_WritesLine()
        {
            const string TextToWrite = "This is a test.";
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                ConsoleExtensions.WriteColored(TextToWrite, ConsoleColor.Red);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(TextToWrite));
        }
    }
}
