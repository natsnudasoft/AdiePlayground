// <copyright file="CommandNotFoundException.cs" company="natsnudasoft">
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
    /// The exception that is thrown when an <see cref="ICommand"/> with a specified name could not
    /// be found.
    /// </summary>
    /// <seealso cref="Exception" />
    [Serializable]
    public sealed class CommandNotFoundException : Exception
    {
        private const string DefaultMessage = "Invalid command name.";

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        public CommandNotFoundException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.
        /// </param>
        public CommandNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// </param>
        public CommandNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="commandName">The command name that was used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        /// <param name="commandArgs">The arguments that were used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        public CommandNotFoundException(string commandName, IEnumerable<string> commandArgs)
            : base(DefaultMessage)
        {
            this.CommandName = commandName;
            this.CommandArgs = new ReadOnlyCollection<string>(commandArgs.ToArray());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class.
        /// </summary>
        /// <param name="commandName">The command name that was used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        /// <param name="commandArgs">The arguments that were used to try to resolve an
        /// <see cref="ICommand"/>.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.
        /// </param>
        public CommandNotFoundException(
            string commandName,
            IEnumerable<string> commandArgs,
            Exception innerException)
            : base(DefaultMessage, innerException)
        {
            this.CommandName = commandName;
            this.CommandArgs = new ReadOnlyCollection<string>(commandArgs.ToArray());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandNotFoundException"/> class
        /// with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that stores all the data needed
        /// to serialize or deserialize an object.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that describes the source and
        /// destination of a given serialized stream.</param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        private CommandNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

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
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            base.GetObjectData(info, context);
            info.AddValue(nameof(this.CommandName), this.CommandName);
            info.AddValue(nameof(this.CommandArgs), this.CommandArgs.ToArray());
        }
    }
}
