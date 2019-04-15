using System;
using System.Collections.Generic;

namespace OnMenu.Models
{
    /// <summary>
    /// Model for a recipe
    /// by: Tomás Cardenal López
    /// </summary>
    public class Recipe
    {
        /// <summary>
        /// The recipe's name
        /// </summary>
        string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The recipe's name.</value>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// List of ingredients on this recipe
        /// </summary>
        List<Ingredient> ingredients;
        /// <summary>
        /// Gets or sets the list of ingredients on this recipe
        /// </summary>
        /// <value>The list of ingredients.</value>
        public List<Ingredient> Ingredients { get => ingredients; set => ingredients = value; }

        /// <summary>
        /// The instructions to follow on this recipe
        /// </summary>
        string instructions;
        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions to follow.</value>
        public string Instructions { get => instructions; set => instructions = value; }

        /// <summary>
        /// This recipe's rating
        /// </summary>
        int rating;
        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>The rating, between 0 and 5, both included.</value>
        public int Rating
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
        /// Initializes a new recipe to it's parameters/>
        /// </summary>
        /// <param name="name">The recipe's name.</param>
        /// <param name="instructions">The instructions to follow on this recipe.</param>
        /// <param name="ingredients">List of ingredients.</param>
        /// <param name="rating">The recipe's name.</param>
        public Recipe(string name, string instructions, List<Ingredient> ingredients, int rating)
        {
            Name = name;
            Instructions = instructions;
            Ingredients = ingredients;
            Rating = rating;
        }

    }
}
