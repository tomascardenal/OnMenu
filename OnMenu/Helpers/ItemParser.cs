using OnMenu.Models.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace OnMenu.Helpers
{
    /// <summary>
    /// Functions to parse items into SQLite valid datatypes
    /// </summary>
    class ItemParser
    {
        /// <summary>
        /// Parses a List of ingredients into a string with their ids separated by commas
        /// </summary>
        /// <param name="ingredients">The list of ingredients to parse</param>
        /// <returns>A string with the ids separated by commas</returns>
        public static string IngredientsToIdCSV(List<Ingredient> ingredients)
        {
            string parsedList = "";
            foreach(Ingredient i in ingredients)
            {
                parsedList += i.Id + ",";
            }
            return parsedList;
        }

        /// <summary>
        /// Parses the comma separated ids from a Recipe into a list of Ingredients
        /// </summary>
        /// <param name="idValues">The CSV containing the ids</param>
        /// <param name="dataStoreReference">A reference to the current ingredients viewmodel</param>
        /// <returns>A List with the corresponding ingredients</returns>
        public static List<Ingredient> IdCSVToIngredientList(string idValues, IngredientsViewModel viewModelReference)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            string[] separatedValues = idValues.Split(',');
            int parsedId;
            if(viewModelReference.Ingredients==null )
            {
                return ingredientList;
            }else if(viewModelReference.Ingredients.Count == 0)
            {
                viewModelReference.LoadIngredientsCommand.Execute(null);
            }
            foreach(string id in separatedValues)
            {
                if (int.TryParse(id,out parsedId)&&parsedId>=0&&parsedId<viewModelReference.Ingredients.Count)
                {
                    ingredientList.Add(viewModelReference.Ingredients[parsedId]);
                }
            }
            return ingredientList;
        }
    }
}
