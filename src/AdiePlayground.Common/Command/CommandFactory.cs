// <copyright file="CommandFactory.cs" company="natsnudasoft">
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
    /// Encapsulates a method that will create instances of <see cref="ICommand"/> from a specified
    /// command name and pass the specified commands to it.
    /// </summary>
    /// <param name="commandName">The name of the <see cref="ICommand"/> to create an instance of.
    /// </param>
    /// <param name="parameters">The parameters to pass to the <see cref="ICommand"/>.</param>
    /// <returns>The <see cref="ICommand"/> instance that was created.</returns>
    public delegate ICommand CommandFactory(string commandName, params object[] parameters);
}
