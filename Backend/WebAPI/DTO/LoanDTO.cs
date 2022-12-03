using Backend.Entities;
using Backend.WebAPI.Dto;

namespace Backend.WebAPI.DTO
{
    public class LoanDTO
    {
        public LoanDTO()
        {
            Thing = new ThingDTO();
            Person = new PersonDTO();
        }

        public ThingDTO Thing { get; set; }
        public PersonDTO Person { get; set; }
        public DateTime Date { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool Status { get; set; }
    }
}
