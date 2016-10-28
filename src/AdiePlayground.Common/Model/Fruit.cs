// <copyright file="Fruit.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Model
{
    using System;

    /// <summary>
    /// Provides an abstract base class for fruit.
    /// </summary>
    public abstract class Fruit
    {
        private const int MinQuality = 0;
        private const int MaxQuality = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fruit"/> class with the specified quality.
        /// </summary>
        /// <param name="quality">The quality of this instance of fruit.</param>
        /// <exception cref="ArgumentOutOfRangeException"><para><paramref name="quality"/> is less
        /// than 0.</para><para>-or-</para><para><paramref name="quality"/> is greater than 100.
        /// </para></exception>
        protected Fruit(int quality)
        {
            ParameterValidation
                .IsBetweenInclusive(quality, MinQuality, MaxQuality, nameof(quality));

            this.Quality = quality;
        }

        /// <summary>
        /// Gets the name of this <see cref="Fruit"/>.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the colour of this <see cref="Fruit"/>.
        /// </summary>
        public abstract string Color { get; }

        /// <summary>
        /// Gets the quality of this <see cref="Fruit"/>.
        /// </summary>
        public int Quality { get; }
    }
}
