// <copyright file="CommandResolver.cs" company="natsnudasoft">
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
    using Autofac.Core;
    using Autofac.Core.Registration;
    using Autofac.Features.Metadata;
    using Metadata;

    /// <summary>
    /// Resolves instances of <see cref="ICommand"/> from a specified command group and
    /// <see cref="ParsedCommand"/>.
    /// </summary>
    internal sealed class CommandResolver
    {
        private readonly CommandFactory commandFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResolver"/> class.
        /// </summary>
        /// <param name="commandFactory">The <see cref="CommandFactory"/> to use to create
        /// instances of <see cref="ICommand"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="commandFactory"/> is
        /// <c>null</c>.</exception>
        public CommandResolver(CommandFactory commandFactory)
        {
            if (commandFactory == null)
            {
                throw new ArgumentNullException(nameof(commandFactory));
            }

            this.commandFactory = commandFactory;
        }

        /// <summary>
        /// Resolves an instance of <see cref="ICommand"/> from the specified command group and
        /// <see cref="ParsedCommand"/>.
        /// </summary>
        /// <param name="commandGroup">The command group name the <see cref="ICommand"/> to resolve
        /// is part of.</param>
        /// <param name="parsedCommand">The <see cref="ParsedCommand"/> that describes the
        /// <see cref="ICommand"/> to resolve.</param>
        /// <returns>The created instance of <see cref="ICommand"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="commandGroup"/>, or
        /// <paramref name="parsedCommand"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="commandGroup"/> is empty.
        /// </exception>
        /// <exception cref="CommandResolveException">The arguments of
        /// <paramref name="parsedCommand"/> could not be resolved.</exception>
        /// <exception cref="CommandNotFoundException">An <see cref="ICommand"/> described by
        /// <paramref name="parsedCommand"/> could not be found.</exception>
        public ICommand Resolve(
            string commandGroup,
            ParsedCommand parsedCommand)
        {
            if (commandGroup == null)
            {
                throw new ArgumentNullException(nameof(commandGroup));
            }

            if (commandGroup.Length == 0)
            {
                throw new ArgumentException("Value must not be empty.", nameof(commandGroup));
            }

            if (parsedCommand == null)
            {
                throw new ArgumentNullException(nameof(parsedCommand));
            }

            var commandMeta = this.ResolveCommandMeta(commandGroup, parsedCommand);
            MatchArguments(commandMeta, parsedCommand);
            return commandMeta.Value;
        }

        private static void MatchArguments(
            Meta<ICommand, CommandMetadata> commandMeta,
            ParsedCommand parsedCommand)
        {
            var parametersMetadata = commandMeta.Metadata.ParametersMetadata;
            var arguments = parsedCommand.Arguments;
            if (arguments.Count > parametersMetadata.Count)
            {
                throw new CommandResolveException(parsedCommand.Name, arguments);
            }

            for (int i = 0; i < parametersMetadata.Count; ++i)
            {
                var parameterMetadata = parametersMetadata[i];
                if (parameterMetadata.Required && i >= arguments.Count)
                {
                    throw new CommandResolveException(parsedCommand.Name, arguments);
                }

                var argumentValue = i < arguments.Count ?
                    parameterMetadata.Converter.Convert(arguments[i]) :
                    parameterMetadata.DefaultValue;
                parameterMetadata.PropertyInfo.SetValue(commandMeta.Value, argumentValue);
            }
        }

        private Meta<ICommand, CommandMetadata> ResolveCommandMeta(
            string commandGroup,
            ParsedCommand parsedCommand)
        {
            try
            {
                return this.commandFactory(
                    commandGroup,
                    parsedCommand.Name);
            }
            catch (ComponentNotRegisteredException ex)
            {
                throw new CommandNotFoundException(
                    parsedCommand.Name,
                    parsedCommand.Arguments,
                    ex);
            }
            catch (DependencyResolutionException ex)
            {
                throw new CommandResolveException(
                    parsedCommand.Name,
                    parsedCommand.Arguments,
                    ex);
            }
        }
    }
}
