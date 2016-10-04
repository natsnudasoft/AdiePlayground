// <copyright file="MoveCommand.cs" company="natsnudasoft">
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
    /// <summary>
    /// Encapsulates an <see cref="ICommand"/> that will move an <see cref="IRobot"/> a specified
    /// distance.
    /// </summary>
    /// <seealso cref="RobotCommand" />
    internal sealed class MoveCommand : RobotCommand
    {
        private readonly double distanceInMeters;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> that this <see cref="MoveCommand"/> will
        /// execute on.</param>
        /// <param name="distanceInMeters">The distance in metres this <see cref="MoveCommand"/>
        /// will move the specified <see cref="IRobot"/>. Supply a positive value to specify forward
        /// movement, and a negative value to specify reverse movement</param>
        public MoveCommand(IRobot robot, double distanceInMeters)
            : base(robot)
        {
            this.distanceInMeters = distanceInMeters;
        }

        /// <inheritdoc/>
        public override void Execute()
        {
            this.Robot.Move(this.distanceInMeters);
        }

        /// <inheritdoc/>
        public override void Undo()
        {
            this.Robot.Move(-this.distanceInMeters);
        }
    }
}
