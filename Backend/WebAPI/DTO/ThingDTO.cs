namespace Backend.WebAPI.Dto
{
    public class ThingDTO
    {
        public int ID { get; set; }
        public string? Description { get; set; }

        public CategoryDTO Category { get; set; }

        public ThingDTO()
        {
            Category = new CategoryDTO();
        }
    }
}
