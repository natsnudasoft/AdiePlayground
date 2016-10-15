// <copyright file="CommandResolveException.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// The exception that is thrown when an <see cref="ICommand"/> can not be resolved from a
    /// command name.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public sealed class CommandResolveException : Exception
    {
        private const string DefaultMessage = "Command could not be resolved.";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResolveException"/> class.
        /// </summary>
        public CommandResolveException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResolveException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.
        /// </param>
        public CommandResolveException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResolveException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// </param>
        public CommandResolveException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResolveException"/> class.
        /// </summary>
        /// <param name="commandName">The command name that was used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        /// <param name="commandArgs">The arguments that were used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        public CommandResolveException(string commandName, IEnumerable<string> commandArgs)
            : base(DefaultMessage)
        {
            this.CommandName = commandName;
            this.CommandArgs = new ReadOnlyCollection<string>(commandArgs.ToArray());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResolveException"/> class.
        /// </summary>
        /// <param name="commandName">The command name that was used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        /// <param name="commandArgs">The arguments that were used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// </param>
        public CommandResolveException(
            string commandName,
            IEnumerable<string> commandArgs,
            Exception innerException)
            : base(DefaultMessage, innerException)
        {
            this.CommandName = commandName;
            this.CommandArgs = new ReadOnlyCollection<string>(commandArgs.ToArray());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandResolveException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that stores all the data needed
        /// to serialize or deserialize an object.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that describes the source and
        /// destination of a given serialized stream.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private CommandResolveException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.CommandName = info.GetString(nameof(this.CommandName));
            var commandArgs = (string[])info.GetValue(nameof(this.CommandArgs), typeof(string[]));
            this.CommandArgs = new ReadOnlyCollection<string>(commandArgs);
        }

        /// <summary>
        /// Gets the command name that was used to try to resolve an <see cref="ICommand"/>.
        /// </summary>
        public string CommandName { get; }

        /// <summary>
        /// Gets the arguments that were used to try to resolve an <see cref="ICommand"/>.
        /// </summary>
        public IReadOnlyList<string> CommandArgs { get; }

        /// <inheritdoc/>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(this.CommandName), this.CommandName);
            info.AddValue(nameof(this.CommandArgs), this.CommandArgs.ToArray());
        }
    }
}
