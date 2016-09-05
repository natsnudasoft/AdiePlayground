// <copyright file="TestEntity.cs" company="natsnudasoft">
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
    using Data.Model;

    /// <summary>
    /// Provides a test entity.
    /// </summary>
    /// <seealso cref="IModelEntity" />
    public sealed class TestEntity : IModelEntity, IEquatable<TestEntity>
    {
        private static readonly byte[] EmptyRowVersion = new byte[] { };

        /// <inheritdoc/>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the test property1.
        /// </summary>
        public string Property1 { get; set; }

        /// <summary>
        /// Gets or sets the test property2.
        /// </summary>
        public DateTime Property2 { get; set; }

        /// <inheritdoc/>
        public byte[] RowVersion => EmptyRowVersion;

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as TestEntity);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <inheritdoc/>
        public bool Equals(TestEntity other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Id == other.Id &&
                this.Property1 == other.Property1 &&
                this.Property2 == other.Property2 &&
                this.RowVersion == other.RowVersion;
        }
    }
}
