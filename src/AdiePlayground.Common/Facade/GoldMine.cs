// <copyright file="GoldMine.cs" company="natsnudasoft">
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
    /// <summary>
    /// Provides an example of a Facade class for a fictitious subsystem representing operations in
    /// a gold mine.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Naming",
        "CA1702:CompoundWordsShouldBeCasedCorrectly",
        MessageId = nameof(GoldMine),
        Justification = "Not a compound word.")]
    public sealed class GoldMine
    {
        private readonly ITunnelDigger tunnelDigger;
        private readonly IGoldProspector goldProspector;
        private readonly IGoldMiner goldMiner;
        private readonly ICartOperator cartOperator;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoldMine"/> class.
        /// </summary>
        public GoldMine()
        {
            this.tunnelDigger = new ConsoleTunnelDigger();
            this.goldProspector = new ConsoleGoldProspector();
            this.goldMiner = new ConsoleGoldMiner();
            this.cartOperator = new ConsoleCartOperator();
        }

        /// <summary>
        /// Operates the gold mine.
        /// </summary>
        public void Operate()
        {
            this.tunnelDigger.DigTunnel();
            this.goldProspector.SearchForGold();
            this.goldMiner.MineGold();
            this.cartOperator.MoveTo("gold");
            this.cartOperator.PickUpGold();
            this.cartOperator.MoveTo("surface");
            this.cartOperator.DepositGold();
        }
    }
}
