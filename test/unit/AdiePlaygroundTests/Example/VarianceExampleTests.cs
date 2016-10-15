// <copyright file="VarianceExampleTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Common.Model;
    using AdiePlayground.Common.Variance;
    using AdiePlayground.Example;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public sealed class VarianceExampleTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string ConstructorOrangeInvariantParam = "orangeInvariant";
        private const string ConstructorBananaCovariantParam = "bananaCovariant";
        private const string ConstructorFruitContravariantParam = "fruitContravariant";
#pragma warning restore CC0021 // Use nameof

        private Mock<IInvariant<Orange>> orangeInvariant;
        private Mock<ICovariant<Banana>> bananaCovariant;
        private Mock<IContravariant<Fruit>> fruitContravariant;

        [SetUp]
        public void BeforeTest()
        {
            this.orangeInvariant = new Mock<IInvariant<Orange>>();
            this.orangeInvariant
                .Setup(o => o.GetValue(It.IsAny<Orange>()))
                .Returns<Orange>(o => o.Quality);
            this.bananaCovariant = new Mock<ICovariant<Banana>>();
            this.bananaCovariant
                .Setup(b => b.Create(It.IsAny<object[]>()))
                .Returns(new Banana(10));
            this.fruitContravariant = new Mock<IContravariant<Fruit>>();
            this.fruitContravariant
                .Setup(f => f.GetValue(It.IsAny<Fruit>()))
                .Returns<Fruit>(f => f.Quality);
        }

        [Test]
        public void Constructor_NullOrangeInvariant_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new VarianceExample(
                    null,
                    this.bananaCovariant.Object,
                    this.fruitContravariant.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorOrangeInvariantParam));
        }

        [Test]
        public void Constructor_NullBananaCovariant_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new VarianceExample(
                    this.orangeInvariant.Object,
                    null,
                    this.fruitContravariant.Object));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorBananaCovariantParam));
        }

        [Test]
        public void Constructor_NullFruitContravariant_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new VarianceExample(
                    this.orangeInvariant.Object,
                    this.bananaCovariant.Object,
                    null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorFruitContravariantParam));
        }

        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new VarianceExample(
                this.orangeInvariant.Object,
                this.bananaCovariant.Object,
                this.fruitContravariant.Object));
        }

        [Test]
        public void Run_RunsExample()
        {
            var varianceExample = new VarianceExample(
                this.orangeInvariant.Object,
                this.bananaCovariant.Object,
                this.fruitContravariant.Object);

            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                varianceExample.Run(CancellationToken.None);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("Running invariance example."));
            Assert.That(outputString, Does.Contain("Running covariance example."));
            Assert.That(outputString, Does.Contain("Running contravariance example."));
        }
    }
}
