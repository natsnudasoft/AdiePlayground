// <copyright file="MethodInvocationCounter.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Interceptor
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Provides a class to maintain a count of method invocations.
    /// </summary>
    public sealed class MethodInvocationCounter
    {
        private readonly ConcurrentDictionary<MethodInfo, int> methodCounts =
            new ConcurrentDictionary<MethodInfo, int>();

        /// <summary>
        /// Gets a copy of the current state of method counts.
        /// </summary>
        public IReadOnlyDictionary<MethodInfo, int> MethodCounts =>
            new Dictionary<MethodInfo, int>(this.methodCounts);

        /// <summary>
        /// Increments the invocation count of the specified method.
        /// </summary>
        /// <param name="method">The method to increment the invocation count of.</param>
        public void IncrementInvocationCount(MethodInfo method)
        {
            this.methodCounts.AddOrUpdate(method, 1, (m, i) => i + 1);
        }
    }
}
