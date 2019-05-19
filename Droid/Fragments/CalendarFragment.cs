using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using OnMenu.Models.Items;
using OnMenu.ViewModels;
using System;
using System.Linq;

namespace OnMenu.Droid
{
    /// <summary>
    /// Fragment to show the calendar
    /// </summary>
    public class CalendarFragment : Android.Support.V4.App.Fragment, IFragmentVisible
    {
        /// <summary>
        /// Starts a new instance
        /// </summary>
        /// <returns>The instance.</returns>
        public static CalendarFragment NewInstance() =>
            new CalendarFragment { Arguments = new Bundle() };
        /// <summary>
        /// The adapter.
        /// </summary>
        private BrowseCalendarAdapter adapter;
        /// <summary>
        /// The recyclerview with the calendar entries
        /// </summary>
        protected RecyclerView recyclerView;
        /// <summary>
        /// The refresher.
        /// </summary>
        protected SwipeRefreshLayout refresher;
        /// <summary>
        /// The calendarView
        /// </summary>
        protected CalendarView calendar;
        /// <summary>
        /// The selected item.
        /// </summary>
        protected int selectedItem;
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>The view model.</value>
        public static CalendarViewModel ViewModel { get; set; }

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
            ViewModel = new CalendarViewModel();
            View view = inflater.Inflate(Resource.Layout.fragment_calendar, container, false);
            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recycler_calendarFragment);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter = new BrowseCalendarAdapter(Activity, ViewModel));

            refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher_calendarFragment);
            refresher.SetColorSchemeColors(Resource.Color.accent);



            return view;
        }

        /// <summary>
        /// Informs that the fragment became visible and executes the actions inside
        /// </summary>
        public void BecameVisible()
        {
        }
    }

    /// <summary>
    /// Adapter class to browse the calendar on a RecyclerView
    /// </summary>
    class BrowseCalendarAdapter : BaseRecycleViewAdapter
    {
        /// <summary>
        /// The view model
        /// </summary>
        CalendarViewModel viewModel;
        /// <summary>
        /// The activity
        /// </summary>
        Activity activity;
        /// <summary>
        /// The viewholder
        /// </summary>
        CalendarViewHolder calendarViewHolder;

        /// <summary>
        /// Instantiates a new adapter
        /// </summary>
        /// <param name="activity">The activity for this adapter</param>
        /// <param name="viewModel">The view model for this adapter</param>
        public BrowseCalendarAdapter(Activity activity, CalendarViewModel viewModel)
        {
            this.viewModel = viewModel;
            this.activity = activity;

            this.viewModel.Calendar.CollectionChanged += (sender, args) =>
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
            View calendarView = null;
            var id = Resource.Layout.calendar_browse;
            calendarView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            calendarViewHolder = new CalendarViewHolder(calendarView, OnClick, OnLongClick);
            return calendarViewHolder;
        }

        /// <summary>
        /// Replace the contents of a view (invoked by the layout manager)
        /// </summary>
        /// <param name="holder">The viewholder</param>
        /// <param name="position">The position</param>        
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var entry = viewModel.Calendar[position];

            // Replace the contents of the view with that element
            var myHolder = holder as CalendarViewHolder;
            Recipe rec = null;
            BrowseRecipeFragment.ViewModel.Recipes.ToList().ForEach((r) =>
            {
                if (r.Id == entry.RecipeId)
                {
                    rec = r;
                }
            });
            if (rec != null)
            {
                myHolder.RecipeTitle.Text = rec.Name;
            }

            myHolder.CalendarTime.Text = entry.DTime.ToShortTimeString();
        }
        /// <summary>
        /// Gets the item count
        /// </summary>
        public override int ItemCount => viewModel.Calendar.Count;
    }

    /// <summary>
    /// Viewholder for the ingredients
    /// </summary>
    public class CalendarViewHolder : RecyclerView.ViewHolder
    {
        /// <summary>
        /// Textfield for the rows on the recycler view
        /// </summary>
        public TextView RecipeTitle { get; set; }
        /// <summary>
        /// Detail textfield for the rows on the recycler view
        /// </summary>
        public TextView CalendarTime { get; set; }

        /// <summary>
        /// Instantiates a new viewholder
        /// </summary>
        /// <param name="itemView">the view of the item</param>
        /// <param name="clickListener">the clicklistener</param>
        /// <param name="longClickListener">the longclicklistener</param>
        public CalendarViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            RecipeTitle = itemView.FindViewById<TextView>(Resource.Id.recipe_calendarRow);
            CalendarTime = itemView.FindViewById<TextView>(Resource.Id.time_calendarRow);
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }

    }
}