﻿// <copyright file="ImplicitOperatorStub.cs" company="natsnudasoft">
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

namespace AdiePlaygroundTests.Common.Extensions
{
    public sealed class ImplicitOperatorStub
    {
        public string Value { get; private set; }

        /// <param name="value">The stub value.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Usage",
            "CA2225:OperatorOverloadsHaveNamedAlternates",
            Justification = "Not a public API.")]
        public static implicit operator ImplicitOperatorStub(string value)
        {
            return new ImplicitOperatorStub { Value = value };
        }
    }
}
