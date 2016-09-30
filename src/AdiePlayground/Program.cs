// <copyright file="Program.cs" company="natsnudasoft">
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

namespace AdiePlayground
{
    using System;
    using Autofac;
    using Data.Services;
    using Example;
    using NLog;
    using Properties;

    /// <summary>
    /// <see cref="Program"/> contains the entry point for the application.
    /// </summary>
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The entry point of <see cref="Program"/>.
        /// </summary>
        public static void Main()
        {
            const int WindowWidth = 155;
            const int WindowHeight = 45;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
            Console.SetWindowSize(
                Math.Min(Console.LargestWindowWidth, WindowWidth),
                Math.Min(Console.LargestWindowHeight, WindowHeight));
            var container = ContainerConfiguration.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var varianceExample = scope.Resolve<VarianceExample>();
                varianceExample.RunExample();

                var interceptorExample = scope.Resolve<InterceptorExample>();
                interceptorExample.RunExample();

                var observerExample = scope.Resolve<ObserverExample>();
                observerExample.RunExample();

                var contextService = scope.Resolve<ContextService>();
                Console.WriteLine(contextService.GetType().Name);
                Console.WriteLine(Resources.ConsolePressEnterToContinue);
                Console.ReadLine();
            }
        }

        private static void CurrentDomainUnhandledException(
            object sender,
            UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex != null)
            {
                Logger.Fatal(ex);
            }
            else
            {
                Logger.Fatal("A fatal unhandled error occurred.");
            }
        }
    }
}
