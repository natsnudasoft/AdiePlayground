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

    public sealed class TestEntity : IModelEntity, IEquatable<TestEntity>
    {
        private static readonly byte[] EmptyRowVersion = new byte[] { };

        public int Id { get; set; }

        public string Property1 { get; set; }

        public DateTime Property2 { get; set; }

        public byte[] RowVersion => EmptyRowVersion;

        public override bool Equals(object obj)
        {
            return this.Equals(obj as TestEntity);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

#pragma warning disable MEN007 // Use a single return
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
#pragma warning restore MEN007 // Use a single return
    }
}
