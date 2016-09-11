﻿// <copyright file="OrangeTests.cs" company="natsnudasoft">
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
    using Common.Model;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="Orange"/> class.
    /// </summary>
    [TestFixture]
    public sealed class OrangeTests
    {
        private const string ConstructorQualityParam = "quality";

        /// <summary>
        /// Tests the constructor with an invalid quality.
        /// </summary>
        /// <param name="fruitQuality">The fruit quality.</param>
        [Test]
        public void Constructor_InvalidQuality_ArgumentException(
            [Values(int.MinValue, int.MaxValue)] int fruitQuality)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Orange(fruitQuality));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorQualityParam));
        }

        /// <summary>
        /// Tests the constructor with a valid quality.
        /// </summary>
        [Test]
        public void Constructor_ValidQuality_CorrectProperties()
        {
            const int FruitQuality = 95;
            const string FruitName = nameof(Orange);
#pragma warning disable CC0021 // Use nameof
            const string FruitColor = "Orange";
#pragma warning restore CC0021 // Use nameof
            var orange = new Orange(FruitQuality);

            Assert.That(orange.Quality, Is.EqualTo(FruitQuality));
            Assert.That(orange.Name, Is.EqualTo(FruitName));
            Assert.That(orange.Color, Is.EqualTo(FruitColor));
        }
    }
}