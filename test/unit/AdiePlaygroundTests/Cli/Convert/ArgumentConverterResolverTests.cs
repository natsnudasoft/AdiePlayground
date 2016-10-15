// <copyright file="ArgumentConverterResolverTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Cli.Convert
{
    using System;
    using AdiePlayground.Cli.Convert;
    using Commands;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ArgumentConverterResolverTests
    {
        private const string ResolvePropertyInfoParam = "propertyInfo";

        [Test]
        public void Resolve_NullPropertyInfo_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => ArgumentConverterResolver.Resolve(null));
            Assert.That(ex.ParamName, Is.EqualTo(ResolvePropertyInfoParam));
        }

        [Test]
        public void Resolve_StringProperty_FindDefaultArgumentConverter()
        {
            var argumentConverter = ArgumentConverterResolver.Resolve(
                typeof(CommandStub).GetProperty(nameof(CommandStub.StringProperty)));
            Assert.That(argumentConverter, Is.Not.Null);
            Assert.That(argumentConverter, Is.TypeOf<DefaultArgumentConverter>());
        }

        [Test]
        public void Resolve_TypeConverterProperty_FindTypeConverterArgumentConverter()
        {
            var argumentConverter = ArgumentConverterResolver.Resolve(
                typeof(CommandStub).GetProperty(nameof(CommandStub.TypeConverterProperty)));
            Assert.That(argumentConverter, Is.Not.Null);
            Assert.That(argumentConverter, Is.TypeOf<TypeConverterArgumentConverter>());
        }

        [Test]
        public void Resolve_ImplicitOperatorProperty_FindImplicitArgumentConverter()
        {
            var argumentConverter = ArgumentConverterResolver.Resolve(
                typeof(CommandStub).GetProperty(nameof(CommandStub.ImplicitOperatorStubProperty)));
            Assert.That(argumentConverter, Is.Not.Null);
            Assert.That(argumentConverter, Is.TypeOf<ImplicitArgumentConverter>());
        }

        [Test]
        public void Resolve_TypeConverterCannotConvertProperty_ArgumentConverterResolveException()
        {
            const string PropertyName = nameof(CommandStub.TypeConverterCannotConvertProperty);
            var ex = Assert.Throws<ArgumentConverterResolveException>(
                () => ArgumentConverterResolver.Resolve(
                    typeof(CommandStub).GetProperty(PropertyName)));
            Assert.That(ex.ArgumentName, Is.EqualTo(PropertyName));
        }

        [Test]
        public void Resolve_CannotResolveProperty_ArgumentConverterResolveException()
        {
            const string PropertyName = nameof(CommandStub.CannotResolveProperty);
            var ex = Assert.Throws<ArgumentConverterResolveException>(
                () => ArgumentConverterResolver.Resolve(
                    typeof(CommandStub).GetProperty(PropertyName)));
            Assert.That(ex.ArgumentName, Is.EqualTo(PropertyName));
        }
    }
}
