// <copyright file="ArgumentConverterResolveException.cs" company="natsnudasoft">
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
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The exception that is thrown when an <see cref="IArgumentConverter"/> can not be resolved on
    /// a property.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public sealed class ArgumentConverterResolveException : Exception
    {
        private const string DefaultMessage = "Could not resolve argument converter.";

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentConverterResolveException"/> class.
        /// </summary>
        public ArgumentConverterResolveException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentConverterResolveException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.
        /// </param>
        public ArgumentConverterResolveException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentConverterResolveException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// </param>
        public ArgumentConverterResolveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentConverterResolveException"/> class.
        /// </summary>
        /// <param name="argumentName">The name of the property that an
        /// <see cref="IArgumentConverter"/> could not be resolved for.</param>
        /// <param name="argumentType">The type of the property that an
        /// <see cref="IArgumentConverter"/> could not be resolved for.</param>
        public ArgumentConverterResolveException(string argumentName, Type argumentType)
            : base(DefaultMessage)
        {
            this.ArgumentName = argumentName;
            this.ArgumentType = argumentType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentConverterResolveException"/> class.
        /// </summary>
        /// <param name="argumentName">The name of the property that an
        /// <see cref="IArgumentConverter"/> could not be resolved for.</param>
        /// <param name="argumentType">The type of the property that an
        /// <see cref="IArgumentConverter"/> could not be resolved for.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// </param>
        public ArgumentConverterResolveException(
            string argumentName,
            Type argumentType,
            Exception innerException)
            : base(DefaultMessage, innerException)
        {
            this.ArgumentName = argumentName;
            this.ArgumentType = argumentType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentConverterResolveException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that stores all the data needed
        /// to serialize or deserialize an object.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that describes the source and
        /// destination of a given serialized stream.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private ArgumentConverterResolveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            this.ArgumentName = info.GetString(nameof(this.ArgumentName));
            var argumentTypeName = info.GetString(nameof(this.ArgumentType));
            this.ArgumentType = Type.GetType(argumentTypeName);
        }

        /// <summary>
        /// Gets the name of the property that an <see cref="IArgumentConverter"/> could not be
        /// resolved for.
        /// </summary>
        public string ArgumentName { get; }

        /// <summary>
        /// Gets the type of the property that an <see cref="IArgumentConverter"/> could not be
        /// resolved for.
        /// </summary>
        public Type ArgumentType { get; }

        /// <inheritdoc/>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);
            info.AddValue(nameof(this.ArgumentName), this.ArgumentName);
            info.AddValue(nameof(this.ArgumentType), this.ArgumentType.AssemblyQualifiedName);
        }
    }
}
