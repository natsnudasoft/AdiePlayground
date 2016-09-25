// <copyright file="SystemDateTimeProviderTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests
{
    using Common;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="SystemDateTimeProvider"/> class.
    /// </summary>
    [TestFixture]
    public sealed class SystemDateTimeProviderTests
    {
        /// <summary>
        /// Tests the Now method.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1804:RemoveUnusedLocals",
            MessageId = "dateTime",
            Justification = "We need to assign the property to test for exception.")]
        [Test]
        public void Now_DoesNotThrow()
        {
            var dateTimeProvider = new SystemDateTimeProvider();

            Assert.DoesNotThrow(() => { var dateTime = dateTimeProvider.Now; });
        }
    }
}
