using OnMenuAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnMenuAPI.Helpers
{
    public class ItemParser
    {
        /// <summary>
        /// Functions to parse items into SQLite valid datatypes
        /// </summary>

        /// <summary>
        /// Parses a List of ingredients into a string with their ids separated by commas
        /// </summary>
        /// <param name="ingredients">The list of ingredients to parse</param>
        /// <returns>A string with the ids separated by commas</returns>
        public static string IngredientsToIdCSV(List<Ingredient> ingredients)
        {
            string parsedList = "";
            foreach (Ingredient i in ingredients)
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
        public static List<Ingredient> IdCSVToIngredientList(string idValues, List<Ingredient> apiIngredients)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            string[] separatedValues = idValues.Split(',');
            int parsedId;
            if (apiIngredients == null)
            {
                return ingredientList;
            }

            foreach (string id in separatedValues)
            {
                if (int.TryParse(id, out parsedId) && parsedId - 1 >= 0 && parsedId - 1 < apiIngredients.Count)
                {
                    ingredientList.Add(apiIngredients[parsedId - 1]);
                }
            }
            return ingredientList;
        }

        /// <summary>
        /// Parses a float list to the quantities separated by front slashes
        /// </summary>
        /// <param name="floatList">The float list to parse</param>
        /// <returns>The slash separated string with the quantities</returns>
        public static string FloatListToQuantityValues(List<float> floatList)
        {
            string ssv = "";
            foreach (float f in floatList)
            {
                ssv += f.ToString() + "/";
            }

            return ssv;
        }

        /// <summary>
        /// Parses a string with the quantities separated by slashes into a float list
        /// </summary>
        /// <param name="floatSSV">The slash separated string with the quantities to parse</param>
        /// <returns>The resulting float list</returns>
        public static List<float> QuantityValuesToFloatList(string floatSSV)
        {
            List<float> floatList = new List<float>();
            string[] ssValues = floatSSV.Split("/");
            foreach (string s in ssValues)
            {
                float f = 0;
                float.TryParse(s, out f);
                floatList.Add(f);
            }

            return floatList;
        }
    }
}

