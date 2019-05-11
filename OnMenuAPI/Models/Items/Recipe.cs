using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnMenuAPI.Models.Items
{
    /// <summary>
    /// Model for a recipe
    /// by: Tomás Cardenal López
    /// </summary>
    public class Recipe : Item
    {
      

        /// <summary>
        /// List of ingredients on this recipe
        /// </summary>
        [Required]
        public List<Recipe> Ingredients { get; set; }

        /// <summary>
        /// The instructions to follow on this recipe
        /// </summary>
        [Required]
        public string Instructions { get; set;}

        /// <summary>
        /// This recipe's rating
        /// </summary>
        [Required]
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
        public Recipe(string name, string instructions, List<Recipe> ingredients, int rating)
        {
            Name = name;
            Instructions = instructions;
            Ingredients = ingredients;
            Rating = rating;
        }

    }
}
