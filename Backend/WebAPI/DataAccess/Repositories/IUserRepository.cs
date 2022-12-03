using Backend.Entities;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public interface IUserRepository<TEntity> : IGenericRepository<User>
        where TEntity : User
    {
        Task<bool> UserExist(string user);

        Task<User?> GetByUser(string user);
    }
}
