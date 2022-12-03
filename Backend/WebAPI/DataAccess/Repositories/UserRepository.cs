using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository<User>
    {
        public UserRepository(ThingsContext context) : base(context) { }

        public async Task<bool> UserExist(string user)
        {
            var exist = await dbSet.FirstOrDefaultAsync(x => x.UserName == user);

            if (exist == null)
                return false;

            return true;
        }

        public async Task<User?> GetByUser(string user)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.UserName == user);
        }
    }
}
