using System;
using System.ComponentModel.DataAnnotations;

namespace OnMenuAPI.Models.Items
{
    public class Item
    {
        /// <summary>
        /// The item's ID
        /// </summary>
        [Required]
        public string ID { get; set; }

        /// <summary>
        /// The item's name
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
