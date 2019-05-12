using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models;
using OnMenu.Models.Items;

namespace OnMenu
{
    public class RecipeDataStore : IDataStore<Recipe>
    {
        List<Recipe> recipes;

        public RecipeDataStore()
        {
            recipes = new List<Recipe>();
            var _recipes = new List<Recipe>
            {
            };

            foreach (Recipe recipe in _recipes)
            {
                recipes.Add(recipe);
            }
        }

        public async Task<bool> AddItemAsync(Recipe recipe)
        {
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Recipe recipe)
        {
            var _recipe = recipes.Where((Recipe arg) => arg.Id == recipe.Id).FirstOrDefault();
            recipes.Remove(_recipe);
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _recipe = recipes.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            recipes.Remove(_recipe);

            return await Task.FromResult(true);
        }

        public async Task<Recipe> GetItemAsync(int id)
        {
            return await Task.FromResult(recipes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(recipes);
        }
    }
}
