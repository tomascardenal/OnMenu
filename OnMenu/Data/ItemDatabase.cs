using OnMenu.Models.Items;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnMenu.Data
{
    /// <summary>
    /// Database controller for CRUD operations on local SQLite database
    /// </summary>
    public class ItemDatabase
    {
        /// <summary>
        /// Async SQLite connection
        /// </summary>
        readonly SQLiteAsyncConnection _database;

        /// <summary>
        /// Command to add ingredients
        /// </summary>
        public Command AddIngredientsCommand { get; set; }
        /// <summary>
        /// Command to delete ingredients
        /// </summary>
        public Command DeleteIngredientsCommand { get; set; }
        /// <summary>
        /// Command to add recipoes
        /// </summary>
        public Command AddRecipesCommand { get; set; }
        /// <summary>
        /// Command to delete recipes
        /// </summary>
        public Command DeleteRecipesCommand { get; set; }

        /// <summary>
        /// List of recipes
        /// </summary>
        private List<Recipe> recipeList;
        public List<Recipe> RecipeList { get; }
        /// <summary>
        /// List of ingredients
        /// </summary>
        private List<Ingredient> ingredientList;
        public List<Ingredient> IngredientList { get; }

        /// <summary>
        /// Default constructor for this controller
        /// </summary>
        /// <param name="dbPath">The path to the SQLite file</param>
        public ItemDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Ingredient>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();

            AddRecipesCommand = new Command<Recipe>(async (Recipe recipe) => await SaveRecipeAsync(recipe));
            DeleteRecipesCommand = new Command<Recipe>(async (Recipe recipe) => await DeleteRecipeAsync(recipe));

            AddIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await SaveIngredientAsync(ingredient));
            DeleteIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await DeleteIngredientAsync(ingredient));
        }

        /// <summary>
        /// Fetches the ingredients asyncronously into the list
        /// </summary>
        public async void GetIngredientsAsync()
        {
            if (ingredientList != null)
            {
                ingredientList.Clear();
            }
            ingredientList = await _database.Table<Ingredient>().ToListAsync();
        }

        /// <summary>
        /// Gets the ingredient from the given id asyncronously
        /// </summary>
        /// <param name="id">The id of the ingredient to retrieve</param>
        /// <returns>The ingredient with the given id</returns>
        public Task<Ingredient> GetIngredientAsync(int id)
        {
            return _database.Table<Ingredient>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves an ingredient to the database asyncronously
        /// </summary>
        /// <param name="ingredient">The ingredient to store</param>
        /// <returns>An integer indicating the number of affected rows</returns>
        public Task<int> SaveIngredientAsync(Ingredient ingredient)
        {
            if (IngredientList!=null && IngredientList.Any(i => i.Id == ingredient.Id || i.Name == ingredient.Name))
            {
                return _database.InsertOrReplaceAsync(ingredient);
            }
            else if (ingredient.Id != 0)
            {
                return _database.UpdateAsync(ingredient);
            }
            else
            {
                return _database.InsertAsync(ingredient);
            }
        }

        /// <summary>
        /// Deletes an ingredient from the database asyncronously
        /// </summary>
        /// <param name="ingredient">The ingredient to delete</param>
        /// <returns>An integer indicating the number of affected rows</returns>
        public Task<int> DeleteIngredientAsync(Ingredient ingredient)
        {
            return _database.DeleteAsync<Ingredient>(ingredient);
        }

        /// <summary>
        /// Fetches the recipes asyncronously into the list
        /// </summary>
        public async void GetRecipesAsync()
        {
            recipeList = await _database.Table<Recipe>().ToListAsync();
        }

        /// <summary>
        /// Gets the recipe from the given id asyncronously
        /// </summary>
        /// <param name="id">The id of the recipe to retrieve</param>
        /// <returns>The recipe with the given id</returns>
        public Task<Recipe> GetRecipeAsync(int id)
        {
            return _database.Table<Recipe>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves a recipe to the database asyncronously
        /// </summary>
        /// <param name="recipe">The recipe to store</param>
        /// <returns>An integer indicating the number of affected rows</returns>
        public Task<int> SaveRecipeAsync(Recipe recipe)
        {
            if (RecipeList!=null && RecipeList.Any(r => r.Id == recipe.Id || r.Name == recipe.Name))
            {
                return _database.InsertOrReplaceAsync(recipe);
            }
            else if (recipe.Id != 0)
            {
                return _database.UpdateAsync(recipe);
            }
            else
            {
                return _database.InsertAsync(recipe);
            }
        }

        /// <summary>
        /// Deletes a recipe from the database asyncronously
        /// </summary>
        /// <param name="recipe">The recipe to delete</param>
        /// <returns>An integer indicating the number of affected rows</returns>
        public Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            return _database.DeleteAsync<Recipe>(recipe);
        }
    }
}
