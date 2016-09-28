// <copyright file="InterceptionExtensionsTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.CommonTests.Extensions
{
    using System.Threading.Tasks;
    using Common.Extensions;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="InterceptionExtensions"/> class.
    /// </summary>
    [TestFixture]
    public sealed class InterceptionExtensionsTests
    {
        /// <summary>
        /// Tests the TryGetAsyncTask method with a null invocation.
        /// </summary>
        [Test]
        public void TryGetAsyncTask_NullInvocation()
        {
            Task task;
            InterceptionExtensions.TryGetAsyncTask(null, out task);

            Assert.That(task, Is.Null);
        }

        /// <summary>
        /// Tests the TryGetAsyncTask method with an invocation which encapsulates a method with a
        /// void return value.
        /// </summary>
        [Test]
        public void TryGetAsyncTask_InvocationVoidReturn()
        {
            var invocationMock = InvocationMockHelper.MockVoidReturnInvocation();

            Task task;
            invocationMock.Object.TryGetAsyncTask(out task);

            Assert.That(task, Is.Null);
        }

        /// <summary>
        /// Tests the TryGetAsyncTask method with an invocation which encapsulates a method with an
        /// int return value.
        /// </summary>
        [Test]
        public void TryGetAsyncTask_InvocationIntReturn()
        {
            var invocationMock = InvocationMockHelper.MockIntReturnInvocation(0);

            Task task;
            invocationMock.Object.TryGetAsyncTask(out task);

            Assert.That(task, Is.Null);
        }

        /// <summary>
        /// Tests the TryGetAsyncTask method with an invocation which encapsulates a method with a
        /// Task return value.
        /// </summary>
        [Test]
        public void TryGetAsyncTask_InvocationTaskReturn()
        {
            var invocationMock = InvocationMockHelper.MockTaskReturnInvocation();

            Task task;
            invocationMock.Object.TryGetAsyncTask(out task);

            Assert.That(task, Is.Null);
        }

        /// <summary>
        /// Tests the TryGetAsyncTask method with an invocation which encapsulates an async method
        /// with a void return value.
        /// </summary>
        [Test]
        public void TryGetAsyncTask_InvocationAsyncVoidReturn()
        {
            var invocationMock = InvocationMockHelper.MockAsyncVoidReturnInvocation();

            Task task;
            invocationMock.Object.TryGetAsyncTask(out task);

            Assert.That(task, Is.Not.Null);
        }

        /// <summary>
        /// Tests the TryGetResult with a null Task.
        /// </summary>
        [Test]
        public void TryGetResult_NullTask()
        {
            object result;
            InterceptionExtensions.TryGetResult(null, typeof(Task), out result);

            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Tests the TryGetResult with a null Task type.
        /// </summary>
        [Test]
        public void TryGetResult_NullTaskType()
        {
            var task = Task.CompletedTask;

            object result;
            task.TryGetResult(null, out result);

            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Tests the TryGetResult with a non generic task type.
        /// </summary>
        [Test]
        public void TryGetResult_NonGenericTaskType()
        {
            var task = Task.CompletedTask;
            var taskType = typeof(Task);

            object result;
            task.TryGetResult(taskType, out result);

            Assert.That(result, Is.Null);
        }

        /// <summary>
        /// Tests the TryGetResult with a generic task type.
        /// </summary>
        [Test]
        public void TryGetResult_GenericTaskType()
        {
            const int Result = 256;
            var task = Task.FromResult(Result);
            var taskType = typeof(Task<int>);

            object result;
            task.TryGetResult(taskType, out result);

            Assert.That(result, Is.EqualTo(Result));
        }
    }
}
