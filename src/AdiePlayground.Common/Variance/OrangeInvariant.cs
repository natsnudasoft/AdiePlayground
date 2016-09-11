// <copyright file="OrangeInvariant.cs" company="natsnudasoft">
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
    using Properties;

    /// <summary>
    /// Example implementation of an invariant interface.
    /// </summary>
    /// <seealso cref="ICovariant{Orange}" />
    internal sealed class OrangeInvariant : IInvariant<Orange>
    {
        /// <inheritdoc/>
        public int GetValue(Orange input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(
                    nameof(input),
                    Resources.OrangeInvariantGetValueInputNull);
            }

            return input.Quality;
        }
    }
}
