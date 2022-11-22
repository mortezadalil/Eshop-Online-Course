using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Eshop.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
        [Display(Name = "تاریخ تولد")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "نام ")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "کلمه عبور الزامی است")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه عبورها یکسان نیست.")]
        [Display(Name = "تکرار کلمه عبور")]
        public string ConfirmPassword { get; set; }

    }
}
