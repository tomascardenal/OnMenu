using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.App;
using Android.Content;
using PopupMenu = Android.Support.V7.Widget.PopupMenu;
using OnMenu.Droid.Helpers;

namespace OnMenu.Droid
{
    /// <summary>
    /// Fragment to browse ingredients
    /// </summary>
    public class BrowseIngredientFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        /// <summary>
        /// Starts a new instance
        /// </summary>
        /// <returns>The instance.</returns>
        public static BrowseIngredientFragment NewInstance() =>
            new BrowseIngredientFragment { Arguments = new Bundle() };
        /// <summary>
        /// The adapter.
        /// </summary>
        private BrowseIngredientsAdapter adapter;
        /// <summary>
        /// The recyclerview with the ingredients
        /// </summary>
        protected RecyclerView recyclerView;
        /// <summary>
        /// The refresher.
        /// </summary>
        protected SwipeRefreshLayout refresher;
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
        public static IngredientsViewModel ViewModel { get; set; }

        /// <summary>
        /// Handles the actions when this fragment is created
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
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
            ViewModel = new IngredientsViewModel();

            View view = inflater.Inflate(Resource.Layout.fragment_browse, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter = new BrowseIngredientsAdapter(Activity, ViewModel));

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

            if (ViewModel.Ingredients.Count == 0)
                ViewModel.LoadIngredientsCommand.Execute(null);
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
            var item = ViewModel.Ingredients[e.Position];
            var intent = new Intent(Activity, typeof(BrowseIngredientDetailActivity));

            intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            Activity.StartActivityForResult(intent, 0);
        }

        /// <summary>
        /// Handles the actions when an activity returns a result to this fragment
        /// </summary>
        /// <param name="requestCode">The request code</param>
        /// <param name="resultCode">The result code</param>
        /// <param name="data">The Intent with embedded data</param>
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Utils.ForceRefreshLayout(refresher, recyclerView);
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
                    if (ViewModel.Ingredients[selectedItem].CanDelete)
                    {
                        ViewModel.DeleteIngredientsCommand.Execute(ViewModel.Ingredients[selectedItem]);
                        Utils.ForceRefreshLayout(refresher, recyclerView);
                    }
                    else
                    {
                        Toast.MakeText(this.Context, Resource.String.cantDelete_toast, ToastLength.Long).Show();
                    }
                    break;
                case Resource.Id.menu_editItem:
                    Intent intent = new Intent(Activity, typeof(AddIngredientsActivity));
                    intent.PutExtra("ingredient", Newtonsoft.Json.JsonConvert.SerializeObject(ViewModel.Ingredients[selectedItem]));
                    Activity.StartActivity(intent);
                    Utils.ForceRefreshLayout(refresher, recyclerView);
                    break;
            }
        }

        /// <summary>
        /// Handles the actions when the refresher is triggered
        /// </summary>
        /// <param name="sender">The refresher</param>
        /// <param name="e">the event args</param>
        void Refresher_Refresh(object sender, EventArgs e)
        {
            ViewModel.LoadIngredientsCommand.Execute(null);
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
    /// Adapter class to browse ingredients on a RecyclerView
    /// </summary>
    class BrowseIngredientsAdapter : BaseRecycleViewAdapter
    {
        /// <summary>
        /// The view model
        /// </summary>
        IngredientsViewModel viewModel;
        /// <summary>
        /// The activity
        /// </summary>
        Activity activity;
        /// <summary>
        /// The viewholder
        /// </summary>
        IngredientsViewHolder ingredientViewHolder;

        /// <summary>
        /// Instantiates a new adapter
        /// </summary>
        /// <param name="activity">The activity for this adapter</param>
        /// <param name="viewModel">The view model for this adapter</param>
        public BrowseIngredientsAdapter(Activity activity, IngredientsViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.activity = activity;

            this.viewModel.Ingredients.CollectionChanged += (sender, args) =>
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
            View ingredientView = null;
            var id = Resource.Layout.ingredient_browse;
            ingredientView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            ingredientViewHolder = new IngredientsViewHolder(ingredientView, OnClick, OnLongClick);
            return ingredientViewHolder;
        }

        /// <summary>
        /// Replace the contents of a view (invoked by the layout manager)
        /// </summary>
        /// <param name="holder">The viewholder</param>
        /// <param name="position">The position</param>        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var ingredient = viewModel.Ingredients[position];

            // Replace the contents of the view with that element
            var myHolder = holder as IngredientsViewHolder;
            myHolder.TextView.Text = ingredient.Name;
            myHolder.DetailTextView.Text = ingredient.Group;
        }
        /// <summary>
        /// Gets the item count
        /// </summary>
        public override int ItemCount => viewModel.Ingredients.Count;
    }

    /// <summary>
    /// Viewholder for the ingredients
    /// </summary>
    public class IngredientsViewHolder : RecyclerView.ViewHolder
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
        public IngredientsViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            TextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
            DetailTextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text2);
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }

    }
}
