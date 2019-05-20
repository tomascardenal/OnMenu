using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using OnMenu.Models.Items;
using System;

namespace OnMenu.Droid
{
    /// <summary>
    /// Activity to add ingredients
    /// </summary>
    [Activity(Label = "AddItemActivity")]
    public class AddIngredientsActivity : Activity
    {
        /// <summary>
        /// The save button.
        /// </summary>
        protected FloatingActionButton saveButton;
        /// <summary>
        /// The name field.
        /// </summary>
        protected EditText nameField;
        /// <summary>
        /// The group field.
        /// </summary>
        protected EditText groupField;
        /// <summary>
        /// The measure field.
        /// </summary>
        protected EditText measureField;
        /// <summary>
        /// The price field.
        /// </summary>
        protected EditText priceField;
        /// <summary>
        /// The estimated per price field.
        /// </summary>
        protected EditText estimatedPerField;
        /// <summary>
        /// The allergen button.
        /// </summary>
        protected ToggleButton allergenButton;

        /// <summary>
        /// Whether this activity is on edit mode or not
        /// </summary>
        protected bool editMode = false;
        /// <summary>
        /// The ingredient to edit
        /// </summary>
        protected Ingredient editIngredient = null;
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public IngredientsViewModel ViewModel { get; set; }

        /// <summary>
        /// Handles the actions to do when this Activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = BrowseIngredientFragment.ViewModel;

            SetContentView(Resource.Layout.activity_add_ingredient);

            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button_addIngredient);
            nameField = FindViewById<EditText>(Resource.Id.nameField_addIngredient);
            groupField = FindViewById<EditText>(Resource.Id.groupField_addIngredient);
            measureField = FindViewById<EditText>(Resource.Id.measureField_addIngredient);
            priceField = FindViewById<EditText>(Resource.Id.estimatedPriceField_addIngredient);
            estimatedPerField = FindViewById<EditText>(Resource.Id.estimatedPerField_addIngredient);
            allergenButton = FindViewById<ToggleButton>(Resource.Id.toggleAllergen_addIngredient);


            var data = Intent.GetStringExtra("ingredient") ?? null;
            if (data != null)
            {
                editMode = true;
                editIngredient = Newtonsoft.Json.JsonConvert.DeserializeObject<Ingredient>(data);
                fillForm();

            }
            saveButton.Click += SaveButton_Click;
        }

        /// <summary>
        /// Fills the form.
        /// </summary>
        private void fillForm()
        {
            nameField.Text = editIngredient.Name;
            groupField.Text = editIngredient.Group;
            measureField.Text = editIngredient.Measure;
            allergenButton.Checked = editIngredient.Allergen;
            priceField.Text = editIngredient.EstimatedPrice.ToString();
            estimatedPerField.Text = editIngredient.EstimatedPer.ToString();
        }

        /// <summary>
        /// Handles the actions to do when the options menu is created
        /// </summary>
        /// <returns><c>true</c>, if create options menu was oned, <c>false</c> otherwise.</returns>
        /// <param name="menu">Menu.</param>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// Handles the actions to do when an menu item is selected
        /// </summary>
        /// <returns><c>true</c>, if completed sucessfully, <c>false</c> otherwise.</returns>
        /// <param name="item">Item.</param>
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        /// <summary>
        /// Handles the actions when the save button is clicked
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        void SaveButton_Click(object sender, EventArgs e)
        {
            if (editMode)
            {
                editIngredient.Name = nameField.Text;
                editIngredient.Group = groupField.Text;
                editIngredient.Measure = measureField.Text;
                editIngredient.Allergen = allergenButton.Checked;
                editIngredient.EstimatedPrice = float.Parse(priceField.Text);
                editIngredient.EstimatedPer = float.Parse(estimatedPerField.Text);
                ViewModel.UpdateIngredientsCommand.Execute(editIngredient);
            }
            else
            {
                Ingredient ingredient = new Ingredient
                (
                    nameField.Text,
                    groupField.Text,
                    measureField.Text,
                    allergenButton.Checked,
                    string.IsNullOrWhiteSpace(priceField.Text) ? 0 : float.Parse(priceField.Text),
                    string.IsNullOrWhiteSpace(estimatedPerField.Text) ? 0 : float.Parse(estimatedPerField.Text)
                );
                ViewModel.AddIngredientsCommand.Execute(ingredient);
            }
            Finish();
        }
    }
}
