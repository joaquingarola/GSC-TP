using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Entities
{
    public class Loan : Base
    {
        public Loan() { }

        public Loan(int id, DateTime returnDate, Thing thing, Person person)
        {
            ID = id;
            ReturnDate = returnDate;
            Thing = thing;
            Person = person;
        }

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime? ReturnDate { get; set; }
        public bool Status { get; set; } = false;
        public virtual Thing Thing { get; set; }
        public virtual Person Person { get; set; }
    }
}
