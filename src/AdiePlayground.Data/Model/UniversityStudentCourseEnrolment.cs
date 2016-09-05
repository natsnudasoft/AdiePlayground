// <copyright file="UniversityStudentCourseEnrolment.cs" company="natsnudasoft">
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

    /// <summary>
    /// Represents a <see cref="UniversityStudent"/> &lt;--&gt; <see cref="UniversityCourse"/>
    /// relationship.
    /// </summary>
    /// <seealso cref="IModelEntity" />
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class UniversityStudentCourseEnrolment : IModelEntity
    {
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
        /// Gets or sets the student id in this enrolment relationship.
        /// </summary>
        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets the course id in this enrolment relationship.
        /// </summary>
        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="UniversityCourse"/> in this enrolment relationship.
        /// </summary>
        [Required]
        public virtual UniversityCourse Course { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="UniversityStudent"/> in this enrolment relationship.
        /// </summary>
        [Required]
        public virtual UniversityStudent Student { get; set; }

        /// <summary>
        /// Gets or sets the grade the <see cref="UniversityStudent"/> achieved in the related
        /// <see cref="UniversityCourse"/>.
        /// </summary>
        public int? PercentageGrade { get; set; }
    }
}
