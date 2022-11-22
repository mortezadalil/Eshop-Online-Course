using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public long? Price { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public ICollection<ProductCategory> ProductCategories { get;  set; }
    }

    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ShortDescription).HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(500);
            ////dotnet
            //builder.Property(x => x.CreatedDate).HasDefaultValue(DateTime.Now);
            //db
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("getdate()");


            builder.HasOne(ur => ur.Company)
                    .WithMany(r => r.Products)
                    .HasForeignKey(ur => ur.CompanyId);
        }
    }
}
