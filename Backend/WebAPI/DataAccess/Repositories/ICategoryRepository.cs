using Backend.Entities;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<bool> CategoryExist(string desc);
    }
}
