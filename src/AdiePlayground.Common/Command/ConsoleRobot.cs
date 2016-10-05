// <copyright file="ConsoleRobot.cs" company="natsnudasoft">
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
    using System.Globalization;
    using Extensions;

    /// <summary>
    /// Provides an implementation of <see cref="IRobot"/> that writes any received
    /// <see cref="ICommand"/> it receives to the <see cref="Console"/>.
    /// </summary>
    /// <seealso cref="IRobot" />
    internal sealed class ConsoleRobot : IRobot
    {
        /// <inheritdoc/>
        public void Move(double distanceInMeters)
        {
            var isForwards = distanceInMeters >= 0;
            ConsoleExtensions.WriteColoredLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Moved {0} {1:0.00} metres.",
                    isForwards ? "forwards" : "backwards",
                    Math.Abs(distanceInMeters)),
                ConsoleColor.DarkGray);
        }

        /// <inheritdoc/>
        public void Turn(double angleInRadians)
        {
            const double radiansToDegrees = 180D / Math.PI;
            var isLeftTurn = angleInRadians < 0;
            ConsoleExtensions.WriteColoredLine(
                string.Format(
                    CultureInfo.InvariantCulture,
                    "Turned {0} {1:0.00} degrees.",
                    isLeftTurn ? "left" : "right",
                    Math.Abs(angleInRadians * radiansToDegrees)),
                ConsoleColor.DarkGray);
        }

        /// <inheritdoc/>
        public void TurnDrillOn()
        {
            ConsoleExtensions.WriteColoredLine(
                "Turned drill on.",
                ConsoleColor.DarkGray);
        }

        /// <inheritdoc/>
        public void TurnDrillOff()
        {
            ConsoleExtensions.WriteColoredLine(
                "Turned drill off.",
                ConsoleColor.DarkGray);
        }
    }
}
