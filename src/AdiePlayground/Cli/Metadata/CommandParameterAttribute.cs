// <copyright file="CommandParameterAttribute.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli.Metadata
{
    using System;
    using System.Reflection;
    using Common;
    using Convert;

    /// <summary>
    /// Specifies the details of an <see cref="ICommand"/> parameter.
    /// </summary>
    /// <seealso cref="Attribute" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design",
        "CA1019:DefineAccessorsForAttributeArguments",
        Justification = "HelpText is retrieved by arguments at runtime.")]
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CommandParameterAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParameterAttribute"/> class.
        /// </summary>
        /// <param name="index">The index of the parameter on an <see cref="ICommand"/> specified by
        /// this <see cref="CommandParameterAttribute"/>.</param>
        /// <param name="name">The name of the parameter on an <see cref="ICommand"/> specified by
        /// this <see cref="CommandParameterAttribute"/>.</param>
        /// <param name="resourceType">The <see cref="Type"/> to use to resolve resource names.
        /// </param>
        /// <param name="helpTextResourceName">The name of the resource that contains the help text
        /// of the parameter on an <see cref="ICommand"/> specified by this
        /// <see cref="CommandParameterAttribute"/></param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.
        /// </exception>
        /// <exception cref="ArgumentException"><para><paramref name="name"/>, or
        /// <paramref name="helpTextResourceName"/> is empty.</para><para>-or-</para><para>
        /// <paramref name="name"/> contains spaces.</para></exception>
        /// <exception cref="ArgumentNullException"><paramref name="name"/>,
        /// <paramref name="resourceType"/>, or <paramref name="helpTextResourceName"/> is
        /// <see langword="null"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "1",
            Justification = "Already validated.")]
        public CommandParameterAttribute(
            int index,
            string name,
            Type resourceType,
            string helpTextResourceName)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(index),
                    "Value must be greater than or equal to 0.");
            }

            ValidateString(name, nameof(name));
            if (name.Contains(" "))
            {
                throw new ArgumentException("Value cannot contain spaces.", nameof(name));
            }

            if (resourceType == null)
            {
                throw new ArgumentNullException(nameof(resourceType));
            }

            ValidateString(helpTextResourceName, nameof(helpTextResourceName));

            this.Index = index;
            this.Name = name;
            this.HelpText =
                ResourceTypeResolver.ResolveResource<string>(resourceType, helpTextResourceName);
        }

        /// <summary>
        /// Gets the index of the parameter on an <see cref="ICommand"/> specified by this
        /// <see cref="CommandParameterAttribute"/>.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Gets the name of the parameter on an <see cref="ICommand"/> specified by this
        /// <see cref="CommandParameterAttribute"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the help text of the parameter on an <see cref="ICommand"/> specified by this
        /// <see cref="CommandParameterAttribute"/>.
        /// </summary>
        public string HelpText { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the parameter on an <see cref="ICommand"/>
        /// specified by this <see cref="CommandParameterAttribute"/> is required.
        /// </summary>
        public bool Required { get; set; } = true;

        /// <summary>
        /// Gets or sets the default of the parameter on an <see cref="ICommand"/> specified by this
        /// <see cref="CommandParameterAttribute"/>.
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// Creates a <see cref="CommandParameterMetadata"/> instance from this
        /// <see cref="CommandParameterAttribute"/>.
        /// </summary>
        /// <param name="targetPropertyInfo">The <see cref="PropertyInfo"/> of the target of this
        /// <see cref="CommandParameterAttribute"/>.
        /// </param>
        /// <returns>An instance of <see cref="CommandParameterMetadata"/>.</returns>
        internal CommandParameterMetadata GetMetadata(PropertyInfo targetPropertyInfo)
        {
            return new CommandParameterMetadata
            {
                Index = this.Index,
                Name = this.Name,
                HelpText = this.HelpText,
                Required = this.Required,
                DefaultValue = this.DefaultValue,
                Converter = ArgumentConverterResolver.Resolve(targetPropertyInfo),
                PropertyInfo = targetPropertyInfo
            };
        }

        private static void ValidateString(string value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }

            if (value.Length == 0)
            {
                throw new ArgumentException("Value must not be empty.", paramName);
            }
        }
    }
}
