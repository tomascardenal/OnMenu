using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using OnMenu.Droid.Activities;

namespace OnMenu.Droid
{
    /// <summary>
    /// Main activity for the OnMenu android app.
    /// </summary>
    [Activity(Label = "@string/app_name", Icon = "@drawable/ic_launcher",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        /// <summary>
        /// Gets the layout resource.
        /// </summary>
        /// <value>The layout resource.</value>
        protected override int LayoutResource => Resource.Layout.activity_main;
        /// <summary>
        /// The menu intent.
        /// </summary>
        protected Intent menuIntent;
        /// <summary>
        /// The pager.
        /// </summary>
        protected ViewPager pager;
        /// <summary>
        /// The adapter.
        /// </summary>
        private TabsAdapter adapter;

        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            adapter = new TabsAdapter(this, SupportFragmentManager);
            pager = FindViewById<ViewPager>(Resource.Id.viewpager);
            var tabs = FindViewById<TabLayout>(Resource.Id.tabs);
            pager.Adapter = adapter;
            tabs.SetupWithViewPager(pager);
            pager.OffscreenPageLimit = 3;

            pager.PageSelected += (sender, args) =>
            {
                var fragment = adapter.InstantiateItem(pager, args.Position) as IFragmentVisible;

                fragment?.BecameVisible();
            };

            Toolbar.MenuItemClick += (sender, e) =>
            {
                switch (e.Item.ItemId)
                {
                    case Resource.Id.menu_addRecipe:
                        StartActivity(new Intent(this, typeof(AddRecipeActivity)));
                        break;
                    case Resource.Id.menu_addIngredient:
                        StartActivity(new Intent(this, typeof(AddIngredientsActivity)));
                        break;
                }
            };

            SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            SupportActionBar.SetHomeButtonEnabled(false);
        }

        /// <summary>
        /// Handles the actions to do when the options menu is created
        /// </summary>
        /// <returns><c>true</c>, if create options menu was created, <c>false</c> otherwise.</returns>
        /// <param name="menu">Menu.</param>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }

    /// <summary>
    /// Adapter for tab navigation
    /// </summary>
    class TabsAdapter : FragmentStatePagerAdapter
    {
        /// <summary>
        /// The tab titles
        /// </summary>
        string[] titles;
        /// <summary>
        /// Gets the tab count
        /// </summary>
        /// <value>The count.</value>
        public override int Count => titles.Length;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:OnMenu.Droid.TabsAdapter"/> class.
        /// </summary>
        /// <param name="context">The current Context.</param>
        /// <param name="fm">The fragment manager Fm.</param>
        public TabsAdapter(Context context, Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            titles = context.Resources.GetTextArray(Resource.Array.sections);
        }

        /// <summary>
        /// Gets the page title formatted.
        /// </summary>
        /// <returns>The page title formatted.</returns>
        /// <param name="position">Position of the page to query</param>
        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position) =>
                            new Java.Lang.String(titles[position]);

        /// <summary>
        /// Gets the item corresponding to the tab position.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="position">The position.</param>
        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0: return BrowseRecipeFragment.NewInstance();
                case 1: return BrowseIngredientFragment.NewInstance();
                case 2: return CalendarFragment.NewInstance();
                case 3: return AboutFragment.NewInstance();
            }
            return null;
        }

        /// <summary>
        /// Gets the item position.
        /// </summary>
        /// <returns>The item position.</returns>
        /// <param name="frag">The fragment.</param>
        public override int GetItemPosition(Java.Lang.Object frag) => PositionNone;
    }
}
