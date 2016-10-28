// <copyright file="ImplicitArgumentConverter.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli.Convert
{
    using System;
    using System.Reflection;
    using Common;

    /// <summary>
    /// Describes an <see cref="IArgumentConverter"/> which uses an implicit operator to convert
    /// an argument.
    /// </summary>
    /// <seealso cref="IArgumentConverter" />
    internal sealed class ImplicitArgumentConverter : IArgumentConverter
    {
        private readonly MethodInfo implicitOperator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImplicitArgumentConverter"/> class.
        /// </summary>
        /// <param name="implicitOperator">The <see cref="MethodInfo"/> which describes the implicit
        /// operator to use to convert an argument.</param>
        /// <exception cref="ArgumentNullException"><paramref name="implicitOperator"/> is
        /// <see langword="null"/>.</exception>
        public ImplicitArgumentConverter(MethodInfo implicitOperator)
        {
            ParameterValidation.IsNotNull(implicitOperator, nameof(implicitOperator));

            this.implicitOperator = implicitOperator;
        }

        /// <inheritdoc/>
        public object Convert(string argument)
        {
            return this.implicitOperator.Invoke(null, new[] { argument });
        }
    }
}
