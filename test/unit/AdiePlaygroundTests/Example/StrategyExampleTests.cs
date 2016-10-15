// <copyright file="StrategyExampleTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Common.Strategy;
    using AdiePlayground.Example;
    using Autofac.Features.Indexed;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class StrategyExampleTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorStrategyResolverParam = "strategyResolver";
#pragma warning restore CC0021 // Use nameof

        private SortStrategyResolver<string> strategyResolver;
        private Mock<IIndex<SortType, ISortStrategy<string>>> sortStrategiesMock;

        [SetUp]
        public void BeforeTest()
        {
            var sortStrategyMock = new Mock<ISortStrategy<string>>();
            this.sortStrategiesMock = new Mock<IIndex<SortType, ISortStrategy<string>>>();
            var sortStrategy = sortStrategyMock.Object;
            this.sortStrategiesMock
                .Setup(i => i.TryGetValue(It.IsAny<SortType>(), out sortStrategy))
                .Returns(true);
            this.strategyResolver =
                new SortStrategyResolver<string>(this.sortStrategiesMock.Object);
        }

        [Test]
        public void Constructor_NullMessageBoard_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new StrategyExample(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorStrategyResolverParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new StrategyExample(this.strategyResolver));
        }

        [Test]
        public void Run_RunsExample()
        {
            var strategyExample = new StrategyExample(this.strategyResolver);

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                strategyExample.Run(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.StartWith("Running strategy example."));
        }
    }
}
