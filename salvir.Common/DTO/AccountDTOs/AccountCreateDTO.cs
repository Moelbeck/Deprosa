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
         
        //[Required]
        [Display(Name = "Bekræft email")]
        public string ConfirmEmail { get; set; }
         
        [Required]
        [Display(Name = "Password"),MinLength(6, ErrorMessage = "Password skal være mindst 6 karaktere lang")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Bekræft password")]
        public string ConfirmPassword { get; set; }
    }
}
