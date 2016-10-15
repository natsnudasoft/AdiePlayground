// <copyright file="TypeConverterArgumentConverterTests.cs" company="natsnudasoft">
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
    using NUnit.Framework;

    [TestFixture]
    public sealed class TypeConverterArgumentConverterTests
    {
        private const string ConstructorTypeConverterParam = "typeConverter";

        [Test]
        public void Constructor_NullTypeConverter_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new TypeConverterArgumentConverter(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorTypeConverterParam));
        }

        [Test]
        public void Convert_ReturnsConvertedValue()
        {
            const string inputValue = "150";
            var defaultArgumentConverter = new TypeConverterArgumentConverter(
                new TypeConverterStub());
            var outputValue = defaultArgumentConverter.Convert(inputValue);
            Assert.That(outputValue, Is.EqualTo(150));
        }
    }
}
