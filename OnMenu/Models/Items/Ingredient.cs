using System;
namespace OnMenu.Models
{
    /// <summary>
    /// Model for an Ingredient
    /// by: Tomás Cardenal López
    /// </summary>
    public class Ingredient : Item
    {
        /// <summary>
        /// The ingredient's name
        /// </summary>
        string name;
        /// <summary>
        /// Gets or sets the ingredient's name.
        /// </summary>
        /// <value>The ingredient's name.</value>
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// The ingredient's group
        /// </summary>
        string group;
        /// <summary>
        /// Gets or sets the food group for this ingredient.
        /// </summary>
        /// <value>The food group.</value>
        public string Group { get => group; set => group = value; }

        /// <summary>
        /// The ingredient's measuring unit
        /// </summary>
        string measure;
        /// <summary>
        /// Gets or sets the measure to use with this ingredient.
        /// </summary>
        /// <value>The measure.</value>
        public string Measure { get => measure; set => measure = value; }

        /// <summary>
        /// Whether this ingredient can be an allergen
        /// </summary>
        bool allergen;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:OnMenu.Models.Ingredient"/> is an allergen.
        /// </summary>
        /// <value><c>true</c> if the ingredient is an allergen; otherwise, <c>false</c>.</value>
        public bool Allergen { get => allergen; set => allergen = value; }

        /// <summary>
        /// This ingredient's estimated market price
        /// </summary>
        float estimatedPrice;
        /// <summary>
        /// Gets or sets the estimated price of the ingredient.
        /// </summary>
        /// <value>The estimated price of this ingredient.</value>
        public float EstimatedPrice { get => estimatedPrice; set => estimatedPrice = value; }

        /// <summary>
        /// The quantity to use of this ingredient.
        /// </summary>
        float quantity;
        /// <summary>
        /// Gets or sets the quantity of this ingredient.
        /// </summary>
        /// <value>The quantity to use, can be zero if the ingredient is to be stored but not used.</value>
        public float Quantity { get => quantity; set => quantity = value; }

        /// <summary>
        /// Initializes a new ingredient to it's parameters
        /// </summary>
        /// <param name="name">The ingredient's name.</param>
        /// <param name="group">The ingredient's group.</param>
        /// <param name="measure">The measuring unit for the ingredient.</param>
        /// <param name="allergen">If set to <c>true</c>, the ingredient is an allergen.</param>
        /// <param name="estimatedPrice">Estimated price for the ingredient.</param>
        public Ingredient(string name, string group, string measure, bool allergen, float estimatedPrice)
        {
            Name = name;
            Group = group;
            Measure = measure;
            Allergen = allergen;
            EstimatedPrice = estimatedPrice;
            Quantity = 0;
        }
    }
}
