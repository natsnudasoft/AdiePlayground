// <copyright file="CommonModule.cs" company="natsnudasoft">
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

namespace AdiePlayground.Common
{
    using Autofac;
    using Model;
    using Variance;

    /// <summary>
    /// Provides Dependency Injection registration module for
    /// <see cref="AdiePlayground.Common"/>.
    /// </summary>
    /// <seealso cref="Module" />
    public sealed class CommonModule : Module
    {
        /// <inheritdoc/>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.Register(c => new OrangeInvariant()).As<IInvariant<Orange>>();
            builder.Register(c => new BananaCovariant()).As<ICovariant<Banana>>();
            builder.Register(c => new FruitContravariant()).As<IContravariant<Fruit>>();
        }
    }
}
