// <copyright file="InstrumentationInterceptAttribute.cs" company="natsnudasoft">
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
    using Autofac.Extras.DynamicProxy;

    /// <summary>
    /// Indicates that a class or interface should be intercepted by an
    /// <see cref="InstrumentationInterceptor"/>.
    /// </summary>
    /// <seealso cref="InterceptAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public sealed class InstrumentationInterceptAttribute : InterceptAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstrumentationInterceptAttribute"/> class.
        /// </summary>
        public InstrumentationInterceptAttribute()
            : base(typeof(InstrumentationInterceptor))
        {
        }
    }
}
