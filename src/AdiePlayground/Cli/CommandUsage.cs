// <copyright file="CommandUsage.cs" company="natsnudasoft">
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
    using System.Globalization;
    using System.Linq;
    using Common.Extensions;
    using Metadata;
    using Properties;
    using static System.FormattableString;

    /// <summary>
    /// Provides members to output details on the usage of instances of <see cref="ICommand"/>.
    /// </summary>
    internal static class CommandUsage
    {
        /// <summary>
        /// Gets the default command group.
        /// </summary>
        public static string DefaultCommandGroup { get; } = "Playground";

        /// <summary>
        /// Displays help text for the specified <see cref="CommandMetadata"/>.
        /// </summary>
        /// <param name="commandMetadata">The <see cref="CommandMetadata"/> to display help text
        /// for.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandMetadata"/> is
        /// <c>null</c>.</exception>
        public static void WriteCommandHelp(CommandMetadata commandMetadata)
        {
            if (commandMetadata == null)
            {
                throw new ArgumentNullException(nameof(commandMetadata));
            }

            WriteCommandUsage(commandMetadata);
            var parametersSeperator = commandMetadata.ParametersMetadata.Count > 0 ?
                Environment.NewLine :
                string.Empty;
            Console.WriteLine(string.Format(
                CultureInfo.InvariantCulture,
                "  " + Resources.CommandHelpText,
                commandMetadata.HelpText + parametersSeperator,
                string.Join(Environment.NewLine, commandMetadata.ParametersMetadata
                    .Select(m => Invariant($"    {m.Name:,-15}    {m.HelpText}")))));
        }

        /// <summary>
        /// Displays details of command usage for the specified <see cref="CommandMetadata"/>.
        /// </summary>
        /// <param name="commandMetadata">The <see cref="CommandMetadata"/> to display command usage
        /// for.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandMetadata"/> is
        /// <c>null</c>.</exception>
        public static void WriteCommandUsage(CommandMetadata commandMetadata)
        {
            if (commandMetadata == null)
            {
                throw new ArgumentNullException(nameof(commandMetadata));
            }

            Console.WriteLine(string.Format(
                CultureInfo.InvariantCulture,
                Resources.CommandUsage,
                commandMetadata.Name,
                string.Join(" ", commandMetadata.ParametersMetadata
                    .Select(m => m.Required ?
                        Invariant($"<{m.Name}>") :
                        Invariant($"[<{m.Name}>]")))));
        }

        /// <summary>
        /// Displays error text for a command that could not have its arguments resolved.
        /// </summary>
        /// <param name="commandName">The name of the command that could not have its arguments
        /// resolved.</param>
        public static void WriteCommandResolveFailed(string commandName)
        {
            ConsoleExtensions.WriteColoredLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.CommandResolveFailed,
                    commandName),
                ConsoleColor.Red);
        }

        /// <summary>
        /// Displays error text for a command that could not be found.
        /// </summary>
        /// <param name="commandName">The name of the command that could not be found.</param>
        public static void WriteCommandNotFound(string commandName)
        {
            ConsoleExtensions.WriteColoredLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.CommandNotFound,
                    commandName),
                ConsoleColor.Red);
        }
    }
}
