// <copyright file="FruitContravarianceTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Variance
{
    using System;
    using AdiePlayground.Common.Variance;
    using Model;
    using NUnit.Framework;

    [TestFixture]
    public sealed class FruitContravarianceTests
    {
        private const string GetValueInputParam = "input";

        [Test]
        public void GetValue_NullInput_ArgumentNullException()
        {
            var fruitContravariant = new FruitContravariant();

            var ex = Assert.Throws<ArgumentNullException>(() => fruitContravariant.GetValue(null));
            Assert.That(ex.ParamName, Is.EqualTo(GetValueInputParam));
        }

        [Test]
        public void GetValue_ReturnsCorrectValue()
        {
            const int FruitQuality = 30;
            var fruit = new FruitStub(FruitQuality);
            var fruitContravariant = new FruitContravariant();

            var value = fruitContravariant.GetValue(fruit);

            Assert.That(value, Is.EqualTo(FruitQuality));
        }
    }
}
