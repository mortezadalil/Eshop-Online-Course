using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Eshop.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="نام کاربری اجباری است")]
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="کلمه عبور اجباری است")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

    }
}
