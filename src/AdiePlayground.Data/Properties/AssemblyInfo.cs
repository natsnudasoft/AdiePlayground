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
[assembly: InternalsVisibleTo("AdiePlaygroundTests, PublicKey=002400000480000094000000060200000024000052534131000400000100010037a0d92f6ab7e83a95022bee0d575dd385ffb35ad271880e03b31cfb5323c8d1350e4d31d52e1e3eae0da688543402ae761e03c441f927f2496743b415de616115434486e9befb727c83ca199c52ee41ec16b3947e88772a4f9a3bb34aa6ade8c2a581b7590f313fe49e34dc1a1a96496cbd7e2c056200f2301ba063398fd699")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2, PublicKey=0024000004800000940000000602000000240000525341310004000001000100c547cac37abd99c8db225ef2f6c8a3602f3b3606cc9891605d02baa56104f4cfc0734aa39b93bf7852f7d9266654753cc297e7d2edfe0bac1cdcf9f717241550e0a7b191195b7667bb4f64bcb8e2121380fd1d9d46ad2d92d2d15605093924cceaf74c4861eff62abf69b9291ed0a340e113be11e6a7d3113e92484cf7045cc7")]
#pragma warning restore MEN002 // Line is too long

[assembly: AssemblyTitle("AdiePlayground.Data")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyProduct("AdiePlayground.Data")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("9605ae85-512a-4a2a-8d6b-10e480277359")]