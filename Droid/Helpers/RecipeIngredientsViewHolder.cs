using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;

namespace OnMenu.Droid.Activities
{
    /// <summary>
    /// Viewholder to add ingredients to a recipe
    /// </summary>
    public class RecipeIngredientsViewHolder : RecyclerView.ViewHolder
    {
        /// <summary>
        /// TextView with the ingredient
        /// </summary>
        public TextView IngredientTextView;
        /// <summary>
        /// Field to indicate the quantity of an ingredient in the recipe
        /// </summary>
        public TextView QuantityTextView;
        /// <summary>
        /// Field to indicate the measurement of an ingredient
        /// </summary>
        public TextView MeasurementTextView;

        public RecipeIngredientsViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            IngredientTextView = itemView.FindViewById<TextView>(Resource.Id.ingredientTextView_addRecipe);
            QuantityTextView = itemView.FindViewById<TextView>(Resource.Id.quantityTextView_addRecipe);
            MeasurementTextView = itemView.FindViewById<TextView>(Resource.Id.measurementTextView_addRecipe);
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
}