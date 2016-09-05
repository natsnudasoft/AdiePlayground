// <copyright file="DataTypeAttributeConvention.cs" company="natsnudasoft">
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

namespace AdiePlayground.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity.ModelConfiguration.Configuration;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides configuration conventions for a <see cref="DataTypeAttribute"/> found on primitive
    /// properties in the model.
    /// </summary>
    /// <seealso cref="PrimitivePropertyAttributeConfigurationConvention{DataTypeAttribute}" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class DataTypeAttributeConvention :
        PrimitivePropertyAttributeConfigurationConvention<DataTypeAttribute>
    {
        /// <inheritdoc/>
        public override void Apply(
            ConventionPrimitivePropertyConfiguration configuration,
            DataTypeAttribute attribute)
        {
            if (configuration != null && attribute != null)
            {
                if (attribute.DataType == DataType.Date)
                {
                    configuration.HasColumnType("date");
                }
            }
        }
    }
}
