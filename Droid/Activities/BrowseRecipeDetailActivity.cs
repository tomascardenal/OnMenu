using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OnMenu.Models.Items;
using OnMenu.ViewModels;

namespace OnMenu.Droid.Activities
{
    [Activity(Label = "Recipe Details", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class BrowseRecipeDetailActivity : BaseActivity
    {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource => Resource.Layout.activity_recipe_details;

        RecipeDetailViewModel viewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");

            Recipe recipe = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe>(data);
            viewModel = new RecipeDetailViewModel(recipe);

            FindViewById<TextView>(Resource.Id.description).Text = recipe.Name;

            SupportActionBar.Title = recipe.Name;
        }

        protected override void OnStart()
        {
            base.OnStart();
        }
        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}