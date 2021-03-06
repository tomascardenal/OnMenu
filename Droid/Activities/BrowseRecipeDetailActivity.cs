﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Text.Method;
using Android.Util;
using Android.Views;
using Android.Widget;
using OnMenu.Droid.Helpers;
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
        protected RecipeDetailViewModel viewModel;
        /// <summary>
        /// TextView to show the recipe details
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
        /// Seekbar to rate ingredients
        /// </summary>
        protected SeekBar ratingSeekBar;
        /// <summary>
        /// Displays the value of the seekbar
        /// </summary>
        protected TextView seekBarValue;
        /// <summary>
        /// The adapter of the recyclerview
        /// </summary>
        protected RecipeIngredientsAdapter adapter;
        /// <summary>
        /// The recipe to show
        /// </summary>
        protected Recipe recipe;
        /// <summary>
        /// Sender row
        /// </summary>
        protected int senderRow;
        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");
            senderRow = Intent.GetIntExtra("row", 0);
            recipe = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe>(data);
            viewModel = new RecipeDetailViewModel(recipe, BrowseIngredientFragment.ViewModel);

            recipeDetails = FindViewById<TextView>(Resource.Id.instructions_recipeDetail);
            recipeAllergen = FindViewById<TextView>(Resource.Id.allergen_recipeDetail);
            recipePrice = FindViewById<TextView>(Resource.Id.price_recipeDetail); ;
            ingredientList = FindViewById<RecyclerView>(Resource.Id.recyclerList_recipeDetail);
            ratingSeekBar = FindViewById<SeekBar>(Resource.Id.seekBar_recipeDetail);
            seekBarValue = FindViewById<TextView>(Resource.Id.seekBarValue_recipeDetail);

            recipeDetails.MovementMethod = new ScrollingMovementMethod();

            ratingSeekBar.ProgressChanged += RatingSeekBar_ProgressChanged;
            

            updateValues();
        }

        private void RatingSeekBar_ProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
        {
            int progress = e.Progress;
            if (progress >= 0 && progress < 25)
            {
                ratingSeekBar.ThumbTintList = GetColorStateList(Android.Resource.Color.HoloRedDark);
                ratingSeekBar.ProgressTintList = GetColorStateList(Android.Resource.Color.HoloRedLight);
            }
            else if (progress >= 25 && progress < 50)
            {
                ratingSeekBar.ThumbTintList = GetColorStateList(Android.Resource.Color.HoloOrangeDark);
                ratingSeekBar.ProgressTintList = GetColorStateList(Android.Resource.Color.HoloOrangeLight);
            }
            else if (progress >= 50 && progress < 75)
            {
                ratingSeekBar.ThumbTintList = GetColorStateList(Android.Resource.Color.HoloBlueDark);
                ratingSeekBar.ProgressTintList = GetColorStateList(Android.Resource.Color.HoloBlueLight);
            }
            else if (progress >= 75)
            {
                ratingSeekBar.ThumbTintList = GetColorStateList(Android.Resource.Color.HoloGreenDark);
                ratingSeekBar.ProgressTintList = GetColorStateList(Android.Resource.Color.HoloGreenLight);
            }
            recipe.Rating = progress;
            Log.Debug("progress", progress.ToString() + ratingSeekBar.ProgressTintList.ToString());

            seekBarValue.Text = progress.ToString();
        }

        /// <summary>
        /// Updates the values on the view
        /// </summary>
        protected void updateValues()
        {
            List<Ingredient> placeholderL = ItemParser.IdCSVToIngredientList(recipe.Ingredients,BrowseIngredientFragment.ViewModel);
            ObservableCollection<Ingredient> ingList = new ObservableCollection<Ingredient>();
            List<float> qList = ItemParser.QuantityValuesToFloatList(recipe.Quantities);
            double price = ItemParser.GetEstimatedPrice(recipe, BrowseIngredientFragment.ViewModel);
            ratingSeekBar.Progress = recipe.Rating;

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
                    if (recipe.CanDelete)
                    {
                        AlertDialog.Builder confirmAlert = new AlertDialog.Builder(this);
                        confirmAlert.SetTitle(recipe.Name);
                        confirmAlert.SetMessage(GetString(Resource.String.recipe_confirmDelete));
                        confirmAlert.SetPositiveButton(GetString(Resource.String.yes), (senderFromAlert, args) =>
                        {
                            List<Ingredient> ingList = ItemParser.IdCSVToIngredientList(recipe.Ingredients, BrowseIngredientFragment.ViewModel);
                            ingList.ForEach(i => i.CanDelete = true);
                            BrowseRecipeFragment.ViewModel.DeleteRecipesCommand.Execute(recipe); 
                            Intent resultIt = new Intent();
                            resultIt.PutExtra("deleted", senderRow);
                            SetResult(Result.FirstUser);
                            this.Finish();
                        });
                        Dialog dialog = confirmAlert.Create();
                        dialog.Show();
                    }
                    else
                    {
                        Toast.MakeText(this.ApplicationContext, Resource.String.recipe_cantDelete_toast,ToastLength.Long).Show();
                    }
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
            updateValues();
        }

        /// <summary>
        /// Handles the actions when this activity is resumed
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            updateValues();
        }

        /// <summary>
        /// Handles the actions when this activity stops
        /// </summary>
        protected override void OnStop()
        {
            BrowseRecipeFragment.ViewModel.UpdateRecipesCommand.Execute(recipe);
            base.OnStop();
        }
    }
}