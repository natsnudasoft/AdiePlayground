// <copyright file="TypeExtensionsTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Extensions
{
    using System;
    using AdiePlayground.Common.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public sealed class TypeExtensionsTests
    {
        private const string GetImplicitOperatorBaseTypeParam = "baseType";
        private const string GetImplicitOperatorSourceTypeParam = "sourceType";
        private const string GetImplicitOperatorDestinationTypeParam = "destinationType";

        [Test]
        public void GetImplicitOperator_NullBaseType_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => TypeExtensions.GetImplicitOperator(
                null,
                typeof(string),
                typeof(ImplicitOperatorStub)));
            Assert.That(ex.ParamName, Is.EqualTo(GetImplicitOperatorBaseTypeParam));
        }

        [Test]
        public void GetImplicitOperator_NullSourceType_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                typeof(ImplicitOperatorStub).GetImplicitOperator(
                    null,
                    typeof(ImplicitOperatorStub)));
            Assert.That(ex.ParamName, Is.EqualTo(GetImplicitOperatorSourceTypeParam));
        }

        [Test]
        public void GetImplicitOperator_NullDestinationType_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                typeof(ImplicitOperatorStub).GetImplicitOperator(
                    typeof(string),
                    null));
            Assert.That(ex.ParamName, Is.EqualTo(GetImplicitOperatorDestinationTypeParam));
        }

        [Test]
        public void GetImplicitOperator_NoImplicitOperatorFound()
        {
            var implicitOperator = typeof(ImplicitOperatorStub).GetImplicitOperator(
                typeof(int),
                typeof(ImplicitOperatorStub));
            Assert.That(implicitOperator, Is.Null);
        }

        [Test]
        public void GetImplicitOperator_ImplicitOperatorFound()
        {
            var implicitOperator = typeof(ImplicitOperatorStub).GetImplicitOperator(
                typeof(string),
                typeof(ImplicitOperatorStub));
            Assert.That(implicitOperator, Is.Not.Null);
            Assert.That(implicitOperator.DeclaringType, Is.EqualTo(typeof(ImplicitOperatorStub)));
            var parameters = implicitOperator.GetParameters();
            Assert.That(parameters.Length, Is.EqualTo(1));
            Assert.That(parameters[0].ParameterType, Is.EqualTo(typeof(string)));
            Assert.That(implicitOperator.ReturnType, Is.EqualTo(typeof(ImplicitOperatorStub)));
        }
    }
}
