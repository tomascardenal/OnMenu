using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models.Items;

namespace OnMenu
{
    /// <summary>
    /// Datastore for ingredients
    /// </summary>
    public class IngredientDataStore : IDataStore<Ingredient>
    {
        /// <summary>
        /// List of ingredients
        /// </summary>
        protected List<Ingredient> ingredients;
        /// <summary>
        /// Whether the data store was initialized or not
        /// </summary>
        private bool initialized;

        /// <summary>
        /// Instantiates a new data store for ingredients
        /// </summary>
        public IngredientDataStore()
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
                ingredients = await App.DB.GetIngredientsAsync();
                if (ingredients == null || ingredients.Count == 0)
                {
                    ingredients = new List<Ingredient>();
                    var _ingredients = new List<Ingredient>
                {
                        new Ingredient ( "Arroz", "Cereales", "g",  true, 0.50f, 100),
                        new Ingredient ( "Huevo", "Proteína", "unidades",  true, 1.0f, 6),
                        new Ingredient ( "Sal", "General", "g", false, 0.30f, 1000),
                        new Ingredient ( "Carne de ternera", "Carne", "g", false, 14.9f, 1000),
                        new Ingredient ( "Espaguetis", "Pasta", "g", true, 0.77f, 1000),
                        new Ingredient ( "Lechuga", "Verdura", "unidades", false, 0.75f, 1),
                        new Ingredient ("Tomate frito", "Preparados", "g", true, 0.40f, 200)
                };
                    foreach (Ingredient ingredient in _ingredients)
                    {
                        //Prevent default ingredients from being deleted
                        ingredient.CanDelete = false;
                        ingredients.Add(ingredient);
                        await App.DB.SaveIngredientAsync(ingredient);
                    }
                }
            }
        }

        /// <summary>
        /// Adds an ingredient asyncronously
        /// </summary>
        /// <param name="ingredient">The name of the ingredient to add</param>
        /// <returns>A boolean indicating if the ingredient was added</returns>
        public async Task<bool> AddItemAsync(Ingredient ingredient)
        {
            ingredients.Add(ingredient);
            int i = await App.DB.SaveIngredientAsync(ingredient);
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
            await App.DB.DeleteIngredientAsync(_ingredient);
            if (await App.DB.SaveIngredientAsync(ingredient) > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }

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
            if (await App.DB.DeleteIngredientAsync(_ingredient) > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
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
            return await App.DB.GetIngredientsAsync();
        }

        /// <summary>
        /// Edits all the items in the db
        /// </summary>
        /// <returns>A task with a boolean indicating it was finished</returns>
        public async Task<bool> EditItemsAsync()
        {
            foreach (Ingredient i in ingredients)
            {
                await App.DB.DeleteIngredientAsync(i);
                await App.DB.SaveIngredientAsync(i);
            }
            return await Task.FromResult(true);
        }
    }
}
