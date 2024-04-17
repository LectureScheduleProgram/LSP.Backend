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

                IConfigurationRoot configuration = new ConfigurationBuilder()

                    .SetBasePath(Directory.GetCurrentDirectory())

                    .AddJsonFile("appsettings.json")

                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("LSPDbContextConnection"));

            }
        }

        // User
        public DbSet<User>? Users { get; set; }
        public DbSet<UserSecurityType>? UserSecurityTypes { get; set; }
        public DbSet<PasswordHistory>? PasswordHistories { get; set; }
        public DbSet<SecurityHistory>? SecurityHistories { get; set; }
        public DbSet<UserStatusHistory>? UserStatusHistories { get; set; }

    }
}
