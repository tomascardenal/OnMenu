using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnMenu
{
    /// <summary>
    /// Interface for a DataStore
    /// </summary>
    /// <typeparam name="T">Type of the datastore</typeparam>
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
