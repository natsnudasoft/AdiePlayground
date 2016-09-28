// <copyright file="PlaygroundDbContext.cs" company="natsnudasoft">
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

namespace AdiePlayground.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;
    using Model;

    /// <summary>
    /// Represents the database context for the playground.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage(
        "Microsoft.Performance",
        "CA1812:AvoidUninstantiatedInternalClasses",
        Justification = "Class is used via Activator.CreateInstance.")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    internal class PlaygroundDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaygroundDbContext"/> class using the
        /// default connection.
        /// </summary>
        public PlaygroundDbContext()
            : base("DefaultConnection")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaygroundDbContext"/> class using the
        /// given string as the name or connection string.
        /// </summary>
        /// <param name="nameOrConnectionString">Either the database name or a connection
        /// string.</param>
        public PlaygroundDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// Gets or sets the collection of all entities of a <see cref="UniversityStudent"/> type in
        /// the underlying context.
        /// </summary>
        public DbSet<UniversityStudent> UniversityStudents { get; set; }

        /// <summary>
        /// Gets or sets the collection of all entities of a <see cref="UniversityStudentAddress"/>
        /// type in the underlying context.
        /// </summary>
        public DbSet<UniversityStudentAddress> UniversityStudentAddresses { get; set; }

        /// <summary>
        /// Gets or sets the collection of all entities of a <see cref="UniversityCourse"/> type in
        /// the underlying context.
        /// </summary>
        public DbSet<UniversityCourse> UniversityCourses { get; set; }

        /// <summary>
        /// This method does nothing and shouldn't be called but exists so that a reference to a
        /// type in EntityFramework.SqlServer.dll is used so that the dll will be copied to any
        /// projects referencing this one.
        /// </summary>
        /// <remarks>This is just a workaround to EntityFramework having an external reference on
        /// EntityFramework.SqlServer but visual studio won't copy the dll to the output unless
        /// it thinks something in EntityFramework.SqlServer is actually used.</remarks>
        /// <returns>An empty string.</returns>
        [Obsolete("This method should not be called.", true)]
        public static string FixEntityFrameworkSqlProviderServices()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            // Ensure instance gets used, but still just return an empty string.
            return new string(instance.ToString().Where(c => false).ToArray());
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            if (modelBuilder != null)
            {
                modelBuilder.Properties<DateTime>()
                    .Configure(c => c.HasColumnType("datetime2"));

                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                modelBuilder.Conventions.Add(new DataTypeAttributeConvention());

                modelBuilder.Entity<UniversityStudentAddress>()
                    .HasRequired(a => a.Student)
                    .WithOptional(s => s.HomeAddress)
                    .WillCascadeOnDelete(true);
            }
        }
    }
}
