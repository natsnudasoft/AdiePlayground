// <copyright file="Apple.cs" company="natsnudasoft">
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
    /// Represents an instance of an apple.
    /// </summary>
    /// <seealso cref="Fruit" />
    public sealed class Apple : Fruit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Apple" /> class with the specified quality.
        /// </summary>
        /// <param name="color">The colour of this <see cref="Apple"/>.</param>
        /// <param name="quality">The quality of this <see cref="Apple"/>.</param>
        public Apple(AppleColor color, int quality)
            : base(quality)
        {
            switch (color)
            {
                case AppleColor.Red:
                    this.Color = "Red";
                    break;

                case AppleColor.Green:
                    this.Color = "Green";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(color));
            }
        }

        /// <inheritdoc/>
        public override string Name => nameof(Apple);

        /// <inheritdoc/>
        public override string Color { get; }
    }
}
