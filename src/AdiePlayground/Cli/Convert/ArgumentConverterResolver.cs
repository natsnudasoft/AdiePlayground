// <copyright file="ArgumentConverterResolver.cs" company="natsnudasoft">
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
    using System.ComponentModel;
    using System.Reflection;
    using Common.Extensions;

    /// <summary>
    /// Provides a way of resolving an <see cref="IArgumentConverter"/> on a property.
    /// </summary>
    internal static class ArgumentConverterResolver
    {
#pragma warning disable MEN007 // Use a single return
        /// <summary>
        /// Attempts to resolve an <see cref="IArgumentConverter"/> from the specified property.
        /// </summary>
        /// <param name="propertyInfo">The <see cref="PropertyInfo"/> that describes the property
        /// to resolve an <see cref="IArgumentConverter"/> from.</param>
        /// <returns>The resolved <see cref="IArgumentConverter"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="propertyInfo"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentConverterResolveException">An <see cref="IArgumentConverter"/>
        /// could not be resolved from <paramref name="propertyInfo"/>.</exception>
        public static IArgumentConverter Resolve(PropertyInfo propertyInfo)
#pragma warning restore MEN007 // Use a single return
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            if (propertyInfo.PropertyType == typeof(string))
            {
                return new DefaultArgumentConverter();
            }

            var typeConverter = FindTypeConverter(propertyInfo);
            if (typeConverter != null)
            {
                return new TypeConverterArgumentConverter(typeConverter);
            }

            var implicitOperator = FindImplicitOperator(propertyInfo);
            if (implicitOperator != null)
            {
                return new ImplicitArgumentConverter(implicitOperator);
            }

            throw new ArgumentConverterResolveException(
                propertyInfo.Name,
                propertyInfo.PropertyType);
        }

        private static TypeConverter FindTypeConverter(PropertyInfo propertyInfo)
        {
            TypeConverter typeConverter;
            if (propertyInfo.IsDefined(typeof(TypeConverterAttribute), false))
            {
                var converter = TypeDescriptor.CreateProperty(
                    propertyInfo.DeclaringType,
                    propertyInfo.Name,
                    propertyInfo.PropertyType)
                    ?.Converter;
                if (converter != null && converter.CanConvertFrom(typeof(string)))
                {
                    typeConverter = converter;
                }
                else
                {
                    typeConverter = null;
                }
            }
            else
            {
                typeConverter = null;
            }

            return typeConverter;
        }

        private static MethodInfo FindImplicitOperator(PropertyInfo propertyInfo)
        {
            var propertyType = propertyInfo.PropertyType;
            return propertyType.GetImplicitOperator(typeof(string), propertyType);
        }
    }
}
