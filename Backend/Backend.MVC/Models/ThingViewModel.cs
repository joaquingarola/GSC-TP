using System.ComponentModel.DataAnnotations;

namespace Backend.MVC.Models
{
    public class ThingViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public CategoryViewModel Category { get; set; }
    }
}
