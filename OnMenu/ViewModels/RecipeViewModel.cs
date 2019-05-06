﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OnMenu
{
    public class RecipeViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Recipe> Recipes { get; set; }
        public Command LoadRecipesCommand { get; set; }
        public Command AddRecipesCommand { get; set; }
        public IDataStore<Models.Recipe> RecipeDataStore => ServiceLocator.Instance.Get<IDataStore<Models.Recipe>>() ?? new RecipeDataStore();


        public RecipeViewModel()
        {
            Title = "Recipe List";
            Recipes = new ObservableCollection<Models.Recipe>();
            LoadRecipesCommand = new Command(async () => await ExecuteLoadRecipesCommand());
            AddRecipesCommand = new Command<Models.Recipe>(async (Models.Recipe recipe) => await AddRecipe(recipe));
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

        async Task AddRecipe(Models.Recipe recipe)
        {
            Recipes.Add(recipe);
            await RecipeDataStore.AddItemAsync(recipe);
        }
    }
}

