using Backend.Entities;
using System.Runtime.CompilerServices;

namespace Backend.MVC.Services
{
    public interface IThingService
    {
            Task<List<Thing>> GetAllAsync();
            Task<Thing?> GetByIdAsync(int id);
            Task<Thing> AddAsync(Thing thing);
            void Update(Thing thing);
            void Delete(Thing thing);
            public Task<int> SaveChangesAsync();

    }
}
