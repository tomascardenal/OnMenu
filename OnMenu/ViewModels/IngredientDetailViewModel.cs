using System;
using OnMenu.Models;

namespace OnMenu
{
    public class IngredientDetailViewModel : BaseViewModel
    {
        public Ingredient Ingredient{ get; set; }
        public IngredientDetailViewModel(Ingredient ingredient = null)
        {
            if (ingredient != null)
            {
                Title = ingredient.Name;
                Ingredient = ingredient;
            }
        }
    }
}
