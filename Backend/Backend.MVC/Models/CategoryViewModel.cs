using System.ComponentModel.DataAnnotations;

namespace Backend.MVC.Models
{
    public class CategoryViewModel
    {
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
