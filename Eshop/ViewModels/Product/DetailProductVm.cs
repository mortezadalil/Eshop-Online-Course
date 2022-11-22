using Eshop.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Eshop.ViewModels.Account
{
    public class DetailProductVm
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public long? Price { get; set; }
        public int CompanyId { get; set; }
        public List<int> CategoryIds { get; set; }

    }
}
