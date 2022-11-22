using Eshop.Data.Models;

namespace Eshop.ViewModels.Product
{
    public class ListShoppingCardVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public long? Price { get; set; }
        public int Count { get; set; }
    }
}
