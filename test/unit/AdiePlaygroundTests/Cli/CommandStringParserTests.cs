// <copyright file="CommandStringParserTests.cs" company="natsnudasoft">
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
    public sealed class CommandStringParserTests
    {
        private const string ParseCommandStringParam = "commandString";

        [Test]
        public void Parse_NullCommandString_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => CommandStringParser.Parse(null));
            Assert.That(ex.ParamName, Is.EqualTo(ParseCommandStringParam));
        }

        [Test]
        public void Parse_CommandString_ParsedCommandNameZeroArgs()
        {
            var parsedCommand = CommandStringParser.Parse("command");

            Assert.That(parsedCommand, Is.Not.Null);
            Assert.That(parsedCommand.Name, Is.EqualTo("command"));
            Assert.That(parsedCommand.Arguments, Is.Empty);
        }

        [Test]
        public void Parse_CommandString_ParsedCommandNameCorrectArgs()
        {
            var parsedCommand = CommandStringParser.Parse("command Arg0 Arg1");

            Assert.That(parsedCommand, Is.Not.Null);
            Assert.That(parsedCommand.Name, Is.EqualTo("command"));
            Assert.That(parsedCommand.Arguments, Is.EqualTo(new[] { "Arg0", "Arg1" }));
        }
    }
}
