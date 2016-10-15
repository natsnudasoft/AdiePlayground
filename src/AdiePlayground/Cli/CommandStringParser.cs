// <copyright file="CommandStringParser.cs" company="natsnudasoft">
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

    /// <summary>
    /// Provides members to parse a command string and create instances of
    /// <see cref="ParsedCommand"/>.
    /// </summary>
    internal static class CommandStringParser
    {
        /// <summary>
        /// Parses the specified command string and creates an instance of
        /// <see cref="ParsedCommand"/> from the parsed string.
        /// </summary>
        /// <param name="commandString">The command string to parse.</param>
        /// <returns>A <see cref="ParsedCommand"/> created from the parsed command string.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="commandString"/> is
        /// <see langword="null"/>.</exception>
        public static ParsedCommand Parse(string commandString)
        {
            if (commandString == null)
            {
                throw new ArgumentNullException(nameof(commandString));
            }

            var commandTokens = commandString.Trim().Split(' ');
            var commandName = commandTokens[0];
            var commandArguments = new string[commandTokens.Length - 1];
            Array.Copy(commandTokens, 1, commandArguments, 0, commandArguments.Length);
            return new ParsedCommand(commandName, commandArguments);
        }
    }
}
