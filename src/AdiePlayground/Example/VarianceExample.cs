// <copyright file="VarianceExample.cs" company="natsnudasoft">
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
    using Common.Model;
    using Common.Variance;
    using Properties;

    /// <summary>
    /// Provides examples of type variance in delegates and interfaces.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Instantiated by IoC container.")]
    [Example("variance")]
    internal sealed class VarianceExample : IExample
    {
        private const int LemonQuality = 90;

        private readonly IInvariant<Orange> orangeInvariantInterface;
        private readonly ICovariant<Banana> bananaCovariantInterface;
        private readonly IContravariant<Fruit> fruitContravariantInterface;
        private readonly Invariant<Fruit> fruitInvariantDelegate = f => f.Quality;
        private readonly Covariant<Lemon> lemonCovariantDelegate = () => new Lemon(LemonQuality);
        private readonly Contravariant<Fruit> fruitContravariantDelegate = f => f.Quality;

        /// <summary>
        /// Initializes a new instance of the <see cref="VarianceExample"/> class.
        /// </summary>
        /// <param name="orangeInvariant">An interface with an invariant type of
        /// <see cref="Orange"/>.</param>
        /// <param name="bananaCovariant">An interface with a covariant type of
        /// <see cref="Banana"/>.</param>
        /// <param name="fruitContravariant">An interface with a contravariant type of
        /// <see cref="Fruit"/>.</param>
        /// <exception cref="ArgumentNullException"><paramref name="orangeInvariant"/>,
        /// <paramref name="bananaCovariant"/>, or <paramref name="fruitContravariant"/> is
        /// <see langword="null"/>.</exception>
        public VarianceExample(
            IInvariant<Orange> orangeInvariant,
            ICovariant<Banana> bananaCovariant,
            IContravariant<Fruit> fruitContravariant)
        {
            ParameterValidation.IsNotNull(orangeInvariant, nameof(orangeInvariant));
            ParameterValidation.IsNotNull(bananaCovariant, nameof(bananaCovariant));
            ParameterValidation.IsNotNull(fruitContravariant, nameof(fruitContravariant));

            this.orangeInvariantInterface = orangeInvariant;
            this.bananaCovariantInterface = bananaCovariant;
            this.fruitContravariantInterface = fruitContravariant;
        }

        /// <inheritdoc/>
        public void Run(CancellationToken cancellationToken)
        {
            this.InvarianceExample();
            this.CovarianceExample();
            this.ContravarianceExample();
        }

        private void InvarianceExample()
        {
            ConsoleExtensions
                .WriteColoredLine(Resources.InvarianceExampleRunning, ConsoleColor.Cyan);

            // An invariant type in an interface cannot be converted to either something more
            // general or something more specific. e.g. The following lines do not compile:
            /*
             * IInvariant<object> generalInvariantInterface = this.orangeInvariantInterface;
             * InvariantDelegate<Banana> specificInvariantDelegate = this.fruitInvariantDelegate;
             * InvariantDelegate<object> generalInvariantDelegate = this.fruitInvariantDelegate;
             */

            const int OrangeQuality = 90;
            const int LemonQuality = 45;
            var orangeValue = this.orangeInvariantInterface.GetValue(new Orange(OrangeQuality));
            var fruitValue = this.fruitInvariantDelegate(new Lemon(LemonQuality));

            Console.WriteLine(
                Resources.InvarianceExampleOrangeValue,
                nameof(orangeValue),
                orangeValue);
            Console.WriteLine(
                Resources.InvarianceExampleFruitValue,
                nameof(fruitValue),
                fruitValue);
            Console.WriteLine();
        }

        private void CovarianceExample()
        {
            ConsoleExtensions
                .WriteColoredLine(Resources.CovarianceExampleRunning, ConsoleColor.Cyan);

            // A covariant type in an interface or delegate can be converted to a more GENERAL type.

            // Interface: ICovariant<Banana> to ICovariant<Fruit>
            ICovariant<Fruit> fruitCovariantInterface = this.bananaCovariantInterface;

            // Delegate: Covariant<Lemon> to Covariant<object>
            Covariant<object> objectCovariantDelegate = this.lemonCovariantDelegate;

            const int FruitQuality = 85;
            var fruit = fruitCovariantInterface.Create(FruitQuality);
            var obj = objectCovariantDelegate();

            Console.WriteLine(Resources.CovarianceExampleFruitColor, fruit.Name, fruit.Color);
            Console.WriteLine(Resources.CovarianceExampleObjString, nameof(obj), obj.ToString());
            Console.WriteLine();
        }

        private void ContravarianceExample()
        {
            ConsoleExtensions
                .WriteColoredLine(Resources.ContravarianceExampleRunning, ConsoleColor.Cyan);

            // A contravariant type in an interface or delegate can be converted to a more SPECIFIC
            // type.

            // Interface: IContravariant<Fruit> to IContravariant<Apple>
            IContravariant<Apple> appleContravariantInterface = this.fruitContravariantInterface;

            // Delegate: Contravariant<Fruit> to Contravariant<Orange>
            Contravariant<Orange> orangeContravariantDelegate =
                this.fruitContravariantDelegate;

            const int AppleQuality = 75;
            const int OrangeQuality = 60;
            var appleValue = appleContravariantInterface.GetValue(
                new Apple(AppleColor.Red, AppleQuality));
            var orangeValue = orangeContravariantDelegate(new Orange(OrangeQuality));

            Console.WriteLine(
                Resources.ContravarianceExampleAppleValue,
                nameof(appleValue),
                appleValue);
            Console.WriteLine(
                Resources.ContravarianceExampleOrangeValue,
                nameof(orangeValue),
                orangeValue);
            Console.WriteLine();
        }
    }
}
