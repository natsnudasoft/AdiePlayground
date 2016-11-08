// <copyright file="FacadeExampleTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Example
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using AdiePlayground.Common.Facade;
    using AdiePlayground.Example;
    using NUnit.Framework;

    [TestFixture]
    public sealed class FacadeExampleTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorGoldMineParam = "goldMine";
#pragma warning restore CC0021 // Use nameof

        private GoldMine goldMine;

        [SetUp]
        public void BeforeTest()
        {
            this.goldMine = new GoldMine();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = nameof(GoldMine),
            Justification = "Not a compound word.")]
        [Test]
        public void Constructor_NullGoldMine_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new FacadeExample(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorGoldMineParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new FacadeExample(this.goldMine));
        }

        [Test]
        public void Run_RunsExample()
        {
            var facadeExample = new FacadeExample(this.goldMine);

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                facadeExample.Run(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith("Running facade example."));
            Assert.That(outputString, Does.Contain("Operating gold mine."));
            Assert.That(outputString, Does.Contain("Gold prospector finds a gold vein."));
        }
    }
}
