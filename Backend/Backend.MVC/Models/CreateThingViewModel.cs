using System.ComponentModel.DataAnnotations;

namespace Backend.MVC.Models
{
    public class CreateThingViewModel
    {
        public CreateThingViewModel()
        {
        }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Category { get; set; }
    }
}
