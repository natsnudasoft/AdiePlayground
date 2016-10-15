// <copyright file="CommandParameterAttributeTests.cs" company="natsnudasoft">
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
    using System;
    using AdiePlayground.Cli.Convert;
    using AdiePlayground.Cli.Metadata;
    using Commands;
    using NUnit.Framework;
    using Properties;

    [TestFixture]
    public sealed class CommandParameterAttributeTests
    {
        private const string ConstructorIndexParam = "index";
        private const string ConstructorNameParam = "name";
        private const string ConstructorResourceTypeParam = "resourceType";
        private const string ConstructorHelpTextResourceNameParam = "helpTextResourceName";

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = "IndexLess",
            Justification = "Not a compound word")]
        [Test]
        public void Constructor_IndexLessThanZero_ArgumentOutOfRangeException()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => new CommandParameterAttribute(
                    int.MinValue,
                    "Arg0",
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorIndexParam));
        }

        [Test]
        public void Constructor_NullName_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandParameterAttribute(
                    0,
                    null,
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorNameParam));
        }

        [Test]
        public void Constructor_EmptyName_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new CommandParameterAttribute(
                    0,
                    string.Empty,
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorNameParam));
        }

        [Test]
        public void Constructor_SpacesInName_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new CommandParameterAttribute(
                    0,
                    "Arg 0",
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorNameParam));
        }

        [Test]
        public void Constructor_NullResourceType_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandParameterAttribute(
                    0,
                    "Arg0",
                    null,
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorResourceTypeParam));
        }

        [Test]
        public void Constructor_NullHelpTextResourceName_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandParameterAttribute(
                    0,
                    "Arg0",
                    typeof(Resources),
                    null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorHelpTextResourceNameParam));
        }

        [Test]
        public void Constructor_NullHelpTextResourceName_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new CommandParameterAttribute(
                    0,
                    "Arg0",
                    typeof(Resources),
                    string.Empty));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorHelpTextResourceNameParam));
        }

        [Test]
        public void Constructor_GetsValues()
        {
            var commandParameterAttribute = new CommandParameterAttribute(
                0,
                "Arg0",
                typeof(Resources),
                "TestResource")
            {
                Required = false,
                DefaultValue = "Default"
            };
            Assert.That(commandParameterAttribute.Index, Is.EqualTo(0));
            Assert.That(commandParameterAttribute.Name, Is.EqualTo("Arg0"));
            Assert.That(commandParameterAttribute.HelpText, Is.EqualTo("This is a test resource."));
            Assert.That(commandParameterAttribute.Required, Is.False);
            Assert.That(commandParameterAttribute.DefaultValue, Is.EqualTo("Default"));
        }

        [Test]
        public void GetMetadata_GetsMetadata()
        {
            var commandParameterAttribute = new CommandParameterAttribute(
                0,
                "Arg0",
                typeof(Resources),
                "TestResource")
            {
                Required = false,
                DefaultValue = "Default"
            };
            var targetProperty = typeof(CommandStub)
                .GetProperty(nameof(CommandStub.StringProperty));

            var commandParameterMetadata = commandParameterAttribute.GetMetadata(targetProperty);

            Assert.That(commandParameterMetadata.Index, Is.EqualTo(0));
            Assert.That(commandParameterMetadata.Name, Is.EqualTo("Arg0"));
            Assert.That(commandParameterMetadata.HelpText, Is.EqualTo("This is a test resource."));
            Assert.That(commandParameterMetadata.Required, Is.False);
            Assert.That(commandParameterMetadata.DefaultValue, Is.EqualTo("Default"));
            Assert.That(commandParameterMetadata.Converter, Is.Not.Null);
            Assert.That(commandParameterMetadata.Converter, Is.TypeOf<DefaultArgumentConverter>());
            Assert.That(commandParameterMetadata.PropertyInfo, Is.EqualTo(targetProperty));
        }
    }
}
