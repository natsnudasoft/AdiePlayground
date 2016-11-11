// <copyright file="ConsoleWorker.cs" company="natsnudasoft">
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
    /// Provides an abstract base class as an example of the Template Method pattern.
    /// </summary>
    public abstract class ConsoleWorker
    {
        /// <summary>
        /// Gets the name of the type of this <see cref="ConsoleWorker"/>.
        /// </summary>
        protected string Name => this.GetType().Name;

        /// <summary>
        /// Performs the daily routine of this <see cref="ConsoleWorker"/>. Provides an example of
        /// the Template Method.
        /// </summary>
        public void PerformDailyRoutine()
        {
            this.WakeUp();
            this.EatBreakfast();
            this.GoToWork();
            this.Work();
            this.GoHome();
            this.Relax();
            this.Sleep();
        }

        /// <summary>
        /// The eat breakfast step in the daily routine Template Method.
        /// </summary>
        protected internal virtual void EatBreakfast()
        {
            Console.WriteLine(Invariant($"{this.Name} doesn't eat breakfast."));
        }

        /// <summary>
        /// The work step in the daily routine Template Method.
        /// </summary>
        protected internal abstract void Work();

        /// <summary>
        /// The relax step in the daily routine Template Method.
        /// </summary>
        protected internal abstract void Relax();

        private void WakeUp()
        {
            Console.WriteLine(Invariant($"{this.Name} wakes up."));
        }

        private void GoToWork()
        {
            Console.WriteLine(Invariant($"{this.Name} goes to work."));
        }

        private void GoHome()
        {
            Console.WriteLine(Invariant($"{this.Name} goes home."));
        }

        private void Sleep()
        {
            Console.WriteLine(Invariant($"{this.Name} goes to sleep."));
        }
    }
}
