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
        /// <summary>
        /// Implement to initialize a data store
        /// </summary>
        /// <returns>A task</returns>
        Task InitializeDataStore();
        /// <summary>
        /// Implement to add items to a data store
        /// </summary>
        /// <param name="item">The item to add, of a generic type</param>
        /// <returns>A boolean indicating if the task was completed</returns>
        Task<bool> AddItemAsync(T item);
        /// <summary>
        /// Implement to update items on a data store
        /// </summary>
        /// <param name="item">The item to update, of a generic type</param>
        /// <returns>A boolean indicating if the task was completed</returns>
        Task<bool> UpdateItemAsync(T item);
        /// <summary>
        /// Implement to delete items on a data store
        /// </summary>
        /// <param name="item">The item to delete, of a generic type</param>
        /// <returns>A boolean indicating if the task was completed</returns>
        Task<bool> DeleteItemAsync(int id);
        /// <summary>
        /// Implement to get items from a data store
        /// </summary>
        /// <param name="id">The id of the item to get</param>
        /// <returns>The item</returns>
        Task<T> GetItemAsync(int id);
        /// <summary>
        /// Implement to get all the items from a data store
        /// </summary>
        /// <param name="forceRefresh">Whether the list should be refreshed</param>
        /// <returns>The list with the items</returns>
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        /// <summary>
        /// Implement to edit all items from a database with the values on the data store list
        /// </summary>
        /// <returns>A boolean indicating if the task was completed</returns>
        Task<bool> EditItemsAsync();
    }
}
