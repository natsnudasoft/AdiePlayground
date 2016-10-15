// <copyright file="CommandFactoryHelper.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Cli
{
    using System.Threading;
    using AdiePlayground.Cli;
    using AdiePlayground.Cli.Metadata;
    using Autofac.Features.Metadata;
    using Moq;

    public static class CommandFactoryHelper
    {
        internal static Meta<ICommand, CommandMetadata> CreateExitCommandMeta(
            CommandLoop commandLoop)
        {
            var exitCommandMock = new Mock<ICommand>();
            exitCommandMock
                .Setup(c => c.Execute(It.IsAny<CancellationToken>()))
                .Callback(commandLoop.Exit);
            var exitCommandMetadata = new CommandMetadata
            {
                Group = "CommandGroup",
                Name = "exit",
                HelpText = "Exit help text.",
                ParametersMetadata = new CommandParameterMetadata[0]
            };
            var exitCommandMeta = new Meta<ICommand, CommandMetadata>(
                exitCommandMock.Object,
                exitCommandMetadata);
            return exitCommandMeta;
        }
    }
}
