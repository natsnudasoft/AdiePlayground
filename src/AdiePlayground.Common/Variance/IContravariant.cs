﻿// <copyright file="IContravariant.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Variance
{
    /// <summary>
    /// Example interface showing how to implement contravariance.
    /// </summary>
    /// <typeparam name="T">The contravariant type this interface will work on.</typeparam>
    public interface IContravariant<in T>
    {
        /// <summary>
        /// Gets a value from the specified input object.
        /// </summary>
        /// <param name="input">An input object of type <typeparamref name="T" />.</param>
        /// <returns>A value from the input object.</returns>
        int GetValue(T input);
    }
}
