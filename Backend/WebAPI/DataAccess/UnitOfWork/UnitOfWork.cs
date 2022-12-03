using Backend.Entities;
using Backend.WebAPI.DataAccess.Repositories;

namespace Backend.WebAPI.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ThingsContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IPersonRepository PersonRepository { get; private set; }
        public IThingRepository ThingRepository { get; private set; }
        public IUserRepository<User> UserRepository { get; private set; }
        public ILoanRepository LoanRepository { get; private set; }

        public UnitOfWork(ThingsContext context)
        {
            this._context = context;
            ThingRepository = new ThingRepository(context);
            CategoryRepository = new CategoryRepository(context);
            PersonRepository = new PersonRepository(context);
            UserRepository = new UserRepository(context);
            LoanRepository = new LoanRepository(context);
        }

        async public Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
