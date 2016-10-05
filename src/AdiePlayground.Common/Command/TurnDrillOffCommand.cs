// <copyright file="TurnDrillOffCommand.cs" company="natsnudasoft">
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
    /// Encapsulates an <see cref="ICommand"/> that turns a drill of an <see cref="IRobot"/> on or
    /// off.
    /// </summary>
    /// <seealso cref="RobotCommand" />
    internal sealed class TurnDrillOffCommand : RobotCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnDrillOffCommand"/> class.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> that this <see cref="TurnDrillOffCommand"/>
        /// will execute on.</param>
        public TurnDrillOffCommand(IRobot robot)
            : base(robot)
        {
        }

        /// <inheritdoc/>
        public override void Execute()
        {
            this.Robot.TurnDrillOff();
        }

        /// <inheritdoc/>
        public override void Undo()
        {
            this.Robot.TurnDrillOn();
        }
    }
}
