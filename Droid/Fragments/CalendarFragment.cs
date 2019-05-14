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
    public class CalendarFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        public static CalendarFragment NewInstance() =>
            new CalendarFragment { Arguments = new Bundle() };

        public void BecameVisible()
        {
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            return inflater.Inflate(Resource.Layout.fragment_calendar, container, false);
        }
    }
}