using System;
using System.ComponentModel.DataAnnotations;

namespace OnMenuAPI.Models.Items
{
    /// <summary>
    /// Model for an Ingredient
    /// by: Tomás Cardenal López
    /// </summary>
    public class Ingredient : Item
    {

        /// <summary>
        /// The ingredient's group
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// The ingredient's measuring unit
        /// </summary>
        public string Measure { get; set; }

        /// <summary>
        /// Whether this ingredient can be an allergen
        /// </summary>
        public bool Allergen { get; set; }

        /// <summary>
        /// This ingredient's estimated market price
        /// </summary>
        public float EstimatedPrice { get; set; }

        /// <summary>
        /// The quantity to use of this ingredient.
        /// </summary>
        public float Quantity { get; set;}

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
