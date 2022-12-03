using Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : Base
    {
        protected ThingsContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(ThingsContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        async public Task<TEntity> AddAsync(TEntity entity)
        {
            var savedEntity = await dbSet.AddAsync(entity);
            return savedEntity.Entity;
        }

        async public Task<bool> DeleteAsync(int id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity == null)
            {
                return false;
            }

            dbSet.Remove(entity);
            return true;
        }

        public TEntity Update(TEntity entity)
        {
            var updatedEntity = dbSet.Update(entity);
            return updatedEntity.Entity;
        }

        async public Task<TEntity?> GetByIdAsync(int id)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.ID == id);
        }

        async public Task<List<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
    }
}
