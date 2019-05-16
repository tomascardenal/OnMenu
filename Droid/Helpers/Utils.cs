using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace OnMenu.Droid.Helpers
{
    class Utils
    {
        public static void HideKeyboardFromInput(Activity activity, EditText input)
        {
            InputMethodManager imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(input.WindowToken, 0);
        }
        public static void ShowKeyboardFromInput(Activity activity, EditText input)
        {
            InputMethodManager imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.ShowSoftInputFromInputMethod(input.WindowToken, 0);
        }

    }
}