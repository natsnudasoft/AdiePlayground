// <copyright file="ArgumentConverterResolveExceptionTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Cli.Convert;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ArgumentConverterResolveExceptionTests
    {
        [Test]
        public void Constructor_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => new ArgumentConverterResolveException());
        }

        [Test]
        public void ConstructorMessage_SetsValues()
        {
            const string message = "This is a message.";
            var argumentConverterResolveException = new ArgumentConverterResolveException(message);
            Assert.That(argumentConverterResolveException.Message, Is.EqualTo(message));
        }

        [Test]
        public void ConstructorMessageInnerException_SetsValues()
        {
            const string message = "This is another message.";
            var innerException = new IOException();
            var argumentConverterResolveException =
                new ArgumentConverterResolveException(message, innerException);
            Assert.That(argumentConverterResolveException.Message, Is.EqualTo(message));
            Assert.That(
                argumentConverterResolveException.InnerException,
                Is.EqualTo(innerException));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = "TypeSets",
            Justification = "Not a compound word.")]
        [Test]
        public void ConstructorArgumentNameArgumentType_SetsValues()
        {
            const string argumentName = "Arg0";
            var argumentConverterResolveException =
                new ArgumentConverterResolveException(argumentName, typeof(double));
            Assert.That(
                argumentConverterResolveException.Message,
                Is.EqualTo("Could not resolve argument converter."));
            Assert.That(argumentConverterResolveException.ArgumentName, Is.EqualTo(argumentName));
            Assert.That(argumentConverterResolveException.ArgumentType, Is.EqualTo(typeof(double)));
        }

        [Test]
        public void ConstructorArgumentNameArgumentTypeInnerException_SetsValues()
        {
            const string argumentName = "Arg1";
            var innerException = new IOException();
            var argumentConverterResolveException =
                new ArgumentConverterResolveException(argumentName, typeof(double), innerException);
            Assert.That(
                argumentConverterResolveException.Message,
                Is.EqualTo("Could not resolve argument converter."));
            Assert.That(argumentConverterResolveException.ArgumentName, Is.EqualTo(argumentName));
            Assert.That(argumentConverterResolveException.ArgumentType, Is.EqualTo(typeof(double)));
            Assert.That(
                argumentConverterResolveException.InnerException,
                Is.EqualTo(innerException));
        }

        [Test]
        public void Serialization_Successful()
        {
            const string argumentName = "Arg2";
            var innerException = new IOException();
            var argumentConverterResolveExceptionOriginal =
                new ArgumentConverterResolveException(argumentName, typeof(double), innerException);
            ArgumentConverterResolveException argumentConverterResolveException;

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, argumentConverterResolveExceptionOriginal);
                memoryStream.Position = 0;
                argumentConverterResolveException =
                    (ArgumentConverterResolveException)binaryFormatter.Deserialize(memoryStream);
            }

            Assert.That(
                argumentConverterResolveException.Message,
                Is.EqualTo("Could not resolve argument converter."));
            Assert.That(argumentConverterResolveException.ArgumentName, Is.EqualTo(argumentName));
            Assert.That(argumentConverterResolveException.ArgumentType, Is.EqualTo(typeof(double)));
            Assert.That(argumentConverterResolveException.InnerException, Is.TypeOf<IOException>());
        }
    }
}
