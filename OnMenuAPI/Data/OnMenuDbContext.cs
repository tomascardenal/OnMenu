using Microsoft.EntityFrameworkCore;
using OnMenu.Models.Items;

namespace OnMenu.Data
{
    /// <summary>
    /// Database context
    /// </summary>
    public class OnMenuDbContext : DbContext
    {
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
