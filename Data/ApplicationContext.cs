using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SqlPersistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=test;User ID=sa;Password=ClassicV2k23;Encrypt=False");
           // optionsBuilder("server=localhost;port=3306;user=root;password=;database=proyectolssharp", ServerVersion.AutoDetect("server=localhost;port=3306;user=root;password=;database=proyectolssharp"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}