using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models.Items;

namespace OnMenu
{
    /// <summary>
    /// Datastore for recipes
    /// </summary>
    public class RecipeDataStore : IDataStore<Recipe>
    {
        /// <summary>
        /// List of recipes
        /// </summary>
        protected List<Recipe> recipes;
        /// <summary>
        /// Whether the data store was initialized or not
        /// </summary>
        private bool initialized;

        /// <summary>
        /// Instantiates a new data store for recipes
        /// </summary>
        public RecipeDataStore()
        {
            initialized = false;
        }

        /// <summary>
        /// Initializes the datastore
        /// </summary>
        /// <returns>An async task</returns>
        public async Task InitializeDataStore()
        {
            if (!initialized)
            {
                initialized = true;
                recipes = await App.DB.GetRecipesAsync();
                if (App.DB.RecipeList != null && App.DB.RecipeList.Count != 0)
                {
                    recipes = App.DB.RecipeList;
                }
                else
                {
                    recipes = new List<Recipe>();
                    List<Recipe> _recipes = new List<Recipe>();
                    

                    foreach (Recipe recipe in _recipes)
                    {
                        recipes.Add(recipe);
                        await App.DB.SaveRecipeAsync(recipe);

                    }
                }
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
            await App.DB.SaveRecipeAsync(recipe);
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
            await App.DB.DeleteRecipeAsync(_recipe);
            if (await App.DB.SaveRecipeAsync(recipe) > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
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
            if (await App.DB.DeleteRecipeAsync(_recipe) > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
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

        /// <summary>
        /// Edits all the items in the db
        /// </summary>
        /// <returns></returns>
        public async Task<bool> EditItemsAsync()
        {
            foreach (Recipe r in recipes)
            {
                await App.DB.DeleteRecipeAsync(r);
                await App.DB.SaveRecipeAsync(r);
            }
            return await Task.FromResult(true);
        }

    }
}
