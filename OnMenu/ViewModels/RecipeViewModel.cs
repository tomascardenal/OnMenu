using OnMenu.Models.Items;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnMenu
{
    /// <summary>
    /// Viewmodel for the recipe
    /// </summary>
    public class RecipeViewModel : BaseViewModel
    {
        /// <summary>
        /// The recipe collection
        /// </summary>
        public ObservableCollection<Recipe> Recipes { get; set; }
        /// <summary>
        /// Command to load recipes
        /// </summary>
        public Command LoadRecipesCommand { get; set; }
        /// <summary>
        /// Command to add recipoes
        /// </summary>
        public Command AddRecipesCommand { get; set; }
        /// <summary>
        /// Command to delete recipes
        /// </summary>
        public Command DeleteRecipesCommand { get; set; }
        /// <summary>
        /// Command to update recipes
        /// </summary>
        public Command UpdateRecipesCommand { get; set; }
        /// <summary>
        /// Recipe datastore
        /// </summary>
        public IDataStore<Recipe> RecipeDataStore => ServiceLocator.Instance.Get<IDataStore<Recipe>>() ?? new RecipeDataStore();

        /// <summary>
        /// Default constructor, instantates a new viewmodel
        /// </summary>
        public RecipeViewModel()
        {
            Title = "Recipe List";
            Recipes = new ObservableCollection<Recipe>();
            LoadRecipesCommand = new Command(async () => await ExecuteLoadRecipesCommand());
            AddRecipesCommand = new Command<Recipe>(async (Recipe recipe) => await AddRecipe(recipe));
            DeleteRecipesCommand = new Command<Recipe>(async (Recipe recipe) => await DeleteRecipe(recipe));
            UpdateRecipesCommand = new Command<Recipe>(async (Recipe recipe) => await UpdateRecipe(recipe));
        }

        /// <summary>
        /// Executes the actions of the loadrecipes command
        /// </summary>
        /// <returns>the task</returns>
        async Task ExecuteLoadRecipesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Recipes.Clear();
                var recipes = await RecipeDataStore.GetItemsAsync();
                foreach (var recipe in recipes)
                {
                    Recipes.Add(recipe);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds a recipe
        /// </summary>
        /// <param name="recipe">the recipe to add</param>
        /// <returns>the task</returns>
        async Task AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
            await RecipeDataStore.AddItemAsync(recipe);
        }

        /// <summary>
        /// Deletes a recipe
        /// </summary>
        /// <param name="recipe">>the recipe to delete</param>
        /// <returns>the task</returns>
        async Task DeleteRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
            await RecipeDataStore.DeleteItemAsync(recipe.Id);
        }

        /// <summary>
        /// Updates a recipe
        /// </summary>
        /// <param name="recipe">>the recipe to update</param>
        /// <returns>the task</returns>
        async Task UpdateRecipe(Recipe recipe)
        {
            Recipe _recipe = null;
            foreach(Recipe rec in Recipes)
            {
                if(rec.Id == recipe.Id)
                {
                    _recipe = rec;
                    break;
                }
            }
            if (_recipe != null)
            {
                Recipes.Remove(_recipe);
                Recipes.Add(recipe);
                await RecipeDataStore.UpdateItemAsync(recipe);
            }

        }
    }
}

