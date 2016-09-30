// <copyright file="IMessageBoardObserver.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.Observer
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides an interface for an observer to a <see cref="MessageBoard"/>.
    /// </summary>
    public interface IMessageBoardObserver
    {
        /// <summary>
        /// Called when the <see cref="MessageBoard"/> this <see cref="IMessageBoardObserver"/> is
        /// attached to changes state.
        /// </summary>
        /// <param name="messages">The current messages on the <see cref="MessageBoard"/>
        /// this <see cref="IMessageBoardObserver"/> is attached to.</param>
        void Update(IEnumerable<string> messages);
    }
}