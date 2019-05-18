using OnMenu.Models.Items;

namespace OnMenu
{
    /// <summary>
    /// Viewmodel for the ingredient details
    /// </summary>
    public class IngredientDetailViewModel : BaseViewModel
    {
        /// <summary>
        /// The ingredient to show
        /// </summary>
        public Ingredient Ingredient{ get; set; }

        /// <summary>
        /// Instantiates a new view model
        /// </summary>
        /// <param name="ingredient">The ingredient to show</param>
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
