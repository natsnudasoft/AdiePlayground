// <copyright file="InstrumentationInterceptor.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Threading.Tasks;
    using Castle.DynamicProxy;
    using Extensions;

    /// <summary>
    /// Provides an interceptor for instrumentation of methods.
    /// </summary>
    /// <seealso cref="IInterceptor" />
    internal sealed class InstrumentationInterceptor : IInterceptor
    {
        private readonly MethodInvocationCounter invocationCounter;
        private readonly MethodInvocationTimer invocationTimer;
        private readonly IEnumerable<IRegistrar> registrars;
        private readonly IGuidProvider guidProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentationInterceptor" /> class.
        /// </summary>
        /// <param name="invocationCounter">The method invocation counter to use to count any
        /// interceptions.</param>
        /// <param name="invocationTimer">The method invocation timer to use to time any
        /// interceptions.</param>
        /// <param name="registrars">The registrars any interceptions will register to.</param>
        /// <param name="guidProvider">The <see cref="Guid"/> provider to use to generate
        /// an interception id.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="invocationCounter"/>,
        /// <paramref name="invocationTimer"/>, <paramref name="registrars"/>, or
        /// <paramref name="guidProvider"/> is <c>null</c>.</exception>
        public InstrumentationInterceptor(
            MethodInvocationCounter invocationCounter,
            MethodInvocationTimer invocationTimer,
            IEnumerable<IRegistrar> registrars,
            IGuidProvider guidProvider)
        {
            if (invocationCounter == null)
            {
                throw new ArgumentNullException(nameof(invocationCounter));
            }

            if (invocationTimer == null)
            {
                throw new ArgumentNullException(nameof(invocationTimer));
            }

            if (registrars == null)
            {
                throw new ArgumentNullException(nameof(registrars));
            }

            if (guidProvider == null)
            {
                throw new ArgumentNullException(nameof(guidProvider));
            }

            this.invocationCounter = invocationCounter;
            this.invocationTimer = invocationTimer;
            this.registrars = registrars;
            this.guidProvider = guidProvider;
        }

        /// <summary>
        /// Create instrumentation on the specified intercepted member.
        /// </summary>
        /// <param name="invocation">The invocation details of the intercepted member.</param>
        public void Intercept(IInvocation invocation)
        {
            var invocationGuid = this.guidProvider.NewGuid();
            this.invocationCounter.IncrementInvocationCount(invocation.MethodInvocationTarget);
            this.RegisterToAll(invocationGuid, FormatEntryString(invocation));
            var stopwatch = Stopwatch.StartNew();
            invocation.Proceed();
            Task task;
            if (invocation.TryGetAsyncTask(out task))
            {
                this.RegisterToAll(invocationGuid, FormatExitString(invocation, task));
                task.ContinueWith(t =>
                {
                    this.RegisterToAll(
                        invocationGuid,
                        FormatTaskContinuationString(invocation, t));
                    this.AddInvocationTime(invocation, stopwatch);
                });
            }
            else
            {
                this.RegisterToAll(invocationGuid, FormatExitString(invocation));
                this.AddInvocationTime(invocation, stopwatch);
            }
        }

        private static string FormatEntryString(IInvocation invocation)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "Entered {0} ({1})",
                invocation.MethodInvocationTarget.Name,
                invocation.TargetType.Name);
        }

        private static string FormatExitString(IInvocation invocation)
        {
            string returnString;
            if (invocation.MethodInvocationTarget.ReturnType == typeof(void))
            {
                returnString = string.Format(
                    CultureInfo.InvariantCulture,
                    "Exited  {0} ({1})",
                    invocation.MethodInvocationTarget.Name,
                    invocation.TargetType.Name);
            }
            else
            {
                returnString = string.Format(
                    CultureInfo.InvariantCulture,
                    "Exited  {0} ({1}) with return value {2}",
                    invocation.MethodInvocationTarget.Name,
                    invocation.TargetType.Name,
                    invocation.ReturnValue);
            }

            return returnString;
        }

        private static string FormatExitString(IInvocation invocation, Task task)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "Exited  {0} ({1}) with an asynchronous Task ({2})",
                invocation.MethodInvocationTarget.Name,
                invocation.TargetType.Name,
                task.Id);
        }

        private static string FormatTaskContinuationString(IInvocation invocation, Task task)
        {
            string returnString;
            if (!task.IsFaulted && !task.IsCanceled)
            {
                object result;
                if (task.TryGetResult(invocation.MethodInvocationTarget.ReturnType, out result))
                {
                    returnString = string.Format(
                        CultureInfo.InvariantCulture,
                        "Task ({0}) from {1} ({2}) completed with return value {3}",
                        task.Id,
                        invocation.MethodInvocationTarget.Name,
                        invocation.TargetType.Name,
                        result);
                }
                else
                {
                    returnString = string.Format(
                        CultureInfo.InvariantCulture,
                        "Task ({0}) from {1} ({2}) completed",
                        task.Id,
                        invocation.MethodInvocationTarget.Name,
                        invocation.TargetType.Name);
                }
            }
            else
            {
                returnString = string.Format(
                    CultureInfo.InvariantCulture,
                    "Task ({0}) from {1} ({2}) ended with a {3} status",
                    task.Id,
                    invocation.MethodInvocationTarget.Name,
                    invocation.TargetType.Name,
                    task.Status);
            }

            return returnString;
        }

        private void AddInvocationTime(
            IInvocation invocation,
            Stopwatch stopwatch)
        {
            stopwatch.Stop();
            this.invocationTimer.AddInvocationTime(
                invocation.MethodInvocationTarget,
                stopwatch.Elapsed);
        }

        private void RegisterToAll(Guid id, string text)
        {
            foreach (var registrar in this.registrars)
            {
                registrar.Register(id, text);
            }
        }
    }
}