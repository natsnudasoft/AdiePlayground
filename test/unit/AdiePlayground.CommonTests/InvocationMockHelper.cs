// <copyright file="InvocationMockHelper.cs" company="natsnudasoft">
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
    using System;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Castle.DynamicProxy;
    using Moq;

    public static class InvocationMockHelper
    {
        public static Mock<IInvocation> MockVoidReturnInvocation()
        {
            Action action = () => { };
            return MockInvocation(action.Method);
        }

        public static Mock<IInvocation> MockIntReturnInvocation(int returnValue)
        {
            Func<int> func = () => returnValue;
            var invocationMock = MockInvocation(func.Method);
            invocationMock
                .SetupGet(i => i.ReturnValue)
                .Returns(returnValue);
            return invocationMock;
        }

        public static Mock<IInvocation> MockTaskReturnInvocation()
        {
            Func<Task> func = () => Task.CompletedTask;
            var invocationMock = MockInvocation(func.Method);
            invocationMock
                .SetupGet(i => i.ReturnValue)
                .Returns(func());
            return invocationMock;
        }

        public static Mock<IInvocation> MockAsyncVoidReturnInvocation()
        {
            Func<Task> func = async () => { await Task.CompletedTask.ConfigureAwait(false); };
            var invocationMock = MockInvocation(func.Method);
            invocationMock
                .SetupGet(i => i.ReturnValue)
                .Returns(func());
            return invocationMock;
        }

        public static Mock<IInvocation> MockAsyncVoidReturnCanceledInvocation()
        {
            Func<Task> func = async () =>
            {
                await Task.FromCanceled(new CancellationToken(true)).ConfigureAwait(false);
            };
            var invocationMock = MockInvocation(func.Method);
            invocationMock
                .SetupGet(i => i.ReturnValue)
                .Returns(func());
            return invocationMock;
        }

        public static Mock<IInvocation> MockAsyncVoidReturnFaultedInvocation()
        {
            Func<Task> func = async () =>
            {
                await Task.FromException(new Exception()).ConfigureAwait(false);
            };
            var invocationMock = MockInvocation(func.Method);
            invocationMock
                .SetupGet(i => i.ReturnValue)
                .Returns(func());
            return invocationMock;
        }

        public static Mock<IInvocation> MockAsyncIntReturnInvocation(int returnValue)
        {
            Func<Task<int>> func = async () =>
            {
                return await Task.FromResult(returnValue).ConfigureAwait(false);
            };
            var invocationMock = MockInvocation(func.Method);
            invocationMock
                .SetupGet(i => i.ReturnValue)
                .Returns(func());
            return invocationMock;
        }

        private static Mock<IInvocation> MockInvocation(MethodInfo method)
        {
            var invocationMock = new Mock<IInvocation>();
            invocationMock
                .SetupGet(i => i.MethodInvocationTarget)
                .Returns(method);
            invocationMock
                .SetupGet(i => i.TargetType)
                .Returns(typeof(InvocationMockHelper));
            return invocationMock;
        }
    }
}
