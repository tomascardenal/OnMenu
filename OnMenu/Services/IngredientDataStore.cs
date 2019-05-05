﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnMenu.Models;

namespace OnMenu
{
    public class IngredientDataStore : IDataStore<Models.Ingredient>
    {
        List<Models.Ingredient> ingredients;


        public IngredientDataStore()
        {
            ingredients = new List<Models.Ingredient>();
            var _ingredients = new List<Models.Ingredient>
            {
                new Models.Ingredient ( "Rice", "Cereals", "g",  true, 0.50f),
                new Models.Ingredient ( "Egg", "Protein", "units",  true, 1.0f),
            };

            foreach (Models.Ingredient ingredient in _ingredients)
            {
                ingredients.Add(ingredient);
            }
        }

        public async Task<bool> AddItemAsync(Models.Ingredient ingredient)
        {
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Ingredient ingredient)
        {
            var _ingredient = ingredients.Where((Ingredient arg) => arg.Name == ingredient.Name).FirstOrDefault();
            ingredients.Remove(_ingredient);
            ingredients.Add(ingredient);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string name)
        {
            var _ingredient = ingredients.Where((Ingredient arg) => arg.Name == name).FirstOrDefault();
            ingredients.Remove(_ingredient);

            return await Task.FromResult(true);
        }

        public async Task<Models.Ingredient> GetItemAsync(string name)
        {
            return await Task.FromResult(ingredients.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<Models.Ingredient>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(ingredients);
        }
    }
}
