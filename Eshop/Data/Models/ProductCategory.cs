using Eshop.Data.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Data.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }


    }

    public class ProductCategoryEntityTypeConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(ur => ur.Product)
                .WithMany(r => r.ProductCategories)
                .HasForeignKey(ur => ur.ProductId);

            builder.HasOne(ur => ur.Category)
                .WithMany(r => r.ProductCategories)
                .HasForeignKey(ur => ur.CategoryId);
        }
    }
}
