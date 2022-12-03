using Backend.Entities;

namespace Backend.MVC.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
    }
}
