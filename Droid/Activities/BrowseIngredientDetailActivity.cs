using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using OnMenu.Models;
using OnMenu.Models.Items;

namespace OnMenu.Droid
{
    /// <summary>
    /// Activity to browse an ingredient detail
    /// </summary>
    [Activity(Label = "Ingredient Details", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class BrowseIngredientDetailActivity : BaseActivity
    {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource => Resource.Layout.activity_ingredient_details;
        /// <summary>
        /// The view model.
        /// </summary>
        IngredientDetailViewModel viewModel;
        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");

            Ingredient ingredient = Newtonsoft.Json.JsonConvert.DeserializeObject<Ingredient>(data);
            viewModel = new IngredientDetailViewModel(ingredient);

            FindViewById<TextView>(Resource.Id.description).Text = ingredient.Name;

            SupportActionBar.Title = ingredient.Name;
        }

        /// <summary>
        /// Handles the actions when this activity is started
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
        }

        /// <summary>
        /// Handles the actions when this activity stops
        /// </summary>
        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
