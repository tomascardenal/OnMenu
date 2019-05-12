using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.App;
using Android.Content;
using PopupMenu = Android.Support.V7.Widget.PopupMenu;
using Android.Util;

namespace OnMenu.Droid
{
    public class BrowseIngredientFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        public static BrowseIngredientFragment NewInstance() =>
            new BrowseIngredientFragment { Arguments = new Bundle() };

        BrowseIngredientsAdapter adapter;
        SwipeRefreshLayout refresher;
        PopupMenu contextMenu;
        int selectedItem;

        ProgressBar progress;
        public static IngredientsViewModel ViewModel { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ViewModel = new IngredientsViewModel();

            View view = inflater.Inflate(Resource.Layout.fragment_browse, container, false);
            var recyclerView =
                view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter = new BrowseIngredientsAdapter(Activity, ViewModel));

            refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);
            refresher.SetColorSchemeColors(Resource.Color.accent);

            progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            progress.Visibility = ViewStates.Gone;
            return view;
        }

        public override void OnStart()
        {
            base.OnStart();

            refresher.Refresh += Refresher_Refresh;
            adapter.ItemClick += Adapter_ItemClick;
            adapter.ItemLongClick += Adapter_ItemLongClick;

            if (ViewModel.Ingredients.Count == 0)
                ViewModel.LoadIngredientsCommand.Execute(null);
        }

        public override void OnStop()
        {
            base.OnStop();
            refresher.Refresh -= Refresher_Refresh;
            adapter.ItemClick -= Adapter_ItemClick;
            adapter.ItemLongClick -= Adapter_ItemLongClick;
        }

        void Adapter_ItemClick(object sender, RecyclerClickEventArgs e)
        {
            var item = ViewModel.Ingredients[e.Position];
            var intent = new Intent(Activity, typeof(BrowseItemDetailActivity));

            intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            Activity.StartActivity(intent);
        }

        void Adapter_ItemLongClick(object sender, RecyclerClickEventArgs e)
        {
            //TODO Edit-Delete context menu
            selectedItem = e.Position;
            BrowseIngredientsAdapter adapter = (BrowseIngredientsAdapter)sender;
            contextMenu = new PopupMenu(this.Context, e.View);
            contextMenu.Inflate(Resource.Menu.browse_context_menus);
            contextMenu.MenuItemClick += OnContextMenuItemClick;
            contextMenu.Show();
        }

        void OnContextMenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            switch (e.Item.ItemId)
            {
                case Resource.Id.menu_deleteItem:
                    ViewModel.DeleteIngredientsCommand.Execute(ViewModel.Ingredients[selectedItem]);
                    adapter.NotifyItemRemoved(selectedItem);
                    break;
                case Resource.Id.menu_editItem:
                    Intent intent = new Intent(Activity, typeof(AddIngredientsActivity));
                    intent.PutExtra("ingredient", Newtonsoft.Json.JsonConvert.SerializeObject(ViewModel.Ingredients[selectedItem]));
                    Activity.StartActivity(intent);
                    break;
            }

        }

        void Refresher_Refresh(object sender, EventArgs e)
        {
            ViewModel.LoadIngredientsCommand.Execute(null);
            refresher.Refreshing = false;
        }

        public void BecameVisible()
        {

        }
    }

    class BrowseIngredientsAdapter : BaseRecycleViewAdapter
    {
        IngredientsViewModel viewModel;
        Activity activity;
        IngredientsViewHolder ingredientViewHolder;

        public BrowseIngredientsAdapter(Activity activity, IngredientsViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.activity = activity;

    
            this.viewModel.Ingredients.CollectionChanged += (sender, args) =>
            {
                this.activity.RunOnUiThread(NotifyDataSetChanged);
            };
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            //Setup your layout here
            View ingredientView = null;
            var id = Resource.Layout.ingredient_browse;
            ingredientView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            ingredientViewHolder = new IngredientsViewHolder(ingredientView, OnClick, OnLongClick);
            return ingredientViewHolder;
        }

        public View GetView()
        {
            return ingredientViewHolder.TextView;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var ingredient = viewModel.Ingredients[position];

            // Replace the contents of the view with that element
            var myHolder = holder as IngredientsViewHolder;
            myHolder.TextView.Text = ingredient.Name;
        }

        public override int ItemCount => viewModel.Ingredients.Count;
    }

    public class IngredientsViewHolder : RecyclerView.ViewHolder
    {
        public TextView TextView { get; set; }
        public TextView DetailTextView { get; set; }
        

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
