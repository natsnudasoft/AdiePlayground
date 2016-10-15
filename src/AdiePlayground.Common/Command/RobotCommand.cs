// <copyright file="RobotCommand.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Command
{
    using System;

    /// <summary>
    /// Provides an abstract base for encapsulating an <see cref="ICommand"/> that will be performed
    /// on an <see cref="IRobot"/>.
    /// </summary>
    /// <seealso cref="ICommand"/>
    internal abstract class RobotCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RobotCommand"/> class.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> that this <see cref="RobotCommand"/> will
        /// execute on.</param>
        /// <exception cref="ArgumentNullException"><paramref name="robot"/> is
        /// <see langword="null"/>.</exception>
        protected RobotCommand(IRobot robot)
        {
            if (robot == null)
            {
                throw new ArgumentNullException(nameof(robot));
            }

            this.Robot = robot;
        }

        /// <summary>
        /// Gets the <see cref="IRobot"/> that this <see cref="RobotCommand"/> will execute on.
        /// </summary>
        public IRobot Robot { get; }

        /// <inheritdoc/>
        public abstract void Execute();

        /// <inheritdoc/>
        public abstract void Undo();
    }
}
