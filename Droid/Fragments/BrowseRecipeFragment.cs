using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.App;
using Android.Content;
using OnMenu.Droid.Activities;
using PopupMenu = Android.Support.V7.Widget.PopupMenu;
using OnMenu.Helpers;
using System.Collections.Generic;
using OnMenu.Models.Items;
using OnMenu.Droid.Helpers;

namespace OnMenu.Droid
{
    /// <summary>
    /// Fragment to browse recipes
    /// </summary>
    public class BrowseRecipeFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        /// <summary>
        /// Starts a new instance
        /// </summary>
        /// <returns>The instance.</returns>
        public static BrowseRecipeFragment NewInstance() =>
            new BrowseRecipeFragment { Arguments = new Bundle() };
        /// <summary>
        /// The adapter.
        /// </summary>
        private BrowseRecipesAdapter adapter;
        /// <summary>
        /// The refresher.
        /// </summary>
        protected SwipeRefreshLayout refresher;
        /// <summary>
        /// Recipes recyclerView
        /// </summary>
        protected RecyclerView recyclerView;
        /// <summary>
        /// The context menu.
        /// </summary>
        protected PopupMenu contextMenu;
        /// <summary>
        /// The selected item.
        /// </summary>
        protected int selectedItem;
        /// <summary>
        /// The progress bar.
        /// </summary>
        protected ProgressBar progress;
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public static RecipeViewModel ViewModel { get; set; }

        /// <summary>
        /// Handles the actions when this fragment is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        /// <summary>
        /// Handles the actions when this fragment view is created
        /// </summary>
        /// <returns>The created view.</returns>
        /// <param name="inflater">Inflater.</param>
        /// <param name="container">Container.</param>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = new RecipeViewModel();

            View view = inflater.Inflate(Resource.Layout.fragment_browse, container, false);
            recyclerView =
                view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter = new BrowseRecipesAdapter(Activity, ViewModel));

            refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            refresher.SetColorSchemeColors(Resource.Color.accent);

            progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            progress.Visibility = ViewStates.Gone;

            return view;
        }

        /// <summary>
        /// Handles the actions when this fragment is started
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            refresher.Refresh += Refresher_Refresh;
            adapter.ItemClick += Adapter_ItemClick;
            adapter.ItemLongClick += Adapter_ItemLongClick;

            if (ViewModel.Recipes.Count == 0)
                ViewModel.LoadRecipesCommand.Execute(null);
        }

        /// <summary>
        /// Handles the actions when this fragment stops
        /// </summary>
        public override void OnStop()
        {
            base.OnStop();
            refresher.Refresh -= Refresher_Refresh;
            adapter.ItemClick -= Adapter_ItemClick;
            adapter.ItemLongClick -= Adapter_ItemLongClick;
        }

        /// <summary>
        /// Handles the actions when an adapter item is clicked
        /// </summary>
        /// <param name="sender">the adapter item</param>
        /// <param name="e">the event args</param>
        void Adapter_ItemClick(object sender, RecyclerClickEventArgs e)
        {
            var item = ViewModel.Recipes[e.Position];
            var intent = new Intent(Activity, typeof(BrowseRecipeDetailActivity));

            intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            Activity.StartActivity(intent);
        }

        /// <summary>
        /// Handles the actions when an adapter item is longclicked
        /// </summary>
        /// <param name="sender">the adapter item</param>
        /// <param name="e">the event args</param>
        void Adapter_ItemLongClick(object sender, RecyclerClickEventArgs e)
        {
            selectedItem = e.Position;
            contextMenu = new PopupMenu(this.Context, e.View);
            contextMenu.Inflate(Resource.Menu.browse_context_menus);
            contextMenu.MenuItemClick += OnContextMenuItemClick;
            contextMenu.Show();
        }

        /// <summary>
        /// Handles the actions when an item of the context menu is clicked
        /// </summary>
        /// <param name="sender">the item</param>
        /// <param name="e">the event args</param>
        void OnContextMenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_deleteItem:
                    List<Ingredient> ingList = ItemParser.IdCSVToIngredientList(ViewModel.Recipes[selectedItem].Ingredients, BrowseIngredientFragment.ViewModel);
                    ingList.ForEach(i => i.CanDelete = true);
                    ViewModel.DeleteRecipesCommand.Execute(ViewModel.Recipes[selectedItem]);
                    adapter.NotifyItemRemoved(selectedItem);
                    break;
                case Resource.Id.menu_editItem:
                    Intent intent = new Intent(Activity, typeof(AddRecipeActivity));
                    intent.PutExtra("recipe", Newtonsoft.Json.JsonConvert.SerializeObject(ViewModel.Recipes[selectedItem]));
                    Activity.StartActivity(intent);
                    break;
            }
            Utils.ForceRefreshLayout(refresher, recyclerView);
        }

        /// <summary>
        /// Handles the actions when the refresher is triggered
        /// </summary>
        /// <param name="sender">The refresher</param>
        /// <param name="e">the event args</param>
        void Refresher_Refresh(object sender, EventArgs e)
        {
            ViewModel.LoadRecipesCommand.Execute(null);
            refresher.Refreshing = false;
        }

        /// <summary>
        /// Informs that the fragment became visible and executes the actions inside
        /// </summary>
        public void BecameVisible()
        {

        }
    }

    /// <summary>
    /// Adapter class to browse recipes on a RecyclerView
    /// </summary>
    class BrowseRecipesAdapter : BaseRecycleViewAdapter
    {
        /// <summary>
        /// The view model
        /// </summary>
        RecipeViewModel viewModel;
        /// <summary>
        /// The activity
        /// </summary>
        Activity activity;
        /// <summary>
        /// The viewholder
        /// </summary>
        RecipesViewHolder recipesViewHolder;

        /// <summary>
        /// Instantiates a new adapter
        /// </summary>
        /// <param name="activity">The activity for this adapter</param>
        /// <param name="viewModel">The view model for this adapter</param>
        public BrowseRecipesAdapter(Activity activity, RecipeViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.activity = activity;

            this.viewModel.Recipes.CollectionChanged += (sender, args) =>
            {
                this.activity.RunOnUiThread(NotifyDataSetChanged);
            };
        }

        /// <summary>
        /// Creates new views (invoked by the layout manager)
        /// </summary>
        /// <param name="parent">The parent ViewGroup</param>
        /// <param name="viewType">the viewtype</param>
        /// <returns>The viewholder</returns>
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View recipeView = null;
            var id = Resource.Layout.ingredient_browse;
            recipeView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            recipesViewHolder = new RecipesViewHolder(recipeView, OnClick, OnLongClick);
            return recipesViewHolder;
        }

        /// <summary>
        /// Replace the contents of a view (invoked by the layout manager)
        /// </summary>
        /// <param name="holder">The viewholder</param>
        /// <param name="position">The position</param>   
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var recipe = viewModel.Recipes[position];

            // Replace the contents of the view with that element
            var myHolder = holder as RecipesViewHolder;
            myHolder.TextView.Text = recipe.Name;
        }
        /// <summary>
        /// Gets the item count
        /// </summary>
        public override int ItemCount => viewModel.Recipes.Count;
    }

    /// <summary>
    /// Viewholder for the recipes
    /// </summary>
    public class RecipesViewHolder : RecyclerView.ViewHolder
    {
        /// <summary>
        /// Textfield for the rows on the recycler view
        /// </summary>
        public TextView TextView { get; set; }
        /// <summary>
        /// Detail textfield for the rows on the recycler view
        /// </summary>
        public TextView DetailTextView { get; set; }

        /// <summary>
        /// Instantiates a new viewholder
        /// </summary>
        /// <param name="itemView">the view of the item</param>
        /// <param name="clickListener">the clicklistener</param>
        /// <param name="longClickListener">the longclicklistener</param>
        public RecipesViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            TextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
            DetailTextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text2);
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
}
