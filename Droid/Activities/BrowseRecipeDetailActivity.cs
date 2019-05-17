using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using OnMenu.Helpers;
using OnMenu.Models.Items;
using OnMenu.ViewModels;

namespace OnMenu.Droid.Activities
{
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
        RecipeDetailViewModel viewModel;
        TextView recipeDetails;
        TextView recipeAllergen;
        TextView recipePrice;
        RecyclerView ingredientList;
        RecipeIngredientsAdapter adapter;
        /// <summary>
        /// The recipe to show
        /// </summary>
        Recipe recipe;
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

        protected void updateValues()
        {
            
            List <Ingredient> placeholderL = ItemParser.IdCSVToIngredientList(recipe.Ingredients, BrowseIngredientFragment.ViewModel);
            ObservableCollection<Ingredient> ingList = new ObservableCollection<Ingredient>();
            placeholderL.ForEach(i => ingList.Add(i));
            List<float> qList = ItemParser.QuantityValuesToFloatList(recipe.Quantities);
            ingredientList.SetAdapter(adapter = new RecipeIngredientsAdapter(this, ingList, qList));
            recipeDetails.Text = recipe.Instructions;
            recipeAllergen.Text = GetString(Resource.String.no);
            double price = 0;
            placeholderL.ForEach(i =>
            {
                if (i.Allergen)
                {
                    recipeAllergen.Text = GetString(Resource.String.yes);
                }
                price += (i.EstimatedPrice / i.EstimatedPer) * qList[placeholderL.IndexOf(i)];
            });
            recipePrice.Text = price.ToString();
            SupportActionBar.Title = recipe.Name;
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