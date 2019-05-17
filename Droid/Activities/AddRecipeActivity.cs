using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Java.Lang;
using OnMenu.Droid.Helpers;
using OnMenu.Helpers;
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
        /// Gets or sets the recipes view model.
        /// </summary>
        /// <value>The view model.</value>
        public RecipeViewModel RecViewModel { get; set; }
        /// <summary>
        /// Gets or sets the ingredients view model.
        /// </summary>
        /// <value>The view model.</value>
        public IngredientsViewModel IngViewModel { get; set; }
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
        /// The ingredient RecyclerView.
        /// </summary>
        RecyclerView ingredientRecyclerView;
        /// <summary>
        /// Spinner with the ingredients
        /// </summary>
        Spinner ingredientSpinner;
        /// <summary>
        /// Refresh layout for the RecyclerView
        /// </summary>
        SwipeRefreshLayout swipeRefreshLayout;
        /// <summary>
        /// The recyclerview adapter.
        /// </summary>
        RecipeIngredientsAdapter adapter;
        /// <summary>
        /// The ingredient names.
        /// </summary>
        string[] ingredientNames;
        /// <summary>
        /// List of arrays for the RecyclerView
        /// </summary>
        ObservableCollection<Ingredient> addedIngredients;
        /// <summary>
        /// Dialog builder for asking ingredient quantities
        /// </summary>
        AlertDialog.Builder alertIngredientBuilder;
        /// <summary>
        /// Whether this activity is on editmode or not
        /// </summary>
        bool editMode;
        /// <summary>
        /// The recipe to edit
        /// </summary>
        Recipe editRecipe;
        /// <summary>
        /// Show the hint to delete an ingredient
        /// </summary>
        bool toastAlert;
        /// <summary>
        /// Stores the ingredient quantities
        /// </summary>
        List<float> quantityStore;
        


        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            toastAlert = true;
            editRecipe = null;
            editMode = false;

            IngViewModel = BrowseIngredientFragment.ViewModel;
            RecViewModel = BrowseRecipeFragment.ViewModel;
            ingredientNames = IngViewModel.Ingredients.Select(ingredient => ingredient.Name).ToArray();
            addedIngredients = new ObservableCollection<Ingredient>();
           
            SetContentView(Resource.Layout.activity_add_recipe);
            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button_addRecipe);
            nameField = FindViewById<EditText>(Resource.Id.nameField_addRecipe);
            instructionsField = FindViewById<EditText>(Resource.Id.instructionsField_addRecipe);
            addIngredientButton = FindViewById<Button>(Resource.Id.addIngredientToRecipe_addRecipe);
            ingredientRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerviewAddIngredients_addRecipe);
            ingredientSpinner = FindViewById<Spinner>(Resource.Id.ingredientSpinner_addRecipe);
            swipeRefreshLayout = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout_addRecipe);

            ingredientRecyclerView.SetAdapter(adapter = new RecipeIngredientsAdapter(this, addedIngredients,quantityStore));
            ingredientSpinner.Adapter = new ArrayAdapter(this.ApplicationContext, Android.Resource.Layout.SimpleListItem1, ingredientNames);

            quantityStore = new List<float>();

            var data = Intent.GetStringExtra("recipe") ?? null;
            if (data != null)
            {
                editMode = true;
                editRecipe = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe>(data);
                fillForm();
            }
            addIngredientButton.Click += AddIngredientButton_Click;
            saveButton.Click += SaveButton_Click;
            adapter.ItemLongClick += IngredientRecyclerView_ItemLongClick;
            swipeRefreshLayout.Refresh += SwipeRefreshLayout_Refresh;
        }

        private void SwipeRefreshLayout_Refresh(object sender, EventArgs e)
        {
            this.RunOnUiThread(adapter.NotifyDataSetChanged);
            swipeRefreshLayout.Refreshing = false;
        }

        private void IngredientRecyclerView_ItemLongClick(object sender, RecyclerClickEventArgs e)
        {
            AlertDialog.Builder confirmAlert = new AlertDialog.Builder(this);
            confirmAlert.SetTitle(addedIngredients[e.Position].Name);
            confirmAlert.SetMessage(GetString(Resource.String.addrecipe_confirmDeleteIngredient));
            confirmAlert.SetPositiveButton(GetString(Resource.String.yes), (senderFromAlert, args) =>
             {
                 addedIngredients.RemoveAt(e.Position);
             });
            Dialog dialog = confirmAlert.Create();
            dialog.Show();
        }

        /// <summary>
        /// Adds ingredients to the list when the button is clicked
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void AddIngredientButton_Click(object sender, EventArgs e)
        {
            Ingredient i = IngViewModel.Ingredients[ingredientSpinner.SelectedItemPosition];
            View alertView = LayoutInflater.From(this).Inflate(Resource.Layout.input_quantity_layout, null);
            EditText quantityInput = alertView.FindViewById<EditText>(Resource.Id.editQuantity_dialog);
            TextView ingredientAlert = alertView.FindViewById<TextView>(Resource.Id.ingredientTitle_dialog);
            TextView unitAlert = alertView.FindViewById<TextView>(Resource.Id.ingredientUnit_dialog);
            ingredientAlert.Text = i.Name;
            unitAlert.Text = i.Measure;
            if (addedIngredients.Contains(i))
            {
                quantityInput.Text = quantityStore[addedIngredients.IndexOf(i)].ToString();
            }

            alertIngredientBuilder = new AlertDialog.Builder(this);
            alertIngredientBuilder.SetView(alertView);
            alertIngredientBuilder.SetCancelable(false)
                .SetPositiveButton(this.ApplicationContext.GetString(Resource.String.accept), delegate
                {
                    float q;
                    if (!float.TryParse(quantityInput.Text, out q))
                    {
                        q = 1;
                    }
                    if (!addedIngredients.Contains(i))
                    {
                        quantityStore.Add(q);
                        adapter.QuantityStore = quantityStore;
                        addedIngredients.Add(i);
                    }
                    else
                    {
                        quantityStore[addedIngredients.IndexOf(i)] = q;
                        adapter.QuantityStore = quantityStore;
                    }
                    //FORCE KEYBOARD TO HIDE, PLEASE
                    Utils.HideKeyboardFromInput(this, quantityInput);
                })
                .SetNegativeButton(this.ApplicationContext.GetString(Resource.String.cancel), delegate
                {
                    alertIngredientBuilder.Dispose();
                });
            AlertDialog dialog = alertIngredientBuilder.Create();
            dialog.Show();
            if (toastAlert)
            {
                Toast.MakeText(this, Resource.String.addrecipe_removeInfoToast, ToastLength.Long).Show();
                toastAlert = false;
            }
            this.RunOnUiThread(adapter.NotifyDataSetChanged);

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
            addedIngredients.Clear();
            quantityStore.Clear();

            //Don't want to have a reference to different objects on the RecyclerView
            ItemParser.IdCSVToIngredientList(editRecipe.Ingredients, IngViewModel).ForEach(item => addedIngredients.Add(item));
            ItemParser.QuantityValuesToFloatList(editRecipe.Quantities).ForEach(q => quantityStore.Add(q));
            adapter.QuantityStore = quantityStore;
            for(int i = 0; i < addedIngredients.Count(); i++)
            {
                addedIngredients[i].CanDelete = true;
            }
        }

        /// <summary>
        /// Handles the actions to do when the save button is clicked
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        void SaveButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < addedIngredients.Count(); i++)
            {
                addedIngredients[i].CanDelete = false;
            }
            if (editMode)
            {
                editRecipe.Name = nameField.Text;
                editRecipe.Instructions = instructionsField.Text;
                editRecipe.Ingredients = ItemParser.IngredientsToIdCSV(addedIngredients.ToList());
                editRecipe.Quantities = ItemParser.FloatListToQuantityValues(quantityStore);
                RecViewModel.UpdateRecipesCommand.Execute(editRecipe);
            }
            else
            {
                Recipe recipe = new Recipe
                (
                    nameField.Text,
                    instructionsField.Text,
                    addedIngredients.ToList(),
                    quantityStore,
                    0
                );
                RecViewModel.AddRecipesCommand.Execute(recipe);
            }
            Finish();
        }
    }
}