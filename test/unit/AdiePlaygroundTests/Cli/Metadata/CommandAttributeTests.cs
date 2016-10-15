// <copyright file="CommandAttributeTests.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using AdiePlayground.Cli.Metadata;
    using Autofac.Extras.AttributeMetadata;
    using Commands;
    using NUnit.Framework;
    using Properties;

    [TestFixture]
    public sealed class CommandAttributeTests
    {
        private const string ConstructorGroupParam = "group";
        private const string ConstructorNameParam = "name";
        private const string ConstructorResourceTypeParam = "resourceType";
        private const string ConstructorHelpTextResourceNameParam = "helpTextResourceName";
        private const string GetMetadataTargetTypeParam = "targetType";

        [Test]
        public void Constructor_NullGroup_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandAttribute(null, "CommandName", typeof(Resources), "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorGroupParam));
        }

        [Test]
        public void Constructor_EmptyGroup_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new CommandAttribute(
                    string.Empty,
                    "CommandName",
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorGroupParam));
        }

        [Test]
        public void Constructor_NullName_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandAttribute(
                    "CommandGroup",
                    null,
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorNameParam));
        }

        [Test]
        public void Constructor_EmptyName_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new CommandAttribute(
                    "CommandGroup",
                    string.Empty,
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorNameParam));
        }

        [Test]
        public void Constructor_SpaceInName_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new CommandAttribute(
                    "CommandGroup",
                    "Command Name",
                    typeof(Resources),
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorNameParam));
        }

        [Test]
        public void Constructor_NullResourceType_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandAttribute(
                    "CommandGroup",
                    "CommandName",
                    null,
                    "TestResource"));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorResourceTypeParam));
        }

        [Test]
        public void Constructor_NullHelpTextResourceName_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CommandAttribute(
                    "CommandGroup",
                    "CommandName",
                    typeof(Resources),
                    null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorHelpTextResourceNameParam));
        }

        [Test]
        public void Constructor_EmptyHelpTextResourceName_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => new CommandAttribute(
                    "CommandGroup",
                    "CommandName",
                    typeof(Resources),
                    string.Empty));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorHelpTextResourceNameParam));
        }

        [Test]
        public void GetMetadata_NullTargetType_ArgumentNullException()
        {
            var commandAttribute = new CommandAttribute(
                "CommandGroup",
                "CommandName",
                typeof(Resources),
                "TestResource");
            var commandAttributeExplicit = (IMetadataProvider)commandAttribute;
            var ex = Assert.Throws<ArgumentNullException>(
                () => commandAttributeExplicit.GetMetadata(null));
            Assert.That(ex.ParamName, Is.EqualTo(GetMetadataTargetTypeParam));
        }

        [Test]
        public void GetMetadata_IndexMismatch_InvalidOperationException()
        {
            var commandAttribute = new CommandAttribute(
                "CommandGroup",
                "CommandName",
                typeof(Resources),
                "TestResource");
            var commandAttributeExplicit = (IMetadataProvider)commandAttribute;
            Assert.Throws<InvalidOperationException>(
                () => commandAttributeExplicit.GetMetadata(typeof(CommandIndexMismatchStub)));
        }

        [Test]
        public void GetMetadata_RequiredMismatch_InvalidOperationException()
        {
            var commandAttribute = new CommandAttribute(
                "CommandGroup",
                "CommandName",
                typeof(Resources),
                "TestResource");
            var commandAttributeExplicit = (IMetadataProvider)commandAttribute;
            Assert.Throws<InvalidOperationException>(
                () => commandAttributeExplicit.GetMetadata(typeof(CommandRequiredMismatchStub)));
        }

        [Test]
        public void GetMetadata_ReturnsMetadata()
        {
            var commandAttribute = new CommandAttribute(
                "CommandGroup",
                "CommandName",
                typeof(Resources),
                "TestResource");
            var commandAttributeExplicit = (IMetadataProvider)commandAttribute;
            var metadata =
                commandAttributeExplicit.GetMetadata(typeof(CommandStub));

            Assert.That(metadata, Is.Not.Null);
            Assert.That(metadata, Contains.Item(new KeyValuePair<string, object>(
                nameof(CommandAttribute.Group),
                "CommandGroup")));
            Assert.That(metadata, Contains.Item(new KeyValuePair<string, object>(
                nameof(CommandAttribute.Name),
                "CommandName")));
            Assert.That(metadata, Contains.Item(new KeyValuePair<string, object>(
                nameof(CommandAttribute.HelpText),
                "This is a test resource.")));
            Assert.That(metadata, Contains.Key(nameof(CommandMetadata.ParametersMetadata)));
            var parametersMetadata = (IList<CommandParameterMetadata>)metadata[
                nameof(CommandMetadata.ParametersMetadata)];
            Assert.That(parametersMetadata, Is.Not.Null);
            Assert.That(parametersMetadata, Has.Length.EqualTo(1));
            var parameterMetadata = parametersMetadata[0];
            Assert.That(parameterMetadata, Is.Not.Null);
            Assert.That(parameterMetadata.Index, Is.EqualTo(0));
            Assert.That(parameterMetadata.Name, Is.EqualTo("valid"));
            Assert.That(parameterMetadata.HelpText, Is.EqualTo("This is a test resource."));
        }
    }
}
