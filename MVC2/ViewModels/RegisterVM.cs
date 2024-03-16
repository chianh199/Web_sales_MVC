using System.ComponentModel.DataAnnotations;

namespace MVC2.ViewModels
{
    public class RegisterVM
    {
        
        

        [Display(Name ="Ten tai khoan")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string? Fullname { get; set; }

        [EmailAddress(ErrorMessage = "Chưa đúng định dạng email")]
        public string? Email { get; set; }

        //[RegularExpression(@"0[9875]\d{8}", ErrorMessage = "Chưa đúng định dạng di động Việt Nam")]
        public int? Phone { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [MinLength(8, ErrorMessage = "Phải có ít nhất 8 kí tự")]
        [DataType(DataType.Password)]
        public string? Pwd { get; set; }

        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        public string? Position { get; set; }
    }
}
