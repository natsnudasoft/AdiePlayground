// <copyright file="ExampleAttribute.cs" company="natsnudasoft">
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

namespace AdiePlayground.Example
{
    using System;
    using System.ComponentModel.Composition;
    using Common;

    /// <summary>
    /// Specifies the details of an <see cref="IExample"/>.
    /// </summary>
    /// <seealso cref="Attribute" />
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    [MetadataAttribute]
    public sealed class ExampleAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExampleAttribute"/> class.
        /// </summary>
        /// <param name="name">the name of an <see cref="IExample"/> specified by this
        /// <see cref="ExampleAttribute"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is
        /// <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> is empty.</exception>
        public ExampleAttribute(string name)
        {
            ParameterValidation.IsNotNull(name, nameof(name));
            ParameterValidation.IsNotEmpty(name, nameof(name));
            ParameterValidation.IsFalse(
                name.Contains(" "),
                "Value cannot contain spaces.",
                nameof(name));

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of an <see cref="IExample"/> specified by this
        /// <see cref="ExampleAttribute"/>.
        /// </summary>
        public string Name { get; }
    }
}
