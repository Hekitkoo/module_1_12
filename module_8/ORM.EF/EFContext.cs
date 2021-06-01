using System.Data;
using Microsoft.EntityFrameworkCore;
using ORM.Core.Models;

namespace ORM.EF
{
    public class EFContext : DbContext
    {
        private readonly string connectionString;
        
        public DbSet<Route> Routes { get; set; }
        
        public EFContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Route>()
                .ToTable(nameof(Route))
                .Property(o => o.Id)
                .HasColumnName("RouteId");

            builder.Entity<Route>()
                .ToTable(nameof(Route))
                .HasKey(o => o.Id);

            builder.Entity<Route>()
                .ToTable(nameof(Route))
                .Ignore(o => o.DestinationWarehouse);
            
            builder.Entity<Route>()
                .ToTable(nameof(Route))
                .Ignore(o => o.OriginWarehouse);
            
            base.OnModelCreating(builder);
        }
    }
}