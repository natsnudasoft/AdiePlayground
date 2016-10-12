// <copyright file="CommandFactory.cs" company="natsnudasoft">
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
    using Autofac.Features.Metadata;
    using Metadata;

    /// <summary>
    /// Encapsulates a method that will create instances of <see cref="Meta{T, TMetadata}"/> for an
    /// <see cref="ICommand"/> with <see cref="CommandMetadata"/> from a specified command group and
    /// command name.
    /// </summary>
    /// <param name="groupName">The name of the group the <see cref="ICommand"/> to create a
    /// <see cref="Meta{T, TMetadata}"/> instance of is part of.
    /// </param>
    /// <param name="commandName">The name of the <see cref="ICommand"/> to create a
    /// <see cref="Meta{T, TMetadata}"/> instance of.
    /// </param>
    /// <returns>The <see cref="Meta{T, TMetadata}"/> instance that was created.</returns>
    internal delegate Meta<ICommand, CommandMetadata> CommandFactory(
        string groupName,
        string commandName);
}
