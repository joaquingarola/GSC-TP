using System.ComponentModel.DataAnnotations;

namespace Backend.WebAPI.Dto
{
    public class CategoryDTO
    {
        [Required]
        public int ID { get; set; }
        
        [Required]
        public string Description { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
