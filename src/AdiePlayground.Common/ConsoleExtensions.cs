// <copyright file="ConsoleExtensions.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common
{
    using System;

    /// <summary>
    /// Provides extensions for the <see cref="Console"/> class.
    /// </summary>
    public static class ConsoleExtensions
    {
        private static readonly object ConsoleColorLock = new object();

        /// <summary>
        /// Writes a single coloured line to the <see cref="Console"/>. The colour is reset after.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <param name="color">The colour of the text.</param>
        public static void WriteColoredLine(string value, ConsoleColor color)
        {
            lock (ConsoleColorLock)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(value);
                Console.ResetColor();
            }
        }
    }
}
