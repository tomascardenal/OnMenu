using System.Collections.Generic;
using System.Collections.ObjectModel;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using OnMenu.Droid.Helpers;
using OnMenu.Helpers;
using OnMenu.Models.Items;
using OnMenu.ViewModels;

namespace OnMenu.Droid.Activities
{
    //TODO rating system
    /// <summary>
    /// Activity to browse recipe details
    /// </summary>
    [Activity(Label = "Recipe Details", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class BrowseRecipeDetailActivity : BaseActivity
    {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource => Resource.Layout.activity_recipe_details;
        /// <summary>
        /// The view model.
        /// </summary>
        protected RecipeDetailViewModel viewModel;
        /// <summary>
        /// Textview to show the recipe details
        /// </summary>
        protected TextView recipeDetails;
        /// <summary>
        /// Textview to show if the recipe has allergens
        /// </summary>
        protected TextView recipeAllergen;
        /// <summary>
        /// Textview to show the calculated price
        /// </summary>
        protected TextView recipePrice;
        /// <summary>
        /// List to display the ingredients
        /// </summary>
        protected RecyclerView ingredientList;
        /// <summary>
        /// The adapter of the recyclerview
        /// </summary>
        protected RecipeIngredientsAdapter adapter;
        /// <summary>
        /// The recipe to show
        /// </summary>
        protected Recipe recipe;
        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");

            recipe = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe>(data);
            viewModel = new RecipeDetailViewModel(recipe, BrowseIngredientFragment.ViewModel);

            recipeDetails = FindViewById<TextView>(Resource.Id.instructions_recipeDetail);
            recipeAllergen = FindViewById<TextView>(Resource.Id.allergen_recipeDetail);
            recipePrice = FindViewById<TextView>(Resource.Id.price_recipeDetail); ;
            ingredientList = FindViewById<RecyclerView>(Resource.Id.recyclerList_recipeDetail); ;

            updateValues();
        }

        /// <summary>
        /// Updates the values on the view
        /// </summary>
        protected void updateValues()
        {
            List<Ingredient> placeholderL = recipe.GetIngredientsAsList(BrowseIngredientFragment.ViewModel);
            ObservableCollection<Ingredient> ingList = new ObservableCollection<Ingredient>();
            List<float> qList = recipe.GetQuantitiesAsList();
            double price = recipe.GetEstimatedPrice(BrowseIngredientFragment.ViewModel);

            placeholderL.ForEach(i => ingList.Add(i));
            ingredientList.SetAdapter(adapter = new RecipeIngredientsAdapter(this, ingList, qList));

            recipeDetails.Text = recipe.Instructions;
            recipeAllergen.Text = GetString(Resource.String.no);
            recipePrice.Text = Utils.ParsePrice(price);
            SupportActionBar.Title = recipe.Name;
        }

        /// <summary>
        /// Handles the creation of the menu
        /// </summary>
        /// <param name="menu">The menu</param>
        /// <returns>true when the menu is created</returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.browse_context_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// Handles the actions when a menu item is selected
        /// </summary>
        /// <param name="item">The selected item</param>
        /// <returns>true when the task is finished</returns>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.menu_deleteItem:
                    List<Ingredient> ingList = ItemParser.IdCSVToIngredientList(recipe.Ingredients, BrowseIngredientFragment.ViewModel);
                    ingList.ForEach(i => i.CanDelete = true);
                    BrowseRecipeFragment.ViewModel.DeleteRecipesCommand.Execute(recipe);
                    SetResult(Result.Ok);
                    this.Finish();
                    break;
                case Resource.Id.menu_editItem:
                    Intent intent = new Intent(this, typeof(AddRecipeActivity));
                    intent.PutExtra("recipe", Newtonsoft.Json.JsonConvert.SerializeObject(recipe));
                    StartActivity(intent);
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