// <copyright file="BananaCovariant.cs" company="natsnudasoft">
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
    using System;
    using Model;

    /// <summary>
    /// Example implementation of a covariant interface.
    /// </summary>
    /// <seealso cref="ICovariant{Banana}" />
    internal sealed class BananaCovariant : ICovariant<Banana>
    {
        /// <inheritdoc/>
        public Banana Create(params object[] args)
        {
            return (Banana)Activator.CreateInstance(typeof(Banana), args);
        }
    }
}
