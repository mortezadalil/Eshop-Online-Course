using System.ComponentModel.DataAnnotations;

namespace Eshop.ViewModels.Account
{
    public class EditUserVm
    {

        public string? Id { get; set; }

        [Required(ErrorMessage = "نام کاربری به شکل ایمیل است")]
        [EmailAddress(ErrorMessage ="فرمت ایمیل صحیح نیست")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "نام الزامی است")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        public string LastName { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();

    }
}
