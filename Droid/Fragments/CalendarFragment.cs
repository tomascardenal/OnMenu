using Android.App;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using OnMenu.Droid.Helpers;
using OnMenu.Helpers;
using OnMenu.Models.Calendar;
using OnMenu.Models.Items;
using OnMenu.ViewModels;
using System;
using System.Collections.ObjectModel;
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
            recyclerView.SetAdapter(adapter = new BrowseCalendarAdapter(Activity, ViewModel.Calendar));
            //recyclerView.SetLayoutManager(

            refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher_calendarFragment);
            refresher.SetColorSchemeColors(Resource.Color.accent);

            calendar = view.FindViewById<CalendarView>(Resource.Id.calendar_calendarFragment);
            calendar.DateChange += Calendar_DateChange;

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

            if (ViewModel.Calendar.Count == 0)
                ViewModel.LoadCalendarEntriesCommand.Execute(null);
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
        private void Adapter_ItemClick(object sender, RecyclerClickEventArgs e)
        {
            Toast.MakeText(Activity.ApplicationContext, Resource.String.deletecalendar_toast, ToastLength.Short).Show();
        }

        /// <summary>
        /// Handles the actions when an adapter item is longclicked
        /// </summary>
        /// <param name="sender">the adapter item</param>
        /// <param name="e">the event args</param>
        private void Adapter_ItemLongClick(object sender, RecyclerClickEventArgs e)
        {
            selectedItem = e.Position;
            ViewModel.DeleteCalendarEntryCommand.Execute(ViewModel.Calendar[selectedItem]);
            adapter.NotifyItemRemoved(selectedItem);
            Utils.ForceRefreshLayout(refresher, recyclerView);
        }

        /// <summary>
        /// Handles the actions when the refresher is triggered
        /// </summary>
        /// <param name="sender">The refresher</param>
        /// <param name="e">the event args</param>
        private void Refresher_Refresh(object sender, EventArgs e)
        {
            ViewModel.LoadCalendarEntriesCommand.Execute(null);
            adapter.NotifyDataSetChanged();
            refresher.Refreshing = false;
        }

        /// <summary>
        /// Handles the events when the date of the calendar changes
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The args</param>
        private void Calendar_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            string date = ItemParser.ParseDateToCompare(e.DayOfMonth, e.Month, e.Year);
            adapter.ShownEntries.Clear();
            foreach (RecipeCalendarEntry entry in ViewModel.Calendar)
            {
                Log.Debug("CALENDAR ADD", "Comparing entry date "+ entry.Date + "to " + date);
                if (entry.Date == date)
                {
                    Log.Debug("CALENDAR ADD", entry.Date);
                    adapter.ShownEntries.Add(entry);
                }
            }
            adapter.NotifyDataSetChanged();

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
        /// The collection to show
        /// </summary>
        public ObservableCollection<RecipeCalendarEntry> ShownEntries;
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
        /// <param name="shownEntries">The list for this adapter</param>
        public BrowseCalendarAdapter(Activity activity, ObservableCollection<RecipeCalendarEntry> shownEntries)
        {
            this.ShownEntries = new ObservableCollection<RecipeCalendarEntry>();
            shownEntries.ToList().ForEach(e => ShownEntries.Add(e));
            this.activity = activity;

            this.ShownEntries.CollectionChanged += (sender, args) =>
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
            var entry = ShownEntries[position];

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
            if (rec != null && myHolder.RecipeTitle != null)
            {
                myHolder.RecipeTitle.Text = rec.Name;
            }
            if (myHolder.CalendarTime != null)
            {
                myHolder.CalendarTime.Text = entry.Time;
            }
        }
        /// <summary>
        /// Gets the item count
        /// </summary>
        public override int ItemCount => ShownEntries.Count;
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