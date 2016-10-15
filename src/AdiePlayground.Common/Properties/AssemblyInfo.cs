// <copyright file="AssemblyInfo.cs" company="natsnudasoft">
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

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]

// Internals visible to unit testing and Moq proxy.
#pragma warning disable MEN002 // Line is too long
[assembly: InternalsVisibleTo("AdiePlayground.CommonTests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100e1f762383ca3e6df0e06de2eca630e9b66c797f8c5712ccefb0f39727ca9ba163098a683e22ecba03e3ff47008fce5e0b0a75ae091c1eef39a5fe258c2ce5140a76cfbe6e6186177d71f6aee11ca05c2c8983f1e2f79c2c1053f217f7df31176c12c5c3611c33575a18f8c60a42c300bb1b4941e2f82c32a624f0cdf114049a4")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
#pragma warning restore MEN002 // Line is too long

[assembly: AssemblyTitle("AdiePlayground.Common")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("AdiePlayground.Common")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("9f889368-3f89-4780-b213-c971e5bfb720")]