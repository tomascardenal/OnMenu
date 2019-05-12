using OnMenuAPI.Interfaces;
using OnMenuAPI.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnMenuAPI.Services
{
    public class RecipeRepository : IItemRepository<Recipe>
    {
        public IEnumerable<Recipe> Index => throw new NotImplementedException();

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public bool DoesItemExist(string id)
        {
            throw new NotImplementedException();
        }

        public Recipe Find(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Recipe item)
        {
            throw new NotImplementedException();
        }

        public void Update(Recipe item)
        {
            throw new NotImplementedException();
        }
    }
}
