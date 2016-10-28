// <copyright file="AppleTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Model
{
    using System;
    using System.Collections.Generic;
    using AdiePlayground.Common.Model;
    using NUnit.Framework;

    [TestFixture]
    public sealed class AppleTests
    {
        private const string ConstructorColorParam = "color";
        private const string ConstructorQualityParam = "quality";

        private static readonly IEnumerable<KeyValuePair<AppleColor, string>> ValidColorMappings =
            new[]
            {
                new KeyValuePair<AppleColor, string>(AppleColor.Green, "Green"),
                new KeyValuePair<AppleColor, string>(AppleColor.Red, "Red")
            };

        [Test]
        public void Constructor_InvalidQuality_ArgumentOutOfRangeException(
            [Values(int.MinValue, int.MaxValue)] int fruitQuality)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new Apple(AppleColor.Green, fruitQuality));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorQualityParam));
        }

        [Test]
        public void Constructor_InvalidColor_ArgumentOutOfRangeException()
        {
            const int FruitQuality = 20;
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new Apple((AppleColor)int.MinValue, FruitQuality));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorColorParam));
        }

        [Test]
        public void Constructor_ValidQualityAndColor_CorrectProperties(
            [ValueSource(nameof(ValidColorMappings))] KeyValuePair<AppleColor, string> color)
        {
            const int FruitQuality = 15;
            const string FruitName = nameof(Apple);
            var expectedFruitColor = color.Value;
            var apple = new Apple(color.Key, FruitQuality);

            Assert.That(apple.Quality, Is.EqualTo(FruitQuality));
            Assert.That(apple.Name, Is.EqualTo(FruitName));
            Assert.That(apple.Color, Is.EqualTo(expectedFruitColor));
        }
    }
}
