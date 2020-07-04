using Microsoft.EntityFrameworkCore;
using o = ProductivityTools.Bank.Millenium.Objects;
using System;
using System.Transactions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace ProductivityTools.Bank.Millenium.Database
{
    public class MilleniumContext : DbContext
    {
        private readonly IConfiguration configuration;
        
        public MilleniumContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<o.Transaction> Transaction { get; set; }
        public DbSet<o.BasicData> BasicData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MilleniumContext"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("bm");
            modelBuilder.Entity<o.Transaction>().HasKey(x => x.TransactionId);
            modelBuilder.Entity<o.BasicData>().HasKey(x => x.BasicDataId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
