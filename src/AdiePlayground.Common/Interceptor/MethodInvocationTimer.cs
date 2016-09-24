// <copyright file="MethodInvocationTimer.cs" company="natsnudasoft">
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
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides a class to maintain timing information of method invocations.
    /// </summary>
    public sealed class MethodInvocationTimer
    {
        private readonly ConcurrentDictionary<MethodInfo, Queue<TimeSpan>> methodTimes =
            new ConcurrentDictionary<MethodInfo, Queue<TimeSpan>>();

        /// <summary>
        /// Gets a copy of the current method timing information.
        /// </summary>
        public IReadOnlyDictionary<MethodInfo, IEnumerable<TimeSpan>> MethodTimes =>
            this.methodTimes.ToDictionary(s => s.Key, s => s.Value.ToList().Skip(0));

        /// <summary>
        /// Adds timing information for the specified method.
        /// </summary>
        /// <param name="method">The method to add the timing information of.</param>
        /// <param name="time">The time of this method invocation.</param>
        public void AddInvocationTime(MethodInfo method, TimeSpan time)
        {
            this.methodTimes.AddOrUpdate(
                method,
                new Queue<TimeSpan>(new[] { time }),
                (m, t) =>
                {
                    t.Enqueue(time);
                    return t;
                });
        }
    }
}
