using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models;
using OnMenu.Models.Items;

namespace OnMenu
{
    /// <summary>
    /// Datastore for ingredients
    /// </summary>
    //TODO add DB connection
    public class IngredientDataStore : IDataStore<Ingredient>
    {
        /// <summary>
        /// List of ingredients
        /// </summary>
        List<Ingredient> ingredients;


        /// <summary>
        /// Instantiates a new data store for ingredients
        /// </summary>
        public IngredientDataStore()
        {
            ingredients = new List<Ingredient>();
            var _ingredients = new List<Ingredient>
            {
                new Ingredient ( "Rice", "Cereals", "g",  true, 0.50f),
                new Ingredient ( "Egg", "Protein", "units",  true, 1.0f),
            };

            foreach (Ingredient ingredient in _ingredients)
            {
                ingredients.Add(ingredient);
            }
        }

        /// <summary>
        /// Adds an ingredient asyncronously
        /// </summary>
        /// <param name="ingredient">The name of the ingredient to add</param>
        /// <returns>A boolean indicating if the ingredient was added</returns>
        public async Task<bool> AddItemAsync(Ingredient ingredient)
        {
            //TODO control same name or id
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Updates an ingredient asyncronously, dependent on the id
        /// </summary>
        /// <param name="ingredient">The ingredient to update</param>
        /// <returns>A boolean indicating if the ingredient was updated</returns>
        public async Task<bool> UpdateItemAsync(Ingredient ingredient)
        {
            var _ingredient = ingredients.Where((Ingredient arg) => arg.Id == ingredient.Id).FirstOrDefault();
            ingredients.Remove(_ingredient);
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Deletes an ingredient asyncronously, dependent on the id
        /// </summary>
        /// <param name="id">The id of the ingredient to delete</param>
        /// <returns>>A boolean indicating if the ingredient was deleted</returns>
        public async Task<bool> DeleteItemAsync(int id)
        {
            var _ingredient = ingredients.Where((Ingredient arg) => arg.Id == id).FirstOrDefault();
            ingredients.Remove(_ingredient);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Gets an ingredient asyncronously, dependent on the id
        /// </summary>
        /// <param name="id">The id of the ingredient to get</param>
        /// <returns>The ingredient</returns>
        public async Task<Ingredient> GetItemAsync(int id)
        {
            return await Task.FromResult(ingredients.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Gets the ingredients
        /// </summary>
        /// <param name="forceRefresh">Whether the list should be forced to refresh (default == false)</param>
        /// <returns>The list of ingredients</returns>
        public async Task<IEnumerable<Ingredient>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(ingredients);
        }
    }
}
