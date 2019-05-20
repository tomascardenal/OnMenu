using System.Runtime.Serialization;

namespace OnMenuAPI.Models
{
    /// <summary>
    /// Model for an Ingredient
    /// </summary>
    [DataContract]  
    public class Ingredient : Item
    {
        /// <summary>
        /// The ingredient's group
        /// </summary>
        private string group;
        /// <summary>
        /// Gets or sets the food group for this ingredient.
        /// </summary>
        /// <value>The food group.</value>
        [DataMember]
        public string Group { get => group; set => group = value; }

        /// <summary>
        /// The ingredient's measuring unit
        /// </summary>
        private string measure;
        /// <summary>
        /// Gets or sets the measure to use with this ingredient.
        /// </summary>
        /// <value>The measure.</value>
        [DataMember]
        public string Measure { get => measure; set => measure = value; }

        /// <summary>
        /// Whether this ingredient can be an allergen
        /// </summary>
        private bool allergen;
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:OnMenu.Models.Ingredient"/> is an allergen.
        /// </summary>
        /// <value><c>true</c> if the ingredient is an allergen; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool Allergen { get => allergen; set => allergen = value; }

        /// <summary>
        /// This ingredient's estimated market price
        /// </summary>
        private float estimatedPrice;
        /// <summary>
        /// Gets or sets the estimated price of the ingredient.
        /// </summary>
        /// <value>The estimated price of this ingredient.</value>
        [DataMember]
        public float EstimatedPrice { get => estimatedPrice; set => estimatedPrice = value; }
        /// <summary>
        /// Indicates if this ingredient can be deleted or not (if it's on a recipe, it shouldn't)
        /// </summary>
        [DataMember]
        public bool CanDelete { get; set; }
        /// <summary>
        /// Indicates per how much of the quantity the ingredient price is estimated
        /// </summary>
        [DataMember]
        public float EstimatedPer { get; set; }

        /// <summary>
        /// Initializes a new ingredient to it's parameters
        /// </summary>
        /// <param name="name">The ingredient's name.</param>
        /// <param name="group">The ingredient's group.</param>
        /// <param name="measure">The measuring unit for the ingredient.</param>
        /// <param name="allergen">If set to <c>true</c>, the ingredient is an allergen.</param>
        /// <param name="estimatedPrice">Estimated price for the ingredient.</param>
        /// <param name="estimatedPer">Factor for the price of the ingredient.</param>
        public Ingredient(string name, string group, string measure, bool allergen, float estimatedPrice, float estimatedPer) : base(name)
        {
            Group = group;
            Measure = measure;
            Allergen = allergen;
            EstimatedPrice = estimatedPrice;
            EstimatedPer = estimatedPer;
            CanDelete = true;
        }

        /// <summary>
        /// Empty constructor (Used by sqlite-net-pcl)
        /// </summary>
        public Ingredient() : base() { }

    }
}
