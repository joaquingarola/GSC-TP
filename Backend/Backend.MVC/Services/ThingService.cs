using Backend.Entities;
using Backend.WebAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Backend.MVC.Services
{
    public class ThingService : IThingService
    {
        protected ThingsContext context;
        internal DbSet<Thing> dbSet;
        protected ICategoryService categoryService;

        public ThingService(ThingsContext context, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.context = context;
            dbSet = context.Set<Thing>();
        }

        async public Task<Thing> AddAsync(Thing thing)
        {
            var entity = await dbSet.AddAsync(thing);
            return entity.Entity;
        }

        public void Delete(Thing thing)
        {
            dbSet.Remove(thing);
        }

        public void Update(Thing thing)
        {
            dbSet.Update(thing);
        }

        async public Task<Thing?> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }

        async public Task<List<Thing>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        async public Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
