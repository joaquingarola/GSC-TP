using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public class LoanRepository : GenericRepository<Loan>, ILoanRepository
    {
        public LoanRepository(ThingsContext context) : base(context) { }
    }
}
