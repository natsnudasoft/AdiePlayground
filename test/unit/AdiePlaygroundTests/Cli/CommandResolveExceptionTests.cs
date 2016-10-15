// <copyright file="CommandResolveExceptionTests.cs" company="natsnudasoft">
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
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using AdiePlayground.Cli;
    using NUnit.Framework;

    [TestFixture]
    public sealed class CommandResolveExceptionTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new CommandResolveException());
        }

        [Test]
        public void ConstructorMessage_SetsValues()
        {
            const string message = "This is a message.";
            var commandResolveException = new CommandResolveException(message);
            Assert.That(commandResolveException.Message, Is.EqualTo(message));
        }

        [Test]
        public void ConstructorMessageInnerException_SetsValues()
        {
            const string message = "This is another message.";
            var innerException = new IOException();
            var commandNotFoundException = new CommandResolveException(message, innerException);
            Assert.That(commandNotFoundException.Message, Is.EqualTo(message));
            Assert.That(commandNotFoundException.InnerException, Is.EqualTo(innerException));
        }

        [Test]
        public void ConstructorCommandNameCommandArgs_SetsValues()
        {
            const string commandName = "This is yet another message.";
            var commandArgs = new[] { "Arg0", "Arg1" };
            var commandResolveException = new CommandResolveException(commandName, commandArgs);
            Assert.That(
                commandResolveException.Message,
                Is.EqualTo("Command could not be resolved."));
            Assert.That(commandResolveException.CommandName, Is.EqualTo(commandName));
            Assert.That(commandResolveException.CommandArgs, Is.EqualTo(commandArgs));
        }

        [Test]
        public void ConstructorCommandNameCommandArgsInnerException_SetsValues()
        {
            const string commandName = "This is just another message.";
            var commandArgs = new[] { "Arg0", "Arg1", "Arg2" };
            var innerException = new IOException();
            var commandResolveException =
                new CommandResolveException(commandName, commandArgs, innerException);
            Assert.That(
                commandResolveException.Message,
                Is.EqualTo("Command could not be resolved."));
            Assert.That(commandResolveException.CommandName, Is.EqualTo(commandName));
            Assert.That(commandResolveException.CommandArgs, Is.EqualTo(commandArgs));
            Assert.That(commandResolveException.InnerException, Is.EqualTo(innerException));
        }

        [Test]
        public void Serialization_Successful()
        {
            const string commandName = "This is yet again another message!";
            var commandArgs = new[] { "Arg0", "Arg1", "Arg2", "Arg3" };
            var innerException = new IOException();
            var commandResolveExceptionOriginal =
                new CommandResolveException(commandName, commandArgs, innerException);
            CommandResolveException commandResolveException;

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, commandResolveExceptionOriginal);
                memoryStream.Position = 0;
                commandResolveException = (CommandResolveException)binaryFormatter
                    .Deserialize(memoryStream);
            }

            Assert.That(
                commandResolveException.Message,
                Is.EqualTo("Command could not be resolved."));
            Assert.That(commandResolveException.CommandName, Is.EqualTo(commandName));
            Assert.That(commandResolveException.CommandArgs, Is.EqualTo(commandArgs));
            Assert.That(commandResolveException.InnerException, Is.TypeOf<IOException>());
        }
    }
}
