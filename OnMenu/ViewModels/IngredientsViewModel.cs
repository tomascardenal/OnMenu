using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using OnMenu.Models.Items;

namespace OnMenu
{
    public class IngredientsViewModel : BaseViewModel
    {
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public Command LoadIngredientsCommand { get; set; }
        public Command AddIngredientsCommand { get; set; }
        public IDataStore<Ingredient> IngredientDataStore => ServiceLocator.Instance.Get<IDataStore<Ingredient>>() ?? new IngredientDataStore();


        public IngredientsViewModel()
        {
            Title = "Ingredient list";
            Ingredients = new ObservableCollection<Ingredient>();
            LoadIngredientsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await AddIngredient(ingredient));
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

        async Task AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            await IngredientDataStore.AddItemAsync(ingredient);
        }
    }
}
