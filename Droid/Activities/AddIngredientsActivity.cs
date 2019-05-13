using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using OnMenu.Models.Items;
using System;

namespace OnMenu.Droid
{
    [Activity(Label = "AddItemActivity")]
    public class AddIngredientsActivity : Activity
    {
        FloatingActionButton saveButton;
        EditText nameField, groupField, measureField, priceField;
        ToggleButton allergenButton;
        bool editMode = false;
        Ingredient editIngredient = null;

        public IngredientsViewModel ViewModel { get; set; }

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

        private void fillForm()
        {
            nameField.Text = editIngredient.Name;
            groupField.Text = editIngredient.Group;
            measureField.Text = editIngredient.Measure;
            allergenButton.Checked = editIngredient.Allergen;
            priceField.Text = editIngredient.EstimatedPrice.ToString();
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
            if (editMode)
            {
                editIngredient.Name = nameField.Text;
                editIngredient.Group = groupField.Text;
                editIngredient.Measure = measureField.Text;
                editIngredient.Allergen = allergenButton.Checked;
                editIngredient.EstimatedPrice = float.Parse(priceField.Text);
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
                    float.Parse(priceField.Text)
                );
                ViewModel.AddIngredientsCommand.Execute(ingredient);
            }
            Finish();
        }
    }
}
