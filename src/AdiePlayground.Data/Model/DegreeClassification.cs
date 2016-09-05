// <copyright file="DegreeClassification.cs" company="natsnudasoft">
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
    /// <summary>
    /// Defines a degree classification.
    /// </summary>
    public enum DegreeClassification
    {
        /// <summary>
        /// Represents no <see cref="DegreeClassification"/>. e.g. a <see cref="UniversityStudent"/>
        /// that has not yet finished their study.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represents a 1st/First-class honours <see cref="DegreeClassification"/>.
        /// </summary>
        FirstClass = 1,

        /// <summary>
        /// Represents a 2:1/Second-class honours, upper division
        /// <see cref="DegreeClassification"/>.
        /// </summary>
        SecondClassUpper = 2,

        /// <summary>
        /// Represents a 2:1/Second-class honours, lower division
        /// <see cref="DegreeClassification"/>.
        /// </summary>
        SecondClassLower = 3,

        /// <summary>
        /// Represents a 3rd/Third-class honours <see cref="DegreeClassification"/>.
        /// </summary>
        ThirdClass = 4,

        /// <summary>
        /// Represents an ordinary (pass) <see cref="DegreeClassification"/>.
        /// </summary>
        Pass = 5,

        /// <summary>
        /// Represents a fail <see cref="DegreeClassification"/>.
        /// </summary>
        Fail = 6
    }
}