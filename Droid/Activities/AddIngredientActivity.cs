
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace OnMenu.Droid.Activities
{
    [Activity(Label = "AddIngredientActivity")]
    public class AddIngredientActivity : Activity
    {
        Toolbar toolbar;
        EditText nameField, groupField, measureField, priceField;
        ToggleButton allergenButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            nameField = FindViewById<EditText>(Resource.Id.nameField_addIngredient);
            groupField = FindViewById<EditText>(Resource.Id.groupField_addIngredient);
            measureField = FindViewById<EditText>(Resource.Id.measureField_addIngredient);
            priceField = FindViewById<EditText>(Resource.Id.estimatedPriceField_addIngredient);
            allergenButton = FindViewById<ToggleButton>(Resource.Id.toggleAllergen_addIngredient);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar_addIngredient);
            SetActionBar(toolbar);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus,menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }
    }
}