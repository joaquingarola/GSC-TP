using System.ComponentModel.DataAnnotations;

namespace Backend.WebAPI.DTO
{
    public class PersonDTO
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
