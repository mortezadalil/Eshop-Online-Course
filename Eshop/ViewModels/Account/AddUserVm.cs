using System.ComponentModel.DataAnnotations;

namespace Eshop.ViewModels.Account
{
    public class AddUserVm
    {


        [Required(ErrorMessage = "نام کاربری به شکل ایمیل است")]
        [EmailAddress(ErrorMessage ="فرمت ایمیل صحیح نیست")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "نام الزامی است")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "نام خانوادگی الزامی است")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "کلمه عبور ضروری است")]
        public string Password { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();
        //public string Role { get; set; }

    }
}
