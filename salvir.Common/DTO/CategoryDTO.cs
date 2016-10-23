using System.ComponentModel.DataAnnotations;

namespace depross.ViewModel
{
    public class CategoryDTO
    {
        public int ID { get; set; }
        [Display(Name = "Navn")]

        public string Name { get; set; }

        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        public ImageDTO Image { get; set; }
    }
}
