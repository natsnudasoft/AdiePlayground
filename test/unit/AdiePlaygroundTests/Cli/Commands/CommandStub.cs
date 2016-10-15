// <copyright file="CommandStub.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Cli.Commands
{
    using System.ComponentModel;
    using System.Threading;
    using AdiePlayground.Cli;
    using AdiePlayground.Cli.Metadata;
    using Convert;
    using Properties;

    public sealed class CommandStub : ICommand
    {
        [CommandParameter(0, "valid", typeof(Resources), "TestResource")]
        public string StringProperty { get; set; }

        [TypeConverter(typeof(TypeConverterStub))]
        public int TypeConverterProperty { get; set; }

        [TypeConverter(typeof(TypeConverter))]
        public int TypeConverterCannotConvertProperty { get; set; }

        public ImplicitOperatorStub ImplicitOperatorStubProperty { get; set; }

        public bool CannotResolveProperty { get; set; }

        public void Execute(CancellationToken cancellationToken)
        {
        }
    }
}
