using System.Runtime.Serialization;

namespace OnMenuAPI.Models
{
    /// <summary>
    /// Model for an Item
    /// </summary>
    [DataContract]
    public class Item
    {
        /// <summary>
        /// The item's ID, autoincremented by SQLite
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// The item's name
        /// </summary>
        [DataMember]
        private string name;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The item's name.</value>
        [DataMember]
        public string Name { get => name; set => name = value; }

        /// <summary>
        /// Starts a new item with the given name
        /// </summary>
        /// <param name="name">The name of the item</param>
        public Item(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Empty constructor (Used by sqlite-net-pcl)
        /// </summary>
        public Item() { }
    }
}
