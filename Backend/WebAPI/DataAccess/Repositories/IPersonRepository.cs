using Backend.Entities;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        Task<bool> EmailExistAsync(string email);
    }
}
