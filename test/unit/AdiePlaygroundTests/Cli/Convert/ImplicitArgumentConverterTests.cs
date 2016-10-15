// <copyright file="ImplicitArgumentConverterTests.cs" company="natsnudasoft">
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
    using AdiePlayground.Common.Extensions;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ImplicitArgumentConverterTests
    {
        private const string ConstructorImplicitOperatorParam = "implicitOperator";

        [Test]
        public void Constructor_NullImplicitOperator_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new ImplicitArgumentConverter(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorImplicitOperatorParam));
        }

        [Test]
        public void Convert_ReturnsConvertedValue()
        {
            const string inputValue = "Hello";
            var defaultArgumentConverter = new ImplicitArgumentConverter(
                typeof(ImplicitOperatorStub).GetImplicitOperator(
                    typeof(string),
                    typeof(ImplicitOperatorStub)));
            var outputValue = defaultArgumentConverter.Convert(inputValue);
            Assert.That(((ImplicitOperatorStub)outputValue)?.Value, Is.EqualTo(inputValue));
        }
    }
}
