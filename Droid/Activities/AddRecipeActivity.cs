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
using OnMenu.Models.Items;

namespace OnMenu.Droid.Activities
{

    [Activity(Label = "AddRecipeActivity")]
    public class AddRecipeActivity : Activity
    {
        FloatingActionButton saveButton;
        EditText nameField, instructionsField;
        Button addIngredientButton;
        Fragment[] ingredientFragments;
        bool editMode = false;
        Recipe editRecipe = null;

        public RecipeViewModel ViewModel { get;set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = BrowseRecipeFragment.ViewModel;

            SetContentView(Resource.Layout.activity_add_recipe);

            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button_addRecipe);
            nameField = FindViewById<EditText>(Resource.Id.nameField_addRecipe);
            instructionsField = FindViewById<EditText>(Resource.Id.instructionsField_addRecipe);
            addIngredientButton = FindViewById<Button>(Resource.Id.addIngredientToRecipe_addRecipe);

            var data = Intent.GetStringExtra("recipe") ?? null;
            if(data != null)
            {
                editMode = true;
                editRecipe = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe>(data);
                fillForm();
            }
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

        private void fillForm()
        {
            nameField.Text = editRecipe.Name;
            instructionsField.Text = editRecipe.Instructions;
        }

        void SaveButton_Click(object sender, EventArgs e)
        {
            if (editMode)
            {

            }
            else
            {

            }
            Finish();
        }
    }
}