using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnMenu
{
    public interface IDataStore<T>
    {
        Task<bool> AddIngredientAsync(T ingredient);
        Task<bool> UpdateIngredientAsync(T ingredient);
        Task<bool> DeleteIngredientAsync(string id);
        Task<T> GetIngredientAsync(string id);
        Task<IEnumerable<T>> GetIngredientsAsync(bool forceRefresh = false);
    }
}
