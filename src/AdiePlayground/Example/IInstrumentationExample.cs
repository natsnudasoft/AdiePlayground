// <copyright file="IInstrumentationExample.cs" company="natsnudasoft">
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
    using Common.Interceptor;

    /// <summary>
    /// Provides an example of intercepting methods with instrumentation.
    /// </summary>
    [InstrumentationIntercept]
    public interface IInstrumentationExample
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        int Value { get; set; }

        /// <summary>
        /// Increments the value.
        /// </summary>
        void IncrementValue();

        /// <summary>
        /// Increments the value asynchronously.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task IncrementValueAsync();

        /// <summary>
        /// Multiplies the value by the specified multiplier.
        /// </summary>
        /// <param name="multiplier">The multiplier.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. The result of the
        /// task is the value multiplied by the specified multiplier.</returns>
        Task<int> MultiplyValueAsync(int multiplier);
    }
}
