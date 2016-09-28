// <copyright file="Iso3166Helper.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Provides a helper class for ISO 3166-1 alpha-2 country codes.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public static class Iso3166Helper
    {
#pragma warning disable CC0021 // Use nameof (Justification = Country names are hard-coded.)
#pragma warning disable SA1124 // Do not use regions (Justification = Reviewed.)
        #region Country code mappings

        /// <summary>
        /// The available country code mappings.
        /// </summary>
        internal static readonly IDictionary<string, string> CountryCodeMappingsInternal =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "AF", "Afghanistan" },
            { "AX", "Åland Islands" },
            { "AL", "Albania" },
            { "DZ", "Algeria" },
            { "AS", "American Samoa" },
            { "AD", "Andorra" },
            { "AO", "Angola" },
            { "AI", "Anguilla" },
            { "AQ", "Antarctica" },
            { "AG", "Antigua and Barbuda" },
            { "AR", "Argentina" },
            { "AM", "Armenia" },
            { "AW", "Aruba" },
            { "AU", "Australia" },
            { "AT", "Austria" },
            { "AZ", "Azerbaijan" },
            { "BS", "Bahamas" },
            { "BH", "Bahrain" },
            { "BD", "Bangladesh" },
            { "BB", "Barbados" },
            { "BY", "Belarus" },
            { "BE", "Belgium" },
            { "BZ", "Belize" },
            { "BJ", "Benin" },
            { "BM", "Bermuda" },
            { "BT", "Bhutan" },
            { "BO", "Bolivia" },
            { "BQ", "Bonaire" },
            { "BA", "Bosnia and Herzegovina" },
            { "BW", "Botswana" },
            { "BV", "Bouvet Island" },
            { "BR", "Brazil" },
            { "IO", "British Indian Ocean Territory" },
            { "BN", "Brunei Darussalam" },
            { "BG", "Bulgaria" },
            { "BF", "Burkina Faso" },
            { "BI", "Burundi" },
            { "KH", "Cambodia" },
            { "CM", "Cameroon" },
            { "CA", "Canada" },
            { "CV", "Cape Verde" },
            { "KY", "Cayman Islands" },
            { "CF", "Central African Republic" },
            { "TD", "Chad" },
            { "CL", "Chile" },
            { "CN", "China" },
            { "CX", "Christmas Island" },
            { "CC", "Cocos (Keeling) Islands" },
            { "CO", "Colombia" },
            { "KM", "Comoros" },
            { "CG", "Congo" },
            { "CD", "Congo" },
            { "CK", "Cook Islands" },
            { "CR", "Costa Rica" },
            { "CI", "Côte d'Ivoire" },
            { "HR", "Croatia" },
            { "CU", "Cuba" },
            { "CW", "Curaçao" },
            { "CY", "Cyprus" },
            { "CZ", "Czech Republic" },
            { "DK", "Denmark" },
            { "DJ", "Djibouti" },
            { "DM", "Dominica" },
            { "DO", "Dominican Republic" },
            { "EC", "Ecuador" },
            { "EG", "Egypt" },
            { "SV", "El Salvador" },
            { "GQ", "Equatorial Guinea" },
            { "ER", "Eritrea" },
            { "EE", "Estonia" },
            { "ET", "Ethiopia" },
            { "FK", "Falkland Islands (Malvinas)" },
            { "FO", "Faroe Islands" },
            { "FJ", "Fiji" },
            { "FI", "Finland" },
            { "FR", "France" },
            { "GF", "French Guiana" },
            { "PF", "French Polynesia" },
            { "TF", "French Southern Territories" },
            { "GA", "Gabon" },
            { "GM", "Gambia" },
            { "GE", "Georgia" },
            { "DE", "Germany" },
            { "GH", "Ghana" },
            { "GI", "Gibraltar" },
            { "GR", "Greece" },
            { "GL", "Greenland" },
            { "GD", "Grenada" },
            { "GP", "Guadeloupe" },
            { "GU", "Guam" },
            { "GT", "Guatemala" },
            { "GG", "Guernsey" },
            { "GN", "Guinea" },
            { "GW", "Guinea-Bissau" },
            { "GY", "Guyana" },
            { "HT", "Haiti" },
            { "HM", "Heard Island and McDonald Islands" },
            { "VA", "Holy See (Vatican City State)" },
            { "HN", "Honduras" },
            { "HK", "Hong Kong" },
            { "HU", "Hungary" },
            { "IS", "Iceland" },
            { "IN", "India" },
            { "ID", "Indonesia" },
            { "IR", "Iran" },
            { "IQ", "Iraq" },
            { "IE", "Ireland" },
            { "IM", "Isle of Man" },
            { "IL", "Israel" },
            { "IT", "Italy" },
            { "JM", "Jamaica" },
            { "JP", "Japan" },
            { "JE", "Jersey" },
            { "JO", "Jordan" },
            { "KZ", "Kazakhstan" },
            { "KE", "Kenya" },
            { "KI", "Kiribati" },
            { "KP", "Korea" },
            { "KR", "Korea" },
            { "KW", "Kuwait" },
            { "KG", "Kyrgyzstan" },
            { "LA", "Lao People's Democratic Republic" },
            { "LV", "Latvia" },
            { "LB", "Lebanon" },
            { "LS", "Lesotho" },
            { "LR", "Liberia" },
            { "LY", "Libya" },
            { "LI", "Liechtenstein" },
            { "LT", "Lithuania" },
            { "LU", "Luxembourg" },
            { "MO", "Macao" },
            { "MK", "Macedonia" },
            { "MG", "Madagascar" },
            { "MW", "Malawi" },
            { "MY", "Malaysia" },
            { "MV", "Maldives" },
            { "ML", "Mali" },
            { "MT", "Malta" },
            { "MH", "Marshall Islands" },
            { "MQ", "Martinique" },
            { "MR", "Mauritania" },
            { "MU", "Mauritius" },
            { "YT", "Mayotte" },
            { "MX", "Mexico" },
            { "FM", "Micronesia" },
            { "MD", "Moldova" },
            { "MC", "Monaco" },
            { "MN", "Mongolia" },
            { "ME", "Montenegro" },
            { "MS", "Montserrat" },
            { "MA", "Morocco" },
            { "MZ", "Mozambique" },
            { "MM", "Myanmar" },
            { "NA", "Namibia" },
            { "NR", "Nauru" },
            { "NP", "Nepal" },
            { "NL", "Netherlands" },
            { "NC", "New Caledonia" },
            { "NZ", "New Zealand" },
            { "NI", "Nicaragua" },
            { "NE", "Niger" },
            { "NG", "Nigeria" },
            { "NU", "Niue" },
            { "NF", "Norfolk Island" },
            { "MP", "Northern Mariana Islands" },
            { "NO", "Norway" },
            { "OM", "Oman" },
            { "PK", "Pakistan" },
            { "PW", "Palau" },
            { "PS", "Palestine" },
            { "PA", "Panama" },
            { "PG", "Papua New Guinea" },
            { "PY", "Paraguay" },
            { "PE", "Peru" },
            { "PH", "Philippines" },
            { "PN", "Pitcairn" },
            { "PL", "Poland" },
            { "PT", "Portugal" },
            { "PR", "Puerto Rico" },
            { "QA", "Qatar" },
            { "RE", "Réunion" },
            { "RO", "Romania" },
            { "RU", "Russian Federation" },
            { "RW", "Rwanda" },
            { "BL", "Saint Barthélemy" },
            { "SH", "Saint Helena" },
            { "KN", "Saint Kitts and Nevis" },
            { "LC", "Saint Lucia" },
            { "MF", "Saint Martin (French part)" },
            { "PM", "Saint Pierre and Miquelon" },
            { "VC", "Saint Vincent and the Grenadines" },
            { "WS", "Samoa" },
            { "SM", "San Marino" },
            { "ST", "Sao Tome and Principe" },
            { "SA", "Saudi Arabia" },
            { "SN", "Senegal" },
            { "RS", "Serbia" },
            { "SC", "Seychelles" },
            { "SL", "Sierra Leone" },
            { "SG", "Singapore" },
            { "SX", "Sint Maarten (Dutch part)" },
            { "SK", "Slovakia" },
            { "SI", "Slovenia" },
            { "SB", "Solomon Islands" },
            { "SO", "Somalia" },
            { "ZA", "South Africa" },
            { "GS", "South Georgia and the South Sandwich Islands" },
            { "SS", "South Sudan" },
            { "ES", "Spain" },
            { "LK", "Sri Lanka" },
            { "SD", "Sudan" },
            { "SR", "Suriname" },
            { "SJ", "Svalbard and Jan Mayen" },
            { "SZ", "Swaziland" },
            { "SE", "Sweden" },
            { "CH", "Switzerland" },
            { "SY", "Syrian Arab Republic" },
            { "TW", "Taiwan" },
            { "TJ", "Tajikistan" },
            { "TZ", "Tanzania" },
            { "TH", "Thailand" },
            { "TL", "Timor-Leste" },
            { "TG", "Togo" },
            { "TK", "Tokelau" },
            { "TO", "Tonga" },
            { "TT", "Trinidad and Tobago" },
            { "TN", "Tunisia" },
            { "TR", "Turkey" },
            { "TM", "Turkmenistan" },
            { "TC", "Turks and Caicos Islands" },
            { "TV", "Tuvalu" },
            { "UG", "Uganda" },
            { "UA", "Ukraine" },
            { "AE", "United Arab Emirates" },
            { "GB", "United Kingdom" },
            { "US", "United States" },
            { "UM", "United States Minor Outlying Islands" },
            { "UY", "Uruguay" },
            { "UZ", "Uzbekistan" },
            { "VU", "Vanuatu" },
            { "VE", "Venezuela" },
            { "VN", "Viet Nam" },
            { "VG", "Virgin Islands" },
            { "VI", "Virgin Islands" },
            { "WF", "Wallis and Futuna" },
            { "EH", "Western Sahara" },
            { "YE", "Yemen" },
            { "ZM", "Zambia" },
            { "ZW", "Zimbabwe" },

            // User-assigned elements as allowed by ISO 3166.
            { "ZZ", "Unknown" }
        };

        #endregion

        private static readonly Lazy<ReadOnlyDictionary<string, string>> ReadOnlyCountryMappings =
            new Lazy<ReadOnlyDictionary<string, string>>(
                () => new ReadOnlyDictionary<string, string>(CountryCodeMappingsInternal));

        #region Country codes

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Afghanistan.
        /// </summary>
        public static string Afghanistan => "AF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Åland Islands.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Åland",
            Justification = "Country names taken from ISO 3166")]
        public static string ÅlandIslands => "AX";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Albania.
        /// </summary>
        public static string Albania => "AL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Algeria.
        /// </summary>
        public static string Algeria => "DZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for American Samoa.
        /// </summary>
        public static string AmericanSamoa => "AS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Andorra.
        /// </summary>
        public static string Andorra => "AD";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Angola.
        /// </summary>
        public static string Angola => "AO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Anguilla.
        /// </summary>
        public static string Anguilla => "AI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Antarctica.
        /// </summary>
        public static string Antarctica => "AQ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Antigua and Barbuda.
        /// </summary>
        public static string AntiguaAndBarbuda => "AG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Argentina.
        /// </summary>
        public static string Argentina => "AR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Armenia.
        /// </summary>
        public static string Armenia => "AM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Aruba.
        /// </summary>
        public static string Aruba => "AW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Australia.
        /// </summary>
        public static string Australia => "AU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Austria.
        /// </summary>
        public static string Austria => "AT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Azerbaijan.
        /// </summary>
        public static string Azerbaijan => "AZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bahamas.
        /// </summary>
        public static string Bahamas => "BS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bahrain.
        /// </summary>
        public static string Bahrain => "BH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bangladesh.
        /// </summary>
        public static string Bangladesh => "BD";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Barbados.
        /// </summary>
        public static string Barbados => "BB";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Belarus.
        /// </summary>
        public static string Belarus => "BY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Belgium.
        /// </summary>
        public static string Belgium => "BE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Belize.
        /// </summary>
        public static string Belize => "BZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Benin.
        /// </summary>
        public static string Benin => "BJ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bermuda.
        /// </summary>
        public static string Bermuda => "BM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bhutan.
        /// </summary>
        public static string Bhutan => "BT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bolivia.
        /// </summary>
        public static string Bolivia => "BO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bonaire.
        /// </summary>
        public static string Bonaire => "BQ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bosnia and Herzegovina.
        /// </summary>
        public static string BosniaAndHerzegovina => "BA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Botswana.
        /// </summary>
        public static string Botswana => "BW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bouvet Island.
        /// </summary>
        public static string BouvetIsland => "BV";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Brazil.
        /// </summary>
        public static string Brazil => "BR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for British Indian Ocean Territory.
        /// </summary>
        public static string BritishIndianOceanTerritory => "IO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Brunei Darussalam.
        /// </summary>
        public static string BruneiDarussalam => "BN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Bulgaria.
        /// </summary>
        public static string Bulgaria => "BG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Burkina Faso.
        /// </summary>
        public static string BurkinaFaso => "BF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Burundi.
        /// </summary>
        public static string Burundi => "BI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cambodia.
        /// </summary>
        public static string Cambodia => "KH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cameroon.
        /// </summary>
        public static string Cameroon => "CM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Canada.
        /// </summary>
        public static string Canada => "CA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cape Verde.
        /// </summary>
        public static string CapeVerde => "CV";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cayman Islands.
        /// </summary>
        public static string CaymanIslands => "KY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Central African Republic.
        /// </summary>
        public static string CentralAfricanRepublic => "CF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Chad.
        /// </summary>
        public static string Chad => "TD";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Chile.
        /// </summary>
        public static string Chile => "CL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for China.
        /// </summary>
        public static string China => "CN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Christmas Island.
        /// </summary>
        public static string ChristmasIsland => "CX";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cocos (Keeling) Islands.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Cocos",
            Justification = "Country names taken from ISO 3166")]
        public static string CocosKeelingIslands => "CC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Colombia.
        /// </summary>
        public static string Colombia => "CO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Comoros.
        /// </summary>
        public static string Comoros => "KM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Congo.
        /// </summary>
        public static string Congo => "CG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cook Islands.
        /// </summary>
        public static string CookIslands => "CK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Costa Rica.
        /// </summary>
        public static string CostaRica => "CR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Côte d'Ivoire.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Ivoire",
            Justification = "Country names taken from ISO 3166")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Côted",
            Justification = "Country names taken from ISO 3166")]
        public static string CôtedIvoire => "CI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Croatia.
        /// </summary>
        public static string Croatia => "HR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cuba.
        /// </summary>
        public static string Cuba => "CU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Curaçao.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Curaçao",
            Justification = "Country names taken from ISO 3166")]
        public static string Curaçao => "CW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Cyprus.
        /// </summary>
        public static string Cyprus => "CY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Czech Republic.
        /// </summary>
        public static string CzechRepublic => "CZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Denmark.
        /// </summary>
        public static string Denmark => "DK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Djibouti.
        /// </summary>
        public static string Djibouti => "DJ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Dominica.
        /// </summary>
        public static string Dominica => "DM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Dominican Republic.
        /// </summary>
        public static string DominicanRepublic => "DO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Ecuador.
        /// </summary>
        public static string Ecuador => "EC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Egypt.
        /// </summary>
        public static string Egypt => "EG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for El Salvador.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1709:IdentifiersShouldBeCasedCorrectly",
            MessageId = "El",
            Justification = "Country names taken from ISO 3166")]
        public static string ElSalvador => "SV";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Equatorial Guinea.
        /// </summary>
        public static string EquatorialGuinea => "GQ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Eritrea.
        /// </summary>
        public static string Eritrea => "ER";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Estonia.
        /// </summary>
        public static string Estonia => "EE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Ethiopia.
        /// </summary>
        public static string Ethiopia => "ET";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Falkland Islands (Malvinas).
        /// </summary>
        public static string FalklandIslandsMalvinas => "FK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Faroe Islands.
        /// </summary>
        public static string FaroeIslands => "FO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Fiji.
        /// </summary>
        public static string Fiji => "FJ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Finland.
        /// </summary>
        public static string Finland => "FI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for France.
        /// </summary>
        public static string France => "FR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for French Guiana.
        /// </summary>
        public static string FrenchGuiana => "GF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for French Polynesia.
        /// </summary>
        public static string FrenchPolynesia => "PF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for French Southern Territories.
        /// </summary>
        public static string FrenchSouthernTerritories => "TF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Gabon.
        /// </summary>
        public static string Gabon => "GA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Gambia.
        /// </summary>
        public static string Gambia => "GM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Georgia.
        /// </summary>
        public static string Georgia => "GE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Germany.
        /// </summary>
        public static string Germany => "DE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Ghana.
        /// </summary>
        public static string Ghana => "GH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Gibraltar.
        /// </summary>
        public static string Gibraltar => "GI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Greece.
        /// </summary>
        public static string Greece => "GR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Greenland.
        /// </summary>
        public static string Greenland => "GL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Grenada.
        /// </summary>
        public static string Grenada => "GD";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Guadeloupe.
        /// </summary>
        public static string Guadeloupe => "GP";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Guam.
        /// </summary>
        public static string Guam => "GU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Guatemala.
        /// </summary>
        public static string Guatemala => "GT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Guernsey.
        /// </summary>
        public static string Guernsey => "GG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Guinea.
        /// </summary>
        public static string Guinea => "GN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Guinea-Bissau.
        /// </summary>
        public static string GuineaBissau => "GW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Guyana.
        /// </summary>
        public static string Guyana => "GY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Haiti.
        /// </summary>
        public static string Haiti => "HT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Heard Island and McDonald Islands.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Mc",
            Justification = "Country names taken from ISO 3166")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1709:IdentifiersShouldBeCasedCorrectly",
            MessageId = "Mc",
            Justification = "Country names taken from ISO 3166")]
        public static string HeardIslandAndMcDonaldIslands => "HM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Holy See (Vatican City State).
        /// </summary>
        public static string HolySeeVaticanCityState => "VA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Honduras.
        /// </summary>
        public static string Honduras => "HN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Hong Kong.
        /// </summary>
        public static string HongKong => "HK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Hungary.
        /// </summary>
        public static string Hungary => "HU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Iceland.
        /// </summary>
        public static string Iceland => "IS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for India.
        /// </summary>
        public static string India => "IN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Indonesia.
        /// </summary>
        public static string Indonesia => "ID";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Iran.
        /// </summary>
        public static string Iran => "IR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Iraq.
        /// </summary>
        public static string Iraq => "IQ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Ireland.
        /// </summary>
        public static string Ireland => "IE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Isle of Man.
        /// </summary>
        public static string IsleOfMan => "IM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Israel.
        /// </summary>
        public static string Israel => "IL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Italy.
        /// </summary>
        public static string Italy => "IT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Jamaica.
        /// </summary>
        public static string Jamaica => "JM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Japan.
        /// </summary>
        public static string Japan => "JP";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Jersey.
        /// </summary>
        public static string Jersey => "JE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Jordan.
        /// </summary>
        public static string Jordan => "JO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Kazakhstan.
        /// </summary>
        public static string Kazakhstan => "KZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Kenya.
        /// </summary>
        public static string Kenya => "KE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Kiribati.
        /// </summary>
        public static string Kiribati => "KI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Korea.
        /// </summary>
        public static string Korea => "KR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Kuwait.
        /// </summary>
        public static string Kuwait => "KW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Kyrgyzstan.
        /// </summary>
        public static string Kyrgyzstan => "KG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Lao People's Democratic Republic.
        /// </summary>
        public static string LaoPeoplesDemocraticRepublic => "LA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Latvia.
        /// </summary>
        public static string Latvia => "LV";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Lebanon.
        /// </summary>
        public static string Lebanon => "LB";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Lesotho.
        /// </summary>
        public static string Lesotho => "LS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Liberia.
        /// </summary>
        public static string Liberia => "LR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Libya.
        /// </summary>
        public static string Libya => "LY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Liechtenstein.
        /// </summary>
        public static string Liechtenstein => "LI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Lithuania.
        /// </summary>
        public static string Lithuania => "LT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Luxembourg.
        /// </summary>
        public static string Luxembourg => "LU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Macao.
        /// </summary>
        public static string Macao => "MO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Macedonia.
        /// </summary>
        public static string Macedonia => "MK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Madagascar.
        /// </summary>
        public static string Madagascar => "MG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Malawi.
        /// </summary>
        public static string Malawi => "MW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Malaysia.
        /// </summary>
        public static string Malaysia => "MY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Maldives.
        /// </summary>
        public static string Maldives => "MV";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Mali.
        /// </summary>
        public static string Mali => "ML";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Malta.
        /// </summary>
        public static string Malta => "MT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Marshall Islands.
        /// </summary>
        public static string MarshallIslands => "MH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Martinique.
        /// </summary>
        public static string Martinique => "MQ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Mauritania.
        /// </summary>
        public static string Mauritania => "MR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Mauritius.
        /// </summary>
        public static string Mauritius => "MU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Mayotte.
        /// </summary>
        public static string Mayotte => "YT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Mexico.
        /// </summary>
        public static string Mexico => "MX";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Micronesia.
        /// </summary>
        public static string Micronesia => "FM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Moldova.
        /// </summary>
        public static string Moldova => "MD";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Monaco.
        /// </summary>
        public static string Monaco => "MC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Mongolia.
        /// </summary>
        public static string Mongolia => "MN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Montenegro.
        /// </summary>
        public static string Montenegro => "ME";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Montserrat.
        /// </summary>
        public static string Montserrat => "MS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Morocco.
        /// </summary>
        public static string Morocco => "MA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Mozambique.
        /// </summary>
        public static string Mozambique => "MZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Myanmar.
        /// </summary>
        public static string Myanmar => "MM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Namibia.
        /// </summary>
        public static string Namibia => "NA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Nauru.
        /// </summary>
        public static string Nauru => "NR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Nepal.
        /// </summary>
        public static string Nepal => "NP";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Netherlands.
        /// </summary>
        public static string Netherlands => "NL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for New Caledonia.
        /// </summary>
        public static string NewCaledonia => "NC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for New Zealand.
        /// </summary>
        public static string NewZealand => "NZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Nicaragua.
        /// </summary>
        public static string Nicaragua => "NI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Niger.
        /// </summary>
        public static string Niger => "NE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Nigeria.
        /// </summary>
        public static string Nigeria => "NG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Niue.
        /// </summary>
        public static string Niue => "NU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Norfolk Island.
        /// </summary>
        public static string NorfolkIsland => "NF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Northern Mariana Islands.
        /// </summary>
        public static string NorthernMarianaIslands => "MP";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Norway.
        /// </summary>
        public static string Norway => "NO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Oman.
        /// </summary>
        public static string Oman => "OM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Pakistan.
        /// </summary>
        public static string Pakistan => "PK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Palau.
        /// </summary>
        public static string Palau => "PW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Palestine.
        /// </summary>
        public static string Palestine => "PS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Panama.
        /// </summary>
        public static string Panama => "PA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Papua New Guinea.
        /// </summary>
        public static string PapuaNewGuinea => "PG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Paraguay.
        /// </summary>
        public static string Paraguay => "PY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Peru.
        /// </summary>
        public static string Peru => "PE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Philippines.
        /// </summary>
        public static string Philippines => "PH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Pitcairn.
        /// </summary>
        public static string Pitcairn => "PN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Poland.
        /// </summary>
        public static string Poland => "PL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Portugal.
        /// </summary>
        public static string Portugal => "PT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Puerto Rico.
        /// </summary>
        public static string PuertoRico => "PR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Qatar.
        /// </summary>
        public static string Qatar => "QA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Réunion.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Réunion",
            Justification = "Country names taken from ISO 3166")]
        public static string Réunion => "RE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Romania.
        /// </summary>
        public static string Romania => "RO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Russian Federation.
        /// </summary>
        public static string RussianFederation => "RU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Rwanda.
        /// </summary>
        public static string Rwanda => "RW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saint Barthélemy.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Barthélemy",
            Justification = "Country names taken from ISO 3166")]
        public static string SaintBarthélemy => "BL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saint Helena.
        /// </summary>
        public static string SaintHelena => "SH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saint Kitts and Nevis.
        /// </summary>
        public static string SaintKittsAndNevis => "KN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saint Lucia.
        /// </summary>
        public static string SaintLucia => "LC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saint Martin (French part).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Frenchpart",
            Justification = "Country names taken from ISO 3166")]
        public static string SaintMartinFrenchpart => "MF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saint Pierre and Miquelon.
        /// </summary>
        public static string SaintPierreAndMiquelon => "PM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saint Vincent and the Grenadines.
        /// </summary>
        public static string SaintVincentAndTheGrenadines => "VC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Samoa.
        /// </summary>
        public static string Samoa => "WS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for San Marino.
        /// </summary>
        public static string SanMarino => "SM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Sao Tome and Principe.
        /// </summary>
        public static string SaoTomeAndPrincipe => "ST";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Saudi Arabia.
        /// </summary>
        public static string SaudiArabia => "SA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Senegal.
        /// </summary>
        public static string Senegal => "SN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Serbia.
        /// </summary>
        public static string Serbia => "RS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Seychelles.
        /// </summary>
        public static string Seychelles => "SC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Sierra Leone.
        /// </summary>
        public static string SierraLeone => "SL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Singapore.
        /// </summary>
        public static string Singapore => "SG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Sint Maarten (Dutch part).
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Sint",
            Justification = "Country names taken from ISO 3166")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Dutchpart",
            Justification = "Country names taken from ISO 3166")]
        public static string SintMaartenDutchpart => "SX";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Slovakia.
        /// </summary>
        public static string Slovakia => "SK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Slovenia.
        /// </summary>
        public static string Slovenia => "SI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Solomon Islands.
        /// </summary>
        public static string SolomonIslands => "SB";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Somalia.
        /// </summary>
        public static string Somalia => "SO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for South Africa.
        /// </summary>
        public static string SouthAfrica => "ZA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for South Georgia and the South Sandwich
        /// Islands.
        /// </summary>
        public static string SouthGeorgiaAndTheSouthSandwichIslands => "GS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for South Sudan.
        /// </summary>
        public static string SouthSudan => "SS";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Spain.
        /// </summary>
        public static string Spain => "ES";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Sri Lanka.
        /// </summary>
        public static string SriLanka => "LK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Sudan.
        /// </summary>
        public static string Sudan => "SD";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Suriname.
        /// </summary>
        public static string Suriname => "SR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Svalbard and Jan Mayen.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Mayen",
            Justification = "Country names taken from ISO 3166")]
        public static string SvalbardAndJanMayen => "SJ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Swaziland.
        /// </summary>
        public static string Swaziland => "SZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Sweden.
        /// </summary>
        public static string Sweden => "SE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Switzerland.
        /// </summary>
        public static string Switzerland => "CH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Syrian Arab Republic.
        /// </summary>
        public static string SyrianArabRepublic => "SY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Taiwan.
        /// </summary>
        public static string Taiwan => "TW";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Tajikistan.
        /// </summary>
        public static string Tajikistan => "TJ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Tanzania.
        /// </summary>
        public static string Tanzania => "TZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Thailand.
        /// </summary>
        public static string Thailand => "TH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Timor-Leste.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            MessageId = "Leste",
            Justification = "Country names taken from ISO 3166")]
        public static string TimorLeste => "TL";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Togo.
        /// </summary>
        public static string Togo => "TG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Tokelau.
        /// </summary>
        public static string Tokelau => "TK";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Tonga.
        /// </summary>
        public static string Tonga => "TO";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Trinidad and Tobago.
        /// </summary>
        public static string TrinidadAndTobago => "TT";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Tunisia.
        /// </summary>
        public static string Tunisia => "TN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Turkey.
        /// </summary>
        public static string Turkey => "TR";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Turkmenistan.
        /// </summary>
        public static string Turkmenistan => "TM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Turks and Caicos Islands.
        /// </summary>
        public static string TurksAndCaicosIslands => "TC";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Tuvalu.
        /// </summary>
        public static string Tuvalu => "TV";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Uganda.
        /// </summary>
        public static string Uganda => "UG";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Ukraine.
        /// </summary>
        public static string Ukraine => "UA";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for United Arab Emirates.
        /// </summary>
        public static string UnitedArabEmirates => "AE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for United Kingdom.
        /// </summary>
        public static string UnitedKingdom => "GB";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for United States.
        /// </summary>
        public static string UnitedStates => "US";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for United States Minor Outlying Islands.
        /// </summary>
        public static string UnitedStatesMinorOutlyingIslands => "UM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Uruguay.
        /// </summary>
        public static string Uruguay => "UY";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Uzbekistan.
        /// </summary>
        public static string Uzbekistan => "UZ";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Vanuatu.
        /// </summary>
        public static string Vanuatu => "VU";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Venezuela.
        /// </summary>
        public static string Venezuela => "VE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Viet Nam.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1702:CompoundWordsShouldBeCasedCorrectly",
            MessageId = "VietNam",
            Justification = "Country names taken from ISO 3166")]
        public static string VietNam => "VN";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Virgin Islands.
        /// </summary>
        public static string VirginIslands => "VI";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Wallis and Futuna.
        /// </summary>
        public static string WallisAndFutuna => "WF";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Western Sahara.
        /// </summary>
        public static string WesternSahara => "EH";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Yemen.
        /// </summary>
        public static string Yemen => "YE";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Zambia.
        /// </summary>
        public static string Zambia => "ZM";

        /// <summary>
        /// Gets the ISO 3166-1 alpha-2 country code for Zimbabwe.
        /// </summary>
        public static string Zimbabwe => "ZW";

        /// <summary>
        /// Gets the user-assigned country code for an Unknown country.
        /// </summary>
        public static string Unknown => "ZZ";

        #endregion
#pragma warning restore CC0021 // Use nameof
#pragma warning restore SA1124 // Do not use regions

        /// <summary>
        /// Gets the available ISO 3166-1 Alpha-2 country codes, and their mappings to a country
        /// name.
        /// </summary>
        public static IReadOnlyDictionary<string, string> CountryCodeMappings
        {
            get { return ReadOnlyCountryMappings.Value; }
        }
    }
}
