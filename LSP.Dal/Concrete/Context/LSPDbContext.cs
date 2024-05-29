using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using LSP.Core.Entities.Concrete;
using LSP.Entity.Concrete;

namespace LSP.Dal.Concrete.Context
{
    public class LSPDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // IConfigurationRoot configuration = new ConfigurationBuilder()
                //     .SetBasePath(Directory.GetCurrentDirectory())
                //     .AddJsonFile("appsettings.json")
                //     .Build();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("LSPDbContextConnection"));
            }

            // TODO: Make it better
            // When migrating, use the following connection string instead of the one above
            // IConfigurationRoot configuration = new ConfigurationBuilder()
            //     .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json")
            //     .Build();
            // optionsBuilder.UseSqlServer(configuration.GetConnectionString("LSPDbContextConnection"));
            // base.OnConfiguring(optionsBuilder);
        }

        // User
        public DbSet<User> Users { get; set; }
        public DbSet<UserSecurityType> UserSecurityTypes { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; }
        public DbSet<SecurityHistory> SecurityHistories { get; set; }
        public DbSet<UserStatusHistory> UserStatusHistories { get; set; }

        // Classroom
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<ClassroomType> ClassroomTypes { get; set; }
        public DbSet<ClassroomCapacity> ClassroomCapacities { get; set; }

        // Lecture
        public DbSet<Lecture> Lectures { get; set; }

        // ScheduleRecord
        public DbSet<ScheduleRecord> ScheduleRecords { get; set; }
    }
}
