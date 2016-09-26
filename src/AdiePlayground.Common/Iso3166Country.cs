// <copyright file="Iso3166Country.cs" company="natsnudasoft">
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
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a country in ISO 3166-1 Alpha-2 country code form.
    /// </summary>
    public sealed class Iso3166Country
    {
        private readonly Lazy<RegionInfo> regionInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Iso3166Country" /> class with the
        /// specified ISO 3166-1 alpha-2 country code.
        /// </summary>
        /// <param name="countryCode">The ISO 3166-1 alpha-2 country code.</param>
        /// <exception cref="ArgumentNullException"><paramref name="countryCode"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException"><paramref name="countryCode"/> is an invalid ISO
        /// 3166-1 alpha-2 country code.
        /// supplied.</exception>
        public Iso3166Country(string countryCode)
        {
            if (countryCode == null)
            {
                throw new ArgumentNullException(nameof(countryCode));
            }

            this.CountryCode = countryCode.ToUpperInvariant();
            string countryName;
            if (countryCode.Length != 2 || !Iso3166Helper.CountryCodeMappingsInternal
                .TryGetValue(this.CountryCode, out countryName))
            {
                throw new ArgumentException(
                    "Value must be a valid ISO 3166-1 alpha-2 country code.",
                    nameof(countryCode));
            }

            this.CountryName = countryName;

            // Use Lazy for its exception caching so countries that don't have region info available
            // don't throw an exception here in the constructor.
            this.regionInfo = new Lazy<RegionInfo>(() => new RegionInfo(this.CountryCode));
        }

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code.
        /// </summary>
        public string CountryCode { get; }

        /// <summary>
        /// Gets the name of the country.
        /// </summary>
        public string CountryName { get; }

        /// <summary>
        /// Gets the region information (if available) for the ISO 3166-1 Alpha-2 country code.
        /// </summary>
        /// <remarks>This value is lazy loaded.</remarks>
        public RegionInfo RegionInfo
        {
            get { return this.regionInfo.Value; }
        }
    }
}
