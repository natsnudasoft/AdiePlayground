// <copyright file="UniversityCourse.cs" company="natsnudasoft">
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a university course.
    /// </summary>
    /// <seealso cref="IModelEntity" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UniversityCourse : IModelEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniversityCourse"/> class.
        /// </summary>
        public UniversityCourse()
        {
            this.StudentEnrolments = new List<UniversityStudentCourseEnrolment>();
        }

        /// <inheritdoc/>
        [Key]
        public int Id { get; private set; }

        /// <inheritdoc/>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Performance",
            "CA1819:PropertiesShouldNotReturnArrays",
            Justification = "Entity Framework requires byte[].")]
        [Timestamp]
        public byte[] RowVersion { get; private set; }

        /// <summary>
        /// Gets or sets the title of this <see cref="UniversityCourse"/>.
        /// </summary>
        [StringLength(255)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the number of credits this <see cref="UniversityCourse"/> awards.
        /// </summary>
        [Required]
        public int Credits { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="UniversityStudent"/> enrolment relationships.
        /// </summary>
        [Required]
        public virtual ICollection<UniversityStudentCourseEnrolment> StudentEnrolments
        {
            get;
            private set;
        }
    }
}
