using System;

namespace OnMenu.Models.Items
{
    public class Item 
    {
        public string Id { get; set; }

        /// <summary>
        /// The item's name
        /// </summary>
        string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The item's name.</value>
        public string Name { get => name; set => name = value; }
    }
}
