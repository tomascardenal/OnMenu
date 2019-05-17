using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
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
        /// The ingredient to show
        /// </summary>
        Ingredient ingredient;
        TextView foodGroupView;
        TextView allergenView;
        TextView unitView;
        TextView priceView;

        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");
            ingredient = Newtonsoft.Json.JsonConvert.DeserializeObject<Ingredient>(data);
            viewModel = new IngredientDetailViewModel(ingredient);
            foodGroupView = FindViewById<TextView>(Resource.Id.foodgroup_ingredientDetail);
            allergenView = FindViewById<TextView>(Resource.Id.allergen_ingredientDetail);
            unitView = FindViewById<TextView>(Resource.Id.unit_ingredientDetail);
            priceView = FindViewById<TextView>(Resource.Id.price_ingredientDetail);
        }

        protected void updateValues()
        {
            foodGroupView.Text = ingredient.Group;
            allergenView.Text = ingredient.Allergen ? GetString(Resource.String.yes) : GetString(Resource.String.no);
            unitView.Text = ingredient.Measure;
            priceView.Text = ingredient.EstimatedPrice.ToString();
            SupportActionBar.Title = ingredient.Name;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.browse_context_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_deleteItem:
                    if (ingredient.CanDelete)
                    {
                        BrowseIngredientFragment.ViewModel.DeleteIngredientsCommand.Execute(ingredient);
                        SetResult(Result.Ok);
                        this.Finish();
                    }
                    else
                    {
                        Toast.MakeText(this.ApplicationContext, Resource.String.cantDelete_toast, ToastLength.Long).Show();
                    }
                    break;
                case Resource.Id.menu_editItem:
                    Intent intent = new Intent(this, typeof(AddIngredientsActivity));
                    intent.PutExtra("ingredient", Newtonsoft.Json.JsonConvert.SerializeObject(ingredient));
                    this.StartActivity(intent);
                    updateValues();
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

        /// <summary>
        /// Handles the actions when this activity is started
        /// </summary>
        protected override void OnStart()
        {
            base.OnStart();
            updateValues();
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
