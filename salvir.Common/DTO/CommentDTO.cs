
using System.ComponentModel.DataAnnotations;

namespace depross.ViewModel
{
    public class CommentDTO
    {
        public int ID { get; set; }
        [Display(Name = "Titel")]
        public string Title { get; set; }
        [Display(Name = "Besked")]
        public string Message { get; set; }

        [Display(Name = "Privat")]
        public bool IsPrivateMessage { get; set; }

        [Display(Name = "Afsender ID")]
        public int SenderID { get; set; }
        [Display(Name = "Afsender fornavn")]
        public string SenderFirstName { get; set; }
        [Display(Name = "Afsender efternavn")]
        public string SenderLastName { get; set; }
        [Display(Name = "Afsender email")]
        public string SenderEmail { get; set; }
        public string SenderToString()
        {
            return SenderFirstName + " " + SenderLastName;
        }
    }
}
