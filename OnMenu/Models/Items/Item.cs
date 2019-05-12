using SQLite;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace OnMenu.Models.Items
{   
    public class Item 
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// The item's name
        /// </summary>
        string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The item's name.</value>
        public string Name { get => name; set => name = value; }

        public Item(string name)
        {
            Name = name;
        }

        public Item() {}

        public static explicit operator Item(Task<Ingredient> v)
        {
            throw new NotImplementedException();
        }
    }
}
