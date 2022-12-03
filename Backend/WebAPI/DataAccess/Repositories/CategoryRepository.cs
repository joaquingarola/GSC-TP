using Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    { 
        public CategoryRepository(ThingsContext context) : base(context)
        { 
        }

        public async Task<bool> CategoryExist(string desc)
        {
            var exist = await dbSet.FirstOrDefaultAsync(x => x.Description == desc);

            if (exist == null)
                return false;

            return true;
        }
    }
}
