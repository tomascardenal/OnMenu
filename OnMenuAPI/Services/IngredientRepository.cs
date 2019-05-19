
using OnMenuAPI.Interfaces;
using OnMenuAPI.Models;
using System;
using System.Collections.Generic;

namespace OnMenuAPI.Services
{
    public class IngredientRepository : IItemRepository<Ingredient>
    {
        public  IEnumerable<Ingredient> Index => throw new NotImplementedException();

        public  void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public bool DoesItemExist(string id)
        {
            throw new NotImplementedException();
        }

        public Ingredient Find(string id)
        {
            throw new NotImplementedException();
        }

        public  void Insert(Ingredient item)
        {
            throw new NotImplementedException();
        }

        public void Update(Ingredient item)
        {
            throw new NotImplementedException();
        }
    }
}
