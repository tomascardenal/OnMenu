using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models;
using OnMenu.Models.Items;

namespace OnMenu
{
    /// <summary>
    /// Datastore for recipoes
    /// </summary>
    //TODO add DB connection
    public class RecipeDataStore : IDataStore<Recipe>
    {
        /// <summary>
        /// List of recipes
        /// </summary>
        List<Recipe> recipes;

        /// <summary>
        /// Instantiates a new data store for recipes
        /// </summary>
        public RecipeDataStore()
        {
            recipes = new List<Recipe>();
            var _recipes = new List<Recipe>
            {
            };

            foreach (Recipe recipe in _recipes)
            {
                recipes.Add(recipe);
            }
        }

        /// <summary>
        /// Adds a recipe asyncronously
        /// </summary>
        /// <param name="recipe">The name of the recipe to add</param>
        /// <returns>A boolean indicating if the recipe was added</returns>
        public async Task<bool> AddItemAsync(Recipe recipe)
        {
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Updates a recipe asyncronously, dependent on the id
        /// </summary>
        /// <param name="recipe">The recipe to update</param>
        /// <returns>A boolean indicating if the recipe was updated</returns>
        public async Task<bool> UpdateItemAsync(Recipe recipe)
        {
            var _recipe = recipes.Where((Recipe arg) => arg.Id == recipe.Id).FirstOrDefault();
            recipes.Remove(_recipe);
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Deletes a recipe asyncronously, dependent on the id
        /// </summary>
        /// <param name="id">The id of the recipe to delete</param>
        /// <returns>>A boolean indicating if the recipe was deleted</returns>
        public async Task<bool> DeleteItemAsync(int id)
        {
            var _recipe = recipes.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            recipes.Remove(_recipe);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Gets a recipe asyncronously, dependent on the id
        /// </summary>
        /// <param name="id">The id of the recipe to get</param>
        /// <returns>The recipe</returns>
        public async Task<Recipe> GetItemAsync(int id)
        {
            return await Task.FromResult(recipes.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Gets the recipes
        /// </summary>
        /// <param name="forceRefresh">Whether the list should be forced to refresh (default == false)</param>
        /// <returns>The list of recipes</returns>
        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(recipes);
        }
    }
}
