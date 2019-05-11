using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using OnMenu.Models;
using OnMenu.Models.Items;

namespace OnMenu.Droid
{
    [Activity(Label = "Details", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class BrowseItemDetailActivity : BaseActivity
    {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource => Resource.Layout.activity_ingredient_details;

        IngredientDetailViewModel viewModel;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");

            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Ingredient>(data);
            viewModel = new IngredientDetailViewModel(item);

            FindViewById<TextView>(Resource.Id.description).Text = item.Name;

            SupportActionBar.Title = item.Name;
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
