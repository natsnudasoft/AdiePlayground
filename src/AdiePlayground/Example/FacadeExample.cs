// <copyright file="FacadeExample.cs" company="natsnudasoft">
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

namespace AdiePlayground.Example
{
    using System;
    using System.Threading;
    using Common;
    using Common.Extensions;
    using Common.Facade;
    using Properties;

    /// <summary>
    /// Provides examples of using the Facade pattern.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Example("facade")]
    internal sealed class FacadeExample : IExample
    {
        private readonly GoldMine goldMine;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacadeExample"/> class.
        /// </summary>
        /// <param name="goldMine">The <see cref="GoldMine"/> to use in this example.</param>
        public FacadeExample(GoldMine goldMine)
        {
            ParameterValidation.IsNotNull(goldMine, nameof(goldMine));

            this.goldMine = goldMine;
        }

        /// <inheritdoc/>
        public void Run(CancellationToken cancellationToken)
        {
            ConsoleExtensions.WriteColoredLine(Resources.FacadeExampleRunning, ConsoleColor.Cyan);
            Console.WriteLine(Resources.FacadeExampleOperateMine);
            this.goldMine.Operate();
        }
    }
}
