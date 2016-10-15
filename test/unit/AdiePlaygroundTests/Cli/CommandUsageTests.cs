// <copyright file="CommandUsageTests.cs" company="natsnudasoft">
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
    using System.Globalization;
    using System.IO;
    using AdiePlayground.Cli;
    using AdiePlayground.Cli.Metadata;
    using Metadata;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandUsageTests
    {
#pragma warning disable CC0021 // Use nameof
        private const string WriteCommandHelpCommandMetadataParam = "commandMetadata";
#pragma warning restore CC0021 // Use nameof

        private CommandMetadata commandMetadata;

        [SetUp]
        public void BeforeTest()
        {
            this.commandMetadata = CommandMetadataHelper.GetCommandMetadata();
        }

        [Test]
        public void DefaultCommandGroup_Correct()
        {
            Assert.That(CommandUsage.DefaultCommandGroup, Is.EqualTo("Playground"));
        }

        [Test]
        public void WriteCommandHelp_NullCommandMetadata_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => CommandUsage.WriteCommandHelp(null));
            Assert.That(ex.ParamName, Is.EqualTo(WriteCommandHelpCommandMetadataParam));
        }

        [Test]
        public void WriteCommandHelp_WritesCommandHelp()
        {
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                CommandUsage.WriteCommandHelp(this.commandMetadata);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("TestCommand"));
            Assert.That(outputString, Does.Contain("This is a test command."));
            Assert.That(outputString, Does.Contain("Arg0"));
            Assert.That(outputString, Does.Contain("Arg1"));
            Assert.That(outputString, Does.Contain("This is Arg0 help text."));
            Assert.That(outputString, Does.Contain("This is Arg1 help text."));
        }

        [Test]
        public void WriteCommandUsage_NullCommandMetadata_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => CommandUsage.WriteCommandUsage(null));
            Assert.That(ex.ParamName, Is.EqualTo(WriteCommandHelpCommandMetadataParam));
        }

        [Test]
        public void WriteCommandUsage_WritesUsage()
        {
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                CommandUsage.WriteCommandUsage(this.commandMetadata);

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(outputString, Does.Contain("TestCommand"));
            Assert.That(outputString, Does.Not.Contain("This is a test command."));
            Assert.That(outputString, Does.Contain("Arg0"));
            Assert.That(outputString, Does.Contain("Arg1"));
            Assert.That(outputString, Does.Not.Contain("This is Arg0 help text."));
            Assert.That(outputString, Does.Not.Contain("This is Arg1 help text."));
        }

        [Test]
        public void WriteCommandResolveFailed_WritesFailed()
        {
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                CommandUsage.WriteCommandResolveFailed("TestCommand");

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(
                outputString,
                Is.EqualTo("Invalid arguments on command 'TestCommand'." + Environment.NewLine));
        }

        [Test]
        public void WriteCommandNotFound_WritesNotFound()
        {
            string outputString;
            using (var newOut = new StringWriter(CultureInfo.InvariantCulture))
            {
                var previousOut = Console.Out;
                Console.SetOut(newOut);

                CommandUsage.WriteCommandNotFound("TestCommand");

                Console.SetOut(previousOut);
                outputString = newOut.ToString();
            }

            Assert.That(
                outputString,
                Is.EqualTo("Invalid command 'TestCommand'." + Environment.NewLine));
        }
    }
}
