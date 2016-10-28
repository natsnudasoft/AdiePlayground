// <copyright file="ParameterValidationTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common
{
    using System;
    using AdiePlayground.Common;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ParameterValidationTests
    {
        private const string TestParameterName = "testParam";

        [Test]
        public void IsNotNull_NullValue_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => ParameterValidation.IsNotNull((string)null, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsNotNull_NotNullValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsNotNull("Not Null.", TestParameterName));
        }

        [Test]
        public void IsNotNull_NullableNoValue_ArgumentNullException()
        {
            int? nullable = null;
            var ex = Assert.Throws<ArgumentNullException>(
                () => ParameterValidation.IsNotNull(nullable, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsNotNull_NullableHasValue_DoesNotThrow()
        {
            int? nullable = 5;
            Assert.DoesNotThrow(
                () => ParameterValidation.IsNotNull(nullable, TestParameterName));
        }

        [Test]
        public void IsNotEmpty_EmptyValue_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => ParameterValidation.IsNotEmpty(string.Empty, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsNotEmpty_NotEmptyValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsNotEmpty("Not Empty.", TestParameterName));
        }

        [Test]
        public void IsGreaterThan_OutOfRangeValue_ArgumentOutOfRangeException(
            [Values(0, -1, int.MinValue)]int value)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => ParameterValidation.IsGreaterThan(value, 0, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsGreaterThan_InRangeValue_DoesNotThrow(
            [Values(1, 2, int.MaxValue)]int value)
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsGreaterThan(value, 0, TestParameterName));
        }

        [Test]
        public void IsLessThan_OutOfRangeValue_ArgumentOutOfRangeException(
            [Values(0, 1, int.MaxValue)]int value)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => ParameterValidation.IsLessThan(value, 0, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsLessThan_InRangeValue_DoesNotThrow(
            [Values(-1, -2, int.MinValue)]int value)
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsLessThan(value, 0, TestParameterName));
        }

        [Test]
        public void IsGreaterThanOrEqualTo_OutOfRangeValue_ArgumentOutOfRangeException(
            [Values(-1, -2, int.MinValue)]int value)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => ParameterValidation.IsGreaterThanOrEqualTo(value, 0, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsGreaterThanOrEqualTo_InRangeValue_DoesNotThrow(
            [Values(0, 1, int.MaxValue)]int value)
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsGreaterThanOrEqualTo(value, 0, TestParameterName));
        }

        [Test]
        public void IsLessThanOrEqualTo_OutOfRangeValue_ArgumentOutOfRangeException(
            [Values(1, 2, int.MaxValue)]int value)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => ParameterValidation.IsLessThanOrEqualTo(value, 0, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsLessThanOrEqualTo_InRangeValue_DoesNotThrow(
            [Values(0, -1, int.MinValue)]int value)
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsLessThanOrEqualTo(value, 0, TestParameterName));
        }

        [Test]
        public void IsBetween_OutOfRangeValue_ArgumentOutOfRangeException(
            [Values(-10, -11, int.MinValue, 10, 11, int.MaxValue)]int value)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => ParameterValidation.IsBetween(value, -10, 10, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsBetween_InRangeValue_DoesNotThrow(
            [Values(-9, -8, 8, 9)]int value)
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsBetween(value, -10, 10, TestParameterName));
        }

        [Test]
        public void IsBetweenInclusive_OutOfRangeValue_ArgumentOutOfRangeException(
            [Values(-11, -12, int.MinValue, 11, 12, int.MaxValue)]int value)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => ParameterValidation.IsBetweenInclusive(value, -10, 10, TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsBetweenInclusive_InRangeValue_DoesNotThrow(
            [Values(-10, -9, 9, 10)]int value)
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsBetweenInclusive(value, -10, 10, TestParameterName));
        }

        [Test]
        public void IsTrue_FalseValue_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => ParameterValidation.IsTrue(false, "Message", TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsTrue_TrueValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsTrue(true, "Message", TestParameterName));
        }

        [Test]
        public void IsFalse_TrueValue_ArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(
                () => ParameterValidation.IsFalse(true, "Message", TestParameterName));
            Assert.That(ex.ParamName, Is.EqualTo(TestParameterName));
        }

        [Test]
        public void IsFalse_FalseValue_DoesNotThrow()
        {
            Assert.DoesNotThrow(
                () => ParameterValidation.IsFalse(false, "Message", TestParameterName));
        }
    }
}
