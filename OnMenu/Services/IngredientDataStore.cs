using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models;
using OnMenu.Models.Items;

namespace OnMenu
{
    public class IngredientDataStore : IDataStore<Ingredient>
    {
        List<Ingredient> ingredients;


        public IngredientDataStore()
        {
            ingredients = new List<Ingredient>();
            var _ingredients = new List<Ingredient>
            {
                new Ingredient ( "Rice", "Cereals", "g",  true, 0.50f),
                new Ingredient ( "Egg", "Protein", "units",  true, 1.0f),
            };

            foreach (Ingredient ingredient in _ingredients)
            {
                ingredients.Add(ingredient);
            }
        }

        public async Task<bool> AddItemAsync(Ingredient ingredient)
        {
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Ingredient ingredient)
        {
            var _ingredient = ingredients.Where((Ingredient arg) => arg.Id == ingredient.Id).FirstOrDefault();
            ingredients.Remove(_ingredient);
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _ingredient = ingredients.Where((Ingredient arg) => arg.Id == id).FirstOrDefault();
            ingredients.Remove(_ingredient);

            return await Task.FromResult(true);
        }

        public async Task<Ingredient> GetItemAsync(string id)
        {
            return await Task.FromResult(ingredients.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Ingredient>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(ingredients);
        }
    }
}
