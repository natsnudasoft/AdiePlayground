// <copyright file="ConsoleGoldMinerTests.cs" company="natsnudasoft">
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

    [TestFixture]
    public sealed class ConsoleGoldMinerTests
    {
        [Test]
        public void MineGold_WritesMessage()
        {
            var expectedString = "Gold miner mines some gold." + Environment.NewLine;
            var consoleGoldMiner = new ConsoleGoldMiner();

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                consoleGoldMiner.MineGold();

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Is.EqualTo(expectedString));
        }
    }
}
