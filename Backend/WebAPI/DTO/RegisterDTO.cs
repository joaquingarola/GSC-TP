using System.ComponentModel.DataAnnotations;

namespace Backend.WebAPI.DTO
{
    public class RegisterDTO
    {
            [Required]
            public string UserName { set; get; }
            [Required]
            public string Password { set; get; }
    }
}
