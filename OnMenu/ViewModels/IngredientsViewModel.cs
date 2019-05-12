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
        public Command DeleteIngredientsCommand { get; set; }
        public Command UpdateIngredientsCommand { get; set; }
        public IDataStore<Ingredient> IngredientDataStore => ServiceLocator.Instance.Get<IDataStore<Ingredient>>() ?? new IngredientDataStore();


        public IngredientsViewModel()
        {
            Title = "Ingredient list";
            Ingredients = new ObservableCollection<Ingredient>();
            LoadIngredientsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            AddIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await AddIngredient(ingredient));
            DeleteIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await DeleteIngredient(ingredient));
            UpdateIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await UpdateIngredient(ingredient));
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

        async Task DeleteIngredient(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
            await IngredientDataStore.DeleteItemAsync(ingredient.Id);
        }
        async Task UpdateIngredient(Ingredient ingredient)
        {
            Ingredient _ingredient = null;
            foreach(Ingredient ing in Ingredients){
                if(ing.Id == ingredient.Id)
                {
                    _ingredient = ing;
                }
            }
            Ingredients.Remove(_ingredient);
            Ingredients.Add(ingredient);
            await IngredientDataStore.UpdateItemAsync(ingredient);
        }
    }
}
