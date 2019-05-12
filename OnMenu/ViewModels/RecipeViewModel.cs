using OnMenu.Models.Items;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnMenu
{
    public class RecipeViewModel : BaseViewModel
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public Command LoadRecipesCommand { get; set; }
        public Command AddRecipesCommand { get; set; }
        public IDataStore<Recipe> RecipeDataStore => ServiceLocator.Instance.Get<IDataStore<Recipe>>() ?? new RecipeDataStore();


        public RecipeViewModel()
        {
            Title = "Recipe List";
            Recipes = new ObservableCollection<Recipe>();
            LoadRecipesCommand = new Command(async () => await ExecuteLoadRecipesCommand());
            AddRecipesCommand = new Command<Recipe>(async (Recipe recipe) => await AddRecipe(recipe));
        }

        async Task ExecuteLoadRecipesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Recipes.Clear();
                var recipes = await RecipeDataStore.GetItemsAsync(true);
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

        async Task AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
            await RecipeDataStore.AddItemAsync(recipe);
        }

        async Task DeleteRecipe(Recipe recipe)
        {
            Recipes.Remove(recipe);
            await RecipeDataStore.DeleteItemAsync(recipe.Id);
        }

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

