// <copyright file="UniversityStudent.cs" company="natsnudasoft">
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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Represents a university student.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UniversityStudent : IModelEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniversityStudent"/> class.
        /// </summary>
        public UniversityStudent()
        {
            this.CourseEnrolments = new List<UniversityStudentCourseEnrolment>();
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
        /// Gets or sets the title of this <see cref="UniversityStudent"/>.
        /// </summary>
        [StringLength(35)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the full name of this <see cref="UniversityStudent"/>.
        /// </summary>
        [StringLength(255)]
        [Required]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of this <see cref="UniversityStudent"/>.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the home address of this <see cref="UniversityStudent"/>.
        /// </summary>
        public virtual UniversityStudentAddress HomeAddress { get; set; }

        /// <summary>
        /// Gets or sets the home phone number of this <see cref="UniversityStudent"/>.
        /// </summary>
        [StringLength(35)]
        [DataType(DataType.PhoneNumber)]
        public string HomePhone { get; set; }

        /// <summary>
        /// Gets or sets the email address of this <see cref="UniversityStudent"/>.
        /// </summary>
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the enrolment date of this <see cref="UniversityStudent"/>.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime EnrolmentDate { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="UniversityCourse"/> enrolment relationships.
        /// </summary>
        [Required]
        public virtual ICollection<UniversityStudentCourseEnrolment> CourseEnrolments
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the degree classification this <see cref="UniversityStudent"/> has
        /// achieved.
        /// </summary>
        public DegreeClassification DegreeClassification { get; set; }
    }
}
