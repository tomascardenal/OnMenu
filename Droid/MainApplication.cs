using System;

using Android.App;
using Android.OS;
using Android.Runtime;

using Plugin.CurrentActivity;

namespace OnMenu.Droid
{
    /// <summary>
    /// Main application class for OnMenu android App
    /// </summary>
    [Application]
    public class MainApplication : Application, Application.IActivityLifecycleCallbacks
    {
        /// <summary>
        /// Default constructor for the application
        /// </summary>
        /// <param name="handle">pointer</param>
        /// <param name="transer">tba</param>
        public MainApplication(IntPtr handle, JniHandleOwnership transer)
        : base(handle, transer)
        {
        }

        /// <summary>
        /// Handles the actions on the creation of the app
        /// </summary>
        public override void OnCreate()
        {
            base.OnCreate();
            RegisterActivityLifecycleCallbacks(this);
            CrossCurrentActivity.Current.Init(this);
            App.Initialize();
        }

        /// <summary>
        /// Handles the actions when the app terminates
        /// </summary>
        public override void OnTerminate()
        {
            base.OnTerminate();
            App.DB.StopConnectionCommand.Execute(null);
            UnregisterActivityLifecycleCallbacks(this);
        }

        /// <summary>
        /// Handles the actions when an activity is created, so is set as the CrossCurrentActivity
        /// </summary>
        /// <param name="activity">the created activity</param>
        /// <param name="savedInstanceState">the saved state</param>
        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        /// <summary>
        /// Handles the actions when an activity is destroyed
        /// </summary>
        /// <param name="activity">the destroyed activity</param>
        public void OnActivityDestroyed(Activity activity)
        {
        }

        /// <summary>
        /// Handles the actions when an activity is paused
        /// </summary>
        /// <param name="activity">the paused activity</param>
        public void OnActivityPaused(Activity activity)
        {
        }

        /// <summary>
        /// Handles the actions when an activity is resumed
        /// </summary>
        /// <param name="activity">the resumed activity</param>
        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        /// <summary>
        /// Handles the actions when an activity is on a saving state
        /// </summary>
        /// <param name="activity">the saving activity</param>
        /// <param name="outState">the saving state</param>
        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {

        }

        /// <summary>
        /// Handles the actions when an activity is started
        /// </summary>
        /// <param name="activity">the started activity</param>
        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        /// <summary>
        /// Handles the actions when an activity is stopped
        /// </summary>
        /// <param name="activity">the stopped activity</param>
        public void OnActivityStopped(Activity activity)
        {

        }
    }
}
