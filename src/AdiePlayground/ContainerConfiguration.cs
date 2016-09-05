// <copyright file="ContainerConfiguration.cs" company="natsnudasoft">
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

namespace AdiePlayground
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Autofac;
    using Data;
    using Data.Services;

    /// <summary>
    /// Provides configuration for the IoC container.
    /// </summary>
    public static class ContainerConfiguration
    {
        /// <summary>
        /// Configures the IoC container.
        /// </summary>
        /// <returns>A configured IoC container.</returns>
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DataServicesModule(new ConnectionStringFactory()));
            return builder.Build();
        }
    }
}
