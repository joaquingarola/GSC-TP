using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public interface ILoanRepository : IGenericRepository<Loan>
    {
    }
}
