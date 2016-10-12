// <copyright file="ParsedCommand.cs" company="natsnudasoft">
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
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Describes the details of a command that has been parsed.
    /// </summary>
    internal sealed class ParsedCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedCommand"/> class.
        /// </summary>
        /// <param name="name">The name of the command described by this
        /// <see cref="ParsedCommand"/>.</param>
        /// <param name="arguments">The arguments of the command described by this
        /// <see cref="ParsedCommand"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>, or
        /// <paramref name="arguments"/> is <c>null</c>.</exception>
        public ParsedCommand(string name, IEnumerable<string> arguments)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            this.Name = name;
            this.Arguments = new ReadOnlyCollection<string>(arguments.ToArray());
        }

        /// <summary>
        /// Gets the name of the command described by this <see cref="ParsedCommand"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the arguments of the command described by this <see cref="ParsedCommand"/>.
        /// </summary>
        public IReadOnlyList<string> Arguments { get; }
    }
}
