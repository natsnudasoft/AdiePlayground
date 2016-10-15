// <copyright file="CommandMetadataHelper.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Cli.Metadata
{
    using AdiePlayground.Cli.Convert;
    using AdiePlayground.Cli.Metadata;
    using Commands;
    using Moq;

    public static class CommandMetadataHelper
    {
        internal static CommandMetadata GetCommandMetadata()
        {
            var converterMock = new Mock<IArgumentConverter>();
            converterMock.Setup(c => c.Convert(It.IsAny<string>())).Returns<string>(s => s);
            var propertyInfo =
                typeof(CommandStub).GetProperty(nameof(CommandStub.StringProperty));
            return new CommandMetadata
            {
                Group = "TestGroup",
                Name = "TestCommand",
                HelpText = "This is a test command.",
                ParametersMetadata = new[]
                {
                    new CommandParameterMetadata
                    {
                        Name = "Arg0",
                        HelpText = "This is Arg0 help text.",
                        Index = 0,
                        Required = true,
                        Converter = converterMock.Object,
                        PropertyInfo = propertyInfo
                    },
                    new CommandParameterMetadata
                    {
                        Name = "Arg1",
                        HelpText = "This is Arg1 help text.",
                        Index = 1,
                        Required = true,
                        Converter = converterMock.Object,
                        PropertyInfo = propertyInfo
                    },
                }
            };
        }
    }
}
