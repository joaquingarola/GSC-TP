using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ThingsContext context) : base(context) { }

        async public Task<bool> EmailExistAsync(string email)
        {
            var person = await dbSet.FirstOrDefaultAsync(p => p.Email == email);
            return (person == null ? false : true);
        }
    }
}
