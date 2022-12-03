using Backend.Entities;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : Base
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<bool> DeleteAsync(int id);

        TEntity Update(TEntity entity);

        Task<TEntity?> GetByIdAsync(int id);

        Task<List<TEntity>> GetAllAsync();
    }
}
