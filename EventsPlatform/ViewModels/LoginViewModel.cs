using System.ComponentModel.DataAnnotations;

namespace EventsPlatform.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Парола")]
        public string Password { get; set; }
    }
}
