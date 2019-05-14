using System;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace OnMenu.Droid
{
    /// <summary>
    /// Fragment to represent the About This App page
    /// </summary>
    public class AboutFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        /// <summary>
        /// Makes a new instance
        /// </summary>
        /// <returns>The instance.</returns>
        public static AboutFragment NewInstance() =>
            new AboutFragment { Arguments = new Bundle() };

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public AboutViewModel ViewModel { get; set; }

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
        /// Button to learn more about this app
        /// </summary>
        Button learnMoreButton;

        /// <summary>
        /// Handles the actions when this fragment view is created
        /// </summary>
        /// <returns>The created view.</returns>
        /// <param name="inflater">Inflater.</param>
        /// <param name="container">Container.</param>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.fragment_about, container, false);
            ViewModel = new AboutViewModel();
            learnMoreButton = view.FindViewById<Button>(Resource.Id.button_learn_more);
            return view;
        }
        /// <summary>
        /// Handles the actions when this fragment is started
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();
            learnMoreButton.Click += LearnMoreButton_Click;
        }


        /// <summary>
        /// Handles the actions when this fragment stops
        /// </summary>
        public override void OnStop()
        {
            base.OnStop();
            learnMoreButton.Click -= LearnMoreButton_Click;
        }

        /// <summary>
        /// Informs that the fragment became visible and executes the actions inside
        /// </summary>
        public void BecameVisible()
        {

        }

        /// <summary>
        /// Controls the actions when the LearnMore button is clicked
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        void LearnMoreButton_Click(object sender, System.EventArgs e)
        {
            ViewModel.OpenWebCommand.Execute(null);
        }
    }
}
