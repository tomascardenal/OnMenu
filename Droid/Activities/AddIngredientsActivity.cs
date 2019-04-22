using System;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.Design.Widget;
using OnMenu.Models;
using Android.Views;

namespace OnMenu.Droid
{
    [Activity(Label = "AddItemActivity")]
    public class AddIngredientsActivity : Activity
    {
        FloatingActionButton saveButton;
        Toolbar toolbar;
        EditText nameField, groupField, measureField, priceField;
        ToggleButton allergenButton;

        public IngredientsViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = BrowseFragment.ViewModel;

            SetContentView(Resource.Layout.activity_add_ingredient);
            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button);

            nameField = FindViewById<EditText>(Resource.Id.nameField_addIngredient);
            groupField = FindViewById<EditText>(Resource.Id.groupField_addIngredient);
            measureField = FindViewById<EditText>(Resource.Id.measureField_addIngredient);
            priceField = FindViewById<EditText>(Resource.Id.estimatedPriceField_addIngredient);
            allergenButton = FindViewById<ToggleButton>(Resource.Id.toggleAllergen_addIngredient);

            saveButton.Click += SaveButton_Click;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        void SaveButton_Click(object sender, EventArgs e)
        {
            var ingredient = new Ingredient
            (
                nameField.Text,
                groupField.Text,
                measureField.Text,
                allergenButton.Checked,
                float.Parse(priceField.Text)
            );
            ViewModel.AddIngredientsCommand.Execute(ingredient);

            Finish();
        }
    }
}
