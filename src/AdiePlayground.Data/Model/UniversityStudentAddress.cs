// <copyright file="UniversityStudentAddress.cs" company="natsnudasoft">
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

namespace AdiePlayground.Data.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Common;

    /// <summary>
    /// Represents an address for a <see cref="UniversityStudent"/>.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UniversityStudentAddress : IModelEntity
    {
        private string isoCountryCode;

        /// <inheritdoc/>
        [Key]
        [ForeignKey(nameof(Student))]
        public int Id { get; private set; }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1819:PropertiesShouldNotReturnArrays",
            Justification = "Entity Framework requires byte[].")]
        [Timestamp]
        public byte[] RowVersion { get; private set; }

        /// <summary>
        /// Gets the university student this address is associated with.
        /// </summary>
        public virtual UniversityStudent Student { get; private set; }

        /// <summary>
        /// Gets or sets the address line 1
        /// (house name/number and street, PO box, company name, c/o etc.)
        /// </summary>
        [StringLength(60)]
        [Required]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or sets the address line 2; this line is OPTIONAL
        /// (apartment, suite, unit, building, floor etc.)
        /// </summary>
        [StringLength(60)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or sets the City.
        /// </summary>
        [StringLength(60)]
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the County, State etc.
        /// </summary>
        [StringLength(60)]
        [Required]
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the postal code, zip code etc.
        /// </summary>
        [StringLength(35)]
        [DataType(DataType.PostalCode)]
        [Required]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the ISO 3166-1 alpha-2 country code.
        /// </summary>
        [StringLength(2, MinimumLength = 2)]
        [Required]
        public string IsoCountryCode
        {
            get
            {
                return this.isoCountryCode;
            }

            set
            {
                this.isoCountryCode = value;
                this.Country = new Iso3166Country(this.isoCountryCode);
            }
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <remarks>
        /// This value is lazily loaded.</remarks>
        [NotMapped]
        public Iso3166Country Country { get; private set; }
    }
}
