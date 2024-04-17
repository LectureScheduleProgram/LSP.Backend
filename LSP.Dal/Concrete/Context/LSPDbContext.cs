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

        // Crypto
        public DbSet<Currency>? Currencies { get; set; }
        public DbSet<Parity>? Parities { get; set; }
        public DbSet<MainNetwork>? MainNetworks { get; set; }
        public DbSet<SubNetwork>? SubNetworks { get; set; }
        public DbSet<BuyOrder>? BuyOrders { get; set; }
        public DbSet<SellOrder>? SellOrders { get; set; }
        public DbSet<Deposit>? Deposits { get; set; }
        public DbSet<Withdrawal>? Withdrawals { get; set; }
        public DbSet<Swap>? Swaps { get; set; }


        // User
        public DbSet<User>? Users { get; set; }
        public DbSet<Balance>? Balancies { get; set; }
        public DbSet<FutureBalance>? FutureBalancies { get; set; }
        public DbSet<UserSecurityType>? UserSecurityTypes { get; set; }
        public DbSet<PasswordHistory>? PasswordHistories { get; set; }
        public DbSet<SecurityHistory>? SecurityHistories { get; set; }
        public DbSet<UserStatusHistory>? UserStatusHistories { get; set; }

        //Kyc
        public DbSet<Kyc>? Kycs { get; set; }

        public DbSet<Wallet>? Wallets { get; set; }
        public DbSet<Afk>? Afk { get; set; }

        //Future Order
        public DbSet<BuyLongOrder>? BuyLongOrders { get; set; }
        public DbSet<SellShortOrder>? SellShortOrders { get; set; }
    }
}
