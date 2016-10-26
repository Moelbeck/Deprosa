
using System.ComponentModel.DataAnnotations;

namespace deprosa.ViewModel
{
    public class AccountLoginDTO
    {
        [Display(Name = "Email")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }
    }
}
