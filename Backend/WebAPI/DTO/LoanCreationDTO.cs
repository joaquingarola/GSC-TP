using Backend.WebAPI.Dto;

namespace Backend.WebAPI.DTO
{
    public class LoanCreationDTO
    {
        public LoanCreationDTO()
        {
            Thing = new ThingDTO();
            Person = new PersonDTO();
        }

        public ThingDTO Thing { get; set; }
        public PersonDTO Person { get; set; }
    }
}
