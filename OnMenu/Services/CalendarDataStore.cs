using Android.Util;
using OnMenu.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnMenu.Services
{
    /// <summary>
    /// Datastore for the calendar
    /// </summary>
    class CalendarDataStore : IDataStore<RecipeCalendarEntry>
    {
        /// <summary>
        /// List of calendar entries
        /// </summary>
        protected List<RecipeCalendarEntry> calendarEntries;
        /// <summary>
        /// Whether the data store was initialized or not
        /// </summary>
        private bool initialized;

        /// <summary>
        /// Instantiates a new data store for the calendar
        /// </summary>
        public CalendarDataStore()
        {
            initialized = false;
        }

        /// <summary>
        /// Initializes the datastore
        /// </summary>
        /// <returns>An async task</returns>
        public async Task InitializeDataStore()
        {
            if (!initialized)
            {
                initialized = true;
                calendarEntries = await App.DB.GetCalendarEntriesAsync();
                if (calendarEntries == null || calendarEntries.Count == 0)
                {
                    calendarEntries = new List<RecipeCalendarEntry>();
                }
                foreach(RecipeCalendarEntry entry in calendarEntries)
                {
                    Log.Debug("Calendar ", entry.RecipeId + " " + entry.Time + " " + entry.Date);
                }
            }
        }

        /// <summary>
        /// Adds a calendar entry asyncronously
        /// </summary>
        /// <param name="entry">The name of the calendar entry to add</param>
        /// <returns>A boolean indicating if the calendar entry was added</returns>
        public async Task<bool> AddItemAsync(RecipeCalendarEntry entry)
        {
            calendarEntries.Add(entry);
            int i = await App.DB.SaveCalendarEntryAsync(entry);
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Updates a calendar entry asyncronously, dependent on the id
        /// </summary>
        /// <param name="entry">The calendar entry to update</param>
        /// <returns>A boolean indicating if the calendar entry was updated</returns>
        public async Task<bool> UpdateItemAsync(RecipeCalendarEntry entry)
        {
            var _calendarEntry = calendarEntries.Where((RecipeCalendarEntry arg) => arg.Id == entry.Id).FirstOrDefault();
            calendarEntries.Remove(_calendarEntry);
            calendarEntries.Add(entry);
            await App.DB.DeleteCalendarEntryAsync(_calendarEntry);
            if (await App.DB.SaveCalendarEntryAsync(entry) > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }

        }

        /// <summary>
        /// Deletes a calendar entry asyncronously, dependent on the id
        /// </summary>
        /// <param name="id">The id of the calendar entry to delete</param>
        /// <returns>>A boolean indicating if the calendar entry was deleted</returns>
        public async Task<bool> DeleteItemAsync(int id)
        {
            var _calendarEntry = calendarEntries.Where((RecipeCalendarEntry arg) => arg.Id == id).FirstOrDefault();
            calendarEntries.Remove(_calendarEntry);
            if (await App.DB.DeleteCalendarEntryAsync(_calendarEntry) > 0)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Gets a calendar entry asyncronously, dependent on the id
        /// </summary>
        /// <param name="id">The id of the calendar entry to get</param>
        /// <returns>The calendar entry</returns>
        public async Task<RecipeCalendarEntry> GetItemAsync(int id)
        {
            return await Task.FromResult(calendarEntries.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Gets the calendar entries
        /// </summary>
        /// <param name="forceRefresh">Whether the list should be forced to refresh (default == false)</param>
        /// <returns>The list of calendar entries</returns>
        public async Task<IEnumerable<RecipeCalendarEntry>> GetItemsAsync(bool forceRefresh = false)
        {
            return await App.DB.GetCalendarEntriesAsync();
        }

        /// <summary>
        /// Edits all the calendar entries in the db
        /// </summary>
        /// <returns></returns>
        public async Task<bool> EditItemsAsync()
        {
            foreach (RecipeCalendarEntry r in calendarEntries)
            {
                await App.DB.DeleteCalendarEntryAsync(r);
                await App.DB.SaveCalendarEntryAsync(r);
            }
            return await Task.FromResult(true);
        }
    }
}
