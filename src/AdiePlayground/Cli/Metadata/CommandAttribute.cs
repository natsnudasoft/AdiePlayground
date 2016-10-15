// <copyright file="CommandAttribute.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Reflection;
    using Autofac.Extras.AttributeMetadata;
    using Common;
    using static System.FormattableString;

    /// <summary>
    /// Specifies the details of an <see cref="ICommand"/>.
    /// </summary>
    /// <seealso cref="Attribute" />
    /// <seealso cref="IMetadataProvider" />
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Design",
        "CA1019:DefineAccessorsForAttributeArguments",
        Justification = "HelpText is retrieved by arguments at runtime.")]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    [MetadataAttribute]
    public sealed class CommandAttribute : Attribute, IMetadataProvider
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandAttribute"/> class.
        /// </summary>
        /// <param name="group">The name of the group that an <see cref="ICommand"/> specified by
        /// this <see cref="CommandAttribute"/> is part of.</param>
        /// <param name="name">The name of an <see cref="ICommand"/> specified by this
        /// <see cref="CommandAttribute"/>.</param>
        /// <param name="resourceType">The <see cref="Type"/> to use to resolve resource names.
        /// </param>
        /// <param name="helpTextResourceName">The name of the resource that contains the help text
        /// of an <see cref="ICommand"/> specified by this <see cref="CommandAttribute"/>.</param>
        /// <exception cref="ArgumentException"><para><paramref name="group"/>,
        /// <paramref name="name"/>, or <paramref name="helpTextResourceName"/> is empty.
        /// </para><para>-or-</para><para><paramref name="name"/> contains spaces.</para>
        /// </exception>
        /// <exception cref="ArgumentNullException"><paramref name="group"/>,
        /// <paramref name="name"/>, <paramref name="resourceType"/>, or
        /// <paramref name="helpTextResourceName"/> is <see langword="null"/>.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            MessageId = "1",
            Justification = "Already validated.")]
        public CommandAttribute(
            string group,
            string name,
            Type resourceType,
            string helpTextResourceName)
        {
            ValidateString(group, nameof(group));
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
            this.Group = group;
            this.Name = name;
            this.HelpText =
                ResourceTypeResolver.ResolveResource<string>(resourceType, helpTextResourceName);
        }

        /// <summary>
        /// Gets the name of the group that an <see cref="ICommand"/> specified by this
        /// <see cref="CommandAttribute"/> is part of.
        /// </summary>
        public string Group { get; }

        /// <summary>
        /// Gets the name of an <see cref="ICommand"/> specified by this
        /// <see cref="CommandAttribute"/>.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the help text of an <see cref="ICommand"/> specified by this
        /// <see cref="CommandAttribute"/>.
        /// </summary>
        public string HelpText { get; }

        /// <inheritdoc/>
        IDictionary<string, object> IMetadataProvider.GetMetadata(Type targetType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }

            var parameterMetadata = targetType
                .GetProperties()
                .Where(p => p.IsDefined(typeof(CommandParameterAttribute), false) && p.CanWrite)
                .Select(p => p
                    .GetCustomAttribute<CommandParameterAttribute>(false)
                    .GetMetadata(p))
                .OrderBy(m => m.Index)
                .ToArray();
            if (!parameterMetadata
                .Select(m => m.Index)
                .SequenceEqual(Enumerable.Range(0, parameterMetadata.Length)))
            {
                throw new InvalidOperationException(
                    Invariant($"Command parameter index mismatch on {targetType.Name}."));
            }

            if (!parameterMetadata
                .SkipWhile(m => m.Required)
                .All(m => !m.Required))
            {
                throw new InvalidOperationException(
                    "A required parameter cannot appear after an optional one on " +
                    targetType.Name + ".");
            }

            var metadata = this
                .GetType()
                .GetProperties()
                .Where(p => p.CanRead &&
                    p.DeclaringType != null &&
                    p.DeclaringType != typeof(Attribute))
                .ToDictionary(p => p.Name, p => p.GetValue(this));
            metadata.Add(nameof(CommandMetadata.ParametersMetadata), parameterMetadata);
            return metadata;
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
