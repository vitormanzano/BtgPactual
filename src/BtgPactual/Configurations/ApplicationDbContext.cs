using BtgPactual.Entities;
using Microsoft.EntityFrameworkCore;

namespace BtgPactual.Configurations
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Fund> Funds { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Rescue> Rescues { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>()
                .HasOne(a => a.Fund)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => x.FundId);

            modelBuilder.Entity<Application>()
                .HasOne(r => r.Client)
                .WithMany(x => x.Applications)
                .HasForeignKey(x => x.ClientId);

            modelBuilder.Entity<Rescue>()
                .HasOne(r => r.Fund)
                .WithMany(x => x.Rescues)
                .HasForeignKey(x => x.FundId);

            modelBuilder.Entity<Rescue>()
                .HasOne(r => r.Client)
                .WithMany(x => x.Rescues)
                .HasForeignKey(x => x.ClientId);

            modelBuilder.Entity<Fund>().HasData(
                new Fund("Investments fund A") { Id = 1 },
                new Fund("Investments fund B") { Id = 2 },
                new Fund("Investments fund C") { Id = 3 }
            );

            modelBuilder.Entity<Client>().HasData(
                new Client(1, "Vitor"),
                new Client(2, "John"),
                new Client(3, "Doe")
            );
                

        }
    }
}
