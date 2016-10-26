using deprosa.Common;
using System.ComponentModel.DataAnnotations;

namespace deprosa.ViewModel
{
    public class AccountUpdateDTO
    {
         
        public int ID { get; set; }
        [Required]
        [Display(Name ="Fornavne")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Fornavne")]
        public string LastName { get; set; }
         
        [Display(Name = "Køn")]
        public eGender Gender { get; set; }
         
        [Display(Name = "Land")]
        public eCountryCode CountryCode { get; set; }
        [Display(Name ="By")]
        public string City { get; set; }
        [Display(Name ="Postnummer")]
        public int? PostalCode { get; set; }
         
        [Display(Name = "Adresse")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
         
        [Display(Name = "Telefon")]
        public string Phone { get; set; }         
         
    }
}
