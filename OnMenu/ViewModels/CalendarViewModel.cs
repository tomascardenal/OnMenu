using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Android.Util;
using OnMenu.Models.Calendar;
using OnMenu.Services;

namespace OnMenu.ViewModels
{
    /// <summary>
    /// Viewmodel for the calendar
    /// </summary>
    public class CalendarViewModel : BaseViewModel
    {
        /// <summary>
        /// The calendar collection
        /// </summary>
        public ObservableCollection<RecipeCalendarEntry> Calendar { get; set; }
        /// <summary>
        /// Command to load calendar entries
        /// </summary>
        public Command LoadCalendarEntryCommand { get; set; }
        /// <summary>
        /// Command to add  calendar entries
        /// </summary>
        public Command AddCalendarEntryCommand { get; set; }
        /// <summary>
        /// Command to delete  calendar entries
        /// </summary>
        public Command DeleteCalendarEntryCommand { get; set; }
        /// <summary>
        /// Command to update  calendar entries
        /// </summary>
        public Command UpdateCalendarEntryCommand { get; set; }
        /// <summary>
        /// Calendar entries datastore
        /// </summary>
        public IDataStore<RecipeCalendarEntry> CalendarDataStore => ServiceLocator.Instance.Get<IDataStore<RecipeCalendarEntry>>() ?? new CalendarDataStore();

        /// <summary>
        /// Default constructor, instantates a new viewmodel
        /// </summary>
        public CalendarViewModel()
        {
            Title = "Calendar";
            Calendar = new ObservableCollection<RecipeCalendarEntry>();
            LoadCalendarEntryCommand = new Command(async () => await ExecuteLoadCalendarCommand());
            AddCalendarEntryCommand = new Command<RecipeCalendarEntry>(async (RecipeCalendarEntry entry) => await AddCalendarEntry(entry));
            DeleteCalendarEntryCommand = new Command<RecipeCalendarEntry>(async (RecipeCalendarEntry entry) => await DeleteCalendarEntry(entry));
            UpdateCalendarEntryCommand = new Command<RecipeCalendarEntry>(async (RecipeCalendarEntry entry) => await UpdateCalendarEntry(entry));
        }

        /// <summary>
        /// Executes the actions of the loadCalendarEntry command
        /// </summary>
        /// <returns>the task</returns>
        async Task ExecuteLoadCalendarCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                await CalendarDataStore.InitializeDataStore();
                Calendar.Clear();

                var calendar = await CalendarDataStore.GetItemsAsync(true);
                foreach (var entry in calendar)
                {
                    Calendar.Add(entry);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DB", " exception : " + ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Adds a calendar entry
        /// </summary>
        /// <param name="entry">the calendar entry to add</param>
        /// <returns>the task</returns>
        async Task AddCalendarEntry(RecipeCalendarEntry entry)
        {
            Calendar.Add(entry);
            await CalendarDataStore.AddItemAsync(entry);
        }

        /// <summary>
        /// Deletes a calendar entry
        /// </summary>
        /// <param name="entry">>the entry to delete</param>
        /// <returns>the task</returns>
        async Task DeleteCalendarEntry(RecipeCalendarEntry entry)
        {
            Calendar.Remove(entry);
            await CalendarDataStore.DeleteItemAsync(entry.Id);
        }

        /// <summary>
        /// Updates an ingredient
        /// </summary>
        /// <param name="ingredient">>the ingredient to update</param>
        /// <returns>the task</returns>
        async Task UpdateCalendarEntry(RecipeCalendarEntry entry)
        {
            RecipeCalendarEntry _entry = null;
            foreach (RecipeCalendarEntry cal in Calendar)
            {
                if (cal.Id == entry.Id)
                {
                    _entry = cal;
                    break;
                }
            }
            if (_entry != null)
            {
                Calendar.Remove(_entry);
                Calendar.Add(entry);
                await CalendarDataStore.UpdateItemAsync(entry);
            }
        }
    }
}
