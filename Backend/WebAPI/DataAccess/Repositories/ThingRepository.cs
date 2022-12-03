using Backend.Entities;

namespace Backend.WebAPI.DataAccess.Repositories
{
    public class ThingRepository : GenericRepository<Thing>, IThingRepository
    {
        public ThingRepository(ThingsContext context) : base(context) { }
    }
}
