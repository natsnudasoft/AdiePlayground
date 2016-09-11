﻿// <copyright file="VarianceModuleTests.cs" company="natsnudasoft">
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

namespace AdiePlayground.DataTests.Services
{
    using Autofac;
    using Common.Model;
    using Common.Variance;
    using NUnit.Framework;

    /// <summary>
    /// Tests the <see cref="VarianceModule"/> class.
    /// </summary>
    [TestFixture]
    public sealed class VarianceModuleTests
    {
        /// <summary>
        /// Tests the constructor with a valid connection string factory.
        /// </summary>
        [Test]
        public void Constructor_DoesNotThrow()
        {
            VarianceModule varianceModule = null;
            Assert.DoesNotThrow(() => varianceModule = new VarianceModule());
            var builder = new ContainerBuilder();
            builder.RegisterModule(varianceModule);
            var container = builder.Build();
            var resolved = container.Resolve<IInvariant<Orange>>();

            Assert.That(resolved, Is.Not.Null);
        }
    }
}