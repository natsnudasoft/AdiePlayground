// <copyright file="Iso3166CountryTests.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common
{
    using System;
    using System.Collections.Generic;
    using AdiePlayground.Common;
    using NUnit.Framework;

    [TestFixture]
    public class Iso3166CountryTests
    {
        private const string ConstructorCountryCodeParam = "countryCode";
        private const string RegionInfoNameParam = "name";

        private static readonly IEnumerable<string> InvalidCountryCodes =
            new[] { "Aa", "AA", "aa", string.Empty, "AAAA" };

        private static readonly IEnumerable<KeyValuePair<string, string>> ValidCountries =
            new[]
            {
                new KeyValuePair<string, string>("gB", "United Kingdom"),
                new KeyValuePair<string, string>("gb", "United Kingdom"),
                new KeyValuePair<string, string>("Gb", "United Kingdom"),
                new KeyValuePair<string, string>("PA", "Panama"),
                new KeyValuePair<string, string>("KR", "Korea"),
                new KeyValuePair<string, string>(Iso3166Helper.Liechtenstein, "Liechtenstein")
            };

        private static readonly IEnumerable<KeyValuePair<string, string>> ValidCountriesNoRegion =
            new[]
            {
                new KeyValuePair<string, string>("ZZ", "Unknown")
            };

        [Test]
        public void Constructor_InvalidCountryCode_ArgumentException(
            [ValueSource(nameof(InvalidCountryCodes))] string countryCode)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Iso3166Country(countryCode));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCountryCodeParam));
        }

        [Test]
        public void Constructor_NullCountryCode_ArgumentNullException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Iso3166Country(null));
            Assert.That(ex.ParamName, Is.EqualTo(ConstructorCountryCodeParam));
        }

        [Test]
        public void Constructor_ValidCountryCodeValidRegionInfo_CorrectProperties(
            [ValueSource(nameof(ValidCountries))] KeyValuePair<string, string> countryCode)
        {
            var country = new Iso3166Country(countryCode.Key);
            var countryName = Iso3166Helper.CountryCodeMappings[countryCode.Key];
            var regionInfo = country.RegionInfo;

            var upperCountryCode = countryCode.Key.ToUpperInvariant();
            Assert.That(country.CountryCode, Is.EqualTo(upperCountryCode));
            Assert.That(
                country.CountryName,
                Is.EqualTo(countryCode.Value).And.EqualTo(countryName));
            Assert.That(regionInfo, Is.Not.Null);
            Assert.That(regionInfo.TwoLetterISORegionName, Is.EqualTo(upperCountryCode));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1804:RemoveUnusedLocals",
            MessageId = "regionInfo",
            Justification = "We need to assign the property to test for exception.")]
        [Test]
        public void Constructor_ValidCountryCodeInvalidRegionInfo_ArgumentException(
            [ValueSource(nameof(ValidCountriesNoRegion))] KeyValuePair<string, string> countryCode)
        {
            var country = new Iso3166Country(countryCode.Key);
            var countryName = Iso3166Helper.CountryCodeMappings[countryCode.Key];
            var ex = Assert.Throws<ArgumentException>(
                () => { var regionInfo = country.RegionInfo; });

            var upperCountryCode = countryCode.Key.ToUpperInvariant();
            Assert.That(country.CountryCode, Is.EqualTo(upperCountryCode));
            Assert.That(
                country.CountryName,
                Is.EqualTo(countryCode.Value).And.EqualTo(countryName));
            Assert.That(ex.ParamName, Is.EqualTo(RegionInfoNameParam));
        }
    }
}
