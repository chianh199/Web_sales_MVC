using System.ComponentModel.DataAnnotations;

namespace MVC2.ViewModels
{
    public class LoginVM
    {
        public string? Fullname { get; set; }
        
        [Display(Name ="Email")]
        [Required(ErrorMessage ="Pleases input your email !")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Pleases input your password !")]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}
