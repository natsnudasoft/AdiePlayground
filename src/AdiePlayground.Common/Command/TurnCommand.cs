// <copyright file="TurnCommand.cs" company="natsnudasoft">
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
    /// Encapsulates a command to turn an <see cref="IRobot"/> a specified amount.
    /// </summary>
    /// <seealso cref="RobotCommand" />
    internal sealed class TurnCommand : RobotCommand
    {
        private readonly double angleInRadians;

        /// <summary>
        /// Initializes a new instance of the <see cref="TurnCommand"/> class.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> that this command will execute on.</param>
        /// <param name="angleInRadians">The angle to turn the robot by in radians. Supply a
        /// negative value to specify a left turn, or a positive value to specify a right turn.
        /// </param>
        public TurnCommand(IRobot robot, double angleInRadians)
            : base(robot)
        {
            this.angleInRadians = angleInRadians;
        }

        /// <inheritdoc/>
        public override void Execute()
        {
            this.Robot.Turn(this.angleInRadians);
        }

        /// <inheritdoc/>
        public override void Undo()
        {
            this.Robot.Turn(-this.angleInRadians);
        }
    }
}
