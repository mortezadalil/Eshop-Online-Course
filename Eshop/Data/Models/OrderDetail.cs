using Eshop.Data.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public long Price { get; set; }


        public int OrderId { get; set; }
        public Order Order { get; set; }


    }

    public class OrderDetailEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(ur => ur.Order)
                    .WithMany(r => r.OrderDetails)
                    .HasForeignKey(ur => ur.OrderId);

        }
    }
}
