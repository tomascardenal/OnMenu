﻿using System.Collections.Generic;
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
        private int rating;
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
                else if (value > 100)
                {
                    rating = 100;
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
        /// Indicates if this recipe can be deleted or not (if it's on the calendar, it shouldn't)
        /// </summary>
        public bool CanDelete { get; set; }


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
            CanDelete = true;
        }

        /// <summary>
        /// Empty constructor (Used by sqlite-net-pcl)
        /// </summary>
        public Recipe() : base() { }

    }
}
