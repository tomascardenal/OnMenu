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
using Java.Lang;
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
        /// The ingredient Listview.
        /// </summary>
        ListView ingredientListView;
        /// <summary>
        /// Spinner with the ingredients
        /// </summary>
        Spinner ingredientSpinner;
        /// <summary>
        /// Whether this activity is on editmode or not
        /// </summary>
        bool editMode = false;
        /// <summary>
        /// The recipe to edit
        /// </summary>
        Recipe editRecipe = null;
        /// <summary>
        /// Gets or sets the recipes view model.
        /// </summary>
        /// <value>The view model.</value>
        public RecipeViewModel RecViewModel { get;set; }
        /// <summary>
        /// Gets or sets the ingredients view model.
        /// </summary>
        /// <value>The view model.</value>
        public IngredientsViewModel IngViewModel { get;set; }
        /// <summary>
        /// The ingredient names.
        /// </summary>
        string[] ingredientNames;
        /// <summary>
        /// List of arrays for the listview
        /// </summary>
        List<Ingredient> AddedIngredients;

        /// <summary>
        /// Handles the actions to do when this activity is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            IngViewModel = BrowseIngredientFragment.ViewModel;
            RecViewModel = BrowseRecipeFragment.ViewModel;
            ingredientNames = IngViewModel.Ingredients.Select(ingredient => ingredient.Name).ToArray();
            AddedIngredients = new List<Ingredient>();
            if (IngViewModel.Ingredients!=null && IngViewModel.Ingredients.Count > 0)
            {
                AddedIngredients.Add(IngViewModel.Ingredients[0]);
            }

            SetContentView(Resource.Layout.activity_add_recipe);
            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button_addRecipe);
            nameField = FindViewById<EditText>(Resource.Id.nameField_addRecipe);
            instructionsField = FindViewById<EditText>(Resource.Id.instructionsField_addRecipe);
            addIngredientButton = FindViewById<Button>(Resource.Id.addIngredientToRecipe_addRecipe);
            ingredientListView = FindViewById<ListView>(Resource.Id.listviewAddIngredients_addRecipe);
            ingredientSpinner = FindViewById<Spinner>(Resource.Id.ingredientSpinner_addRecipe);

            ingredientListView.Adapter = new RecipeIngredientsAdapter(AddedIngredients);
            ingredientSpinner.Adapter = new ArrayAdapter(this.ApplicationContext, Android.Resource.Layout.SimpleListItem1, ingredientNames);

            var data = Intent.GetStringExtra("recipe") ?? null;
            if(data != null)
            {
                editMode = true;
                editRecipe = Newtonsoft.Json.JsonConvert.DeserializeObject<Recipe>(data);
                fillForm();
            }
            addIngredientButton.Click += AddIngredientButton_Click;
            saveButton.Click += SaveButton_Click;
        }

        /// <summary>
        /// Adds ingredients to the list when the button is clicked
        /// </summary>
        /// <param name="sender">the event sender</param>
        /// <param name="e">the event args</param>
        private void AddIngredientButton_Click(object sender, EventArgs e)
        {
            Ingredient i = IngViewModel.Ingredients[ingredientSpinner.SelectedItemPosition];
            AddedIngredients.Add(i);
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

    /// <summary>
    /// An adapter for a listview adding ingredients to a recipe
    /// </summary>
    //A little trick
    public class RecipeIngredientsAdapter : BaseAdapter<Ingredient>{
        /// <summary>
        /// The adapter for the spinner
        /// </summary>
        ArrayAdapter adapter;
        /// <summary>
        /// List of ingredients 
        /// </summary>
        List<Ingredient> ingredientList;
        RecipeIngredientsViewHolder holder;

        int Position = 0;

        /// <summary>
        /// Returns an item from a position
        /// </summary>
        /// <param name="position">The position of the item</param>
        /// <returns>The item on that position</returns>
        public override Ingredient this[int position] {
            get
            {
                return ingredientList[position];
            }
        }

        public void AddIngredient(Ingredient i)
        {
            ingredientList.Add(i);
        }

        public override int Count {
            get
            {
                return ingredientList.Count;
            }
        }
        
        public RecipeIngredientsAdapter(List<Ingredient> repeaterList)
        {
            ingredientList = repeaterList;
        }

        public override long GetItemId(int position)
        {
            this.Position = position;
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.listviewrow_add_ingredient_to_recipe,parent,false);
                TextView ingredientTextView = view.FindViewById<TextView>(Resource.Id.ingredientTextView_addRecipe);
                ImageButton removeIngredientBtn = view.FindViewById<ImageButton>(Resource.Id.removeIngredientBtn_addRecipe);
                view.Tag = new RecipeIngredientsViewHolder() {IngredientTextView = ingredientTextView, RemoveIngredientBtn = removeIngredientBtn };
            }
            holder = (RecipeIngredientsViewHolder)view.Tag;
            holder.IngredientTextView.Text = ingredientList[position].Name;
            holder.RemoveIngredientBtn.Click += RemoveIngredientBtn_Click;
            return view;
        }

        private void RemoveIngredientBtn_Click(object sender, EventArgs e)
        {
            //TODO make an ingredient be removed
            //ingredientList.RemoveAt(Position); ;
        }
    }

    /// <summary>
    /// Viewholder to add ingredients to a recipe
    /// </summary>
    public class RecipeIngredientsViewHolder : Java.Lang.Object
    {
        /// <summary>
        /// TextView with the ingredient
        /// </summary>
        public TextView IngredientTextView;
        /// <summary>
        /// Button to remove an ingredient
        /// </summary>
        public ImageButton RemoveIngredientBtn;
    }
}