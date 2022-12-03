using Backend.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.UnitTests
{
    public class CategoryMock
    {
        public List<Category> Categories = new List<Category>() 
            {
                new Category { ID=1, Description="Herramientas" },
                new Category { ID=2, Description="Ropa" },
                new Category { ID=3, Description="Útiles" }
            };

        public List<Category> GetAll()
        {
            return Categories;
        }

        public Category? GetCategoryById(int id)
        {
            return Categories.FirstOrDefault(c => c.ID == id);
        }
    }
}
