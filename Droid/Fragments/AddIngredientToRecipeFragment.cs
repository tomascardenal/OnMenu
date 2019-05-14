using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using OnMenu.Models.Items;

namespace OnMenu.Droid
{
    /// <summary>
    /// Fragment to add ingredients to a recipe
    /// </summary>
    public class AddIngredientToRecipeFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        /// <summary>
        /// Spinner with the ingredients
        /// </summary>
        Spinner ingredientSpinner;
        /// <summary>
        /// Button to remove an ingredient
        /// </summary>
        ImageButton removeIngredientBtn;
        /// <summary>
        /// The adapter for the spinner
        /// </summary>
        ArrayAdapter adapter;
        /// <summary>
        /// The ingredient names.
        /// </summary>
        List<String> ingredientNames;
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public static IngredientsViewModel ViewModel { get; set; }

        /// <summary>
        /// Handles the actions to do when this fragment is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        /// <summary>
        /// Handles the actions when this fragment view is created
        /// </summary>
        /// <returns>The created view.</returns>
        /// <param name="inflater">Inflater.</param>
        /// <param name="container">Container.</param>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = new IngredientsViewModel();

            // Use this to return your custom view for this Fragment
            View view =  inflater.Inflate(Resource.Layout.fragment_add_ingredient_to_recipe, container, false);
            ingredientSpinner = view.FindViewById<Spinner>(Resource.Id.addRecipe_ingredientSpinner);

            removeIngredientBtn = view.FindViewById<ImageButton>(Resource.Id.removeIngredientBtn);

            return view;
        }

        /// <summary>
        /// Handles the actions when this fragment is started
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            if (ViewModel.Ingredients.Count == 0)
                ViewModel.LoadIngredientsCommand.Execute(null);

            ingredientNames = ViewModel.Ingredients.Select(ingredient => ingredient.Name).ToList();
            adapter = new ArrayAdapter(this.Context,Resource.Id.addRecipe_ingredientSpinner, ingredientNames);
            ingredientSpinner.Adapter = adapter;
        }

        /// <summary>
        /// Informs that the fragment became visible and executes the actions inside
        /// </summary>
        public void BecameVisible()
        {
        }
    }
}