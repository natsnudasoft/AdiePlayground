// <copyright file="InterceptionExtensions.cs" company="natsnudasoft">
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
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Castle.DynamicProxy;

    /// <summary>
    /// Provides extensions for interception.
    /// </summary>
    public static class InterceptionExtensions
    {
        /// <summary>
        /// Attempts to retrieve a <see cref="Task"/> from the <code>ReturnValue</code> of the
        /// <see cref="IInvocation"/>, a <see cref="Task"/> will only be retrieved if the method is
        /// async.
        /// </summary>
        /// <param name="invocation">The invocation object.</param>
        /// <param name="task">When this method returns, contains the <see cref="Task"/> if the
        /// retrieval was successful, or <code>null</code> if the retrieval failed.</param>
        /// <returns><code>true</code> if <paramref name="task"/> was retrieved successfully;
        /// otherwise, false.</returns>
        public static bool TryGetAsyncTask(this IInvocation invocation, out Task task)
        {
            if (invocation != null && invocation.MethodInvocationTarget.ReturnType != typeof(void))
            {
                task = invocation.ReturnValue as Task;
                if (task != null && !invocation.MethodInvocationTarget
                    .IsDefined(typeof(AsyncStateMachineAttribute), false))
                {
                    // We are not interested in the Task if it is not an async method.
                    task = null;
                }
            }
            else
            {
                task = null;
            }

            return task != null;
        }

        /// <summary>
        /// Attempts to retrieve a result from the <see cref="Task"/> via reflection.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="taskType">The declared <see cref="Type"/> of the task.</param>
        /// <param name="result">When this method returns, contains the result of this
        /// <see cref="Task"/> if the retrieval was successful, or <code>null</code> if the
        /// retrieval failed.</param>
        /// <returns><code>true</code> if <paramref name="result"/> was retrieved successfully;
        /// otherwise, false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1007:UseGenericsWhereAppropriate",
            Justification = "We are using reflection and do not know the type beforehand.")]
        public static bool TryGetResult(
            this Task task,
            Type taskType,
            out object result)
        {
            if (task != null && taskType != null)
            {
                if (taskType.IsGenericType && taskType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    result = task.GetType().GetProperty("Result").GetValue(task, null);
                }
                else
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }

            return result != null;
        }
    }
}
