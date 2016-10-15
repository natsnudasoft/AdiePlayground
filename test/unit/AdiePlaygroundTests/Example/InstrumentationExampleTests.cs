// <copyright file="InstrumentationExampleTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Example
{
    using System.Threading.Tasks;
    using AdiePlayground.Example;
    using NUnit.Framework;

    [TestFixture]
    public sealed class InstrumentationExampleTests
    {
        [Test]
        public void IncrementValue()
        {
            var instrumentationExample = new InstrumentationExample
            {
                Value = 6
            };
            instrumentationExample.IncrementValue();
            Assert.That(instrumentationExample.Value, Is.EqualTo(7));
        }

        [Test]
        [Category("LongRunning")]
        public async Task IncrementValueAsync()
        {
            var instrumentationExample = new InstrumentationExample
            {
                Value = 15
            };
            await instrumentationExample.IncrementValueAsync().ConfigureAwait(false);
            Assert.That(instrumentationExample.Value, Is.EqualTo(16));
        }

        [Test]
        [Category("LongRunning")]
        public async Task MultiplyValueAsync()
        {
            var instrumentationExample = new InstrumentationExample
            {
                Value = 5
            };
            var result = await instrumentationExample.MultiplyValueAsync(3).ConfigureAwait(false);
            Assert.That(instrumentationExample.Value, Is.EqualTo(5));
            Assert.That(result, Is.EqualTo(15));
        }
    }
}
