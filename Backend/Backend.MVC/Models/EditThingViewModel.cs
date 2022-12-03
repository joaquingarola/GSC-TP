using System.ComponentModel.DataAnnotations;

namespace Backend.MVC.Models
{
    public class EditThingViewModel
    {
        public EditThingViewModel()
        {
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int Category { get; set; }
    }
}
