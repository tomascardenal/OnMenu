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
    public class AddIngredientToRecipeFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        Spinner ingredientSpinner;
        ImageButton removeIngredientBtn;
        ArrayAdapter adapter;
        List<String> ingredientNames;

        public static IngredientsViewModel ViewModel { get; set; }


        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = new IngredientsViewModel();

            // Use this to return your custom view for this Fragment
            View view =  inflater.Inflate(Resource.Layout.fragment_add_ingredient_to_recipe, container, false);
            ingredientSpinner = view.FindViewById<Spinner>(Resource.Id.addRecipe_ingredientSpinner);

            removeIngredientBtn = view.FindViewById<ImageButton>(Resource.Id.removeIngredientBtn);

            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            if (ViewModel.Ingredients.Count == 0)
                ViewModel.LoadIngredientsCommand.Execute(null);

            ingredientNames = ViewModel.Ingredients.Select(ingredient => ingredient.Name).ToList();
            ArrayAdapter adapter = new ArrayAdapter(this.Context,Resource.Id.addRecipe_ingredientSpinner, ingredientNames);
            ingredientSpinner.Adapter = adapter;
        }

        public void BecameVisible()
        {
        }
    }
}