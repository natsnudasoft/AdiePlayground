// <copyright file="IncludeCriterionTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.DataTests.Services
{
    using System;
    using System.Linq;
    using Data.Services;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="IncludeCriterion{TEntity}"/> class.
    /// </summary>
    [TestFixture]
    public sealed class IncludeCriterionTests
    {
        private const string ConstructorIncludePropertySelectorParam = "includePropertySelector";

        /// <summary>
        /// Tests the constructor with an invalid include property selector.
        /// </summary>
        [Test]
        public void Constructor_NullIncludePropertySelector_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new IncludeCriterion<TestEntity>(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorIncludePropertySelectorParam));
        }

        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            IncludeCriterion<TestEntity> criterion = null;
            Assert.DoesNotThrow(
                () => criterion = new IncludeCriterion<TestEntity>(e => e.Property2));
            Assert.That(criterion.IncludePropertySelector, Is.Not.Null);
        }

        /// <summary>
        /// Tests the apply method.
        /// </summary>
        [Test]
        public void Apply_DoesNotThrow()
        {
            var queryData = TestData.DeepCopyTestEntityData().AsQueryable();
            var criterion = new IncludeCriterion<TestEntity>(e => e.Property2);

            Assert.DoesNotThrow(() => criterion.Apply(queryData));

            // We cannot mock extension methods so cannot verify which query method was called.
        }
    }
}
