using Eshop.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Eshop.ViewModels.Account
{
    public class EditProductVm
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "نام کالا ضروری است")]
        public string Title { get; set; }
        [Required(ErrorMessage = "توضیح کوتاه کالا ضروری است")]
        public string? ShortDescription { get; set; }
        [Required(ErrorMessage = "توضیح کالا ضروری است")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "قیمت کالا ضروری است")]
        public long? Price { get; set; }

        public int CompanyId { get; set; }
        public List<int> CategoryIds { get; set; }

    }
}
