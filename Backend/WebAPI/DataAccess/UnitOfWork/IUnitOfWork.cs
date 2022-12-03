using Backend.Entities;
using Backend.WebAPI.DataAccess.Repositories;

namespace Backend.WebAPI.DataAccess.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
        public IPersonRepository PersonRepository { get; }
        public IThingRepository ThingRepository { get; }
        public IUserRepository<User> UserRepository { get; }
        public ILoanRepository LoanRepository { get; }

        public Task<int> SaveChangesAsync();
    }
}
