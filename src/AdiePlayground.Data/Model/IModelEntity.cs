﻿// <copyright file="IModelEntity.cs" company="natsnudasoft">
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

namespace AdiePlayground.Data.Model
{
    /// <summary>
    /// Provides a standard interface for entities in a database.
    /// </summary>
    public interface IModelEntity
    {
        /// <summary>
        /// Gets the unique database id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the RowVersion used for optimistic concurrency.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1819:PropertiesShouldNotReturnArrays",
            Justification = "Entity Framework requires byte[].")]
        byte[] RowVersion { get; }
    }
}
