using OnMenuAPI.Models;
using System.Collections.Generic;

namespace OnMenuAPI.Data
{
    /// <summary>
    /// Container with dummy data for the app
    /// </summary>
    public static class DataContainer
    {
        /// <summary>
        /// List of ingredients
        /// </summary>
        public static List<Ingredient> Ingredients = new List<Ingredient>
                {
                        new Ingredient ( "Arroz", "Cereales", "g",  true, 0.50f, 100),
                        new Ingredient ( "Huevo", "Proteína", "unidades",  true, 1.0f, 6),
                        new Ingredient ( "Sal", "General", "g", false, 0.30f, 1000),
                        new Ingredient ( "Carne de ternera", "Carne", "g", false, 14.9f, 1000),
                        new Ingredient ( "Espaguetis", "Pasta", "g", true, 0.77f, 1000),
                        new Ingredient ( "Lechuga", "Verdura", "unidades", false, 0.75f, 1),
                        new Ingredient ("Tomate frito", "Preparados", "g", true, 0.40f, 200)
                };
    }


}
