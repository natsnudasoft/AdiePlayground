// <copyright file="Orange.cs" company="natsnudasoft">
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
    /// <summary>
    /// Represents an instance of an orange.
    /// </summary>
    /// <seealso cref="Fruit" />
    public sealed class Orange : Fruit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Orange"/> class with the specified quality.
        /// </summary>
        /// <param name="quality">The quality of this <see cref="Orange"/>.</param>
        public Orange(int quality)
            : base(quality)
        {
        }

        /// <inheritdoc/>
        public override string Name => nameof(Orange);

        /// <inheritdoc/>
#pragma warning disable CC0021 // Use nameof
        public override string Color => "Orange";
#pragma warning restore CC0021 // Use nameof
    }
}
