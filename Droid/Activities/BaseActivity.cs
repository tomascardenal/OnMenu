using Android.OS;
using Android.Support.V7.App;
using Android.Support.V7.Widget;

namespace OnMenu.Droid
{
    /// <summary>
    /// Base activity for the app
    /// </summary>
    public class BaseActivity : AppCompatActivity
    {
        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(LayoutResource);
            Toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            if (Toolbar != null)
            {
                SetSupportActionBar(Toolbar);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
                SupportActionBar.SetHomeButtonEnabled(true);

            }
        }

        /// <summary>
        /// Gets or sets the toolbar.
        /// </summary>
        /// <value>The toolbar.</value>
        public Toolbar Toolbar
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the layout resource.
        /// </summary>
        /// <value>The layout resource.</value>
        protected virtual int LayoutResource
        {
            get;
        }

        /// <summary>
        /// Sets the action bar icon.
        /// </summary>
        /// <value>The action bar icon.</value>
        protected int ActionBarIcon
        {
            set { Toolbar?.SetNavigationIcon(value); }
        }
    }
}
