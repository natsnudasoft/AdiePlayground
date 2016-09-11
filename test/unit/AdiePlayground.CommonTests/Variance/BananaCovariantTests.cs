// <copyright file="BananaCovariantTests.cs" company="natsnudasoft">
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
    using Common.Variance;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="BananaCovariant"/> class.
    /// </summary>
    [TestFixture]
    public sealed class BananaCovariantTests
    {
        /// <summary>
        /// Tests the create method with valid input.
        /// </summary>
        [Test]
        public void Create_CreatesValidInstance()
        {
            const int FruitQuality = 30;
            var bananaCovariant = new BananaCovariant();

            var banana = bananaCovariant.Create(FruitQuality);

            Assert.That(banana.Quality, Is.EqualTo(FruitQuality));
        }
    }
}
