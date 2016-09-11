﻿// <copyright file="FruitContravariant.cs" company="natsnudasoft">
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
    /// Example implementation of a contravariant interface.
    /// </summary>
    /// <seealso cref="IContravariant{Fruit}" />
    internal sealed class FruitContravariant : IContravariant<Fruit>
    {
        /// <inheritdoc/>
        public int GetValue(Fruit input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(
                    nameof(input),
                    Resources.FruitContravariantGetValueInputNull);
            }

            return input.Quality;
        }
    }
}