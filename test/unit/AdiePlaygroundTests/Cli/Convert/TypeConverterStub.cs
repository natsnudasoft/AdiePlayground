// <copyright file="TypeConverterStub.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Cli.Convert
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public sealed class TypeConverterStub : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            bool result;
            if (sourceType == typeof(string))
            {
                result = true;
            }
            else
            {
                result = base.CanConvertFrom(context, sourceType);
            }

            return result;
        }

        public override object ConvertFrom(
            ITypeDescriptorContext context,
            CultureInfo culture,
            object value)
        {
            object result;
            if (value == null)
            {
                result = null;
            }
            else if (value.GetType() == typeof(string))
            {
                result = int.Parse((string)value, CultureInfo.InvariantCulture);
            }
            else
            {
                result = base.ConvertFrom(context, culture, value);
            }

            return result;
        }
    }
}
