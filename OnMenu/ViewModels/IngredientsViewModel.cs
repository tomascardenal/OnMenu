using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using OnMenu.Models;

namespace OnMenu
{
    public class IngredientsViewModel : BaseViewModel
    {
        public ObservableCollection<Models.Ingredient> Ingredients { get; set; }
        public Command LoadIngredientsCommand { get; set; }
        public Command AddIngredientsCommand { get; set; }
        public IDataStore<Models.Ingredient> IngredientDataStore => ServiceLocator.Instance.Get<IDataStore<Models.Ingredient>>() ?? new IngredientDataStore();


        public IngredientsViewModel()
        {
            Title = "Ingredient list";
            Ingredients = new ObservableCollection<Models.Ingredient>();
            LoadIngredientsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddIngredientsCommand = new Command<Models.Ingredient>(async (Models.Ingredient ingredient) => await AddIngredient(ingredient));
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Ingredients.Clear();
                var ingredients = await IngredientDataStore.GetItemsAsync(true);
                foreach (var ingredient in ingredients)
                {
                    Ingredients.Add(ingredient);
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

        async Task AddIngredient(Models.Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            await IngredientDataStore.AddItemAsync(ingredient);
        }
    }
}
