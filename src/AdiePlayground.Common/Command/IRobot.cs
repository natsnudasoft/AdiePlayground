// <copyright file="IRobot.cs" company="natsnudasoft">
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
    /// Provides an interface for a robot which can move forwards and backwards, turn left and right
    /// and operate a mining drill.
    /// </summary>
    public interface IRobot
    {
        /// <summary>
        /// Moves the <see cref="IRobot"/> by the specified amount in metres.
        /// </summary>
        /// <param name="distanceInMeters">The distance to move the <see cref="IRobot"/> in metres.
        /// Supply a positive value to specify forward movement, and a negative value to specify
        /// reverse movement.</param>
        void Move(double distanceInMeters);

        /// <summary>
        /// Turns the <see cref="IRobot"/> by the specified angle in radians.
        /// </summary>
        /// <param name="angleInRadians">The angle to turn the <see cref="IRobot"/> in radians.
        /// Supply a negative value to specify a left turn, or a positive value to specify a right
        /// turn.</param>
        void Turn(double angleInRadians);

        /// <summary>
        /// Turns the drill of this <see cref="IRobot"/> on.
        /// </summary>
        void TurnDrillOn();

        /// <summary>
        /// Turns the drill of this <see cref="IRobot"/> off.
        /// </summary>
        void TurnDrillOff();
    }
}