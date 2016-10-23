using System.ComponentModel.DataAnnotations;

namespace depross.ViewModel
{
    public class AccountUpdatePasswordViewModel
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nuværende password")]
        public string OldPassword { get; set; }
        [Required]
        [Display(Name = "Nyt password")]
        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Gentag password")]
        public string ConfirmedPassword { get; set; }
    }
}
