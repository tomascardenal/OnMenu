using OnMenu.Models.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnMenu.Helpers
{
    class ItemParser
    {
        public static string IngredientsToIdCSV(List<Ingredient> ingredients)
        {
            string parsedList = "";
            foreach(Ingredient i in ingredients)
            {
                parsedList += i.Id + ",";
            }
            return parsedList;
        }

        public static List<Ingredient> IdCSVToINgredientList(string idValues, IngredientDataStore dataStoreReference)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            string[] separatedValues = idValues.Split(',');
            int parsedId;
            foreach(string id in separatedValues)
            {
                if(int.TryParse(id,out parsedId))
                {
                    ingredientList.Add((Ingredient)dataStoreReference.GetItemAsync(parsedId));
                }
            }
            return ingredientList;
        }
    }
}
