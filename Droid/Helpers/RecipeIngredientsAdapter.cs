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
        Activity activity;
        /// <summary>
        /// The viewholder for this adapter
        /// </summary>
        RecipeIngredientsViewHolder holder;

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


        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View ingRecView =
                LayoutInflater.From(parent.Context).Inflate(OnMenu.Droid.Resource.Layout.recyclerviewrow_add_ingredient_to_recipe, parent, false);
            holder = new RecipeIngredientsViewHolder(ingRecView, OnClick, OnLongClick);
            return holder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Ingredient ingredient = IngredientList[position];
            RecipeIngredientsViewHolder aHolder = holder as RecipeIngredientsViewHolder;
            aHolder.IngredientTextView.Text = IngredientList[position].Name;
            aHolder.QuantityTextView.Text = QuantityStore[position].ToString();
            aHolder.MeasurementTextView.Text = IngredientList[position].Measure;
        }

        /// <summary>
        /// Gets the item count
        /// </summary>
        public override int ItemCount => IngredientList.Count;
    }
}