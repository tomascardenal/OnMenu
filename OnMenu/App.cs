using System;
using System.IO;
using OnMenu.Data;
using OnMenu.Models.Items;

namespace OnMenu
{
    /// <summary>
    /// Main app
    /// </summary>
    public class App
    {
        /// <summary>
        /// Whether local storage should be used
        /// </summary>
        public static bool UseLocalStorage = true;
        /// <summary>
        /// The mock backend url
        /// </summary>
        public static string BackendUrl = "http://localhost:5000";
        /// <summary>
        /// Database controller
        /// </summary>
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

        /// <summary>
        /// Initializes the App
        /// </summary>
        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IDataStore<Ingredient>, IngredientDataStore>();
            ServiceLocator.Instance.Register<IDataStore<Recipe>, RecipeDataStore>();
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ItemDB.db3");
            if (!File.Exists(dbPath))
            {
                File.Create(dbPath);
            }
            else if (OnMenu.Models.Constants.ResetDB)
            {
                File.Create(dbPath);
                File.Delete(dbPath);
            }
            _db = new ItemDatabase(dbPath);

        }
    }
}
