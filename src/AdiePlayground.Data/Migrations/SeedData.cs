// <copyright file="SeedData.cs" company="natsnudasoft">
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

namespace AdiePlayground.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Model;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

#pragma warning disable CC0021 // Use nameof
    /// <summary>
    /// Provides seed data from an external source.
    /// </summary>
    public sealed class SeedData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeedData"/> class.
        /// </summary>
        public SeedData()
        {
            var seedData = LoadSeedData();
            this.Courses = GetCoursesFromData(seedData);
            this.Students = GetStudentsFromData(seedData, this.Courses);
        }

        /// <summary>
        /// Gets the seeded courses.
        /// </summary>
        /// <remarks>
        /// Course names generated via: http://fantasynamegenerators.com/school-subjects.php
        /// </remarks>
        public IList<UniversityCourse> Courses { get; }

        /// <summary>
        /// Gets the seeded students.
        /// </summary>
        /// <remarks>
        /// Student data generated via: http://www.generatedata.com
        /// </remarks>
        public IList<UniversityStudent> Students { get; }

        private static JObject LoadSeedData()
        {
            const string SeedDataResourceName = "AdiePlayground.Data.Migrations.SeedData.json";

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(SeedDataResourceName))
            using (var streamReader = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(streamReader))
            {
                return (JObject)JToken.ReadFrom(jsonTextReader);
            }
        }

        private static IList<UniversityCourse> GetCoursesFromData(JObject seedData)
        {
            return seedData["Courses"]
                .Select(t => new UniversityCourse
                {
                    Title = (string)t["Title"],
                    Credits = (int)t["Credits"]
                })
                .ToList();
        }

        private static IList<UniversityStudent> GetStudentsFromData(
            JObject seedData,
            IList<UniversityCourse> courses)
        {
            var studentsJson = seedData["Students"].ToArray();
            var students = studentsJson
                .Select(t => new UniversityStudent
                {
                    FullName = (string)t["FullName"],
                    DateOfBirth = DateTime.Parse(
                        (string)t["DateOfBirth"],
                        CultureInfo.InvariantCulture),
                    HomeAddress = new UniversityStudentAddress
                    {
                        AddressLine1 = (string)t["HomeAddress"]["AddressLine1"],
                        City = (string)t["HomeAddress"]["City"],
                        County = (string)t["HomeAddress"]["County"],
                        PostalCode = (string)t["HomeAddress"]["PostalCode"],
                        IsoCountryCode = (string)t["HomeAddress"]["IsoCountryCode"]
                    },
                    HomePhone = (string)t["HomePhone"],
                    EmailAddress = (string)t["EmailAddress"],
                    EnrolmentDate = DateTime.Parse(
                        (string)t["EnrolmentDate"],
                        CultureInfo.InvariantCulture),
                    DegreeClassification = (DegreeClassification)(int)t["DegreeClassification"],
                })
                .ToList();

            for (int i = 0; i < students.Count; ++i)
            {
                var student = students[i];
                foreach (var enrolment in studentsJson[i]["CourseEnrolments"])
                {
                    student.CourseEnrolments.Add(
                        new UniversityStudentCourseEnrolment
                        {
                            Student = student,
                            Course = courses[(int)enrolment["CourseId"]],
                            PercentageGrade = (int?)enrolment["PercentageGrade"]
                        });
                }
            }

            return students;
        }
    }
#pragma warning restore CC0021 // Use nameof
}
