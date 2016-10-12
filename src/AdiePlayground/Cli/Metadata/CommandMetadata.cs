// <copyright file="CommandMetadata.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli.Metadata
{
    using System.Collections.Generic;

    /// <summary>
    /// Describes the details of an <see cref="ICommand"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    internal sealed class CommandMetadata
    {
        /// <summary>
        /// Gets or sets the name of the group that an <see cref="ICommand"/> described by this
        /// <see cref="CommandMetadata"/> is part of.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the name of an <see cref="ICommand"/> described by this
        /// <see cref="CommandMetadata"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the help text of an <see cref="ICommand"/> described by this
        /// <see cref="CommandMetadata"/>.
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// Gets or sets the metadata of the parameters of an <see cref="ICommand"/> described by
        /// this <see cref="CommandMetadata"/>.
        /// </summary>
        public IList<CommandParameterMetadata> ParametersMetadata { get; set; }
    }
}
