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
        //public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        //{
        //    return base.Set<TEntity>();
        //}

        //public override int SaveChanges()
        //{
        //    var entities = from e in ChangeTracker.Entries()
        //                   where e.State == EntityState.Added
        //                       || e.State == EntityState.Modified
        //                   select e.Entity;
        //    foreach (var entity in entities)
        //    {

        //        var validationContext = new ValidationContext(entity);
        //        Validator.ValidateObject(entity, validationContext);

        //    }
        //    return base.SaveChanges();
        //}

        //public Task<int> SaveChangesAsync()
        //{
        //    var entities = from e in ChangeTracker.Entries()
        //                   where e.State == EntityState.Added
        //                       || e.State == EntityState.Modified
        //                   select e.Entity;
        //    foreach (var entity in entities)
        //    {

        //        var validationContext = new ValidationContext(entity);
        //        Validator.ValidateObject(entity, validationContext);
        //    }
        //    return base.SaveChangesAsync();
        //}
    }
}
