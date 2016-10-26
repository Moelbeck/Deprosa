using System.ComponentModel.DataAnnotations;

namespace deprosa.ViewModel
{
    public class AccountCreateDTO
    {
        [Required]
        [Display(Name ="Fornavn")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Efternavn")]
        public string LastName { get; set; }
         
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
         
        [Required]
        [Display(Name = "Bekræft email")]
        public string ConfirmEmail { get; set; }
         
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
