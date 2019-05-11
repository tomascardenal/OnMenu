using OnMenuAPI.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnMenuAPI.Interfaces
{
    public interface IItemRepository
    {
        bool DoesItemExist(string id);
        IEnumerable<Item> Index { get; }
        Item Find(string id);
        void Insert(Item item);
        void Update(Item item);
        void Delete(string id);
    }
}
