// <copyright file="CommandNotFoundExceptionTests.cs" company="natsnudasoft">
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
    public sealed class CommandNotFoundExceptionTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new CommandNotFoundException());
        }

        [Test]
        public void ConstructorMessage_SetsValues()
        {
            const string message = "This is a message.";
            var commandNotFoundException = new CommandNotFoundException(message);
            Assert.That(commandNotFoundException.Message, Is.EqualTo(message));
        }

        [Test]
        public void ConstructorMessageInnerException_SetsValues()
        {
            const string message = "This is another message.";
            var innerException = new IOException();
            var commandNotFoundException = new CommandNotFoundException(message, innerException);
            Assert.That(commandNotFoundException.Message, Is.EqualTo(message));
            Assert.That(commandNotFoundException.InnerException, Is.EqualTo(innerException));
        }

        [Test]
        public void ConstructorCommandNameCommandArgs_SetsValues()
        {
            const string commandName = "This is yet another message.";
            var commandArgs = new[] { "Arg0", "Arg1" };
            var commandNotFoundException = new CommandNotFoundException(commandName, commandArgs);
            Assert.That(commandNotFoundException.Message, Is.EqualTo("Invalid command name."));
            Assert.That(commandNotFoundException.CommandName, Is.EqualTo(commandName));
            Assert.That(commandNotFoundException.CommandArgs, Is.EqualTo(commandArgs));
        }

        [Test]
        public void ConstructorCommandNameCommandArgsInnerException_SetsValues()
        {
            const string commandName = "This is just another message.";
            var commandArgs = new[] { "Arg0", "Arg1", "Arg2" };
            var innerException = new IOException();
            var commandNotFoundException =
                new CommandNotFoundException(commandName, commandArgs, innerException);
            Assert.That(commandNotFoundException.Message, Is.EqualTo("Invalid command name."));
            Assert.That(commandNotFoundException.CommandName, Is.EqualTo(commandName));
            Assert.That(commandNotFoundException.CommandArgs, Is.EqualTo(commandArgs));
            Assert.That(commandNotFoundException.InnerException, Is.EqualTo(innerException));
        }

        [Test]
        public void Serialization_Successful()
        {
            const string commandName = "This is yet again another message!";
            var commandArgs = new[] { "Arg0", "Arg1", "Arg2", "Arg3" };
            var innerException = new IOException();
            var commandNotFoundExceptionOriginal =
                new CommandNotFoundException(commandName, commandArgs, innerException);
            CommandNotFoundException commandNotFoundException;

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, commandNotFoundExceptionOriginal);
                memoryStream.Position = 0;
                commandNotFoundException = (CommandNotFoundException)binaryFormatter
                    .Deserialize(memoryStream);
            }

            Assert.That(commandNotFoundException.Message, Is.EqualTo("Invalid command name."));
            Assert.That(commandNotFoundException.CommandName, Is.EqualTo(commandName));
            Assert.That(commandNotFoundException.CommandArgs, Is.EqualTo(commandArgs));
            Assert.That(commandNotFoundException.InnerException, Is.TypeOf<IOException>());
        }
    }
}
