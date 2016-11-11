// <copyright file="ConsoleShopAssistant.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common.TemplateMethod
{
    using System;
    using static System.FormattableString;

    /// <summary>
    /// Provides a concrete derived class as an example of the Template Method pattern.
    /// </summary>
    /// <seealso cref="ConsoleWorker" />
    public sealed class ConsoleShopAssistant : ConsoleWorker
    {
        /// <inheritdoc/>
        protected internal override void Work()
        {
            Console.WriteLine(Invariant($"{this.Name} serves some customers."));
        }

        /// <inheritdoc/>
        protected internal override void Relax()
        {
            Console.WriteLine(Invariant($"{this.Name} goes to the pub."));
        }
    }
}
