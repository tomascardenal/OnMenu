using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using OnMenu.Models.Items;

namespace OnMenu
{
    /// <summary>
    /// Viewmodel for the ingredients
    /// </summary>
    public class IngredientsViewModel : BaseViewModel
    {
        /// <summary>
        /// The ingredient collection
        /// </summary>
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        /// <summary>
        /// Command to load ingredients
        /// </summary>
        public Command LoadIngredientsCommand { get; set; }
        /// <summary>
        /// Command to add ingredients
        /// </summary>
        public Command AddIngredientsCommand { get; set; }
        /// <summary>
        /// Command to delete ingredients
        /// </summary>
        public Command DeleteIngredientsCommand { get; set; }
        /// <summary>
        /// Command to update ingredients
        /// </summary>
        public Command UpdateIngredientsCommand { get; set; }
        /// <summary>
        /// Ingredient datastore
        /// </summary>
        public IDataStore<Ingredient> IngredientDataStore => ServiceLocator.Instance.Get<IDataStore<Ingredient>>() ?? new IngredientDataStore();

        /// <summary>
        /// Default constructor, instantates a new viewmodel
        /// </summary>
        public IngredientsViewModel()
        {
            Title = "Ingredient list";
            Ingredients = new ObservableCollection<Ingredient>();
            LoadIngredientsCommand = new Command(async () => await ExecuteLoadIngredientsCommand());
            AddIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await AddIngredient(ingredient));
            DeleteIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await DeleteIngredient(ingredient));
            UpdateIngredientsCommand = new Command<Ingredient>(async (Ingredient ingredient) => await UpdateIngredient(ingredient));
        }

        /// <summary>
        /// Executes the actions of the loadingredients command
        /// </summary>
        /// <returns>the task</returns>
        async Task ExecuteLoadIngredientsCommand()
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

        /// <summary>
        /// Adds an ingredient
        /// </summary>
        /// <param name="ingredient">the ingredient to add</param>
        /// <returns>the task</returns>
        async Task AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
            await IngredientDataStore.AddItemAsync(ingredient);
        }

        /// <summary>
        /// Deletes an ingredient
        /// </summary>
        /// <param name="ingredient">>the ingredient to delete</param>
        /// <returns>the task</returns>
        async Task DeleteIngredient(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
            await IngredientDataStore.DeleteItemAsync(ingredient.Id);
        }

        /// <summary>
        /// Updates an ingredient
        /// </summary>
        /// <param name="ingredient">>the ingredient to update</param>
        /// <returns>the task</returns>
        async Task UpdateIngredient(Ingredient ingredient)
        {
            Ingredient _ingredient = null;
            foreach(Ingredient ing in Ingredients){
                if(ing.Id == ingredient.Id)
                {
                    _ingredient = ing;
                    break;
                }
            }
            if (_ingredient != null)
            {
                Ingredients.Remove(_ingredient);
                Ingredients.Add(ingredient);
                await IngredientDataStore.UpdateItemAsync(ingredient);
            }
        }
    }
}
