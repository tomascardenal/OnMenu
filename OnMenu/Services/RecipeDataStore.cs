using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models;

namespace OnMenu
{
    public class RecipeDataStore : IDataStore<Models.Recipe>
    {
        List<Models.Recipe> recipes;

        public RecipeDataStore()
        {
            recipes = new List<Models.Recipe>();
            var _recipes = new List<Models.Recipe>
            {
            };

            foreach (Models.Recipe recipe in _recipes)
            {
                recipes.Add(recipe);
            }
        }

        public async Task<bool> AddItemAsync(Models.Recipe recipe)
        {
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Recipe recipe)
        {
            var _recipe = recipes.Where((Recipe arg) => arg.Name == recipe.Name).FirstOrDefault();
            recipes.Remove(_recipe);
            recipes.Add(recipe);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string name)
        {
            var _recipe = recipes.Where((Recipe arg) => arg.Name == name).FirstOrDefault();
            recipes.Remove(_recipe);

            return await Task.FromResult(true);
        }

        public async Task<Models.Recipe> GetItemAsync(string name)
        {
            return await Task.FromResult(recipes.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<Models.Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(recipes);
        }
    }
}
