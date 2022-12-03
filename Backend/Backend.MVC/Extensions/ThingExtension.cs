using Backend.Entities;
using Backend.MVC.Models;

namespace Backend.MVC.Extensions
{
    public static class ThingExtension
    {
        public static ThingViewModel ToViewModel(this Thing thing)
        {
            var category = new CategoryViewModel
            {
                ID = thing.Category.ID,
                Description = thing.Category.Description
            };

            return new ThingViewModel
            {
                ID = thing.ID,
                Description = thing.Description,
                CreationDate = thing.CreationDate,
                Category = category
            };
        }

        public static List<ThingViewModel> ToViewModels(this List<Thing> things)
        {
            var list = new List<ThingViewModel>();
            things.ForEach(a => list.Add(a.ToViewModel()));

            return list;
        }
    }
}
