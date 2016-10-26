// <copyright file="LimeTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Common.Model;
    using NUnit.Framework;

    [TestFixture]
    public sealed class LimeTests
    {
        private const string ConstructorQualityParam = "quality";

        [Test]
        public void Constructor_InvalidQuality_ArgumentOutOfRangeException(
            [Values(int.MinValue, int.MaxValue)] int fruitQuality)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Lime(fruitQuality));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorQualityParam));
        }

        [Test]
        public void Constructor_ValidQuality_CorrectProperties()
        {
            const int FruitQuality = 85;
            const string FruitName = nameof(Lime);
            const string FruitColor = "Green";
            var lime = new Lime(FruitQuality);

            Assert.That(lime.Quality, Is.EqualTo(FruitQuality));
            Assert.That(lime.Name, Is.EqualTo(FruitName));
            Assert.That(lime.Color, Is.EqualTo(FruitColor));
        }
    }
}
