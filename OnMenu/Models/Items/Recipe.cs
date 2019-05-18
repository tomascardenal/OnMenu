using System.Collections.Generic;
using OnMenu.Helpers;

namespace OnMenu.Models.Items
{
    /// <summary>
    /// Model for a recipe
    /// by: Tomás Cardenal López
    /// </summary>
    public class Recipe : Item
    {

        /// <summary>
        /// List of ingredients on this recipe, represented as the ingredient ID's separated by commas
        /// <see cref="Helpers.ItemParser"/>
        /// </summary>
        public string Ingredients { get; set; }

        /// <summary>
        /// The instructions to follow on this recipe
        /// </summary>
        public string Instructions { get; set; }

        /// <summary>
        /// This recipe's rating
        /// </summary>
        private float rating;
        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating, between 0 and 5, both included.</value>
        public float Rating
        {
            get => rating;
            set
            {
                if (value < 0)
                {
                    rating = 0;
                }
                else if (value > 5)
                {
                    rating = 5;
                }
                else
                {
                    rating = value;
                }
            }
        }

        /// <summary>
        /// CSV string with quantity references
        /// </summary>
        public string Quantities { get; set; }


        /// <summary>
        /// Initializes a new recipe to it's parameters/>
        /// </summary>
        /// <param name="name">The recipe's name.</param>
        /// <param name="instructions">The instructions to follow on this recipe.</param>
        /// <param name="ingredients">List of ingredients.</param>
        /// <param name="rating">The recipe's name.</param>
        public Recipe(string name, string instructions, List<Ingredient> ingredients, List<float> quantities, int rating) : base(name)
        {
            Instructions = instructions;
            Ingredients = ItemParser.IngredientsToIdCSV(ingredients);
            Rating = rating;
            Quantities = ItemParser.FloatListToQuantityValues(quantities);
        }

        /// <summary>
        /// Empty constructor (Used by sqlite-net-pcl)
        /// </summary>
        public Recipe() : base() { }

        /// <summary>
        /// Gets the estimated total price of this recipe
        /// </summary>
        /// <returns>The total price</returns>
        public double GetEstimatedPrice(IngredientsViewModel viewModel)
        {
            double price = 0;
            List<float> qList = this.GetQuantitiesAsList();
            List<Ingredient> ingList = this.GetIngredientsAsList(viewModel);
            ingList.ForEach(i =>
            {
                price += (i.EstimatedPrice / i.EstimatedPer) * qList[ingList.IndexOf(i)];
            });
            return price;
        }

        /// <summary>
        /// Gets the quantities as a float list
        /// </summary>
        /// <returns>The float list</returns>
        public List<float> GetQuantitiesAsList()
        {
            return ItemParser.QuantityValuesToFloatList(Quantities);
        }

        /// <summary>
        /// Gets the ingredients as a list
        /// </summary>
        /// <returns>The ingredient list</returns>
        public List<Ingredient> GetIngredientsAsList(IngredientsViewModel viewModel)
        {
            return ItemParser.IdCSVToIngredientList(Ingredients, viewModel);
        }
    }
}
