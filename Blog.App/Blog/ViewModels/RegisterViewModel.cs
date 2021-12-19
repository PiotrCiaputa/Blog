using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username nie może być dłuższe, niż 20 znaków")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
