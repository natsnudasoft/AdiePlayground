// <copyright file="MethodInvocationCounterTests.cs" company="natsnudasoft">
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
    using System.Reflection;
    using Common.Interceptor;
    using NUnit.Framework;

    [TestFixture]
    public sealed class MethodInvocationCounterTests
    {
        private const int InvocationCount = 6;

        [Test]
        public void IncrementInvocationCount_MethodInvocationIncremented()
        {
            var invocationCounter = new MethodInvocationCounter();
            var currentMethod = MethodBase.GetCurrentMethod() as MethodInfo;

            for (int i = 0; i < InvocationCount; ++i)
            {
                invocationCounter.IncrementInvocationCount(currentMethod);
            }

            Assert.That(invocationCounter.MethodCounts, Contains.Key(currentMethod));
            Assert.That(invocationCounter.MethodCounts[currentMethod], Is.EqualTo(InvocationCount));
        }
    }
}
