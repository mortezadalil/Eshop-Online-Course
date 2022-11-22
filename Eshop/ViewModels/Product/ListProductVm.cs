using Eshop.Data.Models;

namespace Eshop.ViewModels.Product
{
    public class ListProductVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ShortDescription { get; set; }
        //public string? Description { get; set; }
        public long? Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public Company Company { get; set; }
        public List<Category> Categories { get; set; }

    }
}
