// <copyright file="CommandGroupMetadataFactory.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli
{
    using System.Collections.Generic;
    using Metadata;

    /// <summary>
    /// Encapsulates a method that will retrieve all instances of <see cref="CommandMetadata"/> with
    /// a specified command group name.
    /// </summary>
    /// <param name="groupName">The name of the group to search for instances of
    /// <see cref="CommandMetadata"/>.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of the <see cref="CommandMetadata"/> found.
    /// </returns>
    internal delegate IEnumerable<CommandMetadata> CommandGroupMetadataFactory(string groupName);
}
