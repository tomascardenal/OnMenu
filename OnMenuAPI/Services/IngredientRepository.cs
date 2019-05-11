using OnMenuAPI.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnMenuAPI.Services
{
    public class IngredientRepository : ItemRepository
    {
        public override IEnumerable<Item> Index => throw new NotImplementedException();

        public override void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public override bool DoesItemExist(string id)
        {
            throw new NotImplementedException();
        }

        public override Item Find(string id)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Item item)
        {
            throw new NotImplementedException();
        }

        public override void Update(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
