// <copyright file="TypeExtensions.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides extensions for the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets an implicit operator from a <see cref="Type"/> that casts from the specified source
        /// <see cref="Type"/>, to the specified destination <see cref="Type"/>.
        /// </summary>
        /// <param name="baseType">The <see cref="Type"/> to get the implicit operator from.</param>
        /// <param name="sourceType">The input <see cref="Type"/> of the implicit operator to find.
        /// </param>
        /// <param name="destinationType">The return <see cref="Type"/> of the implicit operator to
        /// find.</param>
        /// <returns>The <see cref="MethodInfo"/> describing the implicit operator found; or,
        /// <see langword="null"/> if no implicit operator was found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="baseType"/>,
        /// <paramref name="sourceType"/>, or <paramref name="destinationType"/> is
        /// <see langword="null"/>.</exception>
        public static MethodInfo GetImplicitOperator(
            this Type baseType,
            Type sourceType,
            Type destinationType)
        {
            if (baseType == null)
            {
                throw new ArgumentNullException(nameof(baseType));
            }

            if (sourceType == null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }

            if (destinationType == null)
            {
                throw new ArgumentNullException(nameof(destinationType));
            }

            return baseType
                .GetMethods()
                .SingleOrDefault(m =>
                {
                    var parameters = m.GetParameters();
                    return m.IsSpecialName &&
                        m.Name == "op_Implicit" &&
                        m.ReturnType == destinationType &&
                        parameters.Length == 1 &&
                        parameters[0].ParameterType == sourceType;
                })
;
        }
    }
}
