// <copyright file="InstrumentationExample.cs" company="natsnudasoft">
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

namespace AdiePlayground.Example
{
    using System.Threading.Tasks;

    /// <summary>
    /// Provides an example of intercepting methods with instrumentation.
    /// </summary>
    /// <seealso cref="IInstrumentationExample" />
    internal sealed class InstrumentationExample : IInstrumentationExample
    {
        /// <inheritdoc/>
        public int Value { get; set; }

        /// <inheritdoc/>
        public void IncrementValue()
        {
            ++this.Value;
        }

        /// <inheritdoc/>
        public async Task IncrementValueAsync()
        {
            const int DelayTime = 750;
            ++this.Value;
            await Task.Delay(DelayTime).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<int> MultiplyValueAsync(int multiplier)
        {
            const int DelayTime = 1000;
            var answer = this.Value * multiplier;
            await Task.Delay(DelayTime).ConfigureAwait(false);
            return answer;
        }
    }
}
