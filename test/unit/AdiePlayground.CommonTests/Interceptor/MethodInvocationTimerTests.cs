// <copyright file="MethodInvocationTimerTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Interceptor
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Common.Interceptor;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MethodInvocationTimerTests
    {
        private static readonly int[] InvocationTimes = new[] { 502468, 851234, 455555 };

        [Test]
        public void AddInvocationTime_MethodTimesAdded()
        {
            var invocationTimer = new MethodInvocationTimer();
            var currentMethod = MethodBase.GetCurrentMethod() as MethodInfo;
            foreach (int invocationTime in InvocationTimes)
            {
                invocationTimer.AddInvocationTime(
                    currentMethod,
                    TimeSpan.FromTicks(invocationTime));
            }

            Assert.That(invocationTimer.MethodTimes, Contains.Key(currentMethod));
            Assert.That(
                invocationTimer.MethodTimes[currentMethod],
                Is.EqualTo(InvocationTimes.Select(t => new TimeSpan(t))));
        }
    }
}
