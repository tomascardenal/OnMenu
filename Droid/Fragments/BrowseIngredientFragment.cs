using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Widget;
using Android.App;
using Android.Content;

namespace OnMenu.Droid
{
    public class BrowseIngredientFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        public static BrowseIngredientFragment NewInstance() =>
            new BrowseIngredientFragment { Arguments = new Bundle() };

        BrowseIngredientsAdapter adapter;
        SwipeRefreshLayout refresher;

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

            if (ViewModel.Ingredients.Count == 0)
                ViewModel.LoadIngredientsCommand.Execute(null);
        }

        public override void OnStop()
        {
            base.OnStop();
            refresher.Refresh -= Refresher_Refresh;
            adapter.ItemClick -= Adapter_ItemClick;
        }

        void Adapter_ItemClick(object sender, RecyclerClickEventArgs e)
        {
            var item = ViewModel.Ingredients[e.Position];
            var intent = new Intent(Activity, typeof(BrowseItemDetailActivity));

            intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            Activity.StartActivity(intent);
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

            var vh = new IngredientsViewHolder(ingredientView, OnClick, OnLongClick);
            return vh;
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
