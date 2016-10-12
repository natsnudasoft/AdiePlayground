// <copyright file="ResourceTypeResolver.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Provides a way of resolving resource strings from a <see cref="Type"/>.
    /// </summary>
    public static class ResourceTypeResolver
    {
        /// <summary>
        /// Resolves a static resource of the specified name on the specified resource
        /// <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the resource that will be resolved.
        /// </typeparam>
        /// <param name="resourceType">The <see cref="Type"/> to search for the resource that will
        /// be resolved.</param>
        /// <param name="resourceName">The name of the resource that will be resolved.</param>
        /// <returns>The value of the resource that was resolved.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="resourceType"/>, or
        /// <paramref name="resourceName"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException"><paramref name="resourceName"/> is empty.
        /// </exception>
        /// <exception cref="InvalidOperationException"><para>The specified resource was not found.
        /// </para></exception>
        /// <exception cref="InvalidCastException">The value of the resolved resource could not be
        /// cast to the type specified by <typeparamref name="T"/>.</exception>
        public static T ResolveResource<T>(Type resourceType, string resourceName)
        {
            if (resourceType == null)
            {
                throw new ArgumentNullException(nameof(resourceType));
            }

            if (resourceName == null)
            {
                throw new ArgumentNullException(nameof(resourceName));
            }

            if (resourceName.Length == 0)
            {
                throw new ArgumentException("Value must not be empty.", nameof(resourceName));
            }

            var propertyInfo = resourceType.GetProperty(
                resourceName,
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            if (propertyInfo == null)
            {
                throw new InvalidOperationException("Specified resource not found.");
            }

            return (T)propertyInfo.GetValue(null);
        }
    }
}
