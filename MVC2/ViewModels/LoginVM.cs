using System.ComponentModel.DataAnnotations;

namespace MVC2.ViewModels
{
    public class LoginVM
    {
        [Display(Name ="email")]
        [Required(ErrorMessage ="*")]
        public string username { get; set; }

        [Display(Name = "password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
