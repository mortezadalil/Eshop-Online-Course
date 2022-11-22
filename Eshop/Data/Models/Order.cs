using Eshop.Data.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eshop.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }

    public enum OrderStatus
    {
        Created=1,
        Pending=2,
        Success=3,
        Failed=4,
        Cancelled=5
            
    }

    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(ur => ur.User)
                    .WithMany()
                    .HasForeignKey(ur => ur.UserId);

        }
    }
}
