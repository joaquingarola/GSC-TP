using Backend.Entities;
using Backend.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Backend.MVC.Services
{
    public class CategoryService : ICategoryService
    {
        protected ThingsContext context;
        public DbSet<Category> dbSet;

        public CategoryService(ThingsContext context)
        {
            this.context = context;
            dbSet = context.Set<Category>();
        }

        async public Task<List<Category>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        async public Task<Category?> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }
    }
}
