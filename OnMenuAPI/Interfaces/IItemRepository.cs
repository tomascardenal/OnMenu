using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnMenuAPI.Interfaces
{
    public interface IItemRepository<T>
    {
        bool DoesItemExist(string id);
        IEnumerable<T> Index { get; }
        T Find(string id);
        void Insert(T item);
        void Update(T item);
        void Delete(string id);
    }
}
