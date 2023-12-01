using Microsoft.EntityFrameworkCore;
using OShop.Domain.Entities;
using OShop.Infrastructures.Persistence.EntityConfiguration;
using System.ComponentModel.DataAnnotations;

namespace OShop.Infrastructures.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=OShopDB;User ID=sa;Password=123;TrustServerCertificate=True;");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig());
        }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(e => (e.State == EntityState.Modified || e.State == EntityState.Added) && e is BaseEntity);
            foreach (var entity in entities)
            {
                var _entity = entity.Entity as BaseEntity;
                if (entity.State is EntityState.Added)
                    _entity.CreateAt = DateTime.Now;

                _entity.LastModifiedAt = DateTime.Now;
            }
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            var entities = ChangeTracker.Entries().Where(e => (e.State == EntityState.Modified || e.State == EntityState.Added) && e is BaseEntity);
            foreach (var entity in entities)
            {
                var _entity = entity.Entity as BaseEntity;
                if (entity.State is EntityState.Added)
                    _entity.CreateAt = DateTime.Now;

                _entity.LastModifiedAt = DateTime.Now;
            }

            return await base.SaveChangesAsync();
        }
    }
}
