﻿// <copyright file="AppleTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Model
{
    using System;
    using System.Collections.Generic;
    using Common.Model;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="Apple"/> class.
    /// </summary>
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

        /// <summary>
        /// Tests the constructor with an invalid quality.
        /// </summary>
        /// <param name="fruitQuality">The fruit quality.</param>
        [Test]
        public void Constructor_InvalidQuality_ArgumentException(
            [Values(int.MinValue, int.MaxValue)] int fruitQuality)
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new Apple(AppleColor.Green, fruitQuality));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorQualityParam));
        }

        /// <summary>
        /// Tests the constructor with an invalid apple colour.
        /// </summary>
        [Test]
        public void Constructor_InvalidColor_ArgumentException()
        {
            const int FruitQuality = 20;
            var ex = Assert.Throws<ArgumentException>(
                () => new Apple((AppleColor)int.MinValue, FruitQuality));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorColorParam));
        }

        /// <summary>
        /// Tests the constructor with a valid quality.
        /// </summary>
        /// <param name="color">The colour of the apple.</param>
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
