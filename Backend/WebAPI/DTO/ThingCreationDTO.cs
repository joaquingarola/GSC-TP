using Backend.WebAPI.Dto;

namespace Backend.WebAPI.DTO
{
    public class ThingCreationDTO
    {
        public string? Description { get; set; }

        public CategoryDTO Category { get; set; }

        public ThingCreationDTO()
        {
            Category = new CategoryDTO();
        }
    }
}
