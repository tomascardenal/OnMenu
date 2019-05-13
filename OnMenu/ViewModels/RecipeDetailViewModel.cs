using OnMenu.Helpers;
using OnMenu.Models.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnMenu.ViewModels
{
    /// <summary>
    /// Viewmodel for the recipe details
    /// </summary>
    class RecipeDetailViewModel : BaseViewModel
    {
        /// <summary>
        /// The recipe to show
        /// </summary>
        public Recipe Recipe { get; set; }
        /// <summary>
        /// List of ingredients for this recipe
        /// </summary>
        public List<Ingredient> IngredientList { get; set; }

        /// <summary>
        /// Instantiates a new view model
        /// </summary>
        /// <param name="recipe">The recipe to show</param>
        /// <param name="ingredientReference">A reference to the list of ingredients, to be loaded on the recipe</param>
        public RecipeDetailViewModel(Recipe recipe = null, IngredientsViewModel ingredientReference = null)
        {
            if (recipe != null)
            {
                Title = recipe.Name;
                Recipe = recipe;
                IngredientList = ItemParser.IdCSVToIngredientList(recipe.Ingredients,ingredientReference);
            }
        }
    }
}
