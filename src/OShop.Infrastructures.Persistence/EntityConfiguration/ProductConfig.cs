using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OShop.Domain.Entities;

namespace OShop.Infrastructures.Persistence.EntityConfiguration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100);
        }
    }
}
