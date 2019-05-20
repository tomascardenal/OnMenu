using OnMenu.Models.Calendar;
using OnMenu.Models.Items;
using SQLite;
using System.Collections.Generic;
using System.Linq;
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
        /// Command to stop the connection
        /// </summary>
        public Command StopConnectionCommand { get; set; }

        /// <summary>
        /// List of recipes
        /// </summary>
        private List<Recipe> recipeList;
        public List<Recipe> RecipeList { get { return recipeList; } }
        /// <summary>
        /// List of ingredients
        /// </summary>
        private List<Ingredient> ingredientList;
        public List<Ingredient> IngredientList { get { return ingredientList; } }
        /// <summary>
        /// List of calendar entries
        /// </summary>
        private List<RecipeCalendarEntry> calendarList;
        public List<RecipeCalendarEntry> CalendarList { get { return calendarList; } }

        /// <summary>
        /// Default constructor for this controller
        /// </summary>
        /// <param name="dbPath">The path to the SQLite file</param>
        public ItemDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Ingredient>().Wait();
            _database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<RecipeCalendarEntry>().Wait();

            StopConnectionCommand = new Command(async () => await StopConnectionAsync());
        }

        /// <summary>
        /// Fetches the ingredients asyncronously into the list
        /// </summary>
        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            if (ingredientList != null)
            {
                ingredientList.Clear();
            }
            ingredientList = await _database.Table<Ingredient>().ToListAsync();
            return ingredientList;
        }

        /// <summary>
        /// Gets the ingredient from the given id asyncronously
        /// </summary>
        /// <param name="id">The id of the ingredient to retrieve</param>
        /// <returns>The ingredient with the given id</returns>
        public async Task<Ingredient> GetIngredientAsync(int id)
        {
            return await _database.Table<Ingredient>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves an ingredient to the database asyncronously
        /// </summary>
        /// <param name="ingredient">The ingredient to store</param>
        /// <returns>An integer indicating the primary key of the ingredient</returns>
        public async Task<int> SaveIngredientAsync(Ingredient ingredient)
        {
            if (IngredientList != null && IngredientList.Any(i => i.Id == ingredient.Id || i.Name == ingredient.Name))
            {
                return await _database.UpdateAsync(ingredient);
            }
            else if (ingredient.Id != 0)
            {
                return await _database.InsertOrReplaceAsync(ingredient);
            }
            else
            {
                return await _database.InsertAsync(ingredient);
            }
        }

        /// <summary>
        /// Deletes an ingredient from the database asyncronously
        /// </summary>
        /// <param name="ingredient">The ingredient to delete</param>
        /// <returns>An integer indicating the deleted id</returns>
        public async Task<int> DeleteIngredientAsync(Ingredient ingredient)
        {
            IngredientList.Remove(ingredient);
            return await _database.DeleteAsync<Ingredient>(ingredient.Id);
        }

        /// <summary>
        /// Fetches the recipes asyncronously into the list
        /// </summary>
        /// <returns>A list with the recipes</returns>
        public async Task<List<Recipe>> GetRecipesAsync()
        {
            if (recipeList != null)
            {
                recipeList.Clear();
            }
            recipeList = await _database.Table<Recipe>().ToListAsync();
            return recipeList;
        }

        /// <summary>
        /// Gets the recipe from the given id asyncronously
        /// </summary>
        /// <param name="id">The id of the recipe to retrieve</param>
        /// <returns>The recipe with the given id</returns>
        public async Task<Recipe> GetRecipeAsync(int id)
        {
            return await _database.Table<Recipe>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves a recipe to the database asyncronously
        /// </summary>
        /// <param name="recipe">The recipe to store</param>
        /// <returns>An integer indicating the key of the recipe or the affected rows (if updating)</returns>
        public async Task<int> SaveRecipeAsync(Recipe recipe)
        {
            if (RecipeList != null && RecipeList.Any(r => r.Id == recipe.Id || r.Name == recipe.Name))
            {
                return await _database.UpdateAsync(recipe);
            }
            else if (recipe.Id != 0)
            {
                return await _database.InsertOrReplaceAsync(recipe);
            }
            else
            {
                return await _database.InsertAsync(recipe);
            }
        }

        /// <summary>
        /// Deletes a recipe from the database asyncronously
        /// </summary>
        /// <param name="recipe">The recipe to delete</param>
        /// <returns>An integer indicating the number of affected rows</returns>
        public async Task<int> DeleteRecipeAsync(Recipe recipe)
        {
            recipeList.Remove(recipe);
            return await _database.DeleteAsync<Recipe>(recipe.Id);
        }


        /// <summary>
        /// Fetches the calendar entries asyncronously into the list
        /// </summary>
        public async Task<List<RecipeCalendarEntry>> GetCalendarEntriesAsync()
        {
            if (calendarList != null)
            {
                calendarList.Clear();
            }
            calendarList = await _database.Table<RecipeCalendarEntry>().ToListAsync();
            return calendarList;
        }

        /// <summary>
        /// Gets the calendar entry from the given id asyncronously
        /// </summary>
        /// <param name="id">The id of the calendar entry to retrieve</param>
        /// <returns>The calendar entry with the given id</returns>
        public async Task<RecipeCalendarEntry> GetCalendarEntryAsync(int id)
        {
            return await _database.Table<RecipeCalendarEntry>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Saves an calendar entry to the database asyncronously
        /// </summary>
        /// <param name="entry">The calendar entry to store</param>
        /// <returns>An integer indicating the primary key of the calendar entry</returns>
        public async Task<int> SaveCalendarEntryAsync(RecipeCalendarEntry entry)
        {
            if (CalendarList != null && CalendarList.Any(e => e.Id == entry.Id))
            {
                return await _database.UpdateAsync(entry);
            }
            else if (entry.Id != 0)
            {
                return await _database.InsertOrReplaceAsync(entry);
            }
            else
            {
                return await _database.InsertAsync(entry);
            }
        }

        /// <summary>
        /// Deletes a calendar entry from the database asyncronously
        /// </summary>
        /// <param name="entry">The calendar entry to delete</param>
        /// <returns>An integer indicating the deleted id</returns>
        public async Task<int> DeleteCalendarEntryAsync(RecipeCalendarEntry entry)
        {
            CalendarList.Remove(entry);
            return await _database.DeleteAsync<RecipeCalendarEntry>(entry.Id);
        }

        /// <summary>
        /// Stops the database connections
        /// </summary>
        /// <returns>A task</returns>
        public async Task StopConnectionAsync()
        {
            await _database.CloseAsync();
        }
    }
}
