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
    /// <summary>
    /// Activity to add recipes
    /// </summary>
    [Activity(Label = "AddRecipeActivity")]
    public class AddRecipeActivity : Activity
    {
        /// <summary>
        /// The save button.
        /// </summary>
        FloatingActionButton saveButton;
        /// <summary>
        /// The name field.
        /// </summary>
        EditText nameField; 
        /// <summary>
        /// The instructions field.
        /// </summary>
        EditText instructionsField;
        /// <summary>
        /// The add ingredient button.
        /// </summary>
        Button addIngredientButton;
        /// <summary>
        /// The ingredient fragments.
        /// </summary>
        Fragment[] ingredientFragments;
        /// <summary>
        /// Whether this activity is on editmode or not
        /// </summary>
        bool editMode = false;
        /// <summary>
        /// The recipe to edit
        /// </summary>
        Recipe editRecipe = null;
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public RecipeViewModel ViewModel { get;set; }

        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
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

        /// <summary>
        /// Handles the actions to do when the options menu is created
        /// </summary>
        /// <returns><c>true</c>, if create options menu was created properly, <c>false</c> otherwise.</returns>
        /// <param name="menu">The menu.</param>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// Handles the actions to do when an options item is selected
        /// </summary>
        /// <returns><c>true</c>, if the actions completed sucessfully, <c>false</c> otherwise.</returns>
        /// <param name="item">The clicked IMenuItem.</param>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        /// <summary>
        /// Fills the form.
        /// </summary>
        private void fillForm()
        {
            nameField.Text = editRecipe.Name;
            instructionsField.Text = editRecipe.Instructions;
        }

        /// <summary>
        /// Handles the actions to do when the save button is clicked
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
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