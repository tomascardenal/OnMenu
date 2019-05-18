using System.Threading;
using Android.App;
using Android.Content;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views.InputMethods;
using Android.Widget;

namespace OnMenu.Droid.Helpers
{
    /// <summary>
    /// Small utils for Android app
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// Hides the keyboard from an input view (generally an edittext)
        /// </summary>
        /// <param name="activity">The activity</param>
        /// <param name="input">The view</param>
        public static void HideKeyboardFromInput(Activity activity, EditText input)
        {
            InputMethodManager imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(input.WindowToken, 0);
        }

        /// <summary>
        /// Shows the keyboard from an input view (generally an edittext)
        /// </summary>
        /// <param name="activity">The activity</param>
        /// <param name="input">The view</param>
        public static void ShowKeyboardFromInput(Activity activity, EditText input)
        {
            InputMethodManager imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.ShowSoftInputFromInputMethod(input.WindowToken, 0);
        }

        /// <summary>
        /// Forces a refresh layout to refresh
        /// </summary>
        /// <param name="refresher">The refresher</param>
        /// <param name="view">The recycler view</param>
        public static void ForceRefreshLayout(SwipeRefreshLayout refresher, RecyclerView view)
        {
            refresher.Post(() =>
            {
                refresher.Refreshing = true;
                view.Clickable = false;
            });
            Thread.Sleep(200);
            refresher.Post(() =>
            {
                refresher.Refreshing = false;
                view.Clickable = true;
            });
        }
    }
}