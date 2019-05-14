using System;
using Android.Views;

namespace OnMenu.Droid
{
    /// <summary>
    /// Event arguments for the RecyclerView adapters
    /// </summary>
    public class RecyclerClickEventArgs : EventArgs
    {
        /// <summary>
        /// The current view
        /// </summary>
        public View View { get; set; }
        /// <summary>
        /// The position
        /// </summary>
        public int Position { get; set; }
    }
}
