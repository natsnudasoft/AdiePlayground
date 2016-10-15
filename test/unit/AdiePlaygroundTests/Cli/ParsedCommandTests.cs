// <copyright file="ParsedCommandTests.cs" company="natsnudasoft">
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
    using System;
    using AdiePlayground.Cli;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ParsedCommandTests
    {
        private const string ConstructorNameParam = "name";
        private const string ConstructorArgumentsParam = "arguments";

        [Test]
        public void Constructor_NullName_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ParsedCommand(null, new[] { "Arg0", "Arg1" }));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorNameParam));
        }

        [Test]
        public void Constructor_NullArguments_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ParsedCommand("CommandName", null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorArgumentsParam));
        }

        [Test]
        public void Constructor_SetsValues()
        {
            var parsedCommand = new ParsedCommand("CommandName", new[] { "Arg0", "Arg1" });
            Assert.That(parsedCommand.Name, Is.EqualTo("CommandName"));
            Assert.That(parsedCommand.Arguments, Is.EqualTo(new[] { "Arg0", "Arg1" }));
        }
    }
}
