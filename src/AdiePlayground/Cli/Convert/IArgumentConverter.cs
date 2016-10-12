// <copyright file="IArgumentConverter.cs" company="natsnudasoft">
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

namespace AdiePlayground.Cli.Convert
{
    using System;

    /// <summary>
    /// Provides an interface which contains members capable of converting a <c>string</c> argument
    /// to a specified <see cref="Type"/>.
    /// </summary>
    internal interface IArgumentConverter
    {
        /// <summary>
        /// Converts the specified argument to the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="argument">The argument to be converted.</param>
        /// <returns>The result of the argument conversion.</returns>
        object Convert(string argument);
    }
}
