using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using OnMenu.Droid.Activities;
using OnMenu.Models.Items;

namespace OnMenu.Droid
{
    /// <summary>
    /// An adapter for a recyclerview adding ingredients to a recipe
    /// </summary>
    public class RecipeIngredientsAdapter : BaseRecycleViewAdapter
    {
        /// <summary>
        /// List of ingredients 
        /// </summary>
        public ObservableCollection<Ingredient> IngredientList;
        /// <summary>
        /// The list of quantities
        /// </summary>
        public List<float> QuantityStore { get; set; }
        /// <summary>
        /// The activity for this adapter
        /// </summary>
        protected Activity activity;
        /// <summary>
        /// The viewholder for this adapter
        /// </summary>
        protected RecipeIngredientsViewHolder holder;

        /// <summary>
        /// Constructor for this adapter
        /// </summary>
        /// <param name="activity">The calling activity</param>
        /// <param name="ingredientList">The list to display</param>
        /// <param name="quantityStore">The quantities to display</param>
        public RecipeIngredientsAdapter(Activity activity, ObservableCollection<Ingredient> ingredientList, List<float> quantityStore)
        {
            this.activity = activity;
            IngredientList = ingredientList;
            this.QuantityStore = quantityStore;
            IngredientList.CollectionChanged += (sender, args) =>
            {
                this.activity.RunOnUiThread(NotifyDataSetChanged);
            };
        }

        /// <summary>
        /// Handles the creation of the viewholders
        /// </summary>
        /// <param name="parent">The parent viewgroup</param>
        /// <param name="viewType">The type of view</param>
        /// <returns></returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View ingRecView =
                LayoutInflater.From(parent.Context).Inflate(OnMenu.Droid.Resource.Layout.recyclerviewrow_add_ingredient_to_recipe, parent, false);
            holder = new RecipeIngredientsViewHolder(ingRecView, OnClick, OnLongClick);
            return holder;
        }

        /// <summary>
        /// Handles the binding of data into the viewholders
        /// </summary>
        /// <param name="holder">The current viewholder</param>
        /// <param name="position">The position to bind</param>
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Ingredient ingredient = IngredientList[position];
            RecipeIngredientsViewHolder aHolder = holder as RecipeIngredientsViewHolder;
            if (aHolder.IngredientTextView != null && aHolder.QuantityTextView != null && aHolder.MeasurementTextView != null)
            {
                aHolder.IngredientTextView.Text = IngredientList[position].Name;
                aHolder.QuantityTextView.Text = QuantityStore[position].ToString();
                aHolder.MeasurementTextView.Text = IngredientList[position].Measure;
            }
        }

        /// <summary>
        /// Gets the item count
        /// </summary>
        public override int ItemCount => IngredientList.Count;
    }
}