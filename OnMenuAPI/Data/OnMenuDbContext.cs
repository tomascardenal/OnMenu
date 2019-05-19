using Microsoft.EntityFrameworkCore;
using OnMenuAPI.Models;

namespace OnMenuAPI.Data
{
    /// <summary>
    /// Database context
    /// </summary>
    public class OnMenuDbContext : DbContext
    {
        /// <summary>
        /// The set for the recipes
        /// </summary>
        public DbSet<Recipe> Recipes { get; set; }
        /// <summary>
        /// The set for the ingredients
        /// </summary>
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Filename=./DbREST.db");
        }

    }
}
