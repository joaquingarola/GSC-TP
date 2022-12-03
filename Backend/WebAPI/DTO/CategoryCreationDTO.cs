using System.ComponentModel.DataAnnotations;

namespace Backend.WebAPI.DTO
{
    public class CategoryCreationDTO
    {
        [Required]
        public string Description { get; set; }
    }
}
