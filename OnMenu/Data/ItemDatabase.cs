using OnMenu.Models.Items;
using SQLite;
using System;
using System.Collections.Generic;
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
        /// Default constructor for this controller
        /// </summary>
        /// <param name="dbPath">The path to the SQLite file</param>
        public ItemDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Ingredient>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();
        }

        /// <summary>
        /// Gets the ingredients asyncronously
        /// </summary>
        /// <returns>The stored list of ingredients</returns>
        public Task<List<Ingredient>> GetIngredientsAsync()
        {
            return _database.Table<Ingredient>().ToListAsync();
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
            if (ingredient.Id != 0)
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
        /// Gets the recipes asyncronously
        /// </summary>
        /// <returns>The stored list of recipes</returns>
        public Task<List<Recipe>> GetRecipesAsync()
        {
            return _database.Table<Recipe>().ToListAsync();
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
            if (recipe.Id != 0)
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
