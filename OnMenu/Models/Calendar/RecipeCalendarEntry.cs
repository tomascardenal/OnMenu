using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnMenu.Models.Calendar
{
    /// <summary>
    /// Represents an entry on the calendar
    /// </summary>
    public class RecipeCalendarEntry
    {

        /// <summary>
        /// The calendarRecipe's ID, autoincremented by SQLite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// The recipe id
        /// </summary>
        public int RecipeId { get; set; }
        /// <summary>
        /// The datetime
        /// </summary>

        /// <summary>
        /// Empty constructor (Used by sqlite-net-pcl)
        /// </summary>
        public DateTime DTime { get; set; }

        
    }
}
