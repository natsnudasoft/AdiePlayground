// <copyright file="TestData.cs" company="natsnudasoft">
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

namespace AdiePlayground.DataTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides test data.
    /// </summary>
    public static class TestData
    {
        private static readonly IList<TestEntity> TestEntityDataInternal = new List<TestEntity>
        {
            new TestEntity
            {
                Id = 0,
                Property1 = "Elephant one.",
                Property2 = new DateTime(1987, 7, 20)
            },
            new TestEntity
            {
                Id = 1,
                Property1 = "Elephant three.",
                Property2 = new DateTime(1947, 1, 30)
            },
            new TestEntity
            {
                Id = 2,
                Property1 = "Kangaroo one.",
                Property2 = new DateTime(1996, 5, 2)
            },
            new TestEntity
            {
                Id = 3,
                Property1 = "Giraffe three.",
                Property2 = new DateTime(2008, 10, 13)
            },
            new TestEntity
            {
                Id = 4,
                Property1 = "Elephant two.",
                Property2 = new DateTime(2006, 4, 11)
            },
            new TestEntity
            {
                Id = 5,
                Property1 = "Giraffe one.",
                Property2 = new DateTime(2014, 3, 24)
            },
            new TestEntity
            {
                Id = 6,
                Property1 = "Giraffe two.",
                Property2 = new DateTime(1960, 1, 28)
            },
            new TestEntity
            {
                Id = 7,
                Property1 = "Kangaroo two.",
                Property2 = new DateTime(1972, 7, 18)
            },
            new TestEntity
            {
                Id = 8,
                Property1 = "Panda two.",
                Property2 = new DateTime(1994, 8, 19)
            },
            new TestEntity
            {
                Id = 9,
                Property1 = "Panda one.",
                Property2 = new DateTime(1999, 11, 23)
            },
        };

        /// <summary>
        /// Gets the default test entity data.
        /// </summary>
        public static IList<TestEntity> TestEntityData => TestEntityDataInternal;

        /// <summary>
        /// Creates a deep copy of the test entity data.
        /// </summary>
        /// <returns>A deep copy of the test entity data.</returns>
        public static IList<TestEntity> DeepCopyTestEntityData()
        {
            return TestEntityData.Select(e => new TestEntity
            {
                Id = e.Id,
                Property1 = e.Property1,
                Property2 = e.Property2
            }).ToArray();
        }
    }
}
