﻿// <copyright file="VarianceExample.cs" company="natsnudasoft">
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
    using Common.Model;
    using Common.Variance;
    using Properties;

    /// <summary>
    /// Provides examples of type variance in delegates and interfaces.
    /// </summary>
    public sealed class VarianceExample
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
        /// <exception cref="ArgumentNullException">Thrown when a supplied argument is null.
        /// </exception>
        public VarianceExample(
            IInvariant<Orange> orangeInvariant,
            ICovariant<Banana> bananaCovariant,
            IContravariant<Fruit> fruitContravariant)
        {
            if (orangeInvariant == null)
            {
                throw new ArgumentNullException(
                    nameof(orangeInvariant),
                    Resources.VarianceExampleOrangeInvariantInvalid);
            }

            if (bananaCovariant == null)
            {
                throw new ArgumentNullException(
                    nameof(bananaCovariant),
                    Resources.VarianceExampleBananaCovariantInvalid);
            }

            if (fruitContravariant == null)
            {
                throw new ArgumentNullException(
                    nameof(fruitContravariant),
                    Resources.VarianceExampleFruitContravariantInvalid);
            }

            this.orangeInvariantInterface = orangeInvariant;
            this.bananaCovariantInterface = bananaCovariant;
            this.fruitContravariantInterface = fruitContravariant;
        }

        /// <summary>
        /// Runs the example.
        /// </summary>
        public void RunExample()
        {
            this.InvarianceExample();
            this.CovarianceExample();
            this.ContravarianceExample();
        }

        private static void WriteColoredLine(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        private void InvarianceExample()
        {
            WriteColoredLine(Resources.InvarianceExampleRunning, ConsoleColor.Cyan);

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
            WriteColoredLine(Resources.CovarianceExampleRunning, ConsoleColor.Cyan);

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
            WriteColoredLine(Resources.ContravarianceExampleRunning, ConsoleColor.Cyan);

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