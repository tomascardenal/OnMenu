﻿using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Util;
using OnMenu.Droid.Activities;

namespace OnMenu.Droid
{
    [Activity(Label = "@string/app_name", Icon = "@mipmap/icon",
        LaunchMode = LaunchMode.SingleInstance,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_main;
        protected Intent menuIntent;

        ViewPager pager;
        TabsAdapter adapter;

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

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    }

    class TabsAdapter : FragmentStatePagerAdapter
    {
        string[] titles;

        public override int Count => titles.Length;

        public TabsAdapter(Context context, Android.Support.V4.App.FragmentManager fm) : base(fm)
        {
            titles = context.Resources.GetTextArray(Resource.Array.sections);
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position) =>
                            new Java.Lang.String(titles[position]);

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            switch (position)
            {
                case 0: return BrowseIngredientFragment.NewInstance();
                case 1: return BrowseRecipeFragment.NewInstance();
                case 2: return CalendarFragment.NewInstance();
                case 3: return AboutFragment.NewInstance();
            }
            return null;
        }

        public override int GetItemPosition(Java.Lang.Object @object) => PositionNone;
    }
}
