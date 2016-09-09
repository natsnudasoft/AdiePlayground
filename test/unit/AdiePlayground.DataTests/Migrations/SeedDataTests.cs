// <copyright file="SeedDataTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.DataTests.Migrations
{
    using Data.Migrations;
    using Data.Model;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="SeedData"/> class.
    /// </summary>
    [TestFixture]
    public sealed class SeedDataTests
    {
        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        public void Constructor_LoadsSeedData()
        {
            SeedData seedData = null;

            Assert.DoesNotThrow(() => seedData = new SeedData());

            Assert.That(seedData?.Courses, Is.Not.Null);
            Assert.That(seedData?.Students, Is.Not.Null);
            Assert.That(
                seedData.Courses,
                Has.Some.Matches<UniversityCourse>(c => c.Title == "Alien Computer Science"));
            Assert.That(
                seedData.Students,
                Has.Some.Matches<UniversityStudent>(s => s.FullName == "Urielle Owens"));
        }
    }
}
