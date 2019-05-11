using OnMenuAPI.Interfaces;
using OnMenuAPI.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnMenuAPI.Services
{
    public class ItemRepository : IItemRepository
    {
        public virtual IEnumerable<Item> Index => throw new NotImplementedException();

        public virtual void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public virtual bool DoesItemExist(string id)
        {
            throw new NotImplementedException();
        }

        public virtual Item Find(string id)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(Item item)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
