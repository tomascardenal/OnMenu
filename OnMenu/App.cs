using System;
using System.IO;
using Android.Util;
using OnMenu.Data;
using OnMenu.Models;
using OnMenu.Models.Items;
using SQLite;

namespace OnMenu
{
    public class App
    {
        public static bool UseMockDataStore = true;
        public static bool UseLocalStorage = true;
        public static string BackendUrl = "http://localhost:5000";
        private static ItemDatabase _db;
        public static ItemDatabase DB
        {
            get
            {
                if (_db == null)
                {
                    _db = new ItemDatabase("Data/itemDb.db3");
                }
                return _db;
            }
        }

        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<Ingredient>, IngredientDataStore>();
            ServiceLocator.Instance.Register<IDataStore<Recipe>, RecipeDataStore>();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ItemDB.db3");
            if (!File.Exists(dbPath))
            {
                File.Create(dbPath);
            }
            _db = new ItemDatabase(dbPath);
            
        }
    }
}
