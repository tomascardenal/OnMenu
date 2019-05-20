using System.Collections.Generic;

namespace OnMenuAPI.Interfaces
{
    /// <summary>
    /// Not implemented, interface for repositories 
    /// </summary>
    /// <typeparam name="T">A generic type</typeparam>
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
