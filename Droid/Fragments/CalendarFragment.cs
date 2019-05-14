using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace OnMenu.Droid
{
    /// <summary>
    /// Fragment to show the calendar
    /// </summary>
    public class CalendarFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        /// <summary>
        /// Starts a new instance
        /// </summary>
        /// <returns>The instance.</returns>
        public static CalendarFragment NewInstance() =>
            new CalendarFragment { Arguments = new Bundle() };

        /// <summary>
        /// Informs that the fragment became visible and executes the actions inside
        /// </summary>
        public void BecameVisible()
        {
        }

        /// <summary>
        /// Handles the actions when this fragment is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        /// <summary>
        /// Handles the actions when this fragment view is created
        /// </summary>
        /// <returns>The created view.</returns>
        /// <param name="inflater">Inflater.</param>
        /// <param name="container">Container.</param>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_calendar, container, false);
        }
    }
}