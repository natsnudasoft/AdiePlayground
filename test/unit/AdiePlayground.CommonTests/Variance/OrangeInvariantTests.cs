// <copyright file="OrangeInvariantTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Variance
{
    using System;
    using Common.Model;
    using Common.Variance;
    using NUnit.Framework;

    [TestFixture]
    public sealed class OrangeInvariantTests
    {
        private const string GetValueInputParam = "input";

        [Test]
        public void GetValue_NullInput_ArgumentNullException()
        {
            var orangeInvariant = new OrangeInvariant();

            var ex = Assert.Throws<ArgumentNullException>(() => orangeInvariant.GetValue(null));
            Assert.That(ex.ParamName, Is.EqualTo(GetValueInputParam));
        }

        [Test]
        public void GetValue_ReturnsCorrectValue()
        {
            const int FruitQuality = 65;
            var orange = new Orange(FruitQuality);
            var orangeInvariant = new OrangeInvariant();

            var value = orangeInvariant.GetValue(orange);

            Assert.That(value, Is.EqualTo(FruitQuality));
        }
    }
}
