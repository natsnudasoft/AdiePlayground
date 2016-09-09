// <copyright file="ConnectionStringFactory.cs" company="natsnudasoft">
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
    using Data.Services;

    /// <summary>
    /// Provides a connection string factory that gets a connection string from the application
    /// settings.
    /// </summary>
    /// <seealso cref="IConnectionStringFactory" />
    public sealed class ConnectionStringFactory : IConnectionStringFactory
    {
        /// <inheritdoc/>
        public string CreateConnectionString()
        {
            return Properties.Settings.Default.ConnectionString;
        }
    }
}
