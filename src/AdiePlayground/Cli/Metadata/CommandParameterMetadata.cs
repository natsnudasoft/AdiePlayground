// <copyright file="CommandParameterMetadata.cs" company="natsnudasoft">
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
    using System.Reflection;
    using Convert;

    /// <summary>
    /// Describes the details of a parameter on an <see cref="ICommand"/>.
    /// </summary>
    internal sealed class CommandParameterMetadata
    {
        /// <summary>
        /// Gets or sets the index of the parameter on an <see cref="ICommand"/> described by this
        /// <see cref="CommandParameterMetadata"/>.
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Gets or sets the name of the parameter on an <see cref="ICommand"/> described by this
        /// <see cref="CommandParameterMetadata"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the help text of the parameter on an <see cref="ICommand"/> described by
        /// this <see cref="CommandParameterMetadata"/>.
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the parameter on an <see cref="ICommand"/>
        /// described by this <see cref="CommandParameterMetadata"/> is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets the default of the parameter on an <see cref="ICommand"/> described by this
        /// <see cref="CommandParameterMetadata"/>.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IArgumentConverter"/> used to convert arguments for an
        /// <see cref="ICommand"/> described by this <see cref="CommandParameterMetadata"/>.
        /// </summary>
        public IArgumentConverter Converter { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Reflection.PropertyInfo"/> of the target of this
        /// <see cref="CommandParameterMetadata"/>.
        /// </summary>
        public PropertyInfo PropertyInfo { get; set; }
    }
}
