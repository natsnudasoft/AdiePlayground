﻿// <copyright file="Covariant.cs" company="natsnudasoft">
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
    /// Example delegate showing how to implement covariance.
    /// </summary>
    /// <typeparam name="T">The covariant type this delegate will work on.</typeparam>
    /// <returns>An output object of type <typeparamref name="T"/>.</returns>
    public delegate T Covariant<out T>();
}
