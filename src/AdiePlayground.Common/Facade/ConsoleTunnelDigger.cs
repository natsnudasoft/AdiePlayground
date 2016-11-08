// <copyright file="ConsoleTunnelDigger.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Facade
{
    using System;
    using Extensions;

    /// <summary>
    /// Provides an implementation of <see cref="ITunnelDigger"/> that simply outputs to the
    /// <see cref="Console"/> .
    /// </summary>
    /// <seealso cref="ITunnelDigger" />
    internal sealed class ConsoleTunnelDigger : ITunnelDigger
    {
        /// <inheritdoc/>
        public void DigTunnel()
        {
            ConsoleExtensions.WriteColoredLine(
                "Tunnel digger digs a nice tunnel.",
                ConsoleColor.DarkGray);
        }
    }
}
